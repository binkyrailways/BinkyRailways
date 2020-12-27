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

type outputSet struct {
	outputSetItems
	moduleEntityContainer
}

type outputSetItems struct {
	Items []*OutputContainer `xml:"Output"`
}

var _ model.OutputSet = &outputSet{}

// UnmarshalXML unmarshals and connects the module.
func (os *outputSet) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&os.outputSetItems, &start); err != nil {
		return err
	}
	for _, x := range os.Items {
		x.SetContainer(os)
	}
	return nil
}

// Get number of entries
func (os *outputSet) GetCount() int {
	return len(os.Items)
}

// Get an item by ID
func (os *outputSet) Get(id string) (model.Output, bool) {
	for _, x := range os.Items {
		if x.GetID() == id {
			return x.Output, true
		}
	}
	return nil, false
}

// Invoke the callback for each item
func (os *outputSet) ForEach(cb func(model.Output)) {
	for _, x := range os.Items {
		cb(x.Output)
	}
}

// Does this set contain an item with the given id?
func (os *outputSet) ContainsID(id string) bool {
	for _, x := range os.Items {
		if x.GetID() == id {
			return true
		}
	}
	return false
}

// Remove the given item from this set.
// Returns true if it was removed, false otherwise
func (os *outputSet) Remove(item model.Output) bool {
	for i, x := range os.Items {
		if x.Output == item {
			os.Items = append(os.Items[:i], os.Items[i+1:]...)
			return true
		}
	}
	return false
}

// Does this set contain the given item?
func (os *outputSet) Contains(item model.Output) bool {
	for _, x := range os.Items {
		if x.Output == item {
			return true
		}
	}
	return false
}

// Add a new binary output
func (os *outputSet) AddNewBinaryOutput() model.BinaryOutput {
	// Create new id
	idx := 1
	for {
		id := fmt.Sprintf("binaryOutput%d", idx)
		if os.ContainsID(id) {
			idx++
			continue
		}
		bo := newBinaryOutput()
		bo.SetID(id)
		bo.SetContainer(os)
		os.Items = append(os.Items, &OutputContainer{Output: bo})
		os.OnModified()
		return bo
	}
}

// Add a new 4-stage clock output
func (os *outputSet) AddNewClock4StageOutput() model.Clock4StageOutput {
	// Create new id
	idx := 1
	for {
		id := fmt.Sprintf("clock4StageOutput%d", idx)
		if os.ContainsID(id) {
			idx++
			continue
		}
		cso := newClock4StageOutput()
		cso.SetID(id)
		cso.SetContainer(os)
		os.Items = append(os.Items, &OutputContainer{Output: cso})
		os.OnModified()
		return cso
	}
}
