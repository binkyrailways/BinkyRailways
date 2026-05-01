// Copyright 2024 Ewout Prangsma
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

type railPointSet struct {
	railPointSetItems
	moduleEntityContainer
}

type railPointSetItems struct {
	Items []*railPoint `xml:"RailPoint"`
}

var _ model.RailPointSet = &railPointSet{}

// UnmarshalXML unmarshals and connects the module.
func (rps *railPointSet) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&rps.railPointSetItems, &start); err != nil {
		return err
	}
	for _, x := range rps.Items {
		x.SetContainer(rps)
	}
	return nil
}

// Get number of entries
func (rps *railPointSet) GetCount() int {
	return len(rps.Items)
}

// Get a rail point by ID
func (rps *railPointSet) Get(id string) (model.RailPoint, bool) {
	for _, x := range rps.Items {
		if x.GetID() == id {
			return x, true
		}
	}
	return nil, false
}

// Invoke the callback for each item
func (rps *railPointSet) ForEach(cb func(model.RailPoint)) {
	for _, x := range rps.Items {
		cb(x)
	}
}

// Does this set contain an item with the given id?
func (rps *railPointSet) ContainsID(id string) bool {
	for _, x := range rps.Items {
		if x.GetID() == id {
			return true
		}
	}
	return false
}

// Remove the given item from this set.
// Returns true if it was removed, false otherwise
func (rps *railPointSet) Remove(item model.RailPoint) bool {
	for i, x := range rps.Items {
		if x == item {
			rps.Items = append(rps.Items[:i], rps.Items[i+1:]...)
			rps.OnModified()
			return true
		}
	}
	return false
}

// Does this set contain the given item?
func (rps *railPointSet) Contains(item model.RailPoint) bool {
	for _, x := range rps.Items {
		if x == item {
			return true
		}
	}
	return false
}

// Add a new item to this set
func (rps *railPointSet) AddNew() model.RailPoint {
	// Find minimum size of existing rail points
	minWidth := 0
	minHeight := 0
	for idx, item := range rps.Items {
		w := item.GetWidth()
		h := item.GetHeight()
		if idx == 0 {
			minWidth = w
			minHeight = h
		} else {
			minWidth = min(minWidth, w)
			minHeight = min(minHeight, h)
		}
	}

	// Create new id
	for {
		id := NewID()
		if rps.ContainsID(id) {
			continue
		}
		rp := newRailPoint()
		if minWidth > 0 {
			rp.SetWidth(minWidth)
		}
		if minHeight > 0 {
			rp.SetHeight(minHeight)
		}
		rp.SetID(id)
		rp.SetContainer(rps)
		rps.Items = append(rps.Items, rp)
		rps.OnModified()
		return rp
	}
}
