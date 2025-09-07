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

// Change the active state of an output of type binary output
func (s *service) SetBinaryOutputActive(ctx context.Context, req *api.SetBinaryOutputActiveRequest) (*api.OutputState, error) {
	rwState, err := s.getRailwayState()
	if err != nil {
		return nil, err
	}
	_, outputID, err := api.SplitParentChildID(req.GetId())
	if err != nil {
		return nil, err
	}
	outputState, err := rwState.GetOutput(outputID)
	if err != nil {
		return nil, err
	}
	if outputState == nil {
		return nil, api.NotFound("Output '%s'", req.GetId())
	}
	boState, ok := outputState.(state.BinaryOutput)
	if !ok {
		return nil, api.InvalidArgument("Output '%s' is not a binary output", req.GetId())
	}
	if err := boState.GetActive().SetRequested(ctx, req.GetActive()); err != nil {
		return nil, err
	}
	if et := rwState.GetEntityTester(); et.GetEnabled().GetRequested(ctx) {
		if et.IsIncluded(ctx, boState) {
			et.Exclude(ctx, boState)
		} else {
			et.Include(ctx, boState)
		}
	}
	var result api.OutputState
	if err := result.FromState(ctx, outputState); err != nil {
		return nil, err
	}
	return &result, nil
}
