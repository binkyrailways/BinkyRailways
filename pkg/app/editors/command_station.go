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
	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/app/settings"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// NewCommandStationEditor constructs an editor for a CommandStation.
func NewCommandStationEditor(cs model.CommandStation, etx EditorContext) Editor {
	editor := &commandStationEditor{
		cs:       cs,
		settings: settings.BuildSettings(cs),
		etx:      etx,
	}
	return editor
}

// commandStationEditor implements an editor for a CommandStation.
type commandStationEditor struct {
	cs       model.CommandStation
	settings settings.Settings
	etx      EditorContext
}

// Handle events and draw the editor
func (e *commandStationEditor) Layout(gtx C, th *material.Theme) D {
	return e.settings.Layout(gtx, th)
}

// Create the buttons for the "Add resource sheet"
func (e *commandStationEditor) CreateAddButtons() []AddButton {
	return CreatePersistentEntityAddButtons(e.cs, e.etx)
}
