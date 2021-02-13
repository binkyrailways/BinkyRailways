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
	"log"

	"gioui.org/layout"
	"gioui.org/unit"
	"gioui.org/widget"
	"gioui.org/x/component"

	"github.com/binkyrailways/BinkyRailways/pkg/app/canvas"
	"github.com/binkyrailways/BinkyRailways/pkg/app/canvas/run"
	"github.com/binkyrailways/BinkyRailways/pkg/app/views"
	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

type runView struct {
	vm         views.ViewManager
	railway    state.Railway
	parent     *railwayView
	modal      *component.ModalLayer
	appBar     *component.AppBar
	buttonEdit widget.Clickable
	canvas     *canvas.EntityCanvas
	locs       *runLocsView
}

// New constructs a new railway view
func newRunView(vm views.ViewManager, railway state.Railway, parent *railwayView) *runView {
	v := &runView{
		vm:      vm,
		railway: railway,
		parent:  parent,
		modal:   component.NewModal(),
		canvas:  canvas.RailwayStateCanvas(railway, run.NewBuilder()),
		locs:    newRunLocsView(vm, railway),
	}
	v.appBar = component.NewAppBar(v.modal)
	return v
}

// Handle events and draw the view
func (v *runView) Layout(gtx layout.Context) layout.Dimensions {
	th := v.vm.GetTheme()

	if v.buttonEdit.Clicked() {
		v.parent.SetRunMode(false, false)
	}

	for _, evt := range v.appBar.Events(gtx) {
		switch event := evt.(type) {
		case component.AppBarNavigationClicked:
			/*if nonModalDrawer.Value {
				navAnim.ToggleVisibility(gtx.Now)
			} else {
				modalNav.Appear(gtx.Now)
				navAnim.Disappear(gtx.Now)
			}*/
		case component.AppBarContextMenuDismissed:
			log.Printf("Context menu dismissed: %v", event)
		case component.AppBarOverflowActionClicked:
			v.modal.Disappear(gtx.Now)
			log.Printf("Overflow action selected: %v", event)
		}
	}

	// Configure appBar
	v.appBar.Title = v.railway.GetDescription()
	v.appBar.SetActions(
		[]component.AppBarAction{
			component.SimpleIconAction(th, &v.buttonEdit, iconEdit, component.OverflowAction{Name: "Edit", Tag: &v.buttonEdit}),
		},
		[]component.OverflowAction{})

	bar := func(gtx C) D { return v.appBar.Layout(gtx, th) }
	canvas := func(gtx C) D { return v.canvas.Layout(gtx, th) }
	hs := widgets.HorizontalSplit(
		func(gtx C) D { return layout.UniformInset(unit.Dp(5)).Layout(gtx, v.locs.Layout) },
		func(gtx C) D { return layout.UniformInset(unit.Dp(5)).Layout(gtx, canvas) },
	)
	hs.Start.Weight = 1
	hs.End.Weight = 5
	vs := widgets.VerticalSplit(bar, hs.Layout)
	vs.Start.Rigid = true

	// Draw layers
	stack := layout.Stack{
		Alignment: layout.Direction(layout.SE),
	}
	stack.Layout(gtx,
		layout.Stacked(vs.Layout),
		layout.Stacked(func(gtx C) D { return v.modal.Layout(gtx, th) }),
	)
	return layout.Dimensions{Size: gtx.Constraints.Max}
}
