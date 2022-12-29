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

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
	"github.com/binkyrailways/BinkyRailways/pkg/core/util"
)

// BoolProperty contains the value of a property in a state object.
// The value contains a requested value and an actual value.
type boolProperty struct {
	actualBoolProperty
	requested      bool
	requestChanges []func(context.Context, bool)
}

// Configure the values of the property
func (p *boolProperty) Configure(name string, subject state.Entity, dispatcher state.EventDispatcher, exclusive util.Exclusive) {
	p.actualBoolProperty.Configure(p, name, subject, dispatcher, exclusive)
}

func (p *boolProperty) IsConsistent(ctx context.Context) bool {
	return p.GetActual(ctx) == p.GetRequested(ctx)
}
func (p *boolProperty) GetRequested(ctx context.Context) bool {
	return p.requested
}
func (p *boolProperty) SetRequested(ctx context.Context, value bool) error {
	return p.exclusive.Exclusive(ctx, func(ctx context.Context) error {
		if p.requested != value {
			p.requested = value
			for _, cb := range p.requestChanges {
				cb(ctx, value)
			}
			p.SendRequestedStateChanged()
		}
		return nil
	})
}

// Subscribe to requested changes
func (p *boolProperty) SubscribeRequestChanges(cb func(context.Context, bool)) {
	p.exclusive.Exclusive(context.Background(), func(c context.Context) error {
		p.requestChanges = append(p.requestChanges, cb)
		return nil
	})
}

// intProperty contains the value of a property in a state object.
// The value contains a requested value and an actual value.
type intProperty struct {
	actualIntProperty
	requested      int
	requestChanges []func(context.Context, int)
}

// Configure the values of the property
func (p *intProperty) Configure(name string, subject state.Entity, dispatcher state.EventDispatcher, exclusive util.Exclusive) {
	p.actualIntProperty.Configure(p, name, subject, dispatcher, exclusive)
}

func (p *intProperty) IsConsistent(ctx context.Context) bool {
	return p.GetActual(ctx) == p.GetRequested(ctx)
}
func (p *intProperty) GetRequested(ctx context.Context) int {
	return p.requested
}
func (p *intProperty) SetRequested(ctx context.Context, value int) error {
	return p.exclusive.Exclusive(ctx, func(ctx context.Context) error {
		if p.requested != value {
			p.requested = value
			for _, cb := range p.requestChanges {
				cb(ctx, value)
			}
			p.SendRequestedStateChanged()
		}
		return nil
	})
}

// Subscribe to requested changes
func (p *intProperty) SubscribeRequestChanges(cb func(context.Context, int)) {
	p.exclusive.Exclusive(context.Background(), func(c context.Context) error {
		p.requestChanges = append(p.requestChanges, cb)
		return nil
	})
}

// locDirectionProperty contains the value of a property in a state object.
// The value contains a requested value and an actual value.
type locDirectionProperty struct {
	actualLocDirectionProperty
	requested      state.LocDirection
	requestChanges []func(context.Context, state.LocDirection)
}

// Configure the values of the property
func (p *locDirectionProperty) Configure(name string, subject state.Entity, dispatcher state.EventDispatcher, exclusive util.Exclusive) {
	p.actualLocDirectionProperty.Configure(p, name, subject, dispatcher, exclusive)
}

func (p *locDirectionProperty) IsConsistent(ctx context.Context) bool {
	return p.GetActual(ctx) == p.GetRequested(ctx)
}
func (p *locDirectionProperty) GetRequested(ctx context.Context) state.LocDirection {
	return p.requested
}
func (p *locDirectionProperty) SetRequested(ctx context.Context, value state.LocDirection) error {
	return p.exclusive.Exclusive(ctx, func(ctx context.Context) error {
		if p.requested != value {
			p.requested = value
			for _, cb := range p.requestChanges {
				cb(ctx, value)
			}
			p.SendRequestedStateChanged()
		}
		return nil
	})
}

// Subscribe to requested changes
func (p *locDirectionProperty) SubscribeRequestChanges(cb func(context.Context, state.LocDirection)) {
	p.exclusive.Exclusive(context.Background(), func(c context.Context) error {
		p.requestChanges = append(p.requestChanges, cb)
		return nil
	})
}

// locDirectionProperty contains the value of a property in a state object.
// The value contains a requested value and an actual value.
type switchDirectionProperty struct {
	actualSwitchDirectionProperty
	requested      model.SwitchDirection
	requestChanges []func(context.Context, model.SwitchDirection)
}

// Configure the values of the property
func (p *switchDirectionProperty) Configure(name string, subject state.Entity, dispatcher state.EventDispatcher, exclusive util.Exclusive) {
	p.actualSwitchDirectionProperty.Configure(p, name, subject, dispatcher, exclusive)
}

func (p *switchDirectionProperty) IsConsistent(ctx context.Context) bool {
	return p.GetActual(ctx) == p.GetRequested(ctx)
}
func (p *switchDirectionProperty) GetRequested(ctx context.Context) model.SwitchDirection {
	return p.requested
}
func (p *switchDirectionProperty) SetRequested(ctx context.Context, value model.SwitchDirection) error {
	return p.exclusive.Exclusive(ctx, func(ctx context.Context) error {
		if p.requested != value {
			p.requested = value
			for _, cb := range p.requestChanges {
				cb(ctx, value)
			}
			p.SendRequestedStateChanged()
		}
		return nil
	})
}

// Subscribe to requested changes
func (p *switchDirectionProperty) SubscribeRequestChanges(cb func(context.Context, model.SwitchDirection)) {
	p.exclusive.Exclusive(context.Background(), func(c context.Context) error {
		p.requestChanges = append(p.requestChanges, cb)
		return nil
	})
}
