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
	"context"
	"fmt"

	"gioui.org/layout"
	"gioui.org/text"
	"gioui.org/widget"
	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/app/views"
	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

type runLocView struct {
	current    state.Loc
	vm         views.ViewManager
	speed      widget.Float
	buttonRev  widget.Clickable
	buttonFor  widget.Clickable
	buttonStop widget.Clickable
	f0         widget.Bool
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
		v.f0.Value = false
	} else {
		ctx := context.Background()
		v.speed.Value = float32(loc.GetSpeed().GetRequested(ctx))
		v.f0.Value = loc.GetF0().GetRequested(ctx)
	}
}

// Handle events and draw the view
func (v *runLocView) Layout(gtx layout.Context) layout.Dimensions {
	ctx := context.Background()
	th := v.vm.GetTheme()

	name := "n/a"
	direction := "n/a"
	speed := ""
	l := v.current
	if v.buttonRev.Clicked() {
		if l != nil {
			l.GetDirection().SetRequested(ctx, state.LocDirectionReverse)
		}
	}
	if v.buttonFor.Clicked() {
		if l != nil {
			l.GetDirection().SetRequested(ctx, state.LocDirectionForward)
		}
	}
	if v.speed.Changed() {
		if l != nil {
			l.GetSpeed().SetRequested(ctx, int(v.speed.Value))
		}
	}
	if v.f0.Changed() {
		if l != nil {
			l.GetF0().SetRequested(ctx, v.f0.Value)
		}
	}
	if v.buttonStop.Clicked() {
		if l != nil {
			l.GetSpeedInSteps().SetRequested(ctx, 0)
		}
	}
	if l != nil {
		// Update UI
		name = l.GetDescription()
		sp := l.GetSpeed()
		v.speed.Value = float32(sp.GetRequested(ctx))
		if sp.IsConsistent(ctx) {
			speed = fmt.Sprintf("%d %%", sp.GetRequested(ctx))
		} else {
			speed = fmt.Sprintf("Changing to %d %%", sp.GetRequested(ctx))
		}
		dir := l.GetDirection()
		if dir.IsConsistent(ctx) {
			direction = dir.GetRequested(ctx).String()
		} else {
			direction = "Changing to " + dir.GetRequested(ctx).String()
		}
		v.f0.Value = l.GetF0().GetRequested(ctx)
	} else {
		return widgets.WithPadding(gtx, material.H5(th, name).Layout)
	}

	buttons := func(gtx C) D {
		return layout.Flex{Axis: layout.Horizontal}.Layout(gtx,
			layout.Flexed(1, func(gtx C) D {
				b := material.Button(th, &v.buttonRev, "Reverse")
				b.Background = widgets.BlueGrey
				return widgets.WithPadding(gtx, b.Layout)
			}),
			layout.Flexed(1, func(gtx C) D {
				b := material.Button(th, &v.buttonFor, "Forward")
				b.Background = widgets.Teal
				return widgets.WithPadding(gtx, b.Layout)
			}),
		)
	}

	speedSlider := func(gtx C) D {
		return layout.Flex{Axis: layout.Vertical}.Layout(gtx,
			layout.Rigid(func(gtx C) D {
				lb := material.Caption(th, speed)
				lb.Alignment = text.Middle
				return lb.Layout(gtx)
			}),
			layout.Rigid(func(gtx C) D {
				return material.Slider(th, &v.speed, 0, 100).Layout(gtx)
			}),
		)
	}

	return layout.Flex{
		Axis: layout.Vertical,
	}.Layout(gtx,
		layout.Rigid(func(gtx C) D {
			return widgets.WithPadding(gtx, material.H5(th, name).Layout)
		}),
		layout.Rigid(func(gtx C) D {
			return widgets.WithPadding(gtx, material.H6(th, direction).Layout)
		}),
		layout.Rigid(buttons),
		layout.Rigid(func(gtx C) D {
			return widgets.WithPadding(gtx, speedSlider)
		}),
		layout.Rigid(func(gtx C) D {
			b := material.Button(th, &v.buttonStop, "Stop")
			b.Background = widgets.Red
			return widgets.WithPadding(gtx, b.Layout)
		}),
		layout.Rigid(func(gtx C) D {
			dims := widgets.WithPadding(gtx, material.CheckBox(th, &v.f0, "Lights").Layout)
			dims.Size.X = gtx.Constraints.Min.X
			return dims
		}),
	)
}
