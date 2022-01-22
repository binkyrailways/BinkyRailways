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

// Gets a block by ID.
func (s *service) GetBlock(ctx context.Context, req *api.IDRequest) (*api.Block, error) {
	moduleID, blockID, err := api.SplitParentChildID(req.GetId())
	if err != nil {
		return nil, err
	}
	mod, err := s.getModule(ctx, moduleID)
	if err != nil {
		return nil, err
	}
	block, ok := mod.GetBlocks().Get(blockID)
	if !ok {
		return nil, api.NotFound(blockID)
	}
	var result api.Block
	if err := result.FromModel(ctx, block); err != nil {
		return nil, err
	}
	return &result, nil
}

// Update a block by ID.
func (s *service) UpdateBlock(ctx context.Context, req *api.Block) (*api.Block, error) {
	moduleID, blockID, err := api.SplitParentChildID(req.GetId())
	if err != nil {
		return nil, err
	}
	mod, err := s.getModule(ctx, moduleID)
	if err != nil {
		return nil, err
	}
	block, ok := mod.GetBlocks().Get(blockID)
	if !ok {
		return nil, api.NotFound(blockID)
	}
	if err := req.ToModel(ctx, block); err != nil {
		return nil, err
	}
	var result api.Block
	if err := result.FromModel(ctx, block); err != nil {
		return nil, err
	}
	return &result, nil
}

// Adds a new block in the module identified by given by ID.
func (s *service) AddBlock(ctx context.Context, req *api.IDRequest) (*api.Block, error) {
	mod, err := s.getModule(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	block := mod.GetBlocks().AddNew()
	var result api.Block
	if err := result.FromModel(ctx, block); err != nil {
		return nil, err
	}
	return &result, nil
}

// Delete a block by ID.
func (s *service) DeleteBlock(ctx context.Context, req *api.IDRequest) (*api.Module, error) {
	moduleID, blockID, err := api.SplitParentChildID(req.GetId())
	if err != nil {
		return nil, err
	}
	mod, err := s.getModule(ctx, moduleID)
	if err != nil {
		return nil, err
	}
	block, ok := mod.GetBlocks().Get(blockID)
	if !ok {
		return nil, api.NotFound(blockID)
	}
	mod.GetBlocks().Remove(block)
	var result api.Module
	if err := result.FromModel(ctx, mod); err != nil {
		return nil, err
	}
	return &result, nil
}
