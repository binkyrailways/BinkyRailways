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
	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// NewRouteEventSetSettings constructs a settings component for a RouteEventSet.
func NewRouteEventSetSettings(entity model.RouteEventSet, module model.Module) Settings {
	s := &routeEventSetSettings{
		entity:     entity,
		eventList:  newRouteEventSetList(entity),
		eventAdder: newRouteEventSetAdder(entity, module),
	}
	return s
}

// routeEventSetSettings implements an settings editor for RouteEventSet.
type routeEventSetSettings struct {
	entity model.RouteEventSet

	eventList  *routeEventSetList
	eventAdder *routeEventSetAdder
}

// Handle events and draw the editor
func (e *routeEventSetSettings) Layout(gtx C, th *material.Theme) D {
	list := widgets.VerticalSplit(
		func(gtx C) D {
			return e.eventList.Layout(gtx, th)
		},
		func(gtx C) D {
			return e.eventAdder.Layout(gtx, th)
		})
	list.End.Rigid = true
	return list.Layout(gtx)
}
