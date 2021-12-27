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

// Gets the current railway
func (s *service) GetRailway(ctx context.Context, req *api.Empty) (*api.Railway, error) {
	rw, err := s.getRailway()
	if err != nil {
		return nil, err
	}
	var result api.Railway
	if err := result.FromModel(ctx, rw); err != nil {
		return nil, err
	}
	return &result, nil
}

// Update the current railway
func (s *service) UpdateRailway(ctx context.Context, req *api.Railway) (*api.Railway, error) {
	return nil, api.Unknown("Not implemented")
}

// Save changes to disk
func (s *service) Save(ctx context.Context, req *api.Empty) (*api.Empty, error) {
	rw, err := s.getRailway()
	if err != nil {
		return nil, err
	}
	if err := rw.GetPackage().Save(); err != nil {
		return nil, err
	}
	return &api.Empty{}, nil
}

// Gets a module by ID.
func (s *service) GetModule(ctx context.Context, req *api.IDRequest) (*api.Module, error) {
	rw, err := s.getRailway()
	if err != nil {
		return nil, err
	}
	modRef, ok := rw.GetModules().Get(req.GetId())
	if !ok {
		return nil, api.NotFound(req.GetId())
	}
	mod := modRef.TryResolve()
	if mod == nil {
		return nil, api.NotFound(req.GetId())
	}
	var result api.Module
	if err := result.FromModel(ctx, mod); err != nil {
		return nil, err
	}
	return &result, nil
}

// Update a module by ID.
func (s *service) UpdateModule(ctx context.Context, req *api.Module) (*api.Module, error) {
	return nil, api.Unknown("Not implemented")
}

// Gets a loc by ID.
func (s *service) GetLoc(ctx context.Context, req *api.IDRequest) (*api.Loc, error) {
	rw, err := s.getRailway()
	if err != nil {
		return nil, err
	}
	locRef, ok := rw.GetLocs().Get(req.GetId())
	if !ok {
		return nil, api.NotFound(req.GetId())
	}
	loc := locRef.TryResolve()
	if loc == nil {
		return nil, api.NotFound(req.GetId())
	}
	var result api.Loc
	if err := result.FromModel(ctx, loc); err != nil {
		return nil, err
	}
	return &result, nil
}

// Update a loc by ID.
func (s *service) UpdateLoc(ctx context.Context, req *api.Loc) (*api.Loc, error) {
	return nil, api.Unknown("Not implemented")
}
