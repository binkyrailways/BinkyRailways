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

// ActionTriggerContainer is implemented by containers of action triggers.
type ActionTriggerContainer interface {
	// Return the containing module
	GetModule() model.Module
	// Invoke when anything has changed
	OnModified()
}

// ActionTrigger extends implementation methods to model.ActionTrigger
type ActionTrigger interface {
	model.ActionTrigger

	// Return the containing module
	GetModule() model.Module
	// Invoke when anything has changed
	OnModified()
}

type actionTrigger struct {
	actionTriggerItems
	description string
	container   ActionTriggerContainer
}

type actionTriggerItems struct {
	Items []*ActionContainer `xml:"Action"`
}

var _ ActionTrigger = &actionTrigger{}

// Initialize this trigger
func (bs *actionTrigger) Initialize(container ActionTriggerContainer, description string) {
	bs.container = container
	bs.description = description
}

// UnmarshalXML unmarshals and connects the module.
func (bs *actionTrigger) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&bs.actionTriggerItems, &start); err != nil {
		return err
	}
	for _, x := range bs.Items {
		x.Action.SetContainer(bs)
	}
	return nil
}

func (bs *actionTrigger) GetName() string {
	return bs.description
}

// Get number of entries
func (bs *actionTrigger) GetCount() int {
	return len(bs.Items)
}

// Get a block by ID
func (bs *actionTrigger) Get(index int) (model.Action, bool) {
	if index < 0 || index >= len(bs.Items) {
		return nil, false
	}
	return bs.Items[index].Action, true
}

// Invoke the callback for each item
func (bs *actionTrigger) ForEach(cb func(model.Action)) {
	for _, x := range bs.Items {
		cb(x.Action)
	}
}

// Does this set contain an item with the given id?
func (bs *actionTrigger) ContainsID(id string) bool {
	for _, x := range bs.Items {
		if x.Action.GetID() == id {
			return true
		}
	}
	return false
}

// Remove the given item from this set.
// Returns true if it was removed, false otherwise
func (bs *actionTrigger) Remove(item model.Action) bool {
	for i, x := range bs.Items {
		if x.Action == item {
			bs.Items = append(bs.Items[:i], bs.Items[i+1:]...)
			return true
		}
	}
	return false
}

// Remove the item at the given index from this list.
// Returns true if it was removed, false otherwise
func (bs *actionTrigger) RemoveAt(index int) bool {
	if index < 0 || index >= len(bs.Items) {
		return false
	}
	bs.Items = append(bs.Items[:index], bs.Items[index+1:]...)
	return true
}

// Does this set contain the given item?
func (bs *actionTrigger) Contains(item model.Action) bool {
	for _, x := range bs.Items {
		if x.Action == item {
			return true
		}
	}
	return false
}

// Add a new item to this list
func (bs *actionTrigger) Add(value model.Action) {
	// Create new id
	action := value.(Action)
	if action.GetID() == "" {
		action.SetID(NewID())
	}
	if bs.ContainsID(action.GetID()) {
		return
	}
	action.SetContainer(bs)
	bs.Items = append(bs.Items, &ActionContainer{Action: action})
	bs.OnModified()
}

// GetContainer returns the container reference of this action.
func (bs *actionTrigger) GetModule() model.Module {
	if bs.container != nil {
		return bs.container.GetModule()
	}
	return nil
}

// Invoke when anything has changed
func (bs *actionTrigger) OnModified() {
	if bs.container != nil {
		bs.container.OnModified()
	}
}
