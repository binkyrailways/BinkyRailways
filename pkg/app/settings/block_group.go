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

// NewBlockGroupSettings constructs a settings component for a BlockGroup.
func NewBlockGroupSettings(entity model.BlockGroup) Settings {
	s := &blockGroupSettings{
		entity: entity,
	}
	s.metaSettings.Initialize(entity)
	return s
}

// blockGroupSettings implements an settings grid for a BlockGroup.
type blockGroupSettings struct {
	entity model.BlockGroup

	metaSettings
}

// Handle events and draw the editor
func (e *blockGroupSettings) Layout(gtx C, th *material.Theme) D {
	e.metaSettings.Update(e.entity)

	// Prepare settings grid
	grid := widgets.NewSettingsGrid(
		e.metaSettings.Rows(th)...,
	)

	return grid.Layout(gtx, th)
}
