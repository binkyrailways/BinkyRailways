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

// Gets a edge by ID.
func (s *service) GetEdge(ctx context.Context, req *api.IDRequest) (*api.Edge, error) {
	moduleID, edgeID, err := api.SplitModuleEntityID(req.GetId())
	if err != nil {
		return nil, err
	}
	mod, err := s.getModule(ctx, moduleID)
	if err != nil {
		return nil, err
	}
	edge, ok := mod.GetEdges().Get(edgeID)
	if !ok {
		return nil, api.NotFound(edgeID)
	}
	var result api.Edge
	if err := result.FromModel(ctx, edge); err != nil {
		return nil, err
	}
	return &result, nil
}

// Update a edge by ID.
func (s *service) UpdateEdge(ctx context.Context, req *api.Edge) (*api.Edge, error) {
	moduleID, edgeID, err := api.SplitModuleEntityID(req.GetId())
	if err != nil {
		return nil, err
	}
	mod, err := s.getModule(ctx, moduleID)
	if err != nil {
		return nil, err
	}
	edge, ok := mod.GetEdges().Get(edgeID)
	if !ok {
		return nil, api.NotFound(edgeID)
	}
	if err := req.ToModel(ctx, edge); err != nil {
		return nil, err
	}
	var result api.Edge
	if err := result.FromModel(ctx, edge); err != nil {
		return nil, err
	}
	return &result, nil
}
