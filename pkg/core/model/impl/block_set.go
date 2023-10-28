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

type blockSet struct {
	blockSetItems
	moduleEntityContainer
}

type blockSetItems struct {
	Items []*block `xml:"Block"`
}

var _ model.BlockSet = &blockSet{}

// UnmarshalXML unmarshals and connects the module.
func (bs *blockSet) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&bs.blockSetItems, &start); err != nil {
		return err
	}
	for _, x := range bs.Items {
		x.SetContainer(bs)
	}
	return nil
}

// ForEachAddress iterates all addresses in this entity and any child entities.
func (bs *blockSet) ForEachAddress(cb func(addr model.Address, onUpdate func(context.Context, model.Address) error)) {
	for _, item := range bs.Items {
		item.ForEachAddress(cb)
	}
}

// Get number of entries
func (bs *blockSet) GetCount() int {
	return len(bs.Items)
}

// Get a block by ID
func (bs *blockSet) Get(id string) (model.Block, bool) {
	for _, x := range bs.Items {
		if x.GetID() == id {
			return x, true
		}
	}
	return nil, false
}

// Invoke the callback for each item
func (bs *blockSet) ForEach(cb func(model.Block)) {
	for _, x := range bs.Items {
		cb(x)
	}
}

// Does this set contain an item with the given id?
func (bs *blockSet) ContainsID(id string) bool {
	for _, x := range bs.Items {
		if x.GetID() == id {
			return true
		}
	}
	return false
}

// Remove the given item from this set.
// Returns true if it was removed, false otherwise
func (bs *blockSet) Remove(item model.Block) bool {
	for i, x := range bs.Items {
		if x == item {
			bs.Items = append(bs.Items[:i], bs.Items[i+1:]...)
			return true
		}
	}
	return false
}

// Does this set contain the given item?
func (bs *blockSet) Contains(item model.Block) bool {
	for _, x := range bs.Items {
		if x == item {
			return true
		}
	}
	return false
}

// Add a new item to this set
func (bs *blockSet) AddNew() model.Block {
	// Create new id
	for {
		id := NewID()
		if bs.ContainsID(id) {
			continue
		}
		b := newBlock()
		b.SetID(id)
		b.SetContainer(bs)
		bs.Items = append(bs.Items, b)
		bs.OnModified()
		return b
	}
}
