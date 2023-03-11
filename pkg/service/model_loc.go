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
	loc, err := locRef.TryResolve()
	if err != nil {
		return nil, err
	}
	if loc == nil {
		return nil, api.NotFound("Failed to resolve loc: %s", locID)
	}
	return loc, nil
}

// Get image of loc by id
// Returns: image, contentType, error
func (s *service) GetLocImage(ctx context.Context, locID string) ([]byte, string, error) {
	loc, err := s.getLoc(ctx, locID)
	if err != nil {
		return nil, "", err
	}
	img := loc.GetImage()
	// TODO detect content type
	return img, "image/png", nil
}

// Gets a loc by ID.
func (s *service) GetLoc(ctx context.Context, req *api.IDRequest) (*api.Loc, error) {
	loc, err := s.getLoc(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	httpHost, err := s.getHttpHost(ctx)
	if err != nil {
		return nil, err
	}
	var result api.Loc
	if err := result.FromModel(ctx, loc, httpHost); err != nil {
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
	httpHost, err := s.getHttpHost(ctx)
	if err != nil {
		return nil, err
	}
	if err := req.ToModel(ctx, loc); err != nil {
		return nil, err
	}
	var result api.Loc
	if err := result.FromModel(ctx, loc, httpHost); err != nil {
		return nil, err
	}
	return &result, nil
}

// Update image of a loc by ID.
func (s *service) UpdateLocImage(ctx context.Context, req *api.ImageIDRequest) (*api.Loc, error) {
	loc, err := s.getLoc(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	httpHost, err := s.getHttpHost(ctx)
	if err != nil {
		return nil, err
	}
	if err := loc.SetImage(req.GetImage()); err != nil {
		return nil, err
	}
	var result api.Loc
	if err := result.FromModel(ctx, loc, httpHost); err != nil {
		return nil, err
	}
	return &result, nil
}

// Add a new loc.
func (s *service) AddLoc(ctx context.Context, req *api.Empty) (*api.Loc, error) {
	rw, err := s.getRailway()
	if err != nil {
		return nil, err
	}
	httpHost, err := s.getHttpHost(ctx)
	if err != nil {
		return nil, err
	}
	loc, err := rw.GetPackage().AddNewLoc()
	if err != nil {
		return nil, err
	}
	rw.GetLocs().Add(loc)
	var result api.Loc
	if err := result.FromModel(ctx, loc, httpHost); err != nil {
		return nil, err
	}
	return &result, nil
}

// Delete a loc by ID.
func (s *service) DeleteLoc(ctx context.Context, req *api.IDRequest) (*api.Empty, error) {
	rw, err := s.getRailway()
	if err != nil {
		return nil, err
	}
	locRef, ok := rw.GetLocs().Get(req.GetId())
	if !ok {
		return nil, api.NotFound("Loc: %s", req.GetId())
	}
	loc, err := locRef.TryResolve()
	if err != nil {
		return nil, err
	}
	rw.GetLocs().Remove(locRef)
	if err := rw.GetPackage().Remove(loc); err != nil {
		return nil, err
	}
	return &api.Empty{}, nil
}
