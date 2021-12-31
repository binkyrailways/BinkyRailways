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

type railwayCommandStationRefSet struct {
	railwayCommandStationRefSetItems
	railwayEntityContainer
}

type railwayCommandStationRefSetItems struct {
	Items []*commandStationRef `xml:"CommandStationRef"`
}

var _ CommandStationRefSet = &railwayCommandStationRefSet{}

// UnmarshalXML unmarshals and connects the module.
func (bs *railwayCommandStationRefSet) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&bs.railwayCommandStationRefSetItems, &start); err != nil {
		return err
	}
	for _, x := range bs.Items {
		x.SetResolver(bs.tryResolve)
	}
	return nil
}

// Get number of entries
func (bs *railwayCommandStationRefSet) GetCount() int {
	return len(bs.Items)
}

// Get an item by ID
func (bs *railwayCommandStationRefSet) Get(id string) (model.CommandStationRef, bool) {
	for _, x := range bs.Items {
		if x.GetID() == id {
			return x, true
		}
	}
	return nil, false
}

// Invoke the callback for each item
func (bs *railwayCommandStationRefSet) ForEach(cb func(model.CommandStationRef)) {
	for _, x := range bs.Items {
		cb(x)
	}
}

// Does this set contain an item with the given id?
func (bs *railwayCommandStationRefSet) ContainsID(id string) bool {
	for _, x := range bs.Items {
		if x.GetID() == id {
			return true
		}
	}
	return false
}

// Remove the given item from this set.
// Returns true if it was removed, false otherwise
func (bs *railwayCommandStationRefSet) Remove(item model.CommandStationRef) bool {
	for i, x := range bs.Items {
		if x == item {
			bs.Items = append(bs.Items[:i], bs.Items[i+1:]...)
			return true
		}
	}
	return false
}

// Does this set contain the given item?
func (bs *railwayCommandStationRefSet) Contains(item model.CommandStationRef) bool {
	for _, x := range bs.Items {
		if x == item {
			return true
		}
	}
	return false
}

// Add a reference to the given entity
func (bs *railwayCommandStationRefSet) Add(item model.CommandStation) model.CommandStationRef {
	for _, x := range bs.Items {
		if x.GetID() == item.GetID() {
			return x
		}
	}
	b := newCommandStationRef(item.GetID(), bs.tryResolve)
	bs.Items = append(bs.Items, &b)
	bs.OnModified()
	return &b
}

// Add a reference to the given entity
func (bs *railwayCommandStationRefSet) AddRef(item model.CommandStationRef) {
	for _, x := range bs.Items {
		if x.GetID() == item.GetID() {
			return
		}
	}
	b := newCommandStationRef(item.GetID(), bs.tryResolve)
	bs.Items = append(bs.Items, &b)
	bs.OnModified()
}

// Copy all entries into the given destination.
func (bs *railwayCommandStationRefSet) CopyTo(destination model.CommandStationRefSet) {
	dst := destination.(CommandStationRefSet)
	bs.ForEach(func(item model.CommandStationRef) {
		dst.AddRef(item)
	})
}

// Try to resolve the given id into a CommandStation.
func (bs *railwayCommandStationRefSet) tryResolve(id string) (model.CommandStation, error) {
	rw, ok := bs.GetRailway().(Railway)
	if !ok {
		return nil, fmt.Errorf("railway is not of type Railway")
	}
	if rw == nil {
		return nil, fmt.Errorf("railway is nil")
	}
	pkg := rw.GetPackage()
	if pkg == nil {
		return nil, fmt.Errorf("package is nil")
	}
	return pkg.GetCommandStation(id)
}
