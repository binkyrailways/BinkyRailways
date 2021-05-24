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

// addressSettings implements a part of settings grid for a address entities.
type addressSettings struct {
	address    w.Editor
	isNotValid bool
}

// Initialize the UI from the given entity
func (e *addressSettings) Initialize(source model.AddressEntity) {
	e.address.SingleLine = true
	e.address.SetText(source.GetAddress().String())
}

// Update the values in the given entity from the UI.
func (e *addressSettings) Update(entity model.AddressEntity) {
	if addr, err := model.NewAddressFromString(e.address.Text()); err == nil {
		entity.SetAddress(addr)
		e.isNotValid = false
	} else {
		e.isNotValid = true
	}
}

// Rows generates rows for a settings grid.
func (e *addressSettings) Rows(th *material.Theme) []widgets.SettingsGridRow {
	return []widgets.SettingsGridRow{
		{Title: "Name", Layout: func(gtx C) D {
			edt := material.Editor(th, &e.address, "Address")
			if e.isNotValid {
				edt.Color = widgets.ARGB(0xFFFF0000)
			}
			return edt.Layout(gtx)
		}},
	}
}
