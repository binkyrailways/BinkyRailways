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

type locPredicateSet struct {
	locPredicateSetItems
	moduleEntityContainer
}

type locPredicateSetItems struct {
	Items []*LocPredicateContainer `xml:"LocPredicate"`
}

var _ model.LocPredicateSet = &locPredicateSet{}

// UnmarshalXML unmarshals and connects the module.
func (lps *locPredicateSet) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&lps.locPredicateSetItems, &start); err != nil {
		return err
	}
	for _, x := range lps.Items {
		x.SetContainer(lps)
	}
	return nil
}

// Get number of entries
func (lps *locPredicateSet) GetCount() int {
	return len(lps.Items)
}

// Get an item by ID
func (lps *locPredicateSet) Get(id string) (model.LocPredicate, bool) {
	for _, x := range lps.Items {
		if x.GetID() == id {
			return x.LocPredicate, true
		}
	}
	return nil, false
}

// Invoke the callback for each item
func (lps *locPredicateSet) ForEach(cb func(model.LocPredicate)) {
	for _, x := range lps.Items {
		cb(x.LocPredicate)
	}
}

// Does this set contain an item with the given id?
func (lps *locPredicateSet) ContainsID(id string) bool {
	for _, x := range lps.Items {
		if x.GetID() == id {
			return true
		}
	}
	return false
}

// Remove the given item from this set.
// Returns true if it was removed, false otherwise
func (lps *locPredicateSet) Remove(item model.LocPredicate) bool {
	for i, x := range lps.Items {
		if x.LocPredicate == item {
			lps.Items = append(lps.Items[:i], lps.Items[i+1:]...)
			return true
		}
	}
	return false
}

// Remove all items.
func (lps *locPredicateSet) Clear() bool {
	if len(lps.Items) == 0 {
		return false
	}
	lps.Items = nil
	return true
}

// Does this set contain the given item?
func (lps *locPredicateSet) Contains(item model.LocPredicate) bool {
	for _, x := range lps.Items {
		if x.LocPredicate == item {
			return true
		}
	}
	return false
}

// Add a given predicate
func (lps *locPredicateSet) Add(item model.LocPredicate) {
	// Ensure type
	x := item.(LocPredicate)
	// Ensure an id is set
	if x.GetID() == "" {
		x.SetID(NewID())
	}
	for {
		// Ensure unique ID
		if lps.ContainsID(x.GetID()) {
			x.SetID(NewID())
			continue
		}
		x.SetContainer(lps)
		lps.Items = append(lps.Items, &LocPredicateContainer{LocPredicate: x})
		lps.OnModified()
		return
	}
}
