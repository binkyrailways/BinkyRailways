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
	"context"
	"fmt"
	"time"

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
	"github.com/binkyrailways/BinkyRailways/pkg/core/util"
)

// Lockable adds implementation methods to state.Lockable
type Lockable interface {
	state.Lockable
}

type lockable struct {
	this            state.Lockable
	iterateChildren func(context.Context, func(state.Lockable) error) error
	onUnlocked      []func(context.Context)

	exclusive util.Exclusive
	lockedBy  Loc
}

const (
	canLockTimeout = time.Millisecond
	lockTimeout    = time.Millisecond
	unlockTimeout  = time.Millisecond
)

// newLockable initializes a new lockable
func newLockable(exclusive util.Exclusive, this state.Lockable, iterateChildren func(context.Context, func(state.Lockable) error) error, onUnlocked ...func(context.Context)) lockable {
	return lockable{
		this:            this,
		exclusive:       exclusive,
		iterateChildren: iterateChildren,
		onUnlocked:      onUnlocked,
	}
}

// Is this resource locked?
func (l *lockable) IsLocked(context.Context) bool {
	return l.lockedBy != nil
}

// Gets the locomotive that has this state locked.
// Returns null if this state is not locked.
func (l *lockable) GetLockedBy(context.Context) state.Loc {
	return l.lockedBy
}

// ValidateLockedBy checks that this entity is locked by the given loc.
func (l *lockable) ValidateLockedBy(ctx context.Context, loc state.Loc) error {
	current := l.lockedBy
	if current != loc {
		if current == nil {
			return fmt.Errorf("expect object to be locked by '%s', but is not locked at all", loc.GetDescription())
		}
		return fmt.Errorf("expect object to be locked by '%s', but it is not locked by '%s'", loc.GetDescription(), current.GetDescription())
	}
	return nil
}

// Can this state be locked by the intended owner?
// Return true is this entity and all underlying entities are not locked.
// Returns: lockedBy, canLock
func (l *lockable) CanLock(ctx context.Context, owner state.Loc) (lockedBy state.Loc, canLock bool) {
	l.exclusive.Exclusive(ctx, canLockTimeout, "CanLock", func(ctx context.Context) error {
		ownerImpl, ok := owner.(Loc)
		if !ok {
			lockedBy, canLock = nil, false
			return nil
		}
		if l.lockedBy != nil && l.lockedBy != ownerImpl {
			lockedBy, canLock = l.lockedBy, false
			return nil
		}
		lockedBy, canLock = nil, true
		l.iterateChildren(ctx, func(child state.Lockable) error {
			if lb, cl := child.CanLock(ctx, ownerImpl); !cl && canLock {
				lockedBy, canLock = lb, cl
			}
			return nil
		})
		return nil
	})
	return
}

// Lock this state by the given owner.
// Also lock all underlying entities.
func (l *lockable) Lock(ctx context.Context, owner state.Loc) error {
	return l.exclusive.Exclusive(ctx, lockTimeout, "Lock", func(ctx context.Context) error {
		if owner == nil {
			return fmt.Errorf("cannot Lock with nil owner")
		}
		ownerImpl, ok := owner.(Loc)
		if !ok {
			return fmt.Errorf("owner does not implement Loc")
		}
		// Lock if not already locked
		var lockedNow []state.Lockable
		if l.lockedBy != ownerImpl {
			if lockedBy, canLock := l.CanLock(ctx, ownerImpl); !canLock {
				return fmt.Errorf("already locked by %s", lockedBy.GetDescription())
			}
			// Lock myself
			l.lockedBy = ownerImpl
			ownerImpl.AddLockedEntity(ctx, l.this)
			lockedNow = append(lockedNow, l.this)
		}
		if err := l.iterateChildren(ctx, func(child state.Lockable) error {
			if err := child.Lock(ctx, ownerImpl); err != nil {
				return err
			}
			lockedNow = append(lockedNow, child)
			return nil
		}); err != nil {
			// Underlying lock failed, unlock everything
			for _, x := range lockedNow {
				x.Unlock(ctx, nil)
			}
			return err
		}
		return nil
	})
}

// Unlock this state from the given owner.
// Also unlock all underlying entities except the given exclusion and the underlying entities of the given exclusion.
func (l *lockable) Unlock(ctx context.Context, exclusion state.Lockable) {
	l.exclusive.Exclusive(ctx, unlockTimeout, "Unlock", func(ctx context.Context) error {
		if l.lockedBy == nil {
			// Not locked, we're done
			return nil
		}
		// Unlock children
		l.iterateChildren(ctx, func(child state.Lockable) error {
			child.Unlock(ctx, exclusion)
			return nil
		})
		// Unlock me
		if !l.excludeMe(ctx, exclusion) {
			l.lockedBy.RemoveLockedEntity(ctx, l.this)
			l.lockedBy = nil
		}
		// TODO
		//		OnActualStateChanged();
		for _, cb := range l.onUnlocked {
			cb(ctx)
		}
		return nil
	})
}

// Should this state (l) be excluded given the exclusion?
func (l *lockable) excludeMe(ctx context.Context, exclusion state.Lockable) bool {
	if exclusion == nil {
		return false
	}
	if exclusion == l.this {
		return true
	}
	if impl, ok := exclusion.(*lockable); ok {
		contains := false
		impl.iterateChildren(ctx, func(l state.Lockable) error {
			if l == exclusion {
				contains = true
			}
			return nil
		})
		return contains
	}
	return false
}
