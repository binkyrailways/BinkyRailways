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
	"gioui.org/layout"
	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// newRouteEventSetSettings constructs a list of RouteEventSet's.
func newRouteEventSetList(entity model.RouteEventSet) *routeEventSetList {
	s := &routeEventSetList{
		entity: entity,
		eventList: layout.List{
			Axis: layout.Vertical,
		},
	}
	return s
}

// routeEventSetList implements a list of RouteEventSet's.
type routeEventSetList struct {
	entity model.RouteEventSet

	eventList      layout.List
	routeEventList []model.RouteEvent
	selectedIndex  int
}

// Handle events and draw the editor
func (e *routeEventSetList) Layout(gtx C, th *material.Theme) D {
	l := e.entity.GetCount()
	if cap(e.routeEventList) != l {
		e.routeEventList = make([]model.RouteEvent, 0, l)
	}
	e.routeEventList = e.routeEventList[:0]
	e.entity.ForEach(func(re model.RouteEvent) {
		e.routeEventList = append(e.routeEventList, re)
	})
	return e.eventList.Layout(gtx, l, func(gtx C, index int) D {
		re := e.routeEventList[index]
		lbl := material.Label(th, th.TextSize, re.GetSensor().GetDescription())
		if index == e.selectedIndex {
			lbl.Color = th.ContrastFg
		}
		return lbl.Layout(gtx)
	})
}
