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

package impl

import (
	"fmt"

	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// BinkyNetDevice represents a hardware device such as a I2C chip.
type binkyNetDevice struct {
	onModified func()
	entity

	Type    api.DeviceType `xml:"Type,omitempty"`
	Address string         `xml:"Address,omitempty"`
}

var _ model.BinkyNetDevice = &binkyNetDevice{}

// Accept a visit by the given visitor
func (d *binkyNetDevice) Accept(v model.EntityVisitor) interface{} {
	return v.VisitBinkyNetDevice(d)
}

// Gets the description of the entity
func (d *binkyNetDevice) GetDescription() string {
	return fmt.Sprintf("%s (%s, %s)", d.GetDeviceType(), d.GetID(), d.GetAddress())
}

// ID of the device (equal to entity ID)
func (d *binkyNetDevice) GetDeviceID() api.DeviceID {
	return api.DeviceID(d.GetID())
}

// Type of the device
func (d *binkyNetDevice) GetDeviceType() api.DeviceType {
	return d.Type
}
func (d *binkyNetDevice) SetDeviceType(value api.DeviceType) error {
	if d.Type != value {
		d.Type = value
		d.OnModified()
	}
	return nil
}

// Address of the device
func (d *binkyNetDevice) GetAddress() string {
	return d.Address
}
func (d *binkyNetDevice) SetAddress(value string) error {
	if d.Address != value {
		d.Address = value
		d.OnModified()
	}
	return nil
}

// OnModified triggers the modified function of the parent (if any)
func (d *binkyNetDevice) OnModified() {
	if d.onModified != nil {
		d.onModified()
	}
	d.entity.OnModified()
}
