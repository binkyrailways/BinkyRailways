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

// Gets a module by ID.
func (s *service) getModule(ctx context.Context, moduleID string) (model.Module, error) {
	log := s.Logger.With().Str("module_id", moduleID).Logger()
	rw, err := s.getRailway()
	if err != nil {
		log.Debug().Err(err).Msg("Failed to load railway")
		return nil, err
	}
	modRef, ok := rw.GetModules().Get(moduleID)
	if !ok {
		log.Debug().Err(err).Msg("Module not found")
		return nil, api.NotFound("Module: %s", moduleID)
	}
	mod, err := modRef.TryResolve()
	if err != nil {
		return nil, api.NotFound("Failed to resolve module '%s': %s", moduleID, err)
	}
	if mod == nil {
		log.Debug().Err(err).Msg("Failed to resolve module")
		return nil, api.NotFound("Failed to resolve module: %s", moduleID)
	}
	return mod, nil
}

// Gets a module by ID.
func (s *service) GetModule(ctx context.Context, req *api.IDRequest) (*api.Module, error) {
	mod, err := s.getModule(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	var result api.Module
	if err := result.FromModel(ctx, mod); err != nil {
		return nil, err
	}
	return &result, nil
}

// Update a module by ID.
func (s *service) UpdateModule(ctx context.Context, req *api.Module) (*api.Module, error) {
	mod, err := s.getModule(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	if err := req.ToModel(ctx, mod); err != nil {
		return nil, err
	}
	var result api.Module
	if err := result.FromModel(ctx, mod); err != nil {
		return nil, err
	}
	return &result, nil
}
