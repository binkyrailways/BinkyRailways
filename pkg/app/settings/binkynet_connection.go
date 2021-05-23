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

// binkyNetConnectionSettings implements an settings grid for a BinkyNetConnection.
type binkyNetConnectionSettings struct {
	pins   binkyNetDevicePinListSettings
	config binkyNetConnectionConfigurationSettings
}

// Update the values in the given entity from the UI.
func (e *binkyNetConnectionSettings) Update(entity model.BinkyNetConnection) {
	e.pins.Update(entity.GetPins())
	e.config.Update(entity.GetConfiguration())
}

// Rows generates rows for a settings grid.
func (e *binkyNetConnectionSettings) Rows(th *material.Theme) []widgets.SettingsGridRow {
	return append(e.pins.Rows(th), e.config.Rows(th)...)
}
