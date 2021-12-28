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
func (s *service) getLoc(ctx context.Context, locID string) (model.Loc, error) {
	rw, err := s.getRailway()
	if err != nil {
		return nil, err
	}
	locRef, ok := rw.GetLocs().Get(locID)
	if !ok {
		return nil, api.NotFound("Loc: %s", locID)
	}
	loc := locRef.TryResolve()
	if loc == nil {
		return nil, api.NotFound("Failed to resolve loc: %s", locID)
	}
	return loc, nil
}

// Gets a loc by ID.
func (s *service) GetLoc(ctx context.Context, req *api.IDRequest) (*api.Loc, error) {
	loc, err := s.getLoc(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	var result api.Loc
	if err := result.FromModel(ctx, loc); err != nil {
		return nil, err
	}
	return &result, nil
}

// Update a loc by ID.
func (s *service) UpdateLoc(ctx context.Context, req *api.Loc) (*api.Loc, error) {
	loc, err := s.getLoc(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	if err := req.ToModel(ctx, loc); err != nil {
		return nil, err
	}
	var result api.Loc
	if err := result.FromModel(ctx, loc); err != nil {
		return nil, err
	}
	return &result, nil
}
