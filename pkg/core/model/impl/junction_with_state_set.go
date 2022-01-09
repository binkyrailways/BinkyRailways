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
	"github.com/binkyrailways/BinkyRailways/pkg/core/model/refs"
)

type junctionWithStateSet struct {
	junctionWithStateSetItems
	moduleEntityContainer
}

type junctionWithStateSetItems struct {
	Items []*JunctionWithStateContainer `xml:"JunctionWithState"`
}

var _ model.JunctionWithStateSet = &junctionWithStateSet{}

// UnmarshalXML unmarshals and connects the module.
func (js *junctionWithStateSet) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&js.junctionWithStateSetItems, &start); err != nil {
		return err
	}
	for _, x := range js.Items {
		x.SetContainer(js)
	}
	return nil
}

// Get number of entries
func (js *junctionWithStateSet) GetCount() int {
	return len(js.Items)
}

// Get an item by ID
func (js *junctionWithStateSet) Get(id string) (model.JunctionWithState, bool) {
	for _, x := range js.Items {
		if x.GetID() == id {
			return x.JunctionWithState, true
		}
	}
	return nil, false
}

// Invoke the callback for each item
func (js *junctionWithStateSet) ForEach(cb func(model.JunctionWithState)) {
	for _, x := range js.Items {
		cb(x.JunctionWithState)
	}
}

// Does this set contain an item with the given id?
func (js *junctionWithStateSet) ContainsID(id string) bool {
	for _, x := range js.Items {
		if x.GetID() == id {
			return true
		}
	}
	return false
}

// Remove the given item from this set.
// Returns true if it was removed, false otherwise
func (js *junctionWithStateSet) Remove(item model.Junction) bool {
	for i, x := range js.Items {
		if x.JunctionWithState.GetJunction() == item {
			js.Items = append(js.Items[:i], js.Items[i+1:]...)
			return true
		}
	}
	return false
}

// Remove all items.
// Returns true if one or more items were removed, false otherwise
func (js *junctionWithStateSet) Clear() bool {
	if js.GetCount() == 0 {
		return false
	}
	js.Items = nil
	return true
}

// Does this set contain the given item?
func (js *junctionWithStateSet) Contains(item model.Junction) bool {
	for _, x := range js.Items {
		if x.JunctionWithState.GetJunction() == item {
			return true
		}
	}
	return false
}

// Copy all my entries to the given destination
func (js *junctionWithStateSet) CopyTo(destination model.JunctionWithStateSet) error {
	dt, ok := destination.(*junctionWithStateSet)
	if !ok {
		return fmt.Errorf("Invalid destination type")
	}
	for _, x := range js.Items {
		clone := x.Clone().(JunctionWithState)
		clone.SetContainer(dt)
		dt.Items = append(dt.Items, &JunctionWithStateContainer{clone})
	}
	return nil
}

/// <summary>
/// Add the given item to this set
/// </summary>
//void Add(IPassiveJunction item);

/// <summary>
/// Add the given item to this set
/// </summary>
func (js *junctionWithStateSet) AddSwitch(item model.Switch, direction model.SwitchDirection) error {
	sws := newSwitchWithState()
	sws.JunctionID = JunctionRef(item.GetID())
	sws.Direction = refs.NewSwitchDirection(direction)
	sws.SetContainer(js)
	js.Items = append(js.Items, &JunctionWithStateContainer{JunctionWithState: sws})
	js.OnModified()
	return nil
}

/// <summary>
/// Add the given item to this set
/// </summary>
//void Add(ITurnTable item, int position);
