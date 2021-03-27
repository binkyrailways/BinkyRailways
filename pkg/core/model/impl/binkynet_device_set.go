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
	"encoding/xml"
	"fmt"

	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type binkyNetDeviceSet struct {
	container *binkyNetLocalWorker
	binkyNetDeviceSetItems
}

type binkyNetDeviceSetItems struct {
	Devices []*binkyNetDevice `xml:"Devices,omitempty"`
}

var _ model.BinkyNetDeviceSet = &binkyNetDeviceSet{}

// UnmarshalXML unmarshals and connects the module.
func (l *binkyNetDeviceSet) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&l.binkyNetDeviceSetItems, &start); err != nil {
		return err
	}
	for _, x := range l.Devices {
		x.SetContainer(l)
	}
	return nil
}

// Gets the local worker this set belongs to
func (l *binkyNetDeviceSet) GetLocalWorker() model.BinkyNetLocalWorker {
	return l.container
}

// SetContainer links this instance to its container
func (l *binkyNetDeviceSet) SetContainer(container *binkyNetLocalWorker) {
	l.container = container
}

// Get number of entries
func (l *binkyNetDeviceSet) GetCount() int {
	return len(l.Devices)
}

// Get an entry by ID.
func (l *binkyNetDeviceSet) Get(id api.DeviceID) (model.BinkyNetDevice, bool) {
	for _, d := range l.Devices {
		if d.GetDeviceID() == id {
			return d, true
		}
	}
	return nil, false
}

// Invoke the callback for each entry.
func (l *binkyNetDeviceSet) ForEach(cb func(model.BinkyNetDevice)) {
	for _, d := range l.Devices {
		cb(d)
	}
}

// Remove the given entry.
// Returns true if it was removed, false otherwise
func (l *binkyNetDeviceSet) Remove(entry model.BinkyNetDevice) bool {
	for idx, x := range l.Devices {
		if x == entry {
			l.Devices = append(l.Devices[:idx], l.Devices[idx+1:]...)
			l.OnModified()
			return true
		}
	}
	return false
}

// Is the given entry contained in this set?
func (l *binkyNetDeviceSet) Contains(entry model.BinkyNetDevice) bool {
	for _, x := range l.Devices {
		if x == entry {
			return true
		}
	}
	return false
}

// Add a new entry
func (l *binkyNetDeviceSet) AddNew() model.BinkyNetDevice {
	d := newBinkyNetDevice()
	d.SetContainer(l)
	d.SetDeviceID(api.DeviceID(fmt.Sprintf("newDevice%d", len(l.Devices)+1)))
	l.Devices = append(l.Devices, d)
	l.OnModified()
	return d
}

// OnModified triggers the modified function of the parent (if any)
func (l *binkyNetDeviceSet) OnModified() {
	if l.container != nil {
		l.container.OnModified()
	}
}
