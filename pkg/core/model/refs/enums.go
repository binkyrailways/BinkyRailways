// Copyright 2020 Ewout Prangsma
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

package refs

import "github.com/binkyrailways/BinkyRailways/pkg/core/model"

func NewBlockSide(value model.BlockSide) *model.BlockSide {
	return &value
}

func NewBlockSignalType(value model.BlockSignalType) *model.BlockSignalType {
	return &value
}

func NewLocSpeedBehavior(value model.LocSpeedBehavior) *model.LocSpeedBehavior {
	return &value
}

func NewChangeDirection(value model.ChangeDirection) *model.ChangeDirection {
	return &value
}

func NewRouteStateBehavior(value model.RouteStateBehavior) *model.RouteStateBehavior {
	return &value
}

func NewStationMode(value model.StationMode) *model.StationMode {
	return &value
}

func NewSwitchDirection(value model.SwitchDirection) *model.SwitchDirection {
	return &value
}

func BlockSideValue(r *model.BlockSide, defaultValue model.BlockSide) model.BlockSide {
	if r == nil {
		return defaultValue
	}
	return *r
}

func BlockSignalTypeValue(r *model.BlockSignalType, defaultValue model.BlockSignalType) model.BlockSignalType {
	if r == nil {
		return defaultValue
	}
	return *r
}

func ChangeDirectionValue(r *model.ChangeDirection, defaultValue model.ChangeDirection) model.ChangeDirection {
	if r == nil {
		return defaultValue
	}
	return *r
}

func LocSpeedBehaviorValue(r *model.LocSpeedBehavior, defaultValue model.LocSpeedBehavior) model.LocSpeedBehavior {
	if r == nil {
		return defaultValue
	}
	return *r
}

func RouteStateBehaviorValue(r *model.RouteStateBehavior, defaultValue model.RouteStateBehavior) model.RouteStateBehavior {
	if r == nil {
		return defaultValue
	}
	return *r
}

func StationModeValue(r *model.StationMode, defaultValue model.StationMode) model.StationMode {
	if r == nil {
		return defaultValue
	}
	return *r
}

func SwitchDirectionValue(r *model.SwitchDirection, defaultValue model.SwitchDirection) model.SwitchDirection {
	if r == nil {
		return defaultValue
	}
	return *r
}
