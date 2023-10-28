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

// Gets a Sensor by ID.
func (s *service) getSensor(ctx context.Context, id string) (model.Sensor, error) {
	moduleID, sensorID, err := api.SplitParentChildID(id)
	if err != nil {
		return nil, err
	}
	mod, err := s.getModule(ctx, moduleID)
	if err != nil {
		return nil, err
	}
	sensor, ok := mod.GetSensors().Get(sensorID)
	if !ok {
		return nil, api.NotFound(sensorID)
	}
	return sensor, nil
}

// Gets a Sensor by ID.
func (s *service) GetSensor(ctx context.Context, req *api.IDRequest) (*api.Sensor, error) {
	sensor, err := s.getSensor(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	var result api.Sensor
	if err := result.FromModel(ctx, sensor); err != nil {
		return nil, err
	}
	return &result, nil
}

// Update a Sensor by ID.
func (s *service) UpdateSensor(ctx context.Context, req *api.Sensor) (*api.Sensor, error) {
	sensor, err := s.getSensor(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	if err := req.ToModel(ctx, sensor); err != nil {
		return nil, err
	}
	var result api.Sensor
	if err := result.FromModel(ctx, sensor); err != nil {
		return nil, err
	}
	return &result, nil
}

// Adds a new sensor of type binary sensor in the module identified by given by ID.
func (s *service) AddBinarySensor(ctx context.Context, req *api.IDRequest) (*api.Sensor, error) {
	mod, err := s.getModule(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	sensor := mod.GetSensors().AddNewBinarySensor()
	var result api.Sensor
	if err := result.FromModel(ctx, sensor); err != nil {
		return nil, err
	}
	return &result, nil
}

// Delete a sensor by ID.
func (s *service) DeleteSensor(ctx context.Context, req *api.IDRequest) (*api.Module, error) {
	moduleID, sensorID, err := api.SplitParentChildID(req.GetId())
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
	sensor, ok := mod.GetSensors().Get(sensorID)
	if !ok {
		return nil, api.NotFound(sensorID)
	}
	mod.GetSensors().Remove(sensor)
	var result api.Module
	if err := result.FromModel(ctx, mod, httpHost, s.HTTPSecure); err != nil {
		return nil, err
	}
	return &result, nil
}
