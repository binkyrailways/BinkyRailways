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

type routeSet struct {
	routeSetItems
	moduleEntityContainer
}

type routeSetItems struct {
	Items []*route `xml:"Route"`
}

var _ model.RouteSet = &routeSet{}

// UnmarshalXML unmarshals and connects the module.
func (rs *routeSet) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&rs.routeSetItems, &start); err != nil {
		return err
	}
	for _, x := range rs.Items {
		x.SetContainer(rs)
	}
	return nil
}

// Get number of entries
func (rs *routeSet) GetCount() int {
	return len(rs.Items)
}

// Get an item by ID
func (rs *routeSet) Get(id string) (model.Route, bool) {
	for _, x := range rs.Items {
		if x.GetID() == id {
			return x, true
		}
	}
	return nil, false
}

// Invoke the callback for each item
func (rs *routeSet) ForEach(cb func(model.Route)) {
	for _, x := range rs.Items {
		cb(x)
	}
}

// Does this set contain an item with the given id?
func (rs *routeSet) ContainsID(id string) bool {
	for _, x := range rs.Items {
		if x.GetID() == id {
			return true
		}
	}
	return false
}

// Remove the given item from this set.
// Returns true if it was removed, false otherwise
func (rs *routeSet) Remove(item model.Route) bool {
	for i, x := range rs.Items {
		if x == item {
			rs.Items = append(rs.Items[:i], rs.Items[i+1:]...)
			return true
		}
	}
	return false
}

// Does this set contain the given item?
func (rs *routeSet) Contains(item model.Route) bool {
	for _, x := range rs.Items {
		if x == item {
			return true
		}
	}
	return false
}

// Add a new item to this set
func (rs *routeSet) AddNew() model.Route {
	// Create new id
	idx := 1
	for {
		id := fmt.Sprintf("route%d", idx)
		if rs.ContainsID(id) {
			idx++
			continue
		}
		r := newRoute()
		r.SetID(id)
		r.SetContainer(rs)
		rs.Items = append(rs.Items, r)
		rs.OnModified()
		return r
	}
}
