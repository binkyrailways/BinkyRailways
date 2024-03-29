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

// Gets a Output by ID.
func (s *service) getOutput(ctx context.Context, id string) (model.Output, error) {
	moduleID, outputID, err := api.SplitParentChildID(id)
	if err != nil {
		return nil, err
	}
	mod, err := s.getModule(ctx, moduleID)
	if err != nil {
		return nil, err
	}
	output, ok := mod.GetOutputs().Get(outputID)
	if !ok {
		return nil, api.NotFound(outputID)
	}
	return output, nil
}

// Gets a Output by ID.
func (s *service) GetOutput(ctx context.Context, req *api.IDRequest) (*api.Output, error) {
	output, err := s.getOutput(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	var result api.Output
	if err := result.FromModel(ctx, output); err != nil {
		return nil, err
	}
	return &result, nil
}

// Update a Output by ID.
func (s *service) UpdateOutput(ctx context.Context, req *api.Output) (*api.Output, error) {
	output, err := s.getOutput(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	if err := req.ToModel(ctx, output); err != nil {
		return nil, err
	}
	var result api.Output
	if err := result.FromModel(ctx, output); err != nil {
		return nil, err
	}
	return &result, nil
}

// Adds a new output of type binary output in the module identified by given by ID.
func (s *service) AddBinaryOutput(ctx context.Context, req *api.IDRequest) (*api.Output, error) {
	mod, err := s.getModule(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	output := mod.GetOutputs().AddNewBinaryOutput()
	var result api.Output
	if err := result.FromModel(ctx, output); err != nil {
		return nil, err
	}
	return &result, nil
}

// Delete an output by ID.
func (s *service) DeleteOutput(ctx context.Context, req *api.IDRequest) (*api.Module, error) {
	moduleID, outputID, err := api.SplitParentChildID(req.GetId())
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
	output, ok := mod.GetOutputs().Get(outputID)
	if !ok {
		return nil, api.NotFound(outputID)
	}
	mod.GetOutputs().Remove(output)
	var result api.Module
	if err := result.FromModel(ctx, mod, httpHost, s.HTTPSecure); err != nil {
		return nil, err
	}
	return &result, nil
}
