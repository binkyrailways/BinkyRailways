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
	"sync/atomic"

	"go.uber.org/multierr"

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
	"github.com/binkyrailways/BinkyRailways/pkg/core/util"
)

// powerProperty implements the power for all command stations of a railway.
type powerProperty struct {
	Railway Railway

	requested            bool
	lastActualChangesID  uint32
	actualChanges        []callbackEntry
	lastRequestChangesID uint32
	requestChanges       []callbackEntry
}

type callbackEntry struct {
	id uint32
	cb func(context.Context, bool)
}

var _ state.BoolProperty = &powerProperty{}

// GetName returns the name of the property
func (sp *powerProperty) GetName() string {
	return "power"
}

// Gets / sets the actual value
func (sp *powerProperty) GetActual(ctx context.Context) bool {
	csOn := 0
	csOff := 0
	sp.Railway.Exclusive(ctx, getTimeout, "power(total).GetActual", func(ctx context.Context) error {
		sp.Railway.ForEachCommandStation(func(cs state.CommandStation) {
			if cs.GetPower().GetActual(ctx) {
				csOn++
			} else {
				csOff++
			}
		})
		return nil
	})
	return csOff == 0 && csOn > 0
}
func (sp *powerProperty) SetActual(ctx context.Context, value bool) (bool, error) {
	changed := false
	err := sp.Railway.Exclusive(ctx, setActualTimeout, "power.SetActual", func(ctx context.Context) error {
		actual := sp.GetActual(ctx)

		var err error
		sp.Railway.ForEachCommandStation(func(cs state.CommandStation) {
			c, e := cs.GetPower().SetActual(ctx, value)
			changed = changed || c
			multierr.AppendInto(&err, e)
		})
		if err != nil {
			return err
		}
		if actual != value {
			for _, entry := range sp.actualChanges {
				entry.cb(ctx, value)
			}
			sp.Railway.Send(state.ActualStateChangedEvent{
				Subject:  sp.Railway,
				Property: sp,
			})
		}
		return nil
	})
	return changed, err
}

// Subscribe to requested changes
func (sp *powerProperty) SubscribeActualChanges(cb func(context.Context, bool)) context.CancelFunc {
	id := atomic.AddUint32(&sp.lastActualChangesID, 1)
	sp.Railway.Exclusive(context.Background(), subscribeTimeout, "power.SubscribeActualChanges", func(c context.Context) error {
		sp.actualChanges = append(sp.actualChanges, callbackEntry{
			id: id,
			cb: cb,
		})
		return nil
	})
	return func() {
		sp.Railway.Exclusive(context.Background(), cancelSubscriptionTimeout, "SubscribeActualChanges.Cancel", func(context.Context) error {
			sp.actualChanges = util.SliceExcept(sp.actualChanges, func(x callbackEntry) bool { return x.id == id })
			return nil
		})
	}
}

// Gets / sets the requested value
func (sp *powerProperty) GetRequested(ctx context.Context) bool {
	return sp.requested
}
func (sp *powerProperty) SetRequested(ctx context.Context, value bool) error {
	return sp.Railway.Exclusive(ctx, setRequestTimeout, "power.SetRequested", func(ctx context.Context) error {
		var err error
		sp.Railway.ForEachCommandStation(func(cs state.CommandStation) {
			multierr.AppendInto(&err, cs.GetPower().SetRequested(ctx, value))
		})
		if err != nil {
			return err
		}
		if sp.requested != value {
			sp.requested = value
			for _, entry := range sp.requestChanges {
				entry.cb(ctx, value)
			}
			sp.Railway.Send(state.RequestedStateChangedEvent{
				Subject:  sp.Railway,
				Property: sp,
			})
		}
		return nil
	})
}

func (sp *powerProperty) IsConsistent(ctx context.Context) bool {
	return sp.GetActual(ctx) == sp.GetRequested(ctx)
}

// Subscribe to requested changes
func (sp *powerProperty) SubscribeRequestChanges(cb func(context.Context, bool)) context.CancelFunc {
	id := atomic.AddUint32(&sp.lastActualChangesID, 1)
	sp.Railway.Exclusive(context.Background(), subscribeTimeout, "power.SubscribeRequestChanges", func(c context.Context) error {
		sp.requestChanges = append(sp.requestChanges, callbackEntry{
			id: id,
			cb: cb,
		})
		return nil
	})
	return func() {
		sp.Railway.Exclusive(context.Background(), cancelSubscriptionTimeout, "SubscribeRequestChanges.Cancel", func(context.Context) error {
			sp.requestChanges = util.SliceExcept(sp.requestChanges, func(x callbackEntry) bool { return x.id == id })
			return nil
		})
	}
}
