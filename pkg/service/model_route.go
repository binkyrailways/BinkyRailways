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

// Gets a route by ID.
func (s *service) getRoute(ctx context.Context, id string) (model.Route, error) {
	moduleID, routeID, err := api.SplitParentChildID(id)
	if err != nil {
		return nil, err
	}
	mod, err := s.getModule(ctx, moduleID)
	if err != nil {
		return nil, err
	}
	route, ok := mod.GetRoutes().Get(routeID)
	if !ok {
		return nil, api.NotFound(routeID)
	}
	return route, nil
}

// Gets a route by ID.
func (s *service) GetRoute(ctx context.Context, req *api.IDRequest) (*api.Route, error) {
	route, err := s.getRoute(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	var result api.Route
	if err := result.FromModel(ctx, route); err != nil {
		return nil, err
	}
	return &result, nil
}

// Update a route by ID.
func (s *service) UpdateRoute(ctx context.Context, req *api.Route) (*api.Route, error) {
	route, err := s.getRoute(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	if err := req.ToModel(ctx, route); err != nil {
		return nil, err
	}
	var result api.Route
	if err := result.FromModel(ctx, route); err != nil {
		return nil, err
	}
	return &result, nil
}

// Adds a new route in the module identified by given by ID.
func (s *service) AddRoute(ctx context.Context, req *api.IDRequest) (*api.Route, error) {
	mod, err := s.getModule(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	route := mod.GetRoutes().AddNew()
	var result api.Route
	if err := result.FromModel(ctx, route); err != nil {
		return nil, err
	}
	return &result, nil
}

// Adds a crossing junction (of type switch) with given junction ID & switch direction.
func (s *service) AddRouteCrossingJunctionSwitch(ctx context.Context, req *api.AddRouteCrossingJunctionSwitchRequest) (*api.Route, error) {
	route, err := s.getRoute(ctx, req.GetRouteId())
	if err != nil {
		return nil, err
	}
	junction, err := s.getJunction(ctx, req.GetJunctionId())
	if err != nil {
		return nil, err
	}
	sw, ok := junction.(model.Switch)
	if !ok {
		return nil, api.InvalidArgument("Junction '%s' is not of type Switch", junction.GetID())
	}
	dir, err := req.GetDirection().ToModel(ctx)
	if err != nil {
		return nil, err
	}
	if err := route.GetCrossingJunctions().AddSwitch(sw, dir); err != nil {
		return nil, err
	}
	var result api.Route
	if err := result.FromModel(ctx, route); err != nil {
		return nil, err
	}
	return &result, nil
}

// Removes a crossing junction with given junction ID.
func (s *service) RemoveRouteCrossingJunction(ctx context.Context, req *api.RemoveRouteCrossingJunctionRequest) (*api.Route, error) {
	route, err := s.getRoute(ctx, req.GetRouteId())
	if err != nil {
		return nil, err
	}
	junction, err := s.getJunction(ctx, req.GetJunctionId())
	if err != nil {
		return nil, err
	}
	route.GetCrossingJunctions().Remove(junction)
	var result api.Route
	if err := result.FromModel(ctx, route); err != nil {
		return nil, err
	}
	return &result, nil
}
