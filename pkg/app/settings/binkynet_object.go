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

	api "github.com/binkynet/BinkyNet/apis/v1"

	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// NewBinkyNetObjectSettings constructs a settings component for a BinkyNetObject.
func NewBinkyNetObjectSettings(entity model.BinkyNetObject) Settings {
	s := &binkyNetObjectSettings{
		entity: entity,
	}
	s.id.SetText(string(entity.GetObjectID()))
	s.otype.Value = string(entity.GetObjectType())
	var values []widgets.LabeledValue
	for _, ot := range api.AllObjectTypes() {
		values = append(values, widgets.LV(string(ot)))
	}
	s.otypeSel = widgets.NewSimpleSelect(&s.otype, values...)
	s.otype.Value = string(entity.GetObjectType())
	return s
}

// binkyNetObjectSettings implements an settings grid for a BinkyNetObject.
type binkyNetObjectSettings struct {
	entity model.BinkyNetObject

	id       w.Editor
	otype    w.Enum
	otypeSel *widgets.SimpleSelect
	binkyNetConnectionSetSettings
}

// Handle events and draw the editor
func (e *binkyNetObjectSettings) Layout(gtx C, th *material.Theme) D {
	e.binkyNetConnectionSetSettings.Update(e.entity.GetConnections())
	e.entity.SetObjectID(api.ObjectID(e.id.Text()))
	e.entity.SetObjectType(api.ObjectType(e.otype.Value))

	// Prepare settings grid
	rows := []widgets.SettingsGridRow{
		{Title: "Object"},
		{Title: "Object ID", Layout: func(gtx C) D {
			return material.Editor(th, &e.id, "Object ID").Layout(gtx)
		}},
		{Title: "Type", Layout: func(gtx C) D {
			return e.otypeSel.Layout(gtx, th)
		}},
		{Title: "Connections"},
	}
	rows = append(rows, e.binkyNetConnectionSetSettings.Rows(th)...)
	grid := widgets.NewSettingsGrid(rows...)

	return grid.Layout(gtx, th)
}
