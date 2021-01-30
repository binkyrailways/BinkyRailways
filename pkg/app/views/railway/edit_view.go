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
	"fmt"
	"log"

	"gioui.org/layout"
	"gioui.org/widget"
	"gioui.org/x/component"
	"golang.org/x/exp/shiny/materialdesign/icons"

	"github.com/binkyrailways/BinkyRailways/pkg/app/editors"
	"github.com/binkyrailways/BinkyRailways/pkg/app/views"
	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type (
	C = layout.Context
	D = layout.Dimensions
)

type editView struct {
	vm         views.ViewManager
	railway    model.Railway
	parent     *railwayView
	modal      *component.ModalLayer
	appBar     *component.AppBar
	buttonRun  widget.Clickable
	entityList widgets.EntityGroupList
	editor     editors.Editor
}

var (
	iconX, _ = widget.NewIcon(icons.AVPlayArrow)
)

// New constructs a new railway view
func newEditView(vm views.ViewManager, railway model.Railway, parent *railwayView) *editView {
	fmt.Println("newEditView")
	v := &editView{
		vm:      vm,
		railway: railway,
		parent:  parent,
		modal:   component.NewModal(),
		entityList: widgets.EntityGroupList{
			Groups: []widgets.EntityGroup{
				{
					Name: "Locs",
					Collection: func() []model.Entity {
						var result []model.Entity
						railway.GetLocs().ForEach(func(c model.LocRef) {
							if x := c.TryResolve(); x != nil {
								result = append(result, x)
							}
						})
						return result
					},
				},
				{
					Name: "Modules",
					Collection: func() []model.Entity {
						var result []model.Entity
						railway.GetModules().ForEach(func(c model.ModuleRef) {
							if x := c.TryResolve(); x != nil {
								result = append(result, x)
							}
						})
						return result
					},
				},
			},
		},
	}
	v.entityList.OnSelect = func(selection model.Entity) {
		if loc, ok := selection.(model.Loc); ok {
			v.editor = editors.NewLocEditor(loc, v.vm)
		} else if module, ok := selection.(model.Module); ok {
			v.editor = editors.NewModuleEditor(module, v.vm)
		} else {
			v.editor = nil
		}
		vm.Invalidate()
	}
	v.appBar = component.NewAppBar(v.modal)
	return v
}

// Handle events and draw the view
func (v *editView) Layout(gtx layout.Context) layout.Dimensions {
	th := v.vm.GetTheme()

	if v.buttonRun.Clicked() {
		v.parent.SetRunMode(true)
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
			component.SimpleIconAction(th, &v.buttonRun, iconX, component.OverflowAction{Name: "Run", Tag: &v.buttonRun}),
		},
		[]component.OverflowAction{})

	content := func(gtx C) D {
		if v.editor != nil {
			return v.editor.Layout(gtx, th)
		}
		return layout.Dimensions{Size: gtx.Constraints.Max}
	}
	bar := func(gtx C) D { return v.appBar.Layout(gtx, th) }
	hs := widgets.HorizontalSplit(func(gtx C) D { return v.entityList.Layout(gtx, th) }, content)
	hs.End.Weight = 3
	vs := widgets.VerticalSplit(bar, hs.Layout)
	vs.Start.Rigid = true
	vs.Layout(gtx)
	v.modal.Layout(gtx, th)
	return layout.Dimensions{Size: gtx.Constraints.Max}
}
