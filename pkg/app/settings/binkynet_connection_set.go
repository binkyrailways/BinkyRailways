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

// binkyNetConnectionSetSettings implements an settings grid for a BinkyNetConnectionSet.
type binkyNetConnectionSetSettings struct {
	connections []binkyNetConnectionSettings
	rows        []widgets.SettingsGridRow
}

// Update the values in the given entity from the UI.
func (e *binkyNetConnectionSetSettings) Update(entity model.BinkyNetConnectionSet) {
	cnt := entity.GetCount()
	if len(e.connections) != cnt {
		e.connections = make([]binkyNetConnectionSettings, cnt)
		e.rows = make([]widgets.SettingsGridRow, cnt)
	}
	for i := range e.connections {
		conn, _ := entity.GetAt(i)
		e.connections[i].Update(conn)
		e.rows[i].Title = string(conn.GetKey())
	}
}

// Rows generates rows for a settings grid.
func (e *binkyNetConnectionSetSettings) Rows(th *material.Theme) []widgets.SettingsGridRow {
	for i := range e.connections {
		i := i // Bring into scope
		e.rows[i].Layout = func(gtx C) D {
			connRows := e.connections[i].Rows(th)
			grid := widgets.NewSettingsGrid(connRows...)
			return grid.Layout(gtx, th)
		}
	}
	return e.rows
}
