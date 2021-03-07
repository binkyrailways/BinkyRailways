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

package editors

import (
	"gioui.org/layout"
	"gioui.org/widget"
	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/app/canvas"
	"github.com/binkyrailways/BinkyRailways/pkg/app/canvas/edit"
	"github.com/binkyrailways/BinkyRailways/pkg/app/settings"
	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// ModuleEditor extends the basic Editor interface
type ModuleEditor interface {
	Editor

	// Module returns the module we're editing
	Module() model.Module
	// OnSelect is called when the currently selected entity has changed.
	OnSelect(entity model.ModuleEntity)
}

// newModuleEditor constructs an editor for a Module.
func newModuleEditor(module model.Module, etx EditorContext) ModuleEditor {
	editor := &moduleEditor{
		module: module,
		etx:    etx,
		canvas: canvas.ModuleCanvas(module, edit.NewBuilder()),
		scaleSlider: &widget.Float{
			Value: 100.0,
			Axis:  layout.Horizontal,
		},
	}
	editor.canvas.OnSelect = func(selection canvas.Entity) {
		switch selection := selection.(type) {
		case model.ModuleEntity:
			editor.OnSelect(selection)
		default:
			editor.OnSelect(nil)
		}
	}
	editor.settings = settings.NewModuleSettings(module)

	return editor
}

// moduleEditor implements an editor for a Module.
type moduleEditor struct {
	module model.Module
	etx    EditorContext

	canvas      *canvas.EntityCanvas
	settings    settings.Settings
	scaleSlider *widget.Float
}

// Module returns the module we're editing
func (e *moduleEditor) Module() model.Module {
	return e.module
}

// OnSelect is called when the currently selected entity has changed.
func (e *moduleEditor) OnSelect(entity model.ModuleEntity) {
	if entity != nil {
		if x, ok := entity.Accept(settings.NewBuilder()).(settings.Settings); ok {
			e.settings = x
		} else {
			e.settings = settings.NewModuleSettings(e.module)
		}
	} else {
		e.settings = settings.NewModuleSettings(e.module)
	}
	e.etx.Invalidate()
}

// Handle events and draw the editor
func (e *moduleEditor) Layout(gtx C, th *material.Theme) D {
	if e.scaleSlider.Changed() {
		scale := (e.scaleSlider.Value + 1) / 100.0
		e.canvas.SetScale(scale)
	}

	// Prepare canvas + slider
	vs := widgets.VerticalSplit(
		func(gtx C) D { return e.canvas.Layout(gtx, th) },
		func(gtx C) D { return material.Slider(th, e.scaleSlider, 0, 199).Layout(gtx) },
	)
	vs.End.Rigid = true

	// Prepare content split
	hs := widgets.HorizontalSplit(
		func(gtx C) D { return vs.Layout(gtx) },
		func(gtx C) D { return e.settings.Layout(gtx, th) },
	)
	hs.Start.Weight = 6
	hs.End.Rigid = true
	hs.End.Weight = 1

	return hs.Layout(gtx)
}

// Create the buttons for the "Add resource sheet"
func (e *moduleEditor) CreateAddButtons() []AddButton {
	return createAddButtonsFor(e.etx, e.module)
}
