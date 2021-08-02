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
	"gioui.org/widget"
	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// NewBlockSettings constructs a settings component for a Block.
func NewBlockSettings(entity model.Block) Settings {
	s := &blockSettings{
		entity: entity,
	}
	s.metaSettings.Initialize(entity)
	s.positionSettings.Initialize(entity)
	s.reverseSides.Value = entity.GetReverseSides()
	return s
}

// blockSettings implements an settings grid for a Block.
type blockSettings struct {
	entity model.Block

	metaSettings
	positionSettings
	reverseSides widget.Bool
}

// Handle events and draw the editor
func (e *blockSettings) Layout(gtx C, th *material.Theme) D {
	e.metaSettings.Update(e.entity)
	e.positionSettings.Update(e.entity)
	e.entity.SetReverseSides(e.reverseSides.Value)

	// Prepare settings grid
	grid := widgets.NewSettingsGrid(
		append(append(
			e.metaSettings.Rows(th),
			e.positionSettings.Rows(th)...),
			widgets.SettingsGridRow{Title: "Reverse sides", Layout: func(gtx C) D {
				return material.CheckBox(th, &e.reverseSides, "").Layout(gtx)
			}},
		)...,
	)

	return grid.Layout(gtx, th)
}
