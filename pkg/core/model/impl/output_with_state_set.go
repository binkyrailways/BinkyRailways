// Copyright 2022 Ewout Prangsma
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

type outputWithStateSet struct {
	outputWithStateSetItems
	moduleEntityContainer
}

type outputWithStateSetItems struct {
	Items []*OutputWithStateContainer `xml:"OutputWithState"`
}

var _ model.OutputWithStateSet = &outputWithStateSet{}

// UnmarshalXML unmarshals and connects the module.
func (js *outputWithStateSet) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&js.outputWithStateSetItems, &start); err != nil {
		return err
	}
	for _, x := range js.Items {
		x.SetContainer(js)
	}
	return nil
}

// Get number of entries
func (os *outputWithStateSet) GetCount() int {
	return len(os.Items)
}

// Get an item by ID
func (os *outputWithStateSet) Get(id string) (model.OutputWithState, bool) {
	for _, x := range os.Items {
		if x.GetID() == id {
			return x.OutputWithState, true
		}
	}
	return nil, false
}

// Invoke the callback for each item
func (os *outputWithStateSet) ForEach(cb func(model.OutputWithState)) {
	for _, x := range os.Items {
		cb(x.OutputWithState)
	}
}

// Does this set contain an item with the given id?
func (js *outputWithStateSet) ContainsID(id string) bool {
	for _, x := range js.Items {
		if x.GetID() == id {
			return true
		}
	}
	return false
}

// Remove the given item from this set.
// Returns true if it was removed, false otherwise
func (os *outputWithStateSet) Remove(item model.Output) bool {
	for i, x := range os.Items {
		if x.OutputWithState.GetOutput() == item {
			os.Items = append(os.Items[:i], os.Items[i+1:]...)
			return true
		}
	}
	return false
}

// Remove all items.
// Returns true if one or more items were removed, false otherwise
func (os *outputWithStateSet) Clear() bool {
	if os.GetCount() == 0 {
		return false
	}
	os.Items = nil
	return true
}

// Does this set contain the given item?
func (os *outputWithStateSet) Contains(item model.Output) bool {
	for _, x := range os.Items {
		if x.OutputWithState.GetOutput() == item {
			return true
		}
	}
	return false
}

// Copy all my entries to the given destination
func (os *outputWithStateSet) CopyTo(destination model.OutputWithStateSet) error {
	dt, ok := destination.(*outputWithStateSet)
	if !ok {
		return fmt.Errorf("Invalid destination type")
	}
	for _, x := range os.Items {
		clone := x.Clone().(OutputWithState)
		clone.SetContainer(dt)
		dt.Items = append(dt.Items, &OutputWithStateContainer{clone})
	}
	return nil
}

// Add the given item to this set
func (os *outputWithStateSet) AddBinaryOutput(item model.BinaryOutput, active bool) error {
	bows := newBinaryOutputWithState()
	bows.OutputID = OutputRef(item.GetID())
	bows.Active = refs.NewBool(active)
	bows.SetContainer(os)
	os.Items = append(os.Items, &OutputWithStateContainer{OutputWithState: bows})
	os.OnModified()
	return nil
}
