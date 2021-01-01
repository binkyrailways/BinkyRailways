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

type edgeSet struct {
	edgeSetItems
	moduleEntityContainer
}

type edgeSetItems struct {
	Items []*edge `xml:"Edge"`
}

var _ model.EdgeSet = &edgeSet{}

// UnmarshalXML unmarshals and connects the module.
func (bs *edgeSet) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&bs.edgeSetItems, &start); err != nil {
		return err
	}
	for _, x := range bs.Items {
		x.SetContainer(bs)
	}
	return nil
}

// Get number of entries
func (bs *edgeSet) GetCount() int {
	return len(bs.Items)
}

// Get a block by ID
func (bs *edgeSet) Get(id string) (model.Edge, bool) {
	for _, x := range bs.Items {
		if x.GetID() == id {
			return x, true
		}
	}
	return nil, false
}

// Invoke the callback for each item
func (bs *edgeSet) ForEach(cb func(model.Edge)) {
	for _, x := range bs.Items {
		cb(x)
	}
}

// Does this set contain an item with the given id?
func (bs *edgeSet) ContainsID(id string) bool {
	for _, x := range bs.Items {
		if x.GetID() == id {
			return true
		}
	}
	return false
}

// Remove the given item from this set.
// Returns true if it was removed, false otherwise
func (bs *edgeSet) Remove(item model.Edge) bool {
	for i, x := range bs.Items {
		if x == item {
			bs.Items = append(bs.Items[:i], bs.Items[i+1:]...)
			return true
		}
	}
	return false
}

// Does this set contain the given item?
func (bs *edgeSet) Contains(item model.Edge) bool {
	for _, x := range bs.Items {
		if x == item {
			return true
		}
	}
	return false
}

// Add a new item to this set
func (bs *edgeSet) AddNew() model.Edge {
	// Create new id
	for {
		id := NewID()
		if bs.ContainsID(id) {
			continue
		}
		b := newEdge()
		b.SetID(id)
		b.SetContainer(bs)
		bs.Items = append(bs.Items, b)
		bs.OnModified()
		return b
	}
}
