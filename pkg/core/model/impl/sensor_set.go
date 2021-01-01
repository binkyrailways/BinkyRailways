// Copyright 2020 Ewout Prangsma
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

type sensorSet struct {
	sensorSetItems
	moduleEntityContainer
}

type sensorSetItems struct {
	Items []*SensorContainer `xml:"Sensor"`
}

var _ model.SensorSet = &sensorSet{}

// UnmarshalXML unmarshals and connects the module.
func (ss *sensorSet) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&ss.sensorSetItems, &start); err != nil {
		return err
	}
	for _, x := range ss.Items {
		x.SetContainer(ss)
	}
	return nil
}

// Get number of entries
func (ss *sensorSet) GetCount() int {
	return len(ss.Items)
}

// Get an item by ID
func (ss *sensorSet) Get(id string) (model.Sensor, bool) {
	for _, x := range ss.Items {
		if x.GetID() == id {
			return x.Sensor, true
		}
	}
	return nil, false
}

// Invoke the callback for each item
func (ss *sensorSet) ForEach(cb func(model.Sensor)) {
	for _, x := range ss.Items {
		cb(x.Sensor)
	}
}

// Does this set contain an item with the given id?
func (ss *sensorSet) ContainsID(id string) bool {
	for _, x := range ss.Items {
		if x.GetID() == id {
			return true
		}
	}
	return false
}

// Remove the given item from this set.
// Returns true if it was removed, false otherwise
func (ss *sensorSet) Remove(item model.Sensor) bool {
	for i, x := range ss.Items {
		if x.Sensor == item {
			ss.Items = append(ss.Items[:i], ss.Items[i+1:]...)
			return true
		}
	}
	return false
}

// Does this set contain the given item?
func (ss *sensorSet) Contains(item model.Sensor) bool {
	for _, x := range ss.Items {
		if x.Sensor == item {
			return true
		}
	}
	return false
}

// Add a new binary sensor
func (ss *sensorSet) AddNewBinarySensor() model.BinarySensor {
	// Create new id
	for {
		id := NewID()
		if ss.ContainsID(id) {
			continue
		}
		bs := newBinarySensor()
		bs.SetID(id)
		bs.SetContainer(ss)
		ss.Items = append(ss.Items, &SensorContainer{Sensor: bs})
		ss.OnModified()
		return bs
	}
}
