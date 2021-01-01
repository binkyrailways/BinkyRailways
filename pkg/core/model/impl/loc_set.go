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
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// LocSet extends implementation methods to model.LocSet
type LocSet interface {
	model.LocSet

	AddRef(model.LocRef)
}

type locSet struct {
	items        map[string]model.LocRef
	onModified   func()
	onTryResolve func(id string) model.Loc
}

var _ model.LocSet = &locSet{}

func (bs *locSet) Initialize(onModified func(), onTryResolve func(id string) model.Loc) {
	bs.items = make(map[string]model.LocRef)
	bs.onModified = onModified
	bs.onTryResolve = onTryResolve
}

// Get number of entries
func (bs *locSet) GetCount() int {
	return len(bs.items)
}

// Get an item by ID
func (bs *locSet) Get(id string) (model.LocRef, bool) {
	if result, found := bs.items[id]; found {
		return result, true
	}
	return nil, false
}

// Invoke the callback for each item
func (bs *locSet) ForEach(cb func(model.LocRef)) {
	for _, x := range bs.items {
		cb(x)
	}
}

// Does this set contain an item with the given id?
func (bs *locSet) ContainsID(id string) bool {
	_, found := bs.items[id]
	return found
}

// Remove the given item from this set.
// Returns true if it was removed, false otherwise
func (bs *locSet) Remove(item model.LocRef) bool {
	if !bs.ContainsID(item.GetID()) {
		return false
	}
	delete(bs.items, item.GetID())
	bs.onModified()
	return true
}

// Does this set contain the given item?
func (bs *locSet) Contains(item model.LocRef) bool {
	for _, x := range bs.items {
		if x == item {
			return true
		}
	}
	return false
}

// Add a new item to this set
func (bs *locSet) Add(item model.Loc) model.LocRef {
	id := item.GetID()
	result, found := bs.items[id]
	if !found {
		result = newLocRef(id, bs.onTryResolve)
		bs.items[id] = result
		bs.onModified()
	}
	return result
}

// Add a new item to this set
func (bs *locSet) AddRef(item model.LocRef) {
	id := item.GetID()
	if _, found := bs.items[id]; !found {
		bs.items[id] = newLocRef(id, bs.onTryResolve)
		bs.onModified()
	}
}

// Copy all entries into the given destination.
func (bs *locSet) CopyTo(destination model.LocSet) {
	dst := destination.(LocSet)
	bs.ForEach(func(lr model.LocRef) {
		dst.AddRef(lr)
	})
}
