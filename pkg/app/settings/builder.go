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

package settings

import (
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// builder implements an entity visitor to create settings components.
type builder struct {
	model.DefaultEntityVisitor
}

// BuildSettings tries to build a settings implementation for the given entity.
func BuildSettings(x interface{}) Settings {
	if x == nil {
		return nil
	}
	switch x := x.(type) {
	case model.Entity:
		b := &builder{}
		if result, ok := x.Accept(b).(Settings); ok {
			return result
		}
	}
	return nil
}

// NewBuilder creates an entity visitor to create settings components.
func NewBuilder() model.EntityVisitor {
	return &builder{}
}

func (v *builder) VisitBinarySensor(x model.BinarySensor) interface{} {
	return NewBinarySensorSettings(x)
}

func (v *builder) VisitBinkyNetCommandStation(x model.BinkyNetCommandStation) interface{} {
	return NewBinkyNetCommandStationSettings(x)
}

func (v *builder) VisitBlock(x model.Block) interface{} {
	return NewBlockSettings(x)
}

func (v *builder) VisitLocoBufferCommandStation(x model.LocoBufferCommandStation) interface{} {
	return NewLocoBufferCommandStationSettings(x)
}

func (v *builder) VisitLoc(x model.Loc) interface{} {
	return NewLocSettings(x)
}

func (v *builder) VisitModule(x model.Module) interface{} {
	return NewModuleSettings(x)
}

func (v *builder) VisitRailway(x model.Railway) interface{} {
	return NewRailwaySettings(x)
}

func (v *builder) VisitRoute(x model.Route) interface{} {
	return NewRouteSettings(x)
}

func (v *builder) VisitSwitch(x model.Switch) interface{} {
	return NewSwitchSettings(x)
}

func (v *builder) VisitBinkyNetLocalWorker(x model.BinkyNetLocalWorker) interface{} {
	return NewBinkyNetLocalWorkerSettings(x)
}
