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
)

// Change the speed and direction of a loc
func (s *service) SetLocSpeedAndDirection(ctx context.Context, req *api.SetLocSpeedAndDirectionRequest) (*api.LocState, error) {
	rwState, err := s.getRailwayState()
	if err != nil {
		return nil, err
	}
	locState, err := rwState.GetLoc(req.GetId())
	if err != nil {
		return nil, err
	}
	if locState == nil {
		return nil, api.NotFound("Loc '%s'", req.GetId())
	}
	direction, err := req.GetDirection().ToState(ctx)
	if err != nil {
		return nil, err
	}
	if err := locState.GetSpeed().SetRequested(ctx, int(req.GetSpeed())); err != nil {
		return nil, err
	}
	if err := locState.GetDirection().SetRequested(ctx, direction); err != nil {
		return nil, err
	}
	var result api.LocState
	if err := result.FromState(ctx, locState); err != nil {
		return nil, err
	}
	return &result, nil
}

// Change the automatically controlled state of a loc
func (s *service) SetLocControlledAutomatically(ctx context.Context, req *api.SetLocControlledAutomaticallyRequest) (*api.LocState, error) {
	rwState, err := s.getRailwayState()
	if err != nil {
		return nil, err
	}
	locState, err := rwState.GetLoc(req.GetId())
	if err != nil {
		return nil, err
	}
	if locState == nil {
		return nil, api.NotFound("Loc '%s'", req.GetId())
	}
	if err := locState.GetControlledAutomatically().SetRequested(ctx, req.GetEnabled()); err != nil {
		return nil, err
	}
	var result api.LocState
	if err := result.FromState(ctx, locState); err != nil {
		return nil, err
	}
	return &result, nil
}

// Change functions of the loc.
func (s *service) SetLocFunctions(ctx context.Context, req *api.SetLocFunctionsRequest) (*api.LocState, error) {
	rwState, err := s.getRailwayState()
	if err != nil {
		return nil, err
	}
	locState, err := rwState.GetLoc(req.GetId())
	if err != nil {
		return nil, err
	}
	if locState == nil {
		return nil, api.NotFound("Loc '%s'", req.GetId())
	}
	for _, lf := range req.GetFunctions() {
		switch lf.GetIndex() {
		case 0:
			if err := locState.GetF0().SetRequested(ctx, lf.GetValue()); err != nil {
				return nil, err
			}
		default:
			return nil, api.InvalidArgument("Unknown function %d", lf.GetIndex())
		}
	}
	var result api.LocState
	if err := result.FromState(ctx, locState); err != nil {
		return nil, err
	}
	return &result, nil
}

// Assign a loc to a block
func (s *service) AssignLocToBlock(ctx context.Context, req *api.AssignLocToBlockRequest) (*api.RailwayState, error) {
	rwState, err := s.getRailwayState()
	if err != nil {
		return nil, err
	}
	locState, err := rwState.GetLoc(req.GetLocId())
	if err != nil {
		return nil, err
	}
	if locState == nil {
		return nil, api.NotFound("Loc '%s'", req.GetLocId())
	}
	_, blockID, err := api.SplitParentChildID(req.GetBlockId())
	if err != nil {
		return nil, err
	}
	blockState, err := rwState.GetBlock(blockID)
	if err != nil {
		return nil, err
	}
	if blockState == nil {
		return nil, api.NotFound("Block '%s'", blockID)
	}
	blockSide, err := req.GetBlockSide().ToModel(ctx)
	if err != nil {
		return nil, err
	}
	if err := locState.AssignTo(ctx, blockState, blockSide.Invert()); err != nil {
		return nil, err
	}
	var result api.RailwayState
	if err := result.FromState(ctx, s.railwayState); err != nil {
		return nil, err
	}
	return &result, nil
}

// Take a loc of the track
func (s *service) TakeLocOfTrack(ctx context.Context, req *api.TakeLocOfTrackRequest) (*api.RailwayState, error) {
	rwState, err := s.getRailwayState()
	if err != nil {
		return nil, err
	}
	locState, err := rwState.GetLoc(req.GetLocId())
	if err != nil {
		return nil, err
	}
	if locState == nil {
		return nil, api.NotFound("Loc '%s'", req.GetLocId())
	}
	locState.Reset(ctx)
	var result api.RailwayState
	if err := result.FromState(ctx, s.railwayState); err != nil {
		return nil, err
	}
	return &result, nil
}
