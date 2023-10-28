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
	"fmt"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type groupLocRefSet struct {
	groupLocSetItems
	railwayEntityContainer
}

type groupLocSetItems struct {
	Items []*groupLocRef `xml:"string"`
}

var _ LocRefSet = &groupLocRefSet{}

// UnmarshalXML unmarshals and connects the module.
func (bs *groupLocRefSet) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&bs.groupLocSetItems, &start); err != nil {
		return err
	}
	for _, x := range bs.Items {
		x.SetResolver(bs.tryResolve)
	}
	return nil
}

// Get number of entries
func (bs *groupLocRefSet) GetCount() int {
	return len(bs.Items)
}

// Get an item by ID
func (bs *groupLocRefSet) Get(id string) (model.LocRef, bool) {
	for _, x := range bs.Items {
		if x.GetID() == id {
			return x, true
		}
	}
	return nil, false
}

// Invoke the callback for each item
func (bs *groupLocRefSet) ForEach(cb func(model.LocRef)) {
	for _, x := range bs.Items {
		cb(x)
	}
}

// Does this set contain an item with the given id?
func (bs *groupLocRefSet) ContainsID(id string) bool {
	for _, x := range bs.Items {
		if x.GetID() == id {
			return true
		}
	}
	return false
}

// Remove the given item from this set.
// Returns true if it was removed, false otherwise
func (bs *groupLocRefSet) Remove(item model.LocRef) bool {
	for i, x := range bs.Items {
		if x == item {
			bs.Items = append(bs.Items[:i], bs.Items[i+1:]...)
			return true
		}
	}
	return false
}

// Does this set contain the given item?
func (bs *groupLocRefSet) Contains(item model.LocRef) bool {
	for _, x := range bs.Items {
		if x == item {
			return true
		}
	}
	return false
}

// Add a reference to the given entity
func (bs *groupLocRefSet) Add(item model.Loc) model.LocRef {
	for _, x := range bs.Items {
		if x.GetID() == item.GetID() {
			return x
		}
	}
	b := newGroupLocRef(item.GetID(), bs.tryResolve)
	bs.Items = append(bs.Items, &b)
	bs.OnModified()
	return &b
}

// Add a reference to the given entity
func (bs *groupLocRefSet) AddRef(item model.LocRef) {
	for _, x := range bs.Items {
		if x.GetID() == item.GetID() {
			return
		}
	}
	b := newGroupLocRef(item.GetID(), bs.tryResolve)
	bs.Items = append(bs.Items, &b)
	bs.OnModified()
}

// Copy all entries into the given destination.
func (bs *groupLocRefSet) CopyTo(destination model.LocRefSet) {
	dst := destination.(LocRefSet)
	bs.ForEach(func(item model.LocRef) {
		dst.AddRef(item)
	})
}

// Try to resolve the given id into a loc.
func (bs *groupLocRefSet) tryResolve(id string) (model.Loc, error) {
	if id == "" {
		return nil, nil
	}
	rw := bs.GetRailway()
	if rw == nil {
		return nil, fmt.Errorf("railway is nil")
	}
	if lr, ok := rw.GetLocs().Get(id); ok {
		return lr.TryResolve()
	}
	return nil, fmt.Errorf("Loc '%s' not found", id)
}
