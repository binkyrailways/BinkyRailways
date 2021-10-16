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

// NewBinaryOutputSettings constructs a settings component for a BinaryOutput.
func NewBinaryOutputSettings(entity model.BinaryOutput) Settings {
	s := &binaryOutputSettings{
		entity: entity,
	}
	s.metaSettings.Initialize(entity)
	s.addressSettings.Initialize(entity)
	s.positionSettings.Initialize(entity)

	s.boType.Value = string(entity.GetBinaryOutputType())
	var values []widgets.LabeledValue
	for _, ot := range model.AllBinaryOutputTypes {
		values = append(values, widgets.LV(string(ot)))
	}
	s.boTypeSel = widgets.NewSimpleSelect(&s.boType, values...)
	s.boType.Value = string(entity.GetBinaryOutputType())

	return s
}

// binaryOutputSettings implements an settings grid for a BinaryOutput.
type binaryOutputSettings struct {
	entity model.BinaryOutput

	metaSettings
	addressSettings
	positionSettings
	boType    w.Enum
	boTypeSel *widgets.SimpleSelect
}

// Handle events and draw the editor
func (e *binaryOutputSettings) Layout(gtx C, th *material.Theme) D {
	e.metaSettings.Update(e.entity)
	e.addressSettings.Update(e.entity)
	e.positionSettings.Update(e.entity)
	e.entity.SetBinaryOutputType(model.BinaryOutputType(e.boType.Value))

	// Prepare settings grid
	typeRows := []widgets.SettingsGridRow{
		{Title: "Output type", Layout: func(gtx C) D {
			return e.boTypeSel.Layout(gtx, th)
		}},
	}
	grid := widgets.NewSettingsGrid(
		append(append(append(
			e.metaSettings.Rows(th),
			typeRows...),
			e.addressSettings.Rows(th)...),
			e.positionSettings.Rows(th)...,
		)...,
	)

	return grid.Layout(gtx, th)
}
