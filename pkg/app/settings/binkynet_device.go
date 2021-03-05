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

// NewBinkyNetDeviceSettings constructs a settings component for a BinkyNetDevice.
func NewBinkyNetDeviceSettings(entity model.BinkyNetDevice) Settings {
	s := &binkyNetDeviceSettings{
		entity: entity,
	}
	s.dtype.SetText(string(entity.GetDeviceType()))
	s.address.SetText(entity.GetAddress())
	return s
}

// binkyNetDeviceSettings implements an settings grid for a BinkyNetDevice.
type binkyNetDeviceSettings struct {
	entity model.BinkyNetDevice

	dtype   w.Editor
	address w.Editor
}

// Handle events and draw the editor
func (e *binkyNetDeviceSettings) Layout(gtx C, th *material.Theme) D {
	e.entity.SetDeviceType(api.DeviceType(e.dtype.Text()))
	e.entity.SetAddress(e.address.Text())

	// Prepare settings grid
	grid := widgets.NewSettingsGrid(
		widgets.SettingsGridRow{Title: "Type", Layout: func(gtx C) D {
			return material.Editor(th, &e.dtype, "Type").Layout(gtx)
		}},
		widgets.SettingsGridRow{Title: "Address", Layout: func(gtx C) D {
			return material.Editor(th, &e.address, "Address").Layout(gtx)
		}},
	)

	return grid.Layout(gtx, th)
}
