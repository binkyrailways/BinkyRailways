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

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type binkyNetLocalWorkerSet struct {
	onModified func()
	binkyNetLocalWorkerSetItems
}

type binkyNetLocalWorkerSetItems struct {
	Items []*binkyNetLocalWorker `xml:"Items,omitempty"`
}

var _ model.BinkyNetLocalWorkerSet = &binkyNetLocalWorkerSet{}

// UnmarshalXML unmarshals and connects the module.
func (l *binkyNetLocalWorkerSet) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&l.binkyNetLocalWorkerSetItems, &start); err != nil {
		return err
	}
	for _, x := range l.Items {
		x.onModified = l.OnModified
	}
	return nil
}

// Get number of entries
func (l *binkyNetLocalWorkerSet) GetCount() int {
	return len(l.Items)
}

// Get an entry by ID or alias.
func (l *binkyNetLocalWorkerSet) Get(id string) (model.BinkyNetLocalWorker, bool) {
	for _, o := range l.Items {
		if o.GetID() == id || o.GetAlias() == id {
			return o, true
		}
	}
	return nil, false
}

// Invoke the callback for each entry.
func (l *binkyNetLocalWorkerSet) ForEach(cb func(model.BinkyNetLocalWorker)) {
	for _, d := range l.Items {
		cb(d)
	}
}

// Remove the given entry.
// Returns true if it was removed, false otherwise
func (l *binkyNetLocalWorkerSet) Remove(entry model.BinkyNetLocalWorker) bool {
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
func (l *binkyNetLocalWorkerSet) Contains(entry model.BinkyNetLocalWorker) bool {
	for _, x := range l.Items {
		if x == entry {
			return true
		}
	}
	return false
}

// Add a new entry
func (l *binkyNetLocalWorkerSet) AddNew(id string) (model.BinkyNetLocalWorker, error) {
	_, found := l.Get(id)
	if found {
		return nil, fmt.Errorf("Duplicate id: %s", id)
	}
	d := &binkyNetLocalWorker{}
	d.SetID(id)
	d.onModified = l.OnModified
	l.Items = append(l.Items, d)
	l.OnModified()
	return d, nil
}

// OnModified triggers the modified function of the parent (if any)
func (l *binkyNetLocalWorkerSet) OnModified() {
	if l.onModified != nil {
		l.onModified()
	}
}
