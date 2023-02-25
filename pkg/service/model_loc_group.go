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

// Gets a loc by ID.
func (s *service) getLocGroup(ctx context.Context, lgID string) (model.LocGroup, error) {
	rw, err := s.getRailway()
	if err != nil {
		return nil, err
	}
	lg, ok := rw.GetLocGroups().Get(lgID)
	if !ok || lg == nil {
		return nil, api.NotFound("Loc group: %s", lgID)
	}
	return lg, nil
}

// Gets a loc group by ID.
func (s *service) GetLocGroup(ctx context.Context, req *api.IDRequest) (*api.LocGroup, error) {
	lg, err := s.getLocGroup(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	var result api.LocGroup
	if err := result.FromModel(ctx, lg); err != nil {
		return nil, err
	}
	return &result, nil
}

// Update a loc group by ID.
func (s *service) UpdateLocGroup(ctx context.Context, req *api.LocGroup) (*api.LocGroup, error) {
	lg, err := s.getLocGroup(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	if err := req.ToModel(ctx, lg); err != nil {
		return nil, err
	}
	var result api.LocGroup
	if err := result.FromModel(ctx, lg); err != nil {
		return nil, err
	}
	return &result, nil
}

// Add a loc group by ID.
func (s *service) AddLocGroup(ctx context.Context, req *api.Empty) (*api.LocGroup, error) {
	rw, err := s.getRailway()
	if err != nil {
		return nil, err
	}
	lg := rw.GetLocGroups().AddNew()
	var result api.LocGroup
	if err := result.FromModel(ctx, lg); err != nil {
		return nil, err
	}
	return &result, nil
}

// Delete a loc group by ID.
func (s *service) DeleteLocGroup(ctx context.Context, req *api.IDRequest) (*api.Empty, error) {
	rw, err := s.getRailway()
	if err != nil {
		return nil, err
	}
	locGroup, ok := rw.GetLocGroups().Get(req.GetId())
	if !ok {
		return nil, api.NotFound("Loc: %s", req.GetId())
	}
	rw.GetLocGroups().Remove(locGroup)
	return &api.Empty{}, nil
}
