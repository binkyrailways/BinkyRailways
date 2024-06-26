// Copyright 2020 Ewout Prangsma
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

package main

import (
	"log"

	_ "image/gif"
	_ "image/jpeg"
	_ "image/png"
	_ "net/http/pprof"

	"github.com/binkyrailways/BinkyRailways/cmd"
)

var (
	projectVersion = "dev"
	projectBuild   = "dev"
)

func main() {
	cmd.SetVersionAndBuild(projectVersion, projectBuild)
	if err := cmd.RootCmd.Execute(); err != nil {
		log.Fatalf("%v\n", err)
	}
}
