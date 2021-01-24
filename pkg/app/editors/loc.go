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
	w "gioui.org/widget"
	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// NewLocEditor constructs an editor for a Loc.
func NewLocEditor(loc model.Loc, etx EditorContext) Editor {
	editor := &locEditor{
		loc: loc,
		etx: etx,
	}
	editor.description.SetText(loc.GetDescription())
	editor.owner.SetText(loc.GetOwner())
	return editor
}

// locEditor implements an editor for a Loc.
type locEditor struct {
	loc model.Loc
	etx EditorContext

	description w.Editor
	owner       w.Editor
}

// Handle events and draw the editor
func (e *locEditor) Layout(gtx C, th *material.Theme) D {
	grid := widgets.NewSettingsGrid(
		widgets.SettingsGridRow{Title: "Name", Layout: func(gtx C) D {
			return material.Editor(th, &e.description, "Name").Layout(gtx)
		}},
		widgets.SettingsGridRow{Title: "Owner", Layout: func(gtx C) D {
			return material.Editor(th, &e.owner, "Owner").Layout(gtx)
		}},
	)
	return grid.Layout(gtx, th)
}
