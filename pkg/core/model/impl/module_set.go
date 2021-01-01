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

type moduleSet struct {
	items        map[string]model.ModuleRef
	onModified   func()
	onTryResolve func(id string) model.Module
}

var _ model.ModuleSet = &moduleSet{}

func (bs *moduleSet) Initialize(onModified func(), onTryResolve func(id string) model.Module) {
	bs.items = make(map[string]model.ModuleRef)
	bs.onModified = onModified
	bs.onTryResolve = onTryResolve
}

// Get number of entries
func (bs *moduleSet) GetCount() int {
	return len(bs.items)
}

// Invoke the callback for each item
func (bs *moduleSet) ForEach(cb func(model.ModuleRef)) {
	for _, x := range bs.items {
		cb(x)
	}
}

// Does this set contain an item with the given id?
func (bs *moduleSet) ContainsID(id string) bool {
	_, found := bs.items[id]
	return found
}

// Remove the given item from this set.
// Returns true if it was removed, false otherwise
func (bs *moduleSet) Remove(item model.ModuleRef) bool {
	if !bs.ContainsID(item.GetID()) {
		return false
	}
	delete(bs.items, item.GetID())
	bs.onModified()
	return true
}

// Does this set contain the given item?
func (bs *moduleSet) Contains(item model.ModuleRef) bool {
	for _, x := range bs.items {
		if x == item {
			return true
		}
	}
	return false
}

// Add a new item to this set
func (bs *moduleSet) Add(item model.Module) model.ModuleRef {
	id := item.GetID()
	result, found := bs.items[id]
	if !found {
		result = newModuleRef(id, bs.onTryResolve)
		bs.items[id] = result
		bs.onModified()
	}
	return result
}
