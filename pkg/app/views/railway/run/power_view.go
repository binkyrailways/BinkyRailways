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

package run

import (
	"gioui.org/layout"
	"gioui.org/widget"
	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/app/views"
	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

type powerView struct {
	railway   state.Railway
	vm        views.ViewManager
	buttonOn  widget.Clickable
	buttonOff widget.Clickable
}

// newPowerView initializes a powerView
func newPowerView(vm views.ViewManager, railway state.Railway) *powerView {
	return &powerView{
		vm:      vm,
		railway: railway,
	}
}

// Handle events and draw the view
func (v *powerView) Layout(gtx layout.Context) layout.Dimensions {
	th := v.vm.GetTheme()

	power := v.railway.GetPower()
	if v.buttonOn.Clicked() {
		power.SetRequested(true)
	}
	if v.buttonOff.Clicked() {
		power.SetRequested(false)
	}

	buttons := func(gtx C) D {
		return layout.Flex{Axis: layout.Horizontal}.Layout(gtx,
			layout.Flexed(1, func(gtx C) D {
				b := material.Button(th, &v.buttonOff, "Power Off")
				b.Background = widgets.Red
				return widgets.WithPadding(gtx, b.Layout)
			}),
			layout.Flexed(1, func(gtx C) D {
				b := material.Button(th, &v.buttonOn, "Power On")
				b.Background = widgets.Green
				return widgets.WithPadding(gtx, b.Layout)
			}),
		)
	}

	state := ""
	if power.IsConsistent() {
		if power.GetActual() {
			state = "Power is On"
		} else {
			state = "Power is Off"
		}
	} else {
		if power.GetRequested() {
			state = "Power is turning on"
		} else {
			state = "Power is turning off"
		}
	}

	return layout.Flex{
		Axis: layout.Vertical,
	}.Layout(gtx,
		layout.Rigid(func(gtx C) D {
			return widgets.WithPadding(gtx, material.H5(th, state).Layout)
		}),
		layout.Rigid(buttons),
	)
}
