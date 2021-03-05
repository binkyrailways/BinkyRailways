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

// newBinkyNetEditor constructs an editor for a BinkyNet entities.
func newBinkyNetEditor(entity interface{}, etx EditorContext) Editor {
	editor := &binkyNetEditor{
		entity:   entity,
		settings: settings.BuildSettings(entity),
		etx:      etx,
	}
	return editor
}

// binkyNetEditor implements an editor for a BinkyNet entities.
type binkyNetEditor struct {
	entity   interface{}
	settings settings.Settings
	etx      EditorContext
}

// Handle events and draw the editor
func (e *binkyNetEditor) Layout(gtx C, th *material.Theme) D {
	if e.settings == nil {
		return D{}
	}
	return e.settings.Layout(gtx, th)
}

// Create the buttons for the "Add resource sheet"
func (e *binkyNetEditor) CreateAddButtons() []AddButton {
	switch entity := e.entity.(type) {
	case model.BinkyNetLocalWorker:
		return []AddButton{
			{
				Title: "Add Device",
				OnClick: func() {
					dev := entity.GetDevices().AddNew()
					e.etx.Select(dev)
				},
			},
			{
				Title: "Add Object",
				OnClick: func() {
					dev := entity.GetObjects().AddNew()
					e.etx.Select(dev)
				},
			},
		}
	}
	return nil
}
