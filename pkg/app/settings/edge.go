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

// NewEdgeSettings constructs a settings component for an Edge.
func NewEdgeSettings(entity model.Edge) Settings {
	s := &edgeSettings{
		entity: entity,
	}
	s.metaSettings.Initialize(entity)
	s.positionSettings.Initialize(entity)
	return s
}

// edgeSettings implements an settings grid for an Edge.
type edgeSettings struct {
	entity model.Edge

	metaSettings
	positionSettings
}

// Handle events and draw the editor
func (e *edgeSettings) Layout(gtx C, th *material.Theme) D {
	e.metaSettings.Update(e.entity)
	e.positionSettings.Update(e.entity)

	// Prepare settings grid
	grid := widgets.NewSettingsGrid(
		append(
			e.metaSettings.Rows(th),
			e.positionSettings.Rows(th)...,
		)...,
	)

	return grid.Layout(gtx, th)
}
