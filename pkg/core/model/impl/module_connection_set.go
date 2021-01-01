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

type moduleConnectionSet struct {
	moduleConnectionSetItems
	railwayEntityContainer
}

type moduleConnectionSetItems struct {
	Items []*moduleConnection `xml:"ModuleConnection"`
}

var _ model.ModuleConnectionSet = &moduleConnectionSet{}

// UnmarshalXML unmarshals and connects the module.
func (bs *moduleConnectionSet) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&bs.moduleConnectionSetItems, &start); err != nil {
		return err
	}
	for _, x := range bs.Items {
		x.SetContainer(bs)
	}
	return nil
}

// Get number of entries
func (bs *moduleConnectionSet) GetCount() int {
	return len(bs.Items)
}

// Get an item by ID
func (bs *moduleConnectionSet) Get(id string) (model.ModuleConnection, bool) {
	for _, x := range bs.Items {
		if x.GetID() == id {
			return x, true
		}
	}
	return nil, false
}

// Invoke the callback for each item
func (bs *moduleConnectionSet) ForEach(cb func(model.ModuleConnection)) {
	for _, x := range bs.Items {
		cb(x)
	}
}

// Does this set contain an item with the given id?
func (bs *moduleConnectionSet) ContainsID(id string) bool {
	for _, x := range bs.Items {
		if x.GetID() == id {
			return true
		}
	}
	return false
}

// Remove the given item from this set.
// Returns true if it was removed, false otherwise
func (bs *moduleConnectionSet) Remove(item model.ModuleConnection) bool {
	for i, x := range bs.Items {
		if x == item {
			bs.Items = append(bs.Items[:i], bs.Items[i+1:]...)
			return true
		}
	}
	return false
}

// Does this set contain the given item?
func (bs *moduleConnectionSet) Contains(item model.ModuleConnection) bool {
	for _, x := range bs.Items {
		if x == item {
			return true
		}
	}
	return false
}

// Add a new item to this set
func (bs *moduleConnectionSet) AddNew() model.ModuleConnection {
	// Create new id
	for {
		id := NewID()
		if bs.ContainsID(id) {
			continue
		}
		b := newModuleConnection()
		b.SetID(id)
		b.SetContainer(bs)
		bs.Items = append(bs.Items, b)
		bs.OnModified()
		return b
	}
}
