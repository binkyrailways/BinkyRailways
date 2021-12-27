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

	"github.com/rs/zerolog"
	"github.com/spf13/cobra"
	"golang.org/x/sync/errgroup"

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
		app    service.Config
		server server.Config
	}
	cliLog = zerolog.New(zerolog.NewConsoleWriter())
)

func init() {
	f := RootCmd.Flags()
	// Server arguments
	f.StringVar(&rootArgs.server.Host, "host", "0.0.0.0", "Host to serve on")
	f.IntVar(&rootArgs.server.GRPCPort, "grpc-port", 18034, "Port number to serve GRPC on")
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
	ctx := context.Background()
	if len(args) == 1 && rootArgs.app.RailwayPath == "" {
		rootArgs.app.RailwayPath = args[0]
	}

	// Construct the service
	svc := service.New(rootArgs.app, service.Dependencies{
		Logger: cliLog,
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
	if err := g.Wait(); err != nil {
		cliLog.Fatal().Err(err).Msg("Application failed")
	}
}
