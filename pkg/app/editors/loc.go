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

// newLocEditor constructs an editor for a Loc.
func newLocEditor(loc model.Loc, etx EditorContext) Editor {
	editor := &locEditor{
		loc:      loc,
		settings: settings.NewLocSettings(loc),
		etx:      etx,
	}
	return editor
}

// locEditor implements an editor for a Loc.
type locEditor struct {
	loc      model.Loc
	settings settings.Settings
	etx      EditorContext
}

// Handle events and draw the editor
func (e *locEditor) Layout(gtx C, th *material.Theme) D {
	return e.settings.Layout(gtx, th)
}

// Create the buttons for the "Add resource sheet"
func (e *locEditor) CreateAddButtons() []AddButton {
	return createAddButtonsFor(e.etx, e.loc)
}
