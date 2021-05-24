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

// NewLocSettings constructs a settings component for a Loc.
func NewLocSettings(entity model.Loc) Settings {
	s := &locSettings{
		entity: entity,
	}
	s.description.SetText(entity.GetDescription())
	s.owner.SetText(entity.GetOwner())
	s.addressSettings.Initialize(entity)
	return s
}

// locSettings implements an settings grid for a Loc.
type locSettings struct {
	entity model.Loc

	description w.Editor
	owner       w.Editor
	addressSettings
}

// Handle events and draw the editor
func (e *locSettings) Layout(gtx C, th *material.Theme) D {
	e.entity.SetDescription(e.description.Text())
	e.entity.SetOwner(e.owner.Text())
	e.addressSettings.Update(e.entity)

	// Prepare settings grid
	grid := widgets.NewSettingsGrid(
		append([]widgets.SettingsGridRow{
			{Title: "Name", Layout: func(gtx C) D {
				return material.Editor(th, &e.description, "Name").Layout(gtx)
			}},
			{Title: "Owner", Layout: func(gtx C) D {
				return material.Editor(th, &e.owner, "Owner").Layout(gtx)
			}},
		}, e.addressSettings.Rows(th)...)...,
	)

	return grid.Layout(gtx, th)
}
