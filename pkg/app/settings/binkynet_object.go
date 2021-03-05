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

	api "github.com/binkynet/BinkyNet/apis/v1"

	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// NewBinkyNetObjectSettings constructs a settings component for a BinkyNetObject.
func NewBinkyNetObjectSettings(entity model.BinkyNetObject) Settings {
	s := &binkyNetObjectSettings{
		entity: entity,
	}
	s.dtype.SetText(string(entity.GetObjectType()))
	return s
}

// binkyNetObjectSettings implements an settings grid for a BinkyNetObject.
type binkyNetObjectSettings struct {
	entity model.BinkyNetObject

	dtype w.Editor
}

// Handle events and draw the editor
func (e *binkyNetObjectSettings) Layout(gtx C, th *material.Theme) D {
	e.entity.SetObjectType(api.ObjectType(e.dtype.Text()))

	// Prepare settings grid
	grid := widgets.NewSettingsGrid(
		widgets.SettingsGridRow{Title: "Type", Layout: func(gtx C) D {
			return material.Editor(th, &e.dtype, "Type").Layout(gtx)
		}},
	)

	return grid.Layout(gtx, th)
}