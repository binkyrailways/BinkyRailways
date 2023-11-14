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
	"slices"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type routeEventSet struct {
	routeEventSetItems
	moduleEntityContainer
}

type routeEventSetItems struct {
	Items []*routeEvent `xml:"RouteEvent"`
}

var _ model.RouteEventSet = &routeEventSet{}

// UnmarshalXML unmarshals and connects the module.
func (rs *routeEventSet) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&rs.routeEventSetItems, &start); err != nil {
		return err
	}
	for _, x := range rs.Items {
		x.SetContainer(rs)
	}
	return nil
}

// Get number of entries
func (rs *routeEventSet) GetCount() int {
	return len(rs.Items)
}

// Get event in this set by ID of the sensor
func (rs *routeEventSet) Get(sensorID string) (model.RouteEvent, bool) {
	for _, x := range rs.Items {
		if x.GetSensor().GetID() == sensorID {
			return x, true
		}
	}
	return nil, false
}

// Invoke the callback for each item
func (rs *routeEventSet) ForEach(cb func(model.RouteEvent)) {
	for _, x := range rs.Items {
		cb(x)
	}
}

// Move the given item up by 1 entry.
// Returns true if it was moved, false otherwise
func (rs *routeEventSet) MoveUp(item model.RouteEvent) bool {
	index := slices.IndexFunc(rs.Items, func(re *routeEvent) bool {
		return re.GetSensor().GetID() == item.GetSensor().GetID()
	})
	if index < 0 {
		// Not found
		return false
	}
	if index == 0 {
		// Already at top
		return false
	}
	// Swap places
	prev, x := rs.Items[index-1], rs.Items[index]
	rs.Items[index-1] = x
	rs.Items[index] = prev
	return true
}

// Move the given item down by 1 entry.
// Returns true if it was moved, false otherwise
func (rs *routeEventSet) MoveDown(item model.RouteEvent) bool {
	index := slices.IndexFunc(rs.Items, func(re *routeEvent) bool {
		return re.GetSensor().GetID() == item.GetSensor().GetID()
	})
	if index < 0 {
		// Not found
		return false
	}
	if index >= len(rs.Items)-1 {
		// Already at bottom
		return false
	}
	// Swap places
	next, x := rs.Items[index+1], rs.Items[index]
	rs.Items[index+1] = x
	rs.Items[index] = next
	return true
}

// Remove the given item from this set.
// Returns true if it was removed, false otherwise
func (rs *routeEventSet) Remove(item model.RouteEvent) bool {
	for i, x := range rs.Items {
		if x == item {
			rs.Items = append(rs.Items[:i], rs.Items[i+1:]...)
			return true
		}
	}
	return false
}

// Remove all items.
// Returns true if one or more items were removed, false otherwise
func (rs *routeEventSet) Clear() bool {
	if rs.GetCount() == 0 {
		return false
	}
	rs.Items = nil
	return true
}

// Add a the given item to this list
func (rs *routeEventSet) Add(sensor model.Sensor) (model.RouteEvent, error) {
	rev := newRouteEvent(sensor)
	rev.SetContainer(rs)
	rs.Items = append(rs.Items, rev)
	rs.OnModified()
	return rev, nil
}
