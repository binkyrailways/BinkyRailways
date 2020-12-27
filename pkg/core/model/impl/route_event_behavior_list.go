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

type routeEventBehaviorList struct {
	routeEventBehaviorListItems
	moduleEntityContainer
}

type routeEventBehaviorListItems struct {
	Items []*routeEventBehavior `xml:"RouteEventBehavior"`
}

var _ model.RouteEventBehaviorList = &routeEventBehaviorList{}

// UnmarshalXML unmarshals and connects the module.
func (rs *routeEventBehaviorList) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&rs.routeEventBehaviorListItems, &start); err != nil {
		return err
	}
	for _, x := range rs.Items {
		x.SetContainer(rs)
	}
	return nil
}

// Get number of entries
func (rs *routeEventBehaviorList) GetCount() int {
	return len(rs.Items)
}

// Invoke the callback for each item
func (rs *routeEventBehaviorList) ForEach(cb func(model.RouteEventBehavior)) {
	for _, x := range rs.Items {
		cb(x)
	}
}

// Remove the given item from this set.
// Returns true if it was removed, false otherwise
func (rs *routeEventBehaviorList) Remove(item model.RouteEventBehavior) bool {
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
func (rs *routeEventBehaviorList) Clear() bool {
	if rs.GetCount() == 0 {
		return false
	}
	rs.Items = nil
	return true
}

// Add a blank route behavior to the list
func (rs *routeEventBehaviorList) AddNew() model.RouteEventBehavior {
	reb := newRouteEventBehavior()
	reb.SetContainer(rs)
	rs.Items = append(rs.Items, reb)
	rs.OnModified()
	return reb
}

/// <summary>
/// Add the given item to this set
/// </summary>
//IRouteEventBehavior Add(ILocPredicate appliesTo);
