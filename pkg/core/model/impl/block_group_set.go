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

type blockGroupSet struct {
	blockGroupSetItems
	moduleEntityContainer
}

type blockGroupSetItems struct {
	Items []*blockGroup `xml:"BlockGroup"`
}

var _ model.BlockGroupSet = &blockGroupSet{}

// UnmarshalXML unmarshals and connects the module.
func (bs *blockGroupSet) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&bs.blockGroupSetItems, &start); err != nil {
		return err
	}
	for _, x := range bs.Items {
		x.SetContainer(bs)
	}
	return nil
}

// Get number of entries
func (bs *blockGroupSet) GetCount() int {
	return len(bs.Items)
}

// Get a block by ID
func (bs *blockGroupSet) Get(id string) (model.BlockGroup, bool) {
	for _, x := range bs.Items {
		if x.GetID() == id {
			return x, true
		}
	}
	return nil, false
}

// Invoke the callback for each item
func (bs *blockGroupSet) ForEach(cb func(model.BlockGroup)) {
	for _, x := range bs.Items {
		cb(x)
	}
}

// Does this set contain an item with the given id?
func (bs *blockGroupSet) ContainsID(id string) bool {
	for _, x := range bs.Items {
		if x.GetID() == id {
			return true
		}
	}
	return false
}

// Remove the given item from this set.
// Returns true if it was removed, false otherwise
func (bs *blockGroupSet) Remove(item model.BlockGroup) bool {
	for i, x := range bs.Items {
		if x == item {
			bs.Items = append(bs.Items[:i], bs.Items[i+1:]...)
			return true
		}
	}
	return false
}

// Does this set contain the given item?
func (bs *blockGroupSet) Contains(item model.BlockGroup) bool {
	for _, x := range bs.Items {
		if x == item {
			return true
		}
	}
	return false
}

// Add a new item to this set
func (bs *blockGroupSet) AddNew() model.BlockGroup {
	// Create new id
	for {
		id := NewID()
		if bs.ContainsID(id) {
			continue
		}
		bg := newBlockGroup()
		bg.SetID(id)
		bg.SetContainer(bs)
		bs.Items = append(bs.Items, bg)
		bs.OnModified()
		return bg
	}
}
