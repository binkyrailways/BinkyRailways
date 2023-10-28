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
func (s *service) getCommandStation(ctx context.Context, csID string) (model.CommandStation, model.CommandStationRef, error) {
	log := s.Logger.With().Str("cs_id", csID).Logger()
	rw, err := s.getRailway()
	if err != nil {
		log.Debug().Err(err).Msg("Failed to load railway")
		return nil, nil, err
	}
	csRef, ok := rw.GetCommandStations().Get(csID)
	if !ok {
		log.Debug().Err(err).Msg("Command station not found")
		return nil, nil, api.NotFound("Command station: %s", csID)
	}
	cs, err := csRef.TryResolve()
	if err != nil {
		return nil, nil, api.NotFound("Failed to resolve command station '%s': %s", csID, err)
	}
	if cs == nil {
		log.Debug().Err(err).Msg("Failed to resolve command station")
		return nil, nil, api.NotFound("Failed to resolve command station: %s", csID)
	}
	return cs, csRef, nil
}

// Gets a CommandStation by ID.
func (s *service) GetCommandStation(ctx context.Context, req *api.IDRequest) (*api.CommandStation, error) {
	cs, csRef, err := s.getCommandStation(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	var result api.CommandStation
	if err := result.FromModel(ctx, cs, csRef); err != nil {
		return nil, err
	}
	return &result, nil
}

// Update a CommandStation by ID.
func (s *service) UpdateCommandStation(ctx context.Context, req *api.CommandStation) (*api.CommandStation, error) {
	cs, csRef, err := s.getCommandStation(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	if err := req.ToModel(ctx, cs, csRef); err != nil {
		return nil, err
	}
	var result api.CommandStation
	if err := result.FromModel(ctx, cs, csRef); err != nil {
		return nil, err
	}
	return &result, nil
}

// Add a bidib CommandStation.
func (s *service) AddBidibCommandStation(ctx context.Context, req *api.Empty) (*api.CommandStation, error) {
	log := s.Logger.With().Logger()
	rw, err := s.getRailway()
	if err != nil {
		log.Debug().Err(err).Msg("Failed to load railway")
		return nil, err
	}
	cs, err := rw.GetPackage().AddNewBidibCommandStation()
	if err != nil {
		log.Debug().Err(err).Msg("Failed to add bidib commandstation to package")
		return nil, err
	}
	csRef := rw.GetCommandStations().Add(cs)
	var result api.CommandStation
	if err := result.FromModel(ctx, cs, csRef); err != nil {
		return nil, err
	}
	return &result, nil
}

// Add a binkynet CommandStation.
func (s *service) AddBinkyNetCommandStation(ctx context.Context, req *api.Empty) (*api.CommandStation, error) {
	log := s.Logger.With().Logger()
	rw, err := s.getRailway()
	if err != nil {
		log.Debug().Err(err).Msg("Failed to load railway")
		return nil, err
	}
	cs, err := rw.GetPackage().AddNewBinkyNetCommandStation()
	if err != nil {
		log.Debug().Err(err).Msg("Failed to add binkynet commandstation to package")
		return nil, err
	}
	csRef := rw.GetCommandStations().Add(cs)
	var result api.CommandStation
	if err := result.FromModel(ctx, cs, csRef); err != nil {
		return nil, err
	}
	return &result, nil
}
