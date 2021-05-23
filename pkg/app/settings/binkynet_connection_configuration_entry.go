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

	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// newBinkyNetConnectionConfigurationEntry constructs a settings component for a
// single configuration key/value of a BinkyNetConnectionConfiguration.
func newBinkyNetConnectionConfigurationEntry(entity model.BinkyNetConnectionConfiguration, key api.ConfigKey) *binkyNetConnectionConfigurationEntry {
	s := &binkyNetConnectionConfigurationEntry{
		entity: entity,
		key:    key,
	}
	value, _ := entity.Get(string(key))
	s.editor.SetText(value)
	return s
}

// binkyNetConnectionConfigurationEntry implements an settings grid for single entry of a BinkyNetConnectionConfiguration.
type binkyNetConnectionConfigurationEntry struct {
	entity model.BinkyNetConnectionConfiguration
	key    api.ConfigKey

	editor widget.Editor
}

// Key returns the key of this entry
func (e *binkyNetConnectionConfigurationEntry) Key() api.ConfigKey {
	return e.key
}

// Update the values in the given entity from the UI.
func (e *binkyNetConnectionConfigurationEntry) Update(entity model.BinkyNetConnectionConfiguration) {
	value := e.editor.Text()
	if err := e.key.ValidateValue(value); err == nil {
		entity.Set(string(e.key), value)
	}
}

// Handle events and draw the editor
func (e *binkyNetConnectionConfigurationEntry) Rows(th *material.Theme) []widgets.SettingsGridRow {
	return []widgets.SettingsGridRow{
		widgets.SettingsGridRow{Title: string(e.key), Layout: func(gtx C) D {
			return material.Editor(th, &e.editor, e.key.DefaultValue()).Layout(gtx)
		}},
	}
}
