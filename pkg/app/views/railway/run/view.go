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
	"log"

	"gioui.org/layout"
	"gioui.org/widget"
	"gioui.org/x/component"

	"github.com/binkyrailways/BinkyRailways/pkg/app/canvas"
	"github.com/binkyrailways/BinkyRailways/pkg/app/canvas/run"
	"github.com/binkyrailways/BinkyRailways/pkg/app/views"
	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
	"github.com/gen2brain/dlgs"
)

type (
	C = layout.Context
	D = layout.Dimensions

	setEditModeFunc func()
)

// View implements the view in which trains are being operated.
type View struct {
	vm             views.ViewManager
	railway        state.Railway
	setEditMode    setEditModeFunc
	modal          *component.ModalLayer
	appBar         *component.AppBar
	buttonEdit     widget.Clickable
	buttonAutoRun  widget.Clickable
	buttonDiscover widget.Clickable
	canvas         *canvas.EntityCanvas
	power          *powerView
	locs           *runLocsView
	loc            *runLocView
}

// New constructs a new railway view
func New(vm views.ViewManager, railway state.Railway, setEditMode setEditModeFunc) *View {
	v := &View{
		vm:          vm,
		railway:     railway,
		setEditMode: setEditMode,
		modal:       component.NewModal(),
		canvas:      canvas.RailwayStateCanvas(railway, run.NewBuilder()),
		power:       newPowerView(vm, railway),
		loc:         newRunLocView(vm),
	}
	v.locs = newRunLocsView(vm, railway, v.loc.Select)
	v.appBar = component.NewAppBar(v.modal)
	railway.Subscribe(context.Background(), v.processEvent)
	return v
}

// processEvent receives events send by railway state
func (v *View) processEvent(evt state.Event) {
	fmt.Println(evt)
	v.vm.Invalidate()
}

func (v *View) onDiscover() {
	if id, ok, err := dlgs.Entry("Discover hardware", "Hardware ID", ""); err == nil && ok {
		v.railway.ForEachCommandStation(func(cs state.CommandStation) {
			cs.TriggerDiscover(context.Background(), id)
		})
	}
}

// Layout handles events and draw the view
func (v *View) Layout(gtx layout.Context) layout.Dimensions {
	th := v.vm.GetTheme()
	vm := v.railway.GetVirtualMode()

	if v.buttonEdit.Clicked() {
		v.setEditMode()
	}
	if v.buttonAutoRun.Clicked() {
		if vm.GetEnabled() {
			vm.SetAutoRun(!vm.GetAutoRun())
		}
	}
	if v.buttonDiscover.Clicked() {
		v.onDiscover()
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
	appBarActions := []component.AppBarAction{
		component.SimpleIconAction(th, &v.buttonDiscover, views.IconSearch, component.OverflowAction{Name: "Discover", Tag: &v.buttonDiscover}),
		component.SimpleIconAction(th, &v.buttonEdit, views.IconEdit, component.OverflowAction{Name: "Edit", Tag: &v.buttonEdit}),
	}
	if vm.GetEnabled() {
		if vm.GetAutoRun() {
			appBarActions = append(appBarActions,
				component.SimpleIconAction(th, &v.buttonAutoRun, views.IconStopAutoRun, component.OverflowAction{Name: "Stop Auto run", Tag: &v.buttonAutoRun}),
			)
		} else {
			appBarActions = append(appBarActions,
				component.SimpleIconAction(th, &v.buttonAutoRun, views.IconStartAutoRun, component.OverflowAction{Name: "Start Auto run", Tag: &v.buttonAutoRun}),
			)
		}
	}
	v.appBar.SetActions(
		appBarActions,
		[]component.OverflowAction{})

	bar := func(gtx C) D { return v.appBar.Layout(gtx, th) }
	canvas := func(gtx C) D { return v.canvas.Layout(gtx, th) }
	vsLeft := func(gtx C) D {
		return layout.Flex{Axis: layout.Vertical}.Layout(gtx,
			layout.Rigid(func(gtx C) D {
				return widgets.WithBorder(gtx, th, v.power.Layout)
			}),
			layout.Rigid(func(gtx C) D {
				return layout.Spacer{Height: widgets.Padding}.Layout(gtx)
			}),
			layout.Flexed(1, func(gtx C) D {
				return widgets.WithBorder(gtx, th, func(gtx C) D {
					return widgets.WithPadding(gtx, v.locs.Layout)
				})
			}),
			layout.Rigid(func(gtx C) D {
				return layout.Spacer{Height: widgets.Padding}.Layout(gtx)
			}),
			layout.Rigid(func(gtx C) D {
				return widgets.WithBorder(gtx, th, v.loc.Layout)
			}),
		)
	}
	hs := widgets.HorizontalSplit(
		func(gtx C) D { return widgets.WithPadding(gtx, vsLeft) },
		func(gtx C) D { return widgets.WithPadding(gtx, canvas) },
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
