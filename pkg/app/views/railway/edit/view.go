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

package edit

import (
	"context"
	"log"

	"gioui.org/layout"
	"gioui.org/unit"
	"gioui.org/widget"
	"gioui.org/widget/material"
	"gioui.org/x/component"
	"github.com/gen2brain/dlgs"

	"github.com/binkyrailways/BinkyRailways/pkg/app/editors"
	"github.com/binkyrailways/BinkyRailways/pkg/app/views"
	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type (
	C = layout.Context
	D = layout.Dimensions
)

type setRunModeFunc = func(virtual bool)

// View represents the view in which a railroad is being edited.
type View struct {
	vm               views.ViewManager
	railway          model.Railway
	setRunMode       setRunModeFunc
	modal            *component.ModalLayer
	appBar           *component.AppBar
	addSheet         *component.ModalSheet
	addSheetList     layout.List
	addSheetButtons  []editors.AddButton
	buttonRun        widget.Clickable
	buttonRunVirtual widget.Clickable
	buttonAdd        widget.Clickable
	buttonSave       widget.Clickable
	entityList       widgets.TreeView
	editor           editors.Editor
}

// New constructs a new editing railway view
func New(vm views.ViewManager, railway model.Railway, setRunMode setRunModeFunc) *View {
	itemCache := widgets.EntityTreeViewItemCache{}
	groupCache := widgets.EntityTreeGroupCache{}
	v := &View{
		vm:         vm,
		railway:    railway,
		setRunMode: setRunMode,
		modal:      component.NewModal(),
		entityList: widgets.TreeView{
			RootItems: []widgets.TreeViewItem{
				&widgets.TreeViewGroup{
					Name: "Railway",
					Collection: func(ctx context.Context, level int) []widgets.TreeViewItem {
						return []widgets.TreeViewItem{
							itemCache.CreateItem(railway, level),
						}
					},
				},
				&widgets.TreeViewGroup{
					Name: "Locs",
					Collection: func(ctx context.Context, level int) []widgets.TreeViewItem {
						var result []widgets.TreeViewItem
						railway.GetLocs().ForEach(func(c model.LocRef) {
							if x := c.TryResolve(); x != nil {
								result = append(result, itemCache.CreateItem(x, level))
							}
						})
						return result
					},
				},
				&widgets.TreeViewGroup{
					Name: "Modules",
					Collection: func(ctx context.Context, level int) []widgets.TreeViewItem {
						var result []widgets.TreeViewItem
						railway.GetModules().ForEach(func(c model.ModuleRef) {
							if x := c.TryResolve(); x != nil {
								result = append(result,
									itemCache.CreateItem(x, level),
									groupCache.CreateItem("Blocks", func(ctx context.Context, level int) []widgets.TreeViewItem {
										var result []widgets.TreeViewItem
										x.GetBlocks().ForEach(func(entity model.Block) {
											result = append(result, itemCache.CreateItem(entity, level+1))
										})
										return result
									}, level+1, x),
									groupCache.CreateItem("Block groups", func(ctx context.Context, level int) []widgets.TreeViewItem {
										var result []widgets.TreeViewItem
										x.GetBlockGroups().ForEach(func(entity model.BlockGroup) {
											result = append(result, itemCache.CreateItem(entity, level+1))
										})
										return result
									}, level+1, x),
									groupCache.CreateItem("Edges", func(ctx context.Context, level int) []widgets.TreeViewItem {
										var result []widgets.TreeViewItem
										x.GetEdges().ForEach(func(entity model.Edge) {
											result = append(result, itemCache.CreateItem(entity, level+1))
										})
										return result
									}, level+1, x),
									groupCache.CreateItem("Junctions", func(ctx context.Context, level int) []widgets.TreeViewItem {
										var result []widgets.TreeViewItem
										x.GetJunctions().ForEach(func(entity model.Junction) {
											result = append(result, itemCache.CreateItem(entity, level+1))
										})
										return result
									}, level+1, x),
									groupCache.CreateItem("Outputs", func(ctx context.Context, level int) []widgets.TreeViewItem {
										var result []widgets.TreeViewItem
										x.GetOutputs().ForEach(func(entity model.Output) {
											result = append(result, itemCache.CreateItem(entity, level+1))
										})
										return result
									}, level+1, x),
									groupCache.CreateItem("Routes", func(ctx context.Context, level int) []widgets.TreeViewItem {
										var result []widgets.TreeViewItem
										x.GetRoutes().ForEach(func(entity model.Route) {
											result = append(result, itemCache.CreateItem(entity, level+1))
										})
										return result
									}, level+1, x),
									groupCache.CreateItem("Sensors", func(ctx context.Context, level int) []widgets.TreeViewItem {
										var result []widgets.TreeViewItem
										x.GetSensors().ForEach(func(entity model.Sensor) {
											result = append(result, itemCache.CreateItem(entity, level+1))
										})
										return result
									}, level+1, x),
									groupCache.CreateItem("Signals", func(ctx context.Context, level int) []widgets.TreeViewItem {
										var result []widgets.TreeViewItem
										x.GetSignals().ForEach(func(entity model.Signal) {
											result = append(result, itemCache.CreateItem(entity, level+1))
										})
										return result
									}, level+1, x),
								)
							}
						})
						return result
					},
				},
				&widgets.TreeViewGroup{
					Name: "Command stations",
					Collection: func(ctx context.Context, level int) []widgets.TreeViewItem {
						var result []widgets.TreeViewItem
						railway.GetCommandStations().ForEach(func(c model.CommandStationRef) {
							if x := c.TryResolve(); x != nil {
								result = append(result, itemCache.CreateItem(x, level))
							}
						})
						return result
					},
				},
			},
		},
	}
	v.entityList.OnSelect = v.onSelect
	v.appBar = component.NewAppBar(v.modal)
	v.addSheet = component.NewModalSheet(v.modal)
	v.addSheetList.Axis = layout.Vertical
	v.entityList.OnSelect(railway)
	return v
}

// Select the given entity in the view
func (v *View) Select(entity model.Entity) {
	v.entityList.OnSelect(entity)
}

// Invalidate the UI
func (v *View) Invalidate() {
	v.vm.Invalidate()
}

// Save the railway
func (v *View) Save() {
	if err := v.railway.GetPackage().Save(); err != nil {
		dlgs.Error("Save failed", err.Error())
	}
}

// onSelect ensures that the given object is selected in the view
func (v *View) onSelect(selection interface{}) {
	switch selection := selection.(type) {
	case model.CommandStation:
		v.editor = editors.NewCommandStationEditor(selection, v)
	case model.Loc:
		v.editor = editors.NewLocEditor(selection, v)
	case model.Module:
		v.editor = editors.NewModuleEditor(selection, v)
	case model.Railway:
		v.editor = editors.NewRailwayEditor(selection, v)
	case model.ModuleEntity:
		// Re-use existing module editor (if possible)
		module := selection.GetModule()
		if modEditor, ok := v.editor.(editors.ModuleEditor); ok && modEditor.Module() == module {
			// Re-use module editor
			modEditor.OnSelect(selection)
		} else {
			// Build new module editor
			modEditor := editors.NewModuleEditor(module, v)
			modEditor.OnSelect(selection)
			v.editor = modEditor
		}
	default:
		v.editor = nil
	}
	if v.editor != nil {
		v.addSheetButtons = v.editor.CreateAddButtons()
	} else {
		v.addSheetButtons = nil
	}
	v.vm.Invalidate()
}

// Handle events and draw the view
func (v *View) Layout(gtx layout.Context) layout.Dimensions {
	th := v.vm.GetTheme()

	if v.buttonSave.Clicked() {
		v.Save()
	}
	if v.buttonRun.Clicked() {
		v.setRunMode(false)
	}
	if v.buttonRunVirtual.Clicked() {
		v.setRunMode(true)
	}
	if v.buttonAdd.Clicked() {
		v.addSheet.Appear(gtx.Now)
	}
	for idx := range v.addSheetButtons {
		btn := &v.addSheetButtons[idx]
		if btn.Clicked() {
			if btn.OnClick != nil {
				btn.OnClick()
			}
			v.addSheet.Disappear(gtx.Now)
			break
		}
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
	if v.railway.GetPackage().GetIsDirty() {
		v.appBar.Title = v.appBar.Title + " *"
	}
	v.appBar.SetActions(
		[]component.AppBarAction{
			component.SimpleIconAction(th, &v.buttonSave, views.IconSave, component.OverflowAction{Name: "Save", Tag: &v.buttonSave}),
			component.SimpleIconAction(th, &v.buttonAdd, views.IconAdd, component.OverflowAction{Name: "Add", Tag: &v.buttonAdd}),
			component.SimpleIconAction(th, &v.buttonRun, views.IconRun, component.OverflowAction{Name: "Run", Tag: &v.buttonRun}),
			component.SimpleIconAction(th, &v.buttonRunVirtual, views.IconRunVirtual, component.OverflowAction{Name: "Run virtual", Tag: &v.buttonRunVirtual}),
		},
		[]component.OverflowAction{})

	// Prepare content layout
	content := func(gtx C) D {
		if v.editor != nil {
			return v.editor.Layout(gtx, th)
		}
		return layout.Dimensions{Size: gtx.Constraints.Max}
	}
	bar := func(gtx C) D { return v.appBar.Layout(gtx, th) }
	hs := widgets.HorizontalSplit(
		func(gtx C) D { return widgets.WithPadding(gtx, func(gtx C) D { return v.entityList.Layout(gtx, th) }) },
		func(gtx C) D { return widgets.WithPadding(gtx, content) },
	)
	hs.End.Weight = 6
	vs := widgets.VerticalSplit(bar, hs.Layout)
	vs.Start.Rigid = true

	// Draw layers
	stack := layout.Stack{
		Alignment: layout.Direction(layout.SE),
	}
	v.addSheet.LayoutModal(func(gtx C, th *material.Theme, anim *component.VisibilityAnimation) D {
		return v.layoutAddSheet(gtx, th)
	})
	stack.Layout(gtx,
		layout.Stacked(vs.Layout),
		layout.Stacked(func(gtx C) D { return v.modal.Layout(gtx, th) }),
	)
	return layout.Dimensions{Size: gtx.Constraints.Max}
}

// Handle events and draw the add sheet
func (v *View) layoutAddSheet(gtx layout.Context, th *material.Theme) layout.Dimensions {
	return v.addSheetList.Layout(gtx, len(v.addSheetButtons), func(gtx C, index int) D {
		btn := &v.addSheetButtons[index]
		if btn.Separator {
			return layout.Spacer{
				Height: unit.Dp(20),
			}.Layout(gtx)
		}
		gtx.Constraints.Min.X = gtx.Constraints.Max.X
		return layout.UniformInset(unit.Dp(6)).Layout(gtx, material.Button(th, &btn.Clickable, btn.Title).Layout)
	})
}
