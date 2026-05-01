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
	"fmt"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type routeRailPointList struct {
	routeRailPointListItems
	moduleEntityContainer
}

type routeRailPointListItems struct {
	RailPointIDs []string `xml:"RailPointID"`
}

var _ model.RouteRailPointList = &routeRailPointList{}

// UnmarshalXML unmarshals the list.
func (l *routeRailPointList) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&l.routeRailPointListItems, &start); err != nil {
		return err
	}
	return nil
}

// Get number of entries
func (l *routeRailPointList) GetCount() int {
	return len(l.RailPointIDs)
}

// Get a rail point by index
func (l *routeRailPointList) Get(index int) (model.RailPoint, bool) {
	if index < 0 || index >= len(l.RailPointIDs) {
		return nil, false
	}
	return l.GetModule().GetRailPoints().Get(l.RailPointIDs[index])
}

// Invoke the callback for each item
func (l *routeRailPointList) ForEach(cb func(model.RailPoint)) {
	rps := l.GetModule().GetRailPoints()
	for _, id := range l.RailPointIDs {
		if rp, ok := rps.Get(id); ok {
			cb(rp)
		}
	}
}

// Add a rail point to the end of this list.
func (l *routeRailPointList) Add(item model.RailPoint) error {
	if item.GetModule() != l.GetModule() {
		return fmt.Errorf("rail point belongs to different module")
	}
	l.RailPointIDs = append(l.RailPointIDs, item.GetID())
	l.OnModified()
	return nil
}

// Remove the given rail point from this list.
func (l *routeRailPointList) Remove(item model.RailPoint) bool {
	id := item.GetID()
	for i, x := range l.RailPointIDs {
		if x == id {
			l.RailPointIDs = append(l.RailPointIDs[:i], l.RailPointIDs[i+1:]...)
			l.OnModified()
			return true
		}
	}
	return false
}

// Remove the rail point at the given index from this list.
func (l *routeRailPointList) RemoveAt(index int) bool {
	if index < 0 || index >= len(l.RailPointIDs) {
		return false
	}
	l.RailPointIDs = append(l.RailPointIDs[:index], l.RailPointIDs[index+1:]...)
	l.OnModified()
	return true
}

// Does this list contain the given item?
func (l *routeRailPointList) Contains(item model.RailPoint) bool {
	id := item.GetID()
	for _, x := range l.RailPointIDs {
		if x == id {
			return true
		}
	}
	return false
}

// RemoveByID removes all references to the rail point with the given ID.
func (l *routeRailPointList) RemoveByID(id string) bool {
	changed := false
	for i := 0; i < len(l.RailPointIDs); {
		if l.RailPointIDs[i] == id {
			l.RailPointIDs = append(l.RailPointIDs[:i], l.RailPointIDs[i+1:]...)
			changed = true
		} else {
			i++
		}
	}
	if changed {
		l.OnModified()
	}
	return changed
}
