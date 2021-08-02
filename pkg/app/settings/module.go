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

package settings

import (
	w "gioui.org/widget"
	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// NewModuleSettings constructs a settings component for a Module.
func NewModuleSettings(entity model.Module) Settings {
	moduleRef, _ := entity.GetPackage().GetRailway().GetModules().Get(entity.GetID())
	s := &moduleSettings{
		entity:    entity,
		moduleRef: moduleRef,
	}
	s.description.SetText(entity.GetDescription())
	s.positionSettings.Initialize(moduleRef, false)
	return s
}

// moduleSettings implements an settings grid for a Module.
type moduleSettings struct {
	entity    model.Module
	moduleRef model.ModuleRef

	description w.Editor
	positionSettings
}

// Handle events and draw the editor
func (e *moduleSettings) Layout(gtx C, th *material.Theme) D {
	e.entity.SetDescription(e.description.Text())
	e.positionSettings.Update(e.moduleRef)

	positionRows := e.positionSettings.Rows(th)
	// Prepare settings grid
	grid := widgets.NewSettingsGrid(
		append([]widgets.SettingsGridRow{
			{Title: "Name", Layout: func(gtx C) D {
				return material.Editor(th, &e.description, "Name").Layout(gtx)
			}},
		}, positionRows...)...,
	)

	return grid.Layout(gtx, th)
}
