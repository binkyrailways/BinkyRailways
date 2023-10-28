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

import (
	"context"
	"fmt"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// Lockable specifies a state that can be locked by a locomotive
type Lockable interface {
	// Gets the locomotive that has this state locked.
	// Returns null if this state is not locked.
	GetLockedBy(context.Context) Loc

	// ValidateLockedBy checks that this entity is locked by the given loc.
	ValidateLockedBy(ctx context.Context, loc Loc) error

	// Can this state be locked by the intended owner?
	// Return true is this entity and all underlying entities are not locked.
	// Returns: lockedBy, canLock
	CanLock(ctx context.Context, owner Loc) (Loc, bool)

	// Lock this state by the given owner.
	// Also lock all underlying entities.
	Lock(ctx context.Context, owner Loc) error

	// Unlock this state from the given owner.
	// Also unlock all underlying entities except the given exclusion and the underlying entities of the given exclusion.
	Unlock(ctx context.Context, exclusion Lockable)
}

// IsLocked returns true if the given lockable is locked by any loc.
func IsLocked(ctx context.Context, l Lockable) bool {
	return l.GetLockedBy(ctx) != nil
}

// IsLockedBy returns true if the given lockable is locked by the given loc.
func IsLockedBy(ctx context.Context, l Lockable, loc Loc) bool {
	return l.GetLockedBy(ctx) == loc
}

/// Validate that the given state is locked by the given loc.
/// If not, throw an error.
func AssertLockedBy(ctx context.Context, l Lockable, loc Loc) {
	if current := l.GetLockedBy(ctx); current != loc {
		if current == nil {
			panic(fmt.Sprintf("Object %s is not locked by %s", describe(l), loc.GetDescription()))
		} else {
			panic(fmt.Sprintf("Object %s is not locked by %s but by %s", describe(l), loc.GetDescription(), current.GetDescription()))
		}
	}
}

func describe(obj interface{}) string {
	if obj == nil {
		return "nil"
	}
	if wd, ok := obj.(model.WithDescription); ok {
		return fmt.Sprintf("%s (%T)", wd.GetDescription(), obj)
	}
	return fmt.Sprintf("%T", obj)
}
