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
	"gioui.org/widget"
	"gioui.org/widget/material"
	"gioui.org/x/component"

	"github.com/binkyrailways/BinkyRailways/pkg/app/canvas"
	"github.com/binkyrailways/BinkyRailways/pkg/app/canvas/run"
	"github.com/binkyrailways/BinkyRailways/pkg/app/views"
	"github.com/binkyrailways/BinkyRailways/pkg/app/views/railway/common"
	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/log"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

type (
	C = layout.Context
	D = layout.Dimensions

	setEditModeFunc func()
)

// View implements the view in which trains are being operated.
type View struct {
	vm                  views.ViewManager
	railway             state.Railway
	setEditMode         setEditModeFunc
	modal               *component.ModalLayer
	navSheet            *component.ModalSheet
	navSheetList        layout.List
	appBar              *component.AppBar
	buttonEdit          widget.Clickable
	buttonAutoRun       widget.Clickable
	canvas              *canvas.EntityCanvas
	power               *powerView
	locs                *runLocsView
	loc                 *runLocView
	hardwareModules     *hardwareModulesView
	showHardwareModules widget.Bool
	discoverHardwareID  widget.Editor
	buttonDiscover      widget.Clickable
	hresizer            component.Resize
	vresizer            component.Resize
	logs                *common.LogView
}

// New constructs a new railway view
func New(vm views.ViewManager, railway state.Railway, setEditMode setEditModeFunc) *View {
	v := &View{
		vm:              vm,
		railway:         railway,
		setEditMode:     setEditMode,
		modal:           component.NewModal(),
		canvas:          canvas.RailwayStateCanvas(railway, run.NewBuilder()),
		power:           newPowerView(vm, railway),
		loc:             newRunLocView(vm),
		hardwareModules: newHardwareModuleView(vm, railway),
		hresizer:        component.Resize{Axis: layout.Horizontal, Ratio: 0.2},
		vresizer:        component.Resize{Axis: layout.Vertical, Ratio: 0.95},
		logs:            common.NewLogView(vm),
	}
	v.navSheet = component.NewModalSheet(v.modal)
	v.navSheetList.Axis = layout.Vertical
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
	if id := v.discoverHardwareID.Text(); id != "" {
		v.railway.ForEachCommandStation(func(cs state.CommandStation) {
			cs.TriggerDiscover(context.Background(), id)
		})
	}
}

// Update the logs view
func (v *View) UpdateLogEvents(events []log.LogEvent) {
	v.logs.OnView(events)
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
	if v.showHardwareModules.Changed() {
		v.navSheet.Disappear(gtx.Now)
	}

	for _, evt := range v.appBar.Events(gtx) {
		switch evt.(type) {
		case component.AppBarNavigationClicked:
			v.navSheet.Appear(gtx.Now)
		case component.AppBarContextMenuDismissed:
			// log.Printf("Context menu dismissed: %v", event)
		case component.AppBarOverflowActionClicked:
			v.modal.Disappear(gtx.Now)
			// log.Printf("Overflow action selected: %v", event)
		}
	}

	// Configure appBar
	v.appBar.NavigationIcon = views.IconMenu
	v.appBar.Title = v.railway.GetDescription()
	var appBarActions []component.AppBarAction
	appBarActions = append(appBarActions,
		component.SimpleIconAction(&v.buttonEdit, views.IconEdit, component.OverflowAction{Name: "Edit", Tag: &v.buttonEdit}),
	)
	if vm.GetEnabled() {
		if vm.GetAutoRun() {
			appBarActions = append(appBarActions,
				component.SimpleIconAction(&v.buttonAutoRun, views.IconStopAutoRun, component.OverflowAction{Name: "Stop Auto run", Tag: &v.buttonAutoRun}),
			)
		} else {
			appBarActions = append(appBarActions,
				component.SimpleIconAction(&v.buttonAutoRun, views.IconStartAutoRun, component.OverflowAction{Name: "Start Auto run", Tag: &v.buttonAutoRun}),
			)
		}
	}
	v.appBar.SetActions(
		appBarActions,
		[]component.OverflowAction{})

	bar := func(gtx C) D { return v.appBar.Layout(gtx, th, "", "") }
	canvas := func(gtx C) D { return v.canvas.Layout(gtx, th) }

	// Prepare left side
	vsLeft := func(gtx C) D {
		var children = []layout.FlexChild{
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
		}
		if v.showHardwareModules.Value {
			children = append(children,
				layout.Flexed(1, func(gtx C) D {
					return widgets.WithBorder(gtx, th, func(gtx C) D {
						return widgets.WithPadding(gtx, v.hardwareModules.Layout)
					})
				}),
			)
		}
		return layout.Flex{Axis: layout.Vertical}.Layout(gtx, children...)
	}

	vs := widgets.VerticalSplit(
		bar,
		func(gtx C) D {
			return v.hresizer.Layout(gtx,
				func(gtx C) D { return widgets.WithPadding(gtx, vsLeft) },
				func(gtx C) D {
					return v.vresizer.Layout(gtx,
						func(gtx C) D { return widgets.WithPadding(gtx, canvas) },
						v.logs.Layout,
						widgets.VerticalResizerHandle,
					)
				},
				widgets.HorizontalResizerHandle,
			)
		})
	vs.Start.Rigid = true

	// Draw layers
	stack := layout.Stack{
		Alignment: layout.Direction(layout.SE),
	}
	v.navSheet.LayoutModal(func(gtx C, th *material.Theme, anim *component.VisibilityAnimation) D {
		return v.layoutNavSheet(gtx, th)
	})
	stack.Layout(gtx,
		layout.Stacked(vs.Layout),
		layout.Stacked(func(gtx C) D { return v.modal.Layout(gtx, th) }),
	)
	return layout.Dimensions{Size: gtx.Constraints.Max}
}

// Handle events and draw the navigation sheet
func (v *View) layoutNavSheet(gtx layout.Context, th *material.Theme) layout.Dimensions {
	options := []layout.Widget{
		material.H5(th, "View").Layout,
		material.CheckBox(th, &v.showHardwareModules, "Show hardware modules").Layout,

		widgets.VerticalSpacer().Layout,
		material.H5(th, "Discover hardware").Layout,
		material.Editor(th, &v.discoverHardwareID, "12345678").Layout,
		material.Button(th, &v.buttonDiscover, "Search").Layout,
	}

	return widgets.WithPadding(gtx, func(gtx C) D {
		return v.navSheetList.Layout(gtx, len(options), func(gtx C, index int) D {
			return options[index](gtx)
		})
	})
}
