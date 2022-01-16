// Copyright 2022 Ewout Prangsma
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

// Change the active state of a sensor in virtual mode
func (s *service) ClickVirtualSensor(ctx context.Context, req *api.ClickVirtualSensorRequest) (*api.RailwayState, error) {
	rwState, err := s.getRailwayState()
	if err != nil {
		return nil, err
	}
	_, sensorID, err := api.SplitParentChildID(req.GetId())
	if err != nil {
		return nil, err
	}
	sensorState, err := rwState.GetSensor(sensorID)
	if err != nil {
		return nil, err
	}
	if sensorState == nil {
		return nil, api.NotFound("Sensor '%s'", req.GetId())
	}
	rwState.GetVirtualMode().EntityClick(ctx, sensorState)
	var result api.RailwayState
	if err := result.FromState(ctx, rwState); err != nil {
		return nil, err
	}
	return &result, nil
}
