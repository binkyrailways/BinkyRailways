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

// Gets a block group by ID.
func (s *service) GetBlockGroup(ctx context.Context, req *api.IDRequest) (*api.BlockGroup, error) {
	moduleID, blockGroupID, err := api.SplitParentChildID(req.GetId())
	if err != nil {
		return nil, err
	}
	mod, err := s.getModule(ctx, moduleID)
	if err != nil {
		return nil, err
	}
	blockGroup, ok := mod.GetBlockGroups().Get(blockGroupID)
	if !ok {
		return nil, api.NotFound(blockGroupID)
	}
	var result api.BlockGroup
	if err := result.FromModel(ctx, blockGroup); err != nil {
		return nil, err
	}
	return &result, nil
}

// Update a block group by ID.
func (s *service) UpdateBlockGroup(ctx context.Context, req *api.BlockGroup) (*api.BlockGroup, error) {
	moduleID, blockGroupID, err := api.SplitParentChildID(req.GetId())
	if err != nil {
		return nil, err
	}
	mod, err := s.getModule(ctx, moduleID)
	if err != nil {
		return nil, err
	}
	blockGroup, ok := mod.GetBlockGroups().Get(blockGroupID)
	if !ok {
		return nil, api.NotFound(blockGroupID)
	}
	if err := req.ToModel(ctx, blockGroup); err != nil {
		return nil, err
	}
	var result api.BlockGroup
	if err := result.FromModel(ctx, blockGroup); err != nil {
		return nil, err
	}
	return &result, nil
}

// Adds a new block group in the module identified by given by ID.
func (s *service) AddBlockGroup(ctx context.Context, req *api.IDRequest) (*api.BlockGroup, error) {
	mod, err := s.getModule(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	blockGroup := mod.GetBlockGroups().AddNew()
	var result api.BlockGroup
	if err := result.FromModel(ctx, blockGroup); err != nil {
		return nil, err
	}
	return &result, nil
}

// Delete a block group by ID.
func (s *service) DeleteBlockGroup(ctx context.Context, req *api.IDRequest) (*api.Module, error) {
	moduleID, blockGroupID, err := api.SplitParentChildID(req.GetId())
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
	blockGroup, ok := mod.GetBlockGroups().Get(blockGroupID)
	if !ok {
		return nil, api.NotFound(blockGroupID)
	}
	mod.GetBlockGroups().Remove(blockGroup)
	var result api.Module
	if err := result.FromModel(ctx, mod, httpHost); err != nil {
		return nil, err
	}
	return &result, nil
}
