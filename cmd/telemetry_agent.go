// Copyright 2024 Ewout Prangsma
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

	"github.com/rs/zerolog"
	"github.com/spf13/cobra"
	"golang.org/x/sync/errgroup"

	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkynet/BinkyNet/loki"
	"github.com/binkyrailways/BinkyRailways/pkg/metrics/promconfig"
	"github.com/binkyrailways/BinkyRailways/pkg/telemetry/agent"
)

var (
	// telemetryCmd is the root of all cli telemetry commands
	telemetryCmd = &cobra.Command{
		Use:   "telemetry",
		Short: "Telemetry comments",
		Run:   runUsage,
	}
	// telemetryAgentCmd is the command that run a telemetry agent.
	telemetryAgentCmd = &cobra.Command{
		Use:   "agent",
		Short: "Telemetry Agent",
		Run:   runTelemetryAgentCmd,
	}
	telemetryAgentArgs struct {
		hostMask         string
		lokiHost         string
		lokiPort         int
		externalLokiPort int
		promConfigPath   string
		promURL          string
		// Host name of the host running this process as seen from prometheus
		//hostNameFromPrometheus string
	}
)

func init() {
	RootCmd.AddCommand(telemetryCmd)
	telemetryCmd.AddCommand(telemetryAgentCmd)

	f := telemetryAgentCmd.Flags()
	// Log arguments
	args := &telemetryAgentArgs
	f.StringVar(&args.hostMask, "host", "0.0.0.0", "Host to serve on")
	// Prometheus config builder arguments
	f.StringVar(&args.promConfigPath, "prom-config-path", "./scripts/prometheus.yml", "Path of prometheus config file")
	f.StringVar(&args.promURL, "prom-url", "http://127.0.0.1:9090", "URL towards prometheus")
	//f.StringVar(&args.hostNameFromPrometheus, "hostname-from-prom", "host.docker.internal", "Host name of the host running this process as seen from prometheus")
	// Loki registration arguments
	f.StringVar(&args.lokiHost, "loki-host", "127.0.0.1", "Host to forward Loki requests to")
	f.IntVar(&args.lokiPort, "loki-port", 3100, "Port to forward Loki requests to")
	f.IntVar(&args.externalLokiPort, "external-loki-port", 13100, "Port to serve Loki requests on")
}

// Run the telemetry agent
func runTelemetryAgentCmd(cmd *cobra.Command, _ []string) {
	// Setup bare logger
	cliLog := zerolog.New(newConsoleWriter()).With().Timestamp().Logger()
	ctx := context.Background()
	args := &telemetryAgentArgs

	// Setup log file
	logWriters := []io.Writer{
		newConsoleWriter(),
	}
	/*if rootArgs.server.LokiURL != "" {
		lokiWriter, err := loki.NewLokiLogger(rootArgs.server.LokiURL, "brw", 0)
		if err != nil {
			cliLog.Fatal().Err(err).Msg("Failed to create Loki writer")
		}
		logWriters = append(logWriters, lokiWriter)
	}*/
	logWriter := zerolog.MultiLevelWriter(logWriters...)
	cliLog = zerolog.New(logWriter).With().Timestamp().Logger()

	// Construct the prometheus config builder
	pcb, err := promconfig.NewPrometheusConfigBuilder(cliLog, args.promConfigPath, args.promURL)
	if err != nil {
		cliLog.Fatal().Err(err).Msg("Prometheus config builder construction failed")
	}

	// Construct telemetry agent
	a, err := agent.New(cliLog, pcb)
	if err != nil {
		cliLog.Fatal().Err(err).Msg("Telemetry agent construction failed")
	}

	// Construct loki proxy
	lp := loki.NewLokiProxy(cliLog, args.lokiHost, args.lokiPort, false)

	// Construct server
	svr, err := agent.NewServer(agent.Config{
		Host:     args.hostMask,
		HTTPPort: args.externalLokiPort,
	}, cliLog, lp)
	if err != nil {
		cliLog.Fatal().Err(err).Msg("Server construction failed")
	}

	// Run the agent & registration service
	g, ctx := errgroup.WithContext(ctx)
	g.Go(func() error {
		return a.Run(ctx)
	})
	g.Go(func() error {
		return svr.Run(ctx)
	})
	g.Go(func() error {
		// Broadcast service info
		sb := api.NewServiceBroadcaster(cliLog, args.hostMask,
			api.ServiceTypeLokiProvider, api.ServiceInfo{
				ApiVersion: "v1",
				ApiPort:    int32(args.externalLokiPort),
				Secure:     false,
			})
		return sb.Run(ctx)
	})
	cliLog.Info().Msg("Starting telemetry agent.")
	if err := g.Wait(); err != nil {
		cliLog.Fatal().Err(err).Msg("Application failed")
	}
}
