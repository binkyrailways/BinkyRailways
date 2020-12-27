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

type signalSet struct {
	signalSetItems
	moduleEntityContainer
}

type signalSetItems struct {
	Items []*SignalContainer `xml:"Signal"`
}

var _ model.SignalSet = &signalSet{}

// UnmarshalXML unmarshals and connects the module.
func (ss *signalSet) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&ss.signalSetItems, &start); err != nil {
		return err
	}
	for _, x := range ss.Items {
		x.SetContainer(ss)
	}
	return nil
}

// Get number of entries
func (ss *signalSet) GetCount() int {
	return len(ss.Items)
}

// Get an item by ID
func (ss *signalSet) Get(id string) (model.Signal, bool) {
	for _, x := range ss.Items {
		if x.GetID() == id {
			return x.Signal, true
		}
	}
	return nil, false
}

// Invoke the callback for each item
func (ss *signalSet) ForEach(cb func(model.Signal)) {
	for _, x := range ss.Items {
		cb(x.Signal)
	}
}

// Does this set contain an item with the given id?
func (ss *signalSet) ContainsID(id string) bool {
	for _, x := range ss.Items {
		if x.GetID() == id {
			return true
		}
	}
	return false
}

// Remove the given item from this set.
// Returns true if it was removed, false otherwise
func (ss *signalSet) Remove(item model.Signal) bool {
	for i, x := range ss.Items {
		if x.Signal == item {
			ss.Items = append(ss.Items[:i], ss.Items[i+1:]...)
			return true
		}
	}
	return false
}

// Does this set contain the given item?
func (ss *signalSet) Contains(item model.Signal) bool {
	for _, x := range ss.Items {
		if x.Signal == item {
			return true
		}
	}
	return false
}

// Add a new block signal
func (ss *signalSet) AddNewBlockSignal() model.BlockSignal {
	// Create new id
	idx := 1
	for {
		id := fmt.Sprintf("binaryOutput%d", idx)
		if ss.ContainsID(id) {
			idx++
			continue
		}
		bs := newBlockSignal()
		bs.SetID(id)
		bs.SetContainer(ss)
		ss.Items = append(ss.Items, &SignalContainer{Signal: bs})
		ss.OnModified()
		return bs
	}
}
