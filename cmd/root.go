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
	"github.com/rs/zerolog"
	"github.com/spf13/cobra"

	"github.com/binkyrailways/BinkyRailways/pkg/app"
)

var (
	// RootCmd is the root of the CLI command structure
	RootCmd = &cobra.Command{
		Use:   "binkyrailways",
		Short: "BinkRailways 2.0",
		Run:   runRootCmd,
	}
	rootArgs struct {
		app app.Config
	}
	cliLog = zerolog.New(zerolog.NewConsoleWriter())
)

func init() {
	f := RootCmd.Flags()
	f.StringVarP(&rootArgs.app.RailwayPath, "railway", "r", "", "Path of railway file")
}

// SetVersionAndBuild configures project version & build number
func SetVersionAndBuild(version, build string) {
	rootArgs.app.ProjectVersion = version
	rootArgs.app.ProjectBuild = build
}

// Run the service
func runRootCmd(cmd *cobra.Command, args []string) {
	if len(args) == 1 && rootArgs.app.RailwayPath == "" {
		rootArgs.app.RailwayPath = args[0]
	}
	a := app.New(rootArgs.app, app.Dependencies{
		Logger: cliLog,
	})
	if err := a.Run(); err != nil {
		cliLog.Fatal().Err(err).Msg("Application failed")
	}
}
