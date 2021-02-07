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
	"fmt"

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

type lockable struct {
	Children []state.Lockable

	lockedBy state.Loc
}

// Gets the locomotive that has this state locked.
// Returns null if this state is not locked.
func (l *lockable) GetLockedBy() state.Loc {
	return l.lockedBy
}

// Can this state be locked by the intended owner?
// Return true is this entity and all underlying entities are not locked.
// Returns: lockedBy, canLock
func (l *lockable) CanLock(owner state.Loc) (state.Loc, bool) {
	if l.lockedBy != nil && l.lockedBy != owner {
		return l.lockedBy, false
	}
	for _, c := range l.Children {
		if lockedBy, canLock := c.CanLock(owner); !canLock {
			return lockedBy, canLock
		}
	}
	return nil, true
}

// Lock this state by the given owner.
// Also lock all underlying entities.
func (l *lockable) Lock(owner state.Loc) error {
	if lockedBy, canLock := l.CanLock(owner); !canLock {
		return fmt.Errorf("Already locked by %s", lockedBy.GetDescription())
	}
	l.lockedBy = owner
	for _, c := range l.Children {
		if err := c.Lock(owner); err != nil {
			return err
		}
	}
	return nil
}

// Unlock this state from the given owner.
// Also unlock all underlying entities except the given exclusion and the underlying entities of the given exclusion.
func (l *lockable) Unlock(exclusion state.Lockable) {
	if l == exclusion {
		return
	}
	l.lockedBy = nil
	for _, c := range l.Children {
		c.Unlock(exclusion)
	}
}
