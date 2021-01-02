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

type locFunctions struct {
	locFunctionsItems
	railwayEntityContainer
}

type locFunctionsItems struct {
	Items []*locFunction `xml:"LocFunctionEntity"`
}

var _ model.LocFunctions = &locFunctions{}

// UnmarshalXML unmarshals and connects the module.
func (bs *locFunctions) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&bs.locFunctionsItems, &start); err != nil {
		return err
	}
	for _, x := range bs.Items {
		x.SetContainer(bs)
	}
	return nil
}

// Get number of entries
func (bs *locFunctions) GetCount() int {
	return len(bs.Items)
}

// Get an item by ID
func (bs *locFunctions) Get(id string) (model.LocFunction, bool) {
	for _, x := range bs.Items {
		if x.GetID() == id {
			return x, true
		}
	}
	return nil, false
}

// Invoke the callback for each item
func (bs *locFunctions) ForEach(cb func(model.LocFunction)) {
	for _, x := range bs.Items {
		cb(x)
	}
}

// Does this set contain an item with the given id?
func (bs *locFunctions) ContainsID(id string) bool {
	for _, x := range bs.Items {
		if x.GetID() == id {
			return true
		}
	}
	return false
}

// Remove the given item from this set.
// Returns true if it was removed, false otherwise
func (bs *locFunctions) Remove(item model.LocFunction) bool {
	for i, x := range bs.Items {
		if x == item {
			bs.Items = append(bs.Items[:i], bs.Items[i+1:]...)
			return true
		}
	}
	return false
}

// Does this set contain the given item?
func (bs *locFunctions) Contains(item model.LocFunction) bool {
	for _, x := range bs.Items {
		if x == item {
			return true
		}
	}
	return false
}

// Add a new item to this set
func (bs *locFunctions) Add(nr model.LocFunctionNumber) model.LocFunction {
	// Create new id
	for {
		id := NewID()
		if bs.ContainsID(id) {
			continue
		}
		lf := newLocFunction(nr)
		lf.SetID(id)
		lf.SetContainer(bs)
		bs.Items = append(bs.Items, lf)
		bs.OnModified()
		return lf
	}
}
