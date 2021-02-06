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

// metaSettings implements a part of settings grid for a positioned entities.
type metaSettings struct {
	description w.Editor
}

// Initialize the UI from the given entity
func (e *metaSettings) Initialize(source model.Entity) {
	e.description.SingleLine = true
	e.description.SetText(source.GetDescription())
}

// Update the values in the given entity from the UI.
func (e *metaSettings) Update(entity model.Entity) {
	entity.SetDescription(e.description.Text())
}

// Rows generates rows for a settings grid.
func (e *metaSettings) Rows(th *material.Theme) []widgets.SettingsGridRow {
	return []widgets.SettingsGridRow{
		{Title: "Name", Layout: func(gtx C) D {
			return material.Editor(th, &e.description, "Name").Layout(gtx)
		}},
	}
}
