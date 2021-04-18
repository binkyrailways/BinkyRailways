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

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type binkyNetConnectionPinList struct {
	container *binkyNetConnection
	binkyNetConnectionPinListItems
}

type binkyNetConnectionPinListItems struct {
	Pins []*binkyNetDevicePin `xml:"Pins,omitempty"`
}

var _ model.BinkyNetConnectionPinList = &binkyNetConnectionPinList{}

// UnmarshalXML unmarshals and connects the module.
func (l *binkyNetConnectionPinList) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&l.binkyNetConnectionPinListItems, &start); err != nil {
		return err
	}
	for _, x := range l.Pins {
		x.SetContainer(l)
	}
	return nil
}

// Gets the containing local worker
func (l *binkyNetConnectionPinList) GetLocalWorker() model.BinkyNetLocalWorker {
	if l.container != nil {
		return l.container.GetLocalWorker()
	}
	return nil
}

// Gets the connection that contains this pin
func (l *binkyNetConnectionPinList) GetConnection() model.BinkyNetConnection {
	return l.container
}

// SetContainer links this instance to its container
func (l *binkyNetConnectionPinList) SetContainer(container *binkyNetConnection) {
	l.container = container
}

// Get number of entries
func (l *binkyNetConnectionPinList) GetCount() int {
	return len(l.Pins)
}

// Get a pin by index
func (l *binkyNetConnectionPinList) Get(index int) (model.BinkyNetDevicePin, bool) {
	if index >= 0 && index < len(l.Pins) {
		return l.Pins[index], true
	}
	return nil, false
}

// Invoke the callback for each pin
func (l *binkyNetConnectionPinList) ForEach(cb func(model.BinkyNetDevicePin)) {
	for _, p := range l.Pins {
		cb(p)
	}
}

// Remove the item at given index.
// Returns true if it was removed, false otherwise
func (l *binkyNetConnectionPinList) Remove(index int) bool {
	if index >= 0 && index < len(l.Pins) {
		l.Pins = append(l.Pins[:index], l.Pins[index+1:]...)
		l.OnModified()
		return true
	}
	return false
}

// Add a new pin
func (l *binkyNetConnectionPinList) AddNew() model.BinkyNetDevicePin {
	p := &binkyNetDevicePin{}
	p.SetContainer(l)
	l.Pins = append(l.Pins, p)
	return p
}

// OnModified calls the parents modified callback
func (l *binkyNetConnectionPinList) OnModified() {
	if l.container != nil {
		l.container.OnModified()
	}
}
