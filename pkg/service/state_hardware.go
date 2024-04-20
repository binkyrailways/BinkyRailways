// Copyright 2022 Ewout Prangsma
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
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// Trigger a hardware discovery request
func (s *service) DiscoverHardware(ctx context.Context, req *api.DiscoverHardwareRequest) (*api.DiscoverHardwareResponse, error) {
	rwState, err := s.getRailwayState()
	if err != nil {
		return nil, err
	}
	rwState.ForEachCommandStation(func(cs state.CommandStation) {
		cs.TriggerDiscover(ctx, req.GetHardwareModuleId())
	})
	var result api.DiscoverHardwareResponse
	return &result, nil
}

// Request a reset of hardware module with given ID
func (s *service) ResetHardwareModule(ctx context.Context, req *api.IDRequest) (*api.Empty, error) {
	rwState, err := s.getRailwayState()
	if err != nil {
		return nil, err
	}
	rwState.ForEachCommandStation(func(cs state.CommandStation) {
		cs.ResetHardwareModule(ctx, req.GetId())
	})
	var result api.Empty
	return &result, nil
}
