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
	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// positionSettings implements a part of settings grid for a positioned entities.
type positionSettings struct {
	x        widgets.IntEditor
	y        widgets.IntEditor
	width    widgets.IntEditor
	height   widgets.IntEditor
	rotation widgets.IntEditor
}

// Initialize the UI from the given entity
func (e *positionSettings) Initialize(source model.PositionedEntity) {
	e.x.SetValue(source.GetX())
	e.y.SetValue(source.GetY())
	e.width.SetValue(source.GetWidth())
	e.height.SetValue(source.GetHeight())
	e.rotation.SetValue(source.GetRotation())
}

// Update the values in the given entity from the UI.
func (e *positionSettings) Update(entity model.PositionedEntity) {
	if value, err := e.x.GetValue(); err == nil {
		entity.SetX(value)
	}
	if value, err := e.y.GetValue(); err == nil {
		entity.SetY(value)
	}
	if value, err := e.width.GetValue(); err == nil {
		entity.SetWidth(value)
	}
	if value, err := e.height.GetValue(); err == nil {
		entity.SetHeight(value)
	}
	if value, err := e.rotation.GetValue(); err == nil {
		entity.SetRotation(value)
	}
}

// Rows generates rows for a settings grid.
func (e *positionSettings) Rows(th *material.Theme) []widgets.SettingsGridRow {
	return []widgets.SettingsGridRow{
		{Title: "X", Layout: func(gtx C) D {
			return material.Editor(th, &e.x.Editor, "").Layout(gtx)
		}},
		{Title: "Y", Layout: func(gtx C) D {
			return material.Editor(th, &e.y.Editor, "").Layout(gtx)
		}},
		{Title: "Width", Layout: func(gtx C) D {
			return material.Editor(th, &e.width.Editor, "").Layout(gtx)
		}},
		{Title: "Height", Layout: func(gtx C) D {
			return material.Editor(th, &e.height.Editor, "").Layout(gtx)
		}},
		{Title: "Rotation", Layout: func(gtx C) D {
			return material.Editor(th, &e.rotation.Editor, "").Layout(gtx)
		}},
	}
}
