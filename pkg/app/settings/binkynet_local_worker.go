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

// NewBinkyNetLocalWorkerSettings constructs a settings component for a BinkyNetLocalWorker.
func NewBinkyNetLocalWorkerSettings(entity model.BinkyNetLocalWorker) Settings {
	s := &binkyNetLocalWorkerSettings{
		entity: entity,
	}
	s.hardwareID.SetText(entity.GetHardwareID())
	s.alias.SetText(entity.GetAlias())
	return s
}

// binkyNetLocalWorkerSettings implements an settings grid for a BinkyNetLocalWorker.
type binkyNetLocalWorkerSettings struct {
	entity model.BinkyNetLocalWorker

	hardwareID w.Editor
	alias      w.Editor
}

// Handle events and draw the editor
func (e *binkyNetLocalWorkerSettings) Layout(gtx C, th *material.Theme) D {
	e.entity.SetHardwareID(e.hardwareID.Text())
	e.entity.SetAlias(e.alias.Text())

	// Prepare settings grid
	grid := widgets.NewSettingsGrid(
		widgets.SettingsGridRow{Title: "Hardware ID", Layout: func(gtx C) D {
			return material.Editor(th, &e.hardwareID, "Hardware ID").Layout(gtx)
		}},
		widgets.SettingsGridRow{Title: "Alias", Layout: func(gtx C) D {
			return material.Editor(th, &e.alias, "Alias").Layout(gtx)
		}},
	)

	return grid.Layout(gtx, th)
}
