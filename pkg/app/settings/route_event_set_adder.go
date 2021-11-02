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
	"gioui.org/widget"
	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// newRouteEventSetAdder constructs a component to add events to a RouteSet.
func newRouteEventSetAdder(entity model.RouteEventSet, module model.Module) *routeEventSetAdder {
	s := &routeEventSetAdder{
		eventSet: entity,
		module:   module,
	}
	lvs := s.constructAvailableSensorLVs()
	s.sensors = widgets.NewSimpleSelect(&s.selectorSensorID, lvs...)
	return s
}

// routeEventSetAdder implements a component to add RouteEvent's to a Set
type routeEventSetAdder struct {
	eventSet model.RouteEventSet
	module   model.Module

	selectorSensorID widget.Enum
	sensors          *widgets.SimpleSelect
	addButton        widget.Clickable
}

// constructAvailableSensorLVs creates a list of all sensor IDs that
// are not used in the route event set.
func (e *routeEventSetAdder) constructAvailableSensorLVs() []widgets.LabeledValue {
	sensors := e.module.GetSensors()
	lvs := make([]widgets.LabeledValue, 0, sensors.GetCount())
	sensors.ForEach(func(s model.Sensor) {
		if _, found := e.eventSet.Get(s.GetID()); !found {
			lvs = append(lvs, widgets.LV(s.GetID(), s.GetDescription()))
		}
	})
	return lvs
}

// Handle events and draw the editor
func (e *routeEventSetAdder) Layout(gtx C, th *material.Theme) D {
	if e.addButton.Clicked() {
		sensorID := e.selectorSensorID.Value
		if sensorID != "" {
			if sensor, found := e.module.GetSensors().Get(sensorID); found {
				e.eventSet.Add(sensor)
			}
		}
		e.selectorSensorID.Value = ""
	}

	hs := widgets.HorizontalSplit(
		func(gtx C) D {
			return e.sensors.Layout(gtx, th)
		},
		func(gtx C) D {
			b := material.Button(th, &e.addButton, "Add")
			return b.Layout(gtx)
		},
	)
	hs.End.Rigid = true
	return hs.Layout(gtx)
}
