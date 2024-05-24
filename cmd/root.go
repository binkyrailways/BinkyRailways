// Copyright 2021 Ewout Prangsma
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// Author Ewout Prangsma
//

package cmd

import (
	"context"
	"io"
	"os"
	"time"

	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkynet/BinkyNet/loki"
	"github.com/rs/zerolog"
	"github.com/spf13/cobra"
	"golang.org/x/sync/errgroup"

	"github.com/binkyrailways/BinkyRailways/pkg/core/util"
	"github.com/binkyrailways/BinkyRailways/pkg/server"
	"github.com/binkyrailways/BinkyRailways/pkg/service"
)

var (
	// RootCmd is the root of the CLI command structure
	RootCmd = &cobra.Command{
		Use:   "binkyrailways",
		Short: "BinkRailways 2.0",
		Run:   runRootCmd,
	}
	rootArgs struct {
		app      service.Config
		server   server.Config
		logFile  string
		logLevel string
	}
)

const (
	jobName = "brw"
)

func newConsoleWriter() zerolog.ConsoleWriter {
	zerolog.TimeFieldFormat = time.RFC3339Nano
	return zerolog.NewConsoleWriter(func(w *zerolog.ConsoleWriter) {
		w.TimeFormat = "15:04:05.000"
	})
}

func init() {
	f := RootCmd.Flags()
	// Log arguments
	f.StringVar(&rootArgs.logFile, "logfile", "./binkyrailways.log", "Path of log file")
	f.StringVar(&rootArgs.logLevel, "loglevel", "DEBUG", "Minimum log level to display")
	// Server arguments
	f.StringVar(&rootArgs.server.Host, "host", "0.0.0.0", "Host to serve on")
	f.StringVar(&rootArgs.server.PublishedHostIP, "published-host", "", "IP Address of the current host that we publish on")
	f.StringVar(&rootArgs.server.PublishedHostDNSName, "published-hostname", "localhost", "Full DNS name of the current host that we publish on")
	f.StringVar(&rootArgs.server.CertificatePath, "certificate-path", "cert.json", "Certificate file path")
	f.IntVar(&rootArgs.server.HTTPPort, "http-port", 18032, "Port number to serve HTTP on")
	f.IntVar(&rootArgs.server.HTTPSPort, "https-port", 18033, "Port number to serve HTTPS on")
	f.IntVar(&rootArgs.server.GRPCPort, "grpc-port", 18034, "Port number to serve GRPC on")
	f.BoolVar(&rootArgs.server.WebDevelopment, "web-development", false, "If set, web application is served from live filesystem")
	// Service arguments
	f.StringVar(&rootArgs.app.RailwayStoragePath, "storage-path", "./Fixtures", "Path of railway files")
}

// SetVersionAndBuild configures project version & build number
func SetVersionAndBuild(version, build string) {
	rootArgs.app.ProjectVersion = version
	rootArgs.app.ProjectBuild = build
}

// Run the service
func runRootCmd(cmd *cobra.Command, args []string) {
	// Setup bare logger
	cliLog := zerolog.New(newConsoleWriter()).With().Timestamp().Logger()
	ctx := context.Background()

	// Setup log file
	logFile, err := os.Create(rootArgs.logFile)
	if err != nil {
		cliLog.Fatal().Err(err).Msg("Failed to open log file")
	}
	defer logFile.Close()
	logWriters := []io.Writer{
		newConsoleWriter(),
	}
	// Prepare Loki logger
	lokiWriter, err := loki.NewLokiLogger("", jobName, 0)
	if err != nil {
		cliLog.Fatal().Err(err).Msg("Failed to create Loki writer")
	}
	logWriters = append(logWriters, lokiWriter)
	// Prepare CLI log
	logWriter := zerolog.MultiLevelWriter(logWriters...)
	cliLog = zerolog.New(logWriter).With().Timestamp().Logger()

	// Parse log level
	if level, err := zerolog.ParseLevel(rootArgs.logLevel); err != nil {
		cliLog.Fatal().Err(err).
			Str("level", rootArgs.logLevel).Msg("Invalid log level")
	} else {
		cliLog = cliLog.Level(level)
	}

	// Find our external host address
	if rootArgs.server.PublishedHostIP == "" {
		publishedHostIP, err := util.FindServerHostAddress(rootArgs.server.Host)
		if err != nil {
			cliLog.Fatal().Err(err).Msg("Failed to find server host")
		}
		rootArgs.server.PublishedHostIP = publishedHostIP
	}

	// Construct the service
	rootArgs.app.HTTPPort = rootArgs.server.HTTPSPort
	rootArgs.app.HTTPSecure = true
	rootArgs.app.WebDevelopment = rootArgs.server.WebDevelopment
	svc, err := service.New(rootArgs.app, service.Dependencies{
		Logger: cliLog,
	})
	if err != nil {
		cliLog.Fatal().Err(err).Msg("Service construction failed")
	}

	// Construct the server
	svr, err := server.New(rootArgs.server, cliLog, svc)
	if err != nil {
		cliLog.Fatal().Err(err).Msg("Server construction failed")
	}

	// Run the server & service
	g, ctx := errgroup.WithContext(ctx)
	g.Go(func() error {
		if err := svc.Run(ctx); err != nil {
			cliLog.Error().Err(err).Msg("Service run failed")
			return err
		}
		return nil
	})
	g.Go(func() error {
		if err := svr.Run(ctx); err != nil {
			cliLog.Error().Err(err).Msg("Server run failed")
			return err
		}
		return nil
	})
	g.Go(func() error {
		if err := lokiWriter.DiscoverLokiServer(ctx, cliLog); err != nil {
			cliLog.Error().Err(err).Msg("DiscoverLokiServer failed")
			return err
		}
		return nil
	})
	g.Go(func() error {
		// Broadcast service info
		sb := api.NewServiceBroadcaster(cliLog, rootArgs.server.Host,
			api.ServiceTypePrometheusProvider, api.ServiceInfo{
				ApiVersion:   "v1",
				ApiPort:      int32(rootArgs.server.HTTPPort),
				Secure:       false,
				ProviderName: jobName,
			})
		return sb.Run(ctx)
	})
	cliLog.Info().Msgf("Starting server... visit https://%s:%d or binkyrailways://%s:%d to open app.", rootArgs.server.PublishedHostDNSName, rootArgs.server.HTTPSPort, rootArgs.server.PublishedHostDNSName, rootArgs.server.GRPCPort)
	if err := g.Wait(); err != nil {
		cliLog.Fatal().Err(err).Msg("Application failed")
	}
}

// Show usage for the given command
func runUsage(cmd *cobra.Command, args []string) {
	cmd.Usage()
}
