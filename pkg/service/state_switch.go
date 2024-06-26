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
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// Change the direction of a junction of type switch
func (s *service) SetSwitchDirection(ctx context.Context, req *api.SetSwitchDirectionRequest) (*api.JunctionState, error) {
	rwState, err := s.getRailwayState()
	if err != nil {
		return nil, err
	}
	_, junctionID, err := api.SplitParentChildID(req.GetId())
	if err != nil {
		return nil, err
	}
	junctionState, err := rwState.GetJunction(junctionID)
	if err != nil {
		return nil, err
	}
	if junctionState == nil {
		return nil, api.NotFound("Junction '%s'", req.GetId())
	}
	swState, ok := junctionState.(state.Switch)
	if !ok {
		return nil, api.InvalidArgument("Junction '%s' is not a switch", req.GetId())
	}
	direction, err := req.GetDirection().ToModel(ctx)
	if err != nil {
		return nil, err
	}
	if err := swState.GetDirection().SetRequested(ctx, direction); err != nil {
		return nil, err
	}
	if et := rwState.GetEntityTester(); et.GetEnabled().GetRequested(ctx) {
		if et.IsIncluded(ctx, swState) {
			et.Exclude(ctx, swState)
		} else {
			et.Include(ctx, swState)
		}
	}
	var result api.JunctionState
	if err := result.FromState(ctx, junctionState); err != nil {
		return nil, err
	}
	return &result, nil
}
