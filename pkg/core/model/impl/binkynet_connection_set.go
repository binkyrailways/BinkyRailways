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

type binkyNetConnectionSet struct {
	onModified func()
	binkyNetConnectionSetItems
}

type binkyNetConnectionSetItems struct {
	Items []*binkyNetConnection `xml:"Items,omitempty"`
}

var _ model.BinkyNetConnectionSet = &binkyNetConnectionSet{}

// UnmarshalXML unmarshals and connects the module.
func (l *binkyNetConnectionSet) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&l.binkyNetConnectionSetItems, &start); err != nil {
		return err
	}
	for _, x := range l.Items {
		x.onModified = l.OnModified
	}
	return nil
}

// Get number of entries
func (l *binkyNetConnectionSet) GetCount() int {
	return len(l.Items)
}

// Get an entry by ID.
func (l *binkyNetConnectionSet) Get(key api.ConnectionName) (model.BinkyNetConnection, bool) {
	for _, d := range l.Items {
		if d.GetKey() == key {
			return d, true
		}
	}
	return nil, false
}

// Invoke the callback for each entry.
func (l *binkyNetConnectionSet) ForEach(cb func(model.BinkyNetConnection)) {
	for _, d := range l.Items {
		cb(d)
	}
}

// Remove the given entry.
// Returns true if it was removed, false otherwise
func (l *binkyNetConnectionSet) Remove(entry model.BinkyNetConnection) bool {
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
func (l *binkyNetConnectionSet) Contains(entry model.BinkyNetConnection) bool {
	for _, x := range l.Items {
		if x == entry {
			return true
		}
	}
	return false
}

// Add a new entry
func (l *binkyNetConnectionSet) AddNew(key api.ConnectionName) (model.BinkyNetConnection, error) {
	if _, found := l.Get(key); found {
		return nil, fmt.Errorf("Duplicate key '%s'", key)
	}
	d := &binkyNetConnection{
		Key: key,
	}
	d.onModified = l.OnModified
	l.Items = append(l.Items, d)
	l.OnModified()
	return d, nil
}

// OnModified triggers the modified function of the parent (if any)
func (l *binkyNetConnectionSet) OnModified() {
	if l.onModified != nil {
		l.onModified()
	}
}
