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

package widgets

import (
	"gioui.org/layout"
	"gioui.org/widget/material"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type EntityGroupList struct {
	Groups   []EntityGroup
	list     layout.List
	OnSelect func(model.Entity)
	Selected model.Entity
	widgets  []entityGroupWidget
}

type entityGroupWidget interface {
	// Process events, return an entity if it is clicked.
	ProcessEvents() model.Entity
	Layout(C, *material.Theme, model.Entity) D
}

// Layout processes events and redraws the list.
func (v *EntityGroupList) Layout(gtx C, th *material.Theme) D {
	// Process events of widgets
	var newSelection model.Entity
	for _, w := range v.widgets {
		if x := w.ProcessEvents(); x != nil {
			if newSelection == nil {
				newSelection = x
			}
		}
	}
	if newSelection != v.Selected && newSelection != nil {
		v.Selected = newSelection
		v.OnSelect(newSelection)
	}

	// Rebuild widgets
	v.list.Axis = layout.Vertical
	selection := v.Selected
	var widgets []entityGroupWidget
	for idx := range v.Groups {
		widgets = append(widgets, v.Groups[idx].generateWidgets()...)
	}
	v.widgets = widgets
	// Redraw widgets
	return v.list.Layout(gtx, len(widgets), func(gtx C, idx int) D {
		//pointer.PassOp{Pass: true}.Add(gtx.Ops)
		return widgets[idx].Layout(gtx, th, selection)
	})
}
