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
	"context"
	"encoding/xml"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type junctionSet struct {
	junctionSetItems
	moduleEntityContainer
}

type junctionSetItems struct {
	Items []*JunctionContainer `xml:"Junction"`
}

var _ model.JunctionSet = &junctionSet{}

// UnmarshalXML unmarshals and connects the module.
func (js *junctionSet) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&js.junctionSetItems, &start); err != nil {
		return err
	}
	for _, x := range js.Items {
		x.SetContainer(js)
	}
	return nil
}

// ForEachAddress iterates all addresses in this entity and any child entities.
func (bs *junctionSet) ForEachAddress(cb func(addr model.Address, onUpdate func(context.Context, model.Address) error)) {
	for _, item := range bs.Items {
		item.Junction.ForEachAddress(cb)
	}
}

// Get number of entries
func (js *junctionSet) GetCount() int {
	return len(js.Items)
}

// Get an item by ID
func (js *junctionSet) Get(id string) (model.Junction, bool) {
	for _, x := range js.Items {
		if x.GetID() == id {
			return x.Junction, true
		}
	}
	return nil, false
}

// Invoke the callback for each item
func (js *junctionSet) ForEach(cb func(model.Junction)) {
	for _, x := range js.Items {
		cb(x.Junction)
	}
}

// Does this set contain an item with the given id?
func (js *junctionSet) ContainsID(id string) bool {
	for _, x := range js.Items {
		if x.GetID() == id {
			return true
		}
	}
	return false
}

// Remove the given item from this set.
// Returns true if it was removed, false otherwise
func (js *junctionSet) Remove(item model.Junction) bool {
	for i, x := range js.Items {
		if x.Junction == item {
			js.Items = append(js.Items[:i], js.Items[i+1:]...)
			return true
		}
	}
	return false
}

// Does this set contain the given item?
func (js *junctionSet) Contains(item model.Junction) bool {
	for _, x := range js.Items {
		if x.Junction == item {
			return true
		}
	}
	return false
}

// Add a new passive junction
func (js *junctionSet) AddPassiveJunction() model.PassiveJunction {
	// Create new id
	for {
		id := NewID()
		if js.ContainsID(id) {
			continue
		}
		sw := newPassiveJunction()
		sw.SetID(id)
		sw.SetContainer(js)
		js.Items = append(js.Items, &JunctionContainer{Junction: sw})
		js.OnModified()
		return sw
	}
}

// Add a new standard switch
func (js *junctionSet) AddSwitch() model.Switch {
	// Create new id
	for {
		id := NewID()
		if js.ContainsID(id) {
			continue
		}
		sw := newSwitch()
		sw.SetID(id)
		sw.SetContainer(js)
		js.Items = append(js.Items, &JunctionContainer{Junction: sw})
		js.OnModified()
		return sw
	}
}

// Add a new turn table
func (js *junctionSet) AddTurnTable() model.TurnTable {
	// Create new id
	for {
		id := NewID()
		if js.ContainsID(id) {
			continue
		}
		sw := newTurnTable()
		sw.SetID(id)
		sw.SetContainer(js)
		js.Items = append(js.Items, &JunctionContainer{Junction: sw})
		js.OnModified()
		return sw
	}
}
