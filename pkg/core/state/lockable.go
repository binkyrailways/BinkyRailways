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

package state

// Lockable specifies a state that can be locked by a locomotive
type Lockable interface {
	// Gets the locomotive that has this state locked.
	// Returns null if this state is not locked.
	GetLockedBy() Loc

	// Can this state be locked by the intended owner?
	// Return true is this entity and all underlying entities are not locked.
	// Returns: lockedBy, canLock
	CanLock(owner Loc) (Loc, bool)

	// Lock this state by the given owner.
	// Also lock all underlying entities.
	Lock(owner Loc) error

	// Unlock this state from the given owner.
	// Also unlock all underlying entities except the given exclusion and the underlying entities of the given exclusion.
	Unlock(exclusion Lockable)
}
