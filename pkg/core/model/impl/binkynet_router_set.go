// Copyright 2021 Ewout Prangsma
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

type binkyNetRouterSet struct {
	container *binkyNetLocalWorker
	binkyNetRouterSetItems
}

type binkyNetRouterSetItems struct {
	Items []*binkyNetRouter `xml:"Items,omitempty"`
}

var _ model.BinkyNetRouterSet = &binkyNetRouterSet{}

// UnmarshalXML unmarshals and connects the module.
func (l *binkyNetRouterSet) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&l.binkyNetRouterSetItems, &start); err != nil {
		return err
	}
	for _, x := range l.Items {
		x.SetContainer(l)
	}
	return nil
}

// Gets the local worker this object belongs to
func (l *binkyNetRouterSet) GetLocalWorker() model.BinkyNetLocalWorker {
	return l.container
}

// SetContainer links this instance to its container
func (l *binkyNetRouterSet) SetContainer(container *binkyNetLocalWorker) {
	l.container = container
}

// Get number of entries
func (l *binkyNetRouterSet) GetCount() int {
	return len(l.Items)
}

// Get an entry by ID or alias.
func (l *binkyNetRouterSet) Get(id string) (model.BinkyNetRouter, bool) {
	for _, o := range l.Items {
		if o.GetID() == id {
			return o, true
		}
	}
	return nil, false
}

// Get an entry by index.
func (l *binkyNetRouterSet) GetAt(index int) (model.BinkyNetRouter, bool) {
	if index >= 0 && index < len(l.Items) {
		return l.Items[index], true
	}
	return nil, false
}

// Invoke the callback for each entry.
func (l *binkyNetRouterSet) ForEach(cb func(model.BinkyNetRouter)) {
	for _, d := range l.Items {
		cb(d)
	}
}

// Remove the given entry.
// Returns true if it was removed, false otherwise
func (l *binkyNetRouterSet) Remove(entry model.BinkyNetRouter) bool {
	for idx, x := range l.Items {
		if x == entry {
			l.Items = append(l.Items[:idx], l.Items[idx+1:]...)
			l.OnModified()
			return true
		}
	}
	return false
}

// Is the given entry contained in this set?
func (l *binkyNetRouterSet) Contains(entry model.BinkyNetRouter) bool {
	for _, x := range l.Items {
		if x == entry {
			return true
		}
	}
	return false
}

// Add a new entry
func (l *binkyNetRouterSet) AddNew() (model.BinkyNetRouter, error) {
	d := newBinkyNetRouter()
	d.SetContainer(l)
	l.Items = append(l.Items, d)
	l.OnModified()
	return d, nil
}

// OnModified triggers the modified function of the parent (if any)
func (l *binkyNetRouterSet) OnModified() {
	if l.container != nil {
		l.container.OnModified()
	}
}
