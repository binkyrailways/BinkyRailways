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
	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type binkyNetDevicePin struct {
	container *binkyNetConnectionPinList

	DeviceID api.DeviceID    `xml:"DeviceID,omitempty"`
	Index    api.DeviceIndex `xml:"Index,omiyempty"`
}

// Gets the containing local worker
func (p *binkyNetDevicePin) GetLocalWorker() model.BinkyNetLocalWorker {
	if p.container != nil {
		return p.container.GetLocalWorker()
	}
	return nil
}

// Gets the connection that contains this pin
func (p *binkyNetDevicePin) GetConnection() model.BinkyNetConnection {
	if p.container != nil {
		return p.container.GetConnection()
	}
	return nil
}

// SetContainer links this instance to its container
func (p *binkyNetDevicePin) SetContainer(container *binkyNetConnectionPinList) {
	p.container = container
}

// ID of the device that this connection refers to.
func (p *binkyNetDevicePin) GetDeviceID() api.DeviceID {
	return p.DeviceID
}
func (p *binkyNetDevicePin) SetDeviceID(value api.DeviceID) error {
	if p.DeviceID != value {
		p.DeviceID = value
		p.OnModified()
	}
	return nil
}

// Index on the device (1...)
func (p *binkyNetDevicePin) GetIndex() api.DeviceIndex {
	return p.Index
}
func (p *binkyNetDevicePin) SetIndex(value api.DeviceIndex) error {
	if p.Index != value {
		p.Index = value
		p.OnModified()
	}
	return nil
}

// OnModified calls the parents modified callback.
func (p *binkyNetDevicePin) OnModified() {
	if p.container != nil {
		p.container.OnModified()
	}
}
