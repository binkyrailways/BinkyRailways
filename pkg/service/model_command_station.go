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

package service

import (
	"context"

	api "github.com/binkyrailways/BinkyRailways/pkg/api/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// Gets a command station by ID.
func (s *service) getCommandStation(ctx context.Context, csID string) (model.CommandStation, error) {
	log := s.Logger.With().Str("cs_id", csID).Logger()
	rw, err := s.getRailway()
	if err != nil {
		log.Debug().Err(err).Msg("Failed to load railway")
		return nil, err
	}
	csRef, ok := rw.GetCommandStations().Get(csID)
	if !ok {
		log.Debug().Err(err).Msg("Command station not found")
		return nil, api.NotFound("Command station: %s", csID)
	}
	cs, err := csRef.TryResolve()
	if err != nil {
		return nil, api.NotFound("Failed to resolve command station '%s': %s", csID, err)
	}
	if cs == nil {
		log.Debug().Err(err).Msg("Failed to resolve command station")
		return nil, api.NotFound("Failed to resolve command station: %s", csID)
	}
	return cs, nil
}

// Gets a CommandStation by ID.
func (s *service) GetCommandStation(ctx context.Context, req *api.IDRequest) (*api.CommandStation, error) {
	cs, err := s.getCommandStation(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	var result api.CommandStation
	if err := result.FromModel(ctx, cs); err != nil {
		return nil, err
	}
	return &result, nil
}

// Update a CommandStation by ID.
func (s *service) UpdateCommandStation(ctx context.Context, req *api.CommandStation) (*api.CommandStation, error) {
	cs, err := s.getCommandStation(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	if err := req.ToModel(ctx, cs); err != nil {
		return nil, err
	}
	var result api.CommandStation
	if err := result.FromModel(ctx, cs); err != nil {
		return nil, err
	}
	return &result, nil
}
