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

package main

import (
	"context"

	"github.com/rs/zerolog"
	"google.golang.org/grpc"

	api "github.com/binkyrailways/BinkyRailways/pkg/api/v1"
)

func main() {
	log := zerolog.New(zerolog.NewConsoleWriter())
	conn, err := grpc.Dial("127.0.0.1:18034", grpc.WithInsecure())
	if err != nil {
		log.Fatal().Err(err).Msg("Dial failed")
	}
	modelClient := api.NewModelServiceClient(conn)
	rw, err := modelClient.GetRailway(context.Background(), &api.Empty{})
	if err != nil {
		log.Fatal().Err(err).Msg("GetRailway failed")
	}
	log.Info().Str("description", rw.GetDescription()).Msg("GetRailway succeeded")
}
