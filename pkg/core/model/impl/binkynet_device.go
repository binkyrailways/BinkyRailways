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
	"context"
	"fmt"

	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// BinkyNetDevice represents a hardware device such as a I2C chip.
type binkyNetDevice struct {
	container *binkyNetDeviceSet
	entity

	DeviceID api.DeviceID   `xml:"DeviceID,omitempty"`
	Type     api.DeviceType `xml:"Type,omitempty"`
	Address  string         `xml:"Address,omitempty"`
	Disabled bool           `xml:"Disabled,omitempty"`
	RouterID string         `xml:"RouterID,omitempty"`
}

var _ model.BinkyNetDevice = &binkyNetDevice{}

// newBinkyNetDevice creates and initializes a new binky device.
func newBinkyNetDevice() *binkyNetDevice {
	o := &binkyNetDevice{}
	o.EnsureID()
	return o
}

// SetContainer links this instance to its container
func (d *binkyNetDevice) SetContainer(container *binkyNetDeviceSet) {
	d.container = container
}

// Gets the local worker this object belongs to
func (d *binkyNetDevice) GetLocalWorker() model.BinkyNetLocalWorker {
	if d.container != nil {
		return d.container.GetLocalWorker()
	}
	return nil
}

// Gets the router that routes command & state to/from this device
func (d *binkyNetDevice) GetRouter() model.BinkyNetRouter {
	if d.RouterID == "" {
		return nil
	}
	r, _ := d.GetLocalWorker().GetRouters().Get(d.RouterID)
	return r
}
func (d *binkyNetDevice) SetRouter(ctx context.Context, value model.BinkyNetRouter) error {
	newRouterID := ""
	if value != nil {
		newRouterID = value.GetID()
	}
	if d.RouterID != newRouterID {
		d.RouterID = newRouterID
		d.OnModified()
	}
	return nil
}

// Accept a visit by the given visitor
func (d *binkyNetDevice) Accept(v model.EntityVisitor) interface{} {
	return v.VisitBinkyNetDevice(d)
}

// Gets the description of the entity
func (d *binkyNetDevice) GetDescription() string {
	return fmt.Sprintf("%s, %s (%s)", d.GetDeviceID(), d.GetDeviceType(), d.GetAddress())
}

// ID of the device
func (d *binkyNetDevice) GetDeviceID() api.DeviceID {
	return d.DeviceID
}
func (d *binkyNetDevice) SetDeviceID(ctx context.Context, value api.DeviceID) error {
	if d.DeviceID != value {
		oldValue := d.DeviceID
		d.DeviceID = value
		d.OnModified()

		// Update objects using this device
		if lw := d.GetLocalWorker(); lw != nil {
			for bnObj := range lw.GetObjects().All() {
				for bnc := range bnObj.GetConnections().All() {
					for pin := range bnc.GetPins().All() {
						if pin.GetDeviceID() == oldValue {
							fmt.Printf("Updating deviceID in pin of %s\n", bnObj.GetObjectID())
							pin.SetDeviceID(ctx, value)
						}
					}
				}
			}
		}
	}
	return nil
}

// Type of the device
func (d *binkyNetDevice) GetDeviceType() api.DeviceType {
	return d.Type
}
func (d *binkyNetDevice) SetDeviceType(ctx context.Context, value api.DeviceType) error {
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
func (d *binkyNetDevice) SetAddress(ctx context.Context, value string) error {
	if d.Address != value {
		d.Address = value
		d.OnModified()
	}
	return nil
}

// Is this device disabled?
func (d *binkyNetDevice) GetIsDisabled() bool {
	return d.Disabled
}
func (d *binkyNetDevice) SetIsDisabled(ctx context.Context, value bool) error {
	if d.Disabled != value {
		d.Disabled = value
		d.OnModified()
	}
	return nil
}

// OnModified triggers the modified function of the parent (if any)
func (d *binkyNetDevice) OnModified() {
	if d.container != nil {
		d.container.OnModified()
	}
	d.entity.OnModified()
}
