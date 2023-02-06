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
	"os"
	"time"

	"github.com/rs/zerolog"
	"github.com/spf13/cobra"
	"golang.org/x/sync/errgroup"

	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkynet/BinkyNet/loki"
	"github.com/binkyrailways/BinkyRailways/pkg/core/util"
	"github.com/binkyrailways/BinkyRailways/pkg/metrics/promconfig"
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
		app            service.Config
		server         server.Config
		logFile        string
		promConfigPath string
		promURL        string
		// Host name of the host running this process as seen from prometheus
		hostNameFromPrometheus string
	}
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
	// Prometheus config builder arguments
	f.StringVar(&rootArgs.promConfigPath, "prom-config-path", "./scripts/prometheus.yml", "Path of prometheus config file")
	f.StringVar(&rootArgs.promURL, "prom-url", "http://localhost:9090", "URL towards prometheus")
	f.StringVar(&rootArgs.hostNameFromPrometheus, "hostname-from-prom", "host.docker.internal", "Host name of the host running this process as seen from prometheus")
	// Server arguments
	f.StringVar(&rootArgs.server.Host, "host", "0.0.0.0", "Host to serve on")
	f.IntVar(&rootArgs.server.HTTPPort, "http-port", 18033, "Port number to serve HTTP on")
	f.IntVar(&rootArgs.server.GRPCPort, "grpc-port", 18034, "Port number to serve GRPC on")
	f.StringVar(&rootArgs.server.LokiURL, "loki-url", "http://127.0.0.1:3100", "URL of loki")
	f.IntVar(&rootArgs.server.LokiPort, "loki-port", 13100, "Port to serve Loki requests on")
	// Service arguments
	f.StringVarP(&rootArgs.app.RailwayPath, "railway", "r", "", "Path of railway file")
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
	if len(args) == 1 && rootArgs.app.RailwayPath == "" {
		rootArgs.app.RailwayPath = args[0]
	}

	// Setup log file
	logFile, err := os.Create(rootArgs.logFile)
	if err != nil {
		cliLog.Fatal().Err(err).Msg("Failed to open log file")
	}
	defer logFile.Close()
	var logWriter zerolog.LevelWriter
	lokiWriter, err := loki.NewLokiLogger(rootArgs.server.LokiURL, "brw", 0)
	if err != nil {
		cliLog.Fatal().Err(err).Msg("Failed to create Loki writer")
	}
	logWriter = zerolog.MultiLevelWriter(
		newConsoleWriter(),
		lokiWriter,
	)
	cliLog = zerolog.New(logWriter).With().Timestamp().Logger()

	// Construct the prometheus config builder
	pcb, err := promconfig.NewPrometheusConfigBuilder(cliLog, rootArgs.promConfigPath, rootArgs.promURL)
	if err != nil {
		cliLog.Fatal().Err(err).Msg("Prometheus config builder construction failed")
	}
	pcb.RegisterTarget("binkyrailways", rootArgs.hostNameFromPrometheus, rootArgs.server.HTTPPort, false)

	// Construct the service
	rootArgs.app.HTTPPort = rootArgs.server.HTTPPort
	svc := service.New(rootArgs.app, service.Dependencies{
		Logger:                  cliLog,
		PrometheusConfigBuilder: pcb,
	})

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
		host, err := util.FindServerHostAddress(rootArgs.server.Host)
		if err != nil {
			cliLog.Fatal().Err(err).Msg("Failed to find server host")
		}
		cliLog.Info().Str("host", host).Msg("Registering loki service")
		ctx := api.WithServiceInfoHost(ctx, host)
		return api.RegisterServiceEntry(ctx, api.ServiceTypeLokiProvider, api.ServiceInfo{
			ApiVersion: "v1",
			ApiPort:    int32(rootArgs.server.LokiPort),
			Secure:     false,
		})
	})
	if err := g.Wait(); err != nil {
		cliLog.Fatal().Err(err).Msg("Application failed")
	}
}
