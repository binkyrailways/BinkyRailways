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
	"fmt"

	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// binkyNetDevicePinListSettings implements an settings grid for a BinkyNetDevicePinList.
type binkyNetDevicePinListSettings struct {
	pins []*binkyNetDevicePinSettings
}

// Update the values in the given entity from the UI.
func (e *binkyNetDevicePinListSettings) Update(entity model.BinkyNetConnectionPinList) {
	cnt := entity.GetCount()
	if len(e.pins) != cnt {
		e.pins = make([]*binkyNetDevicePinSettings, cnt)
	}
	for i, p := range e.pins {
		i, p := i, p // Bring into scope
		pin, _ := entity.Get(i)
		if p == nil || p.entity != pin {
			e.pins[i] = newBinkyNetDevicePinSettings(pin)
		}
		e.pins[i].Update(pin)
	}
}

// Rows generates rows for a settings grid.
func (e *binkyNetDevicePinListSettings) Rows(th *material.Theme) []widgets.SettingsGridRow {
	var rows []widgets.SettingsGridRow
	for i, ps := range e.pins {
		rows = append(rows, widgets.SettingsGridRow{
			Title:      fmt.Sprintf("Pin %d", i),
			TitleScale: 1,
		})
		rows = append(rows, ps.Rows(th)...)
	}
	return rows
}
