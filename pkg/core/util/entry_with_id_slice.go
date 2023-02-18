// Copyright 2023 Ewout Prangsma
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

package util

import "context"

// SliceWithIdEntries is a helper to build slices where each entry has an identifier to
// it can easily be removed.
type SliceWithIdEntries[T any] []SliceWithIdEntry[T]

// SliceWithIdEntry is the generic entry of an SliceWithIdEntries.
type SliceWithIdEntry[T any] struct {
	id    uint32
	Value T
}

// Append a value to the slice.
// Updates the reference to the slice and returns a cancel function to remove the entry.
func (slice *SliceWithIdEntries[T]) Append(value T) context.CancelFunc {
	// Find new ID
	id := uint32(0)
	old := *slice
	for _, x := range old {
		if id < x.id {
			id = x.id
		}
	}
	id++
	*slice = append(old, SliceWithIdEntry[T]{
		id:    id,
		Value: value,
	})
	return func() {
		current := *slice
		*slice = SliceExcept(current, func(t SliceWithIdEntry[T]) bool {
			return t.id == id
		})
	}
}
