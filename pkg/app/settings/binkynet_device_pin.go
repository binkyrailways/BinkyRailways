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

// newBinkyNetDevicePinSettings constructs a settings component for a BinkyNetDevicePin.
func newBinkyNetDevicePinSettings(entity model.BinkyNetDevicePin) *binkyNetDevicePinSettings {
	s := &binkyNetDevicePinSettings{
		entity: entity,
	}
	s.devID.Value = string(entity.GetDeviceID())
	var values []widgets.LabeledValue
	conn := entity.GetConnection()
	if conn == nil {
		return nil
	}
	obj := conn.GetObject()
	if obj == nil {
		return nil
	}
	lw := obj.GetLocalWorker()
	if lw == nil {
		return nil
	}
	lw.GetDevices().ForEach(func(d model.BinkyNetDevice) {
		values = append(values, widgets.LV(string(d.GetDeviceID()), d.GetDescription()))
	})
	s.devIDSel = widgets.NewSimpleSelect(&s.devID, values...)
	s.index.SetValue(int(entity.GetIndex()))
	return s
}

// binkyNetConnectionSettings implements an settings grid for a BinkyNetConnection.
type binkyNetDevicePinSettings struct {
	entity model.BinkyNetDevicePin

	devID    w.Enum
	devIDSel *widgets.SimpleSelect
	index    widgets.IntEditor
}

// Update the values in the given entity from the UI.
func (e *binkyNetDevicePinSettings) Update(entity model.BinkyNetDevicePin) {
	e.entity.SetDeviceID(api.DeviceID(e.devID.Value))
	if x, err := e.index.GetValue(); err == nil {
		e.entity.SetIndex(api.DeviceIndex(x))
	}
}

// Handle events and draw the editor
func (e *binkyNetDevicePinSettings) Rows(th *material.Theme) []widgets.SettingsGridRow {
	return []widgets.SettingsGridRow{
		widgets.SettingsGridRow{Title: "Device ID", Layout: func(gtx C) D {
			return e.devIDSel.Layout(gtx, th)
		}},
		widgets.SettingsGridRow{Title: "Index", Layout: func(gtx C) D {
			return e.index.Layout(gtx, th)
		}},
	}
}
