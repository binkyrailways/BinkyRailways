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

	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// binkyNetConnectionConfigurationSettings implements an settings grid for a BinkyNetConnectionConfiguration.
type binkyNetConnectionConfigurationSettings struct {
	entries []*binkyNetConnectionConfigurationEntry
}

// Update the values in the given entity from the UI.
func (e *binkyNetConnectionConfigurationSettings) Update(entity model.BinkyNetConnectionConfiguration) {
	cnt := entity.GetCount()
	keys := make([]api.ConfigKey, 0, cnt)
	entity.ForEach(func(key, value string) {
		keys = append(keys, api.ConfigKey(key))
	})
	if len(e.entries) != cnt {
		e.entries = make([]*binkyNetConnectionConfigurationEntry, cnt)
	}
	for i, p := range e.entries {
		i, p := i, p // Bring into scope
		key := keys[i]
		if p == nil || p.Key() != key {
			e.entries[i] = newBinkyNetConnectionConfigurationEntry(entity, key)
		}
		e.entries[i].Update(entity)
	}
}

// Rows generates rows for a settings grid.
func (e *binkyNetConnectionConfigurationSettings) Rows(th *material.Theme) []widgets.SettingsGridRow {
	var rows []widgets.SettingsGridRow
	for _, ps := range e.entries {
		rows = append(rows, ps.Rows(th)...)
	}
	return rows
}
