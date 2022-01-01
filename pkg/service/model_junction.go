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

// Gets a Junction by ID.
func (s *service) GetJunction(ctx context.Context, req *api.IDRequest) (*api.Junction, error) {
	moduleID, junctionID, err := api.SplitParentChildID(req.GetId())
	if err != nil {
		return nil, err
	}
	mod, err := s.getModule(ctx, moduleID)
	if err != nil {
		return nil, err
	}
	junction, ok := mod.GetJunctions().Get(junctionID)
	if !ok {
		return nil, api.NotFound(junctionID)
	}
	var result api.Junction
	if err := result.FromModel(ctx, junction); err != nil {
		return nil, err
	}
	return &result, nil
}

// Update a loc by ID.
func (s *service) UpdateJunction(ctx context.Context, req *api.Junction) (*api.Junction, error) {
	moduleID, junctionID, err := api.SplitParentChildID(req.GetId())
	if err != nil {
		return nil, err
	}
	mod, err := s.getModule(ctx, moduleID)
	if err != nil {
		return nil, err
	}
	junction, ok := mod.GetJunctions().Get(junctionID)
	if !ok {
		return nil, api.NotFound(junctionID)
	}
	if err := req.ToModel(ctx, junction); err != nil {
		return nil, err
	}
	var result api.Junction
	if err := result.FromModel(ctx, junction); err != nil {
		return nil, err
	}
	return &result, nil
}
