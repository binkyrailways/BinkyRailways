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

package railway

import (
	"gioui.org/layout"
	"gioui.org/widget"
	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/app/views"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

type runLocView struct {
	current   state.Loc
	vm        views.ViewManager
	direction widget.Enum
	speed     widget.Float
}

// newRunLocView initializes a runLocView
func newRunLocView(vm views.ViewManager) *runLocView {
	return &runLocView{
		vm: vm,
	}
}

// Select the given loc in this view
func (v *runLocView) Select(loc state.Loc) {
	if v.current == loc {
		return
	}
	v.current = loc
	if loc == nil {
		v.speed.Value = 0
		v.direction.Value = state.LocDirectionForward.String()
	} else {
		v.speed.Value = float32(loc.GetSpeed().GetRequested())
		v.direction.Value = loc.GetDirection().GetRequested().String()
	}
}

// Handle events and draw the view
func (v *runLocView) Layout(gtx layout.Context) layout.Dimensions {
	th := v.vm.GetTheme()

	name := ""
	if l := v.current; l != nil {
		// Update requested value
		if v.direction.Changed() {
			if dir, err := state.ParseLocDirection(v.direction.Value); err == nil {
				l.GetDirection().SetRequested(dir)
			}
		}
		if v.speed.Changed() {
			l.GetSpeed().SetRequested(int(v.speed.Value))
		}

		// Update UI
		name = l.GetDescription()
		v.direction.Value = l.GetDirection().GetRequested().String()
		v.speed.Value = float32(l.GetSpeed().GetRequested())
	}

	direction := func(gtx C) D {
		return layout.Flex{Axis: layout.Horizontal}.Layout(gtx,
			layout.Flexed(1, material.RadioButton(th, &v.direction, state.LocDirectionReverse.String(), "Reverse").Layout),
			layout.Flexed(1, material.RadioButton(th, &v.direction, state.LocDirectionForward.String(), "Forward").Layout),
		)
	}

	return layout.Flex{
		Axis: layout.Vertical,
	}.Layout(gtx,
		layout.Rigid(material.H5(th, name).Layout),
		layout.Rigid(direction),
		layout.Rigid(material.Slider(th, &v.speed, 0, 100).Layout),
	)
}
