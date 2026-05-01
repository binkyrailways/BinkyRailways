// Copyright 2024 Ewout Prangsma
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

// Gets a RailPoint by ID.
func (s *service) getRailPoint(ctx context.Context, id string) (model.RailPoint, error) {
	moduleID, railPointID, err := api.SplitParentChildID(id)
	if err != nil {
		return nil, err
	}
	mod, err := s.getModule(ctx, moduleID)
	if err != nil {
		return nil, err
	}
	railPoint, ok := mod.GetRailPoints().Get(railPointID)
	if !ok {
		return nil, api.NotFound("%s", railPointID)
	}
	return railPoint, nil
}

// Gets a RailPoint by ID.
func (s *service) GetRailPoint(ctx context.Context, req *api.IDRequest) (*api.RailPoint, error) {
	railPoint, err := s.getRailPoint(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	var result api.RailPoint
	if err := result.FromModel(ctx, railPoint); err != nil {
		return nil, err
	}
	return &result, nil
}

// Update a RailPoint by ID.
func (s *service) UpdateRailPoint(ctx context.Context, req *api.RailPoint) (*api.RailPoint, error) {
	railPoint, err := s.getRailPoint(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	if err := req.ToModel(ctx, railPoint); err != nil {
		return nil, err
	}
	var result api.RailPoint
	if err := result.FromModel(ctx, railPoint); err != nil {
		return nil, err
	}
	return &result, nil
}

// Adds a new rail point in the module identified by given by ID.
func (s *service) AddRailPoint(ctx context.Context, req *api.IDRequest) (*api.RailPoint, error) {
	mod, err := s.getModule(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	railPoint := mod.GetRailPoints().AddNew()
	var result api.RailPoint
	if err := result.FromModel(ctx, railPoint); err != nil {
		return nil, err
	}
	return &result, nil
}

// Delete a rail point by ID.
func (s *service) DeleteRailPoint(ctx context.Context, req *api.IDRequest) (*api.Module, error) {
	moduleID, railPointID, err := api.SplitParentChildID(req.GetId())
	if err != nil {
		return nil, err
	}
	mod, err := s.getModule(ctx, moduleID)
	if err != nil {
		return nil, err
	}
	httpHost, err := s.getHttpHost(ctx)
	if err != nil {
		return nil, err
	}
	railPoint, ok := mod.GetRailPoints().Get(railPointID)
	if !ok {
		return nil, api.NotFound("%s", railPointID)
	}
	mod.GetRailPoints().Remove(railPoint)
	var result api.Module
	if err := result.FromModel(ctx, mod, httpHost, s.HTTPSecure); err != nil {
		return nil, err
	}
	return &result, nil
}
