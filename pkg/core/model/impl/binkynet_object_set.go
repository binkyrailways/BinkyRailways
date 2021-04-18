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
	"fmt"

	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type binkyNetObjectSet struct {
	container *binkyNetLocalWorker
	binkyNetObjectSetItems
}

type binkyNetObjectSetItems struct {
	Objects []*binkyNetObject `xml:"Objects,omitempty"`
}

var _ model.BinkyNetObjectSet = &binkyNetObjectSet{}

// UnmarshalXML unmarshals and connects the module.
func (l *binkyNetObjectSet) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&l.binkyNetObjectSetItems, &start); err != nil {
		return err
	}
	for _, x := range l.Objects {
		x.SetContainer(l)
		x.EnsureID()
	}
	return nil
}

// Gets the local worker this set belongs to
func (l *binkyNetObjectSet) GetLocalWorker() model.BinkyNetLocalWorker {
	return l.container
}

// SetContainer links this instance to its container
func (l *binkyNetObjectSet) SetContainer(container *binkyNetLocalWorker) {
	l.container = container
}

// Get number of entries
func (l *binkyNetObjectSet) GetCount() int {
	return len(l.Objects)
}

// Get an entry by ID.
func (l *binkyNetObjectSet) Get(id api.ObjectID) (model.BinkyNetObject, bool) {
	for _, o := range l.Objects {
		if o.GetObjectID() == id {
			return o, true
		}
	}
	return nil, false
}

// Get an entry by index.
func (l *binkyNetObjectSet) GetAt(index int) (model.BinkyNetObject, bool) {
	if index >= 0 && index < len(l.Objects) {
		return l.Objects[index], true
	}
	return nil, false
}

// Invoke the callback for each entry.
func (l *binkyNetObjectSet) ForEach(cb func(model.BinkyNetObject)) {
	for _, d := range l.Objects {
		cb(d)
	}
}

// Remove the given entry.
// Returns true if it was removed, false otherwise
func (l *binkyNetObjectSet) Remove(entry model.BinkyNetObject) bool {
	for idx, x := range l.Objects {
		if x == entry {
			l.Objects = append(l.Objects[:idx], l.Objects[idx+1:]...)
			l.OnModified()
			return true
		}
	}
	return false
}

// Is the given entry contained in this set?
func (l *binkyNetObjectSet) Contains(entry model.BinkyNetObject) bool {
	for _, x := range l.Objects {
		if x == entry {
			return true
		}
	}
	return false
}

// Add a new entry
func (l *binkyNetObjectSet) AddNew() model.BinkyNetObject {
	d := newBinkyNetObject()
	d.SetContainer(l)
	d.SetObjectID(api.ObjectID(fmt.Sprintf("newObject%d", len(l.Objects)+1)))
	l.Objects = append(l.Objects, d)
	l.OnModified()
	return d
}

// OnModified triggers the modified function of the parent (if any)
func (l *binkyNetObjectSet) OnModified() {
	if l.container != nil {
		l.container.OnModified()
	}
}
