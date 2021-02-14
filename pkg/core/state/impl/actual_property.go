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
	"time"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
	"github.com/binkyrailways/BinkyRailways/pkg/core/util"
)

// propertyBase contains the value of a property in a state object.
// The value contains an actual value.
type propertyBase struct {
	Subject    state.Entity
	Dispatcher state.EventDispatcher
	exclusive  util.Exclusive
}

// Configure the values of the property
func (p *propertyBase) Configure(subject state.Entity, dispatcher state.EventDispatcher, exclusive util.Exclusive) {
	p.Subject = subject
	p.Dispatcher = dispatcher
	p.exclusive = exclusive
}

// SendActualStateChanged dispatches an ActualStateChangedEvent
func (p *propertyBase) SendActualStateChanged() {
	if p.Dispatcher != nil {
		p.Dispatcher.Send(state.ActualStateChangedEvent{
			Subject:  p.Subject,
			Property: p,
		})
	}
}

// SendActualStateChanged dispatches an RequestedStateChangedEvent
func (p *propertyBase) SendRequestedStateChanged(prop state.Property) {
	if p.Dispatcher != nil {
		p.Dispatcher.Send(state.RequestedStateChangedEvent{
			Subject:  p.Subject,
			Property: prop,
		})
	}
}

// actualBoolProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualBoolProperty struct {
	propertyBase
	actual          bool
	OnActualChanged func(context.Context, bool)
}

func (p *actualBoolProperty) GetActual(ctx context.Context) bool {
	return p.actual
}
func (p *actualBoolProperty) SetActual(ctx context.Context, value bool) error {
	return p.exclusive.Exclusive(ctx, func(ctx context.Context) error {
		if p.actual != value {
			p.actual = value
			if p.OnActualChanged != nil {
				p.OnActualChanged(ctx, value)
			}
			p.SendActualStateChanged()
		}
		return nil
	})
}

// actualIntProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualIntProperty struct {
	propertyBase
	actual          int
	OnActualChanged func(context.Context, int)
}

func (p *actualIntProperty) GetActual(ctx context.Context) int {
	return p.actual
}
func (p *actualIntProperty) SetActual(ctx context.Context, value int) error {
	return p.exclusive.Exclusive(ctx, func(ctx context.Context) error {
		if p.actual != value {
			p.actual = value
			if p.OnActualChanged != nil {
				p.OnActualChanged(ctx, value)
			}
			p.SendActualStateChanged()
		}
		return nil
	})
}

// actualTimeProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualTimeProperty struct {
	propertyBase
	actual          time.Time
	OnActualChanged func(context.Context, time.Time)
}

func (p *actualTimeProperty) GetActual(ctx context.Context) time.Time {
	return p.actual
}
func (p *actualTimeProperty) SetActual(ctx context.Context, value time.Time) error {
	return p.exclusive.Exclusive(ctx, func(ctx context.Context) error {
		if !p.actual.Equal(value) {
			p.actual = value
			if p.OnActualChanged != nil {
				p.OnActualChanged(ctx, value)
			}
			p.SendActualStateChanged()
		}
		return nil
	})
}

// actualAutoLocStateProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualAutoLocStateProperty struct {
	propertyBase
	actual          state.AutoLocState
	OnActualChanged func(context.Context, state.AutoLocState)
}

func (p *actualAutoLocStateProperty) GetActual(ctx context.Context) state.AutoLocState {
	return p.actual
}
func (p *actualAutoLocStateProperty) SetActual(ctx context.Context, value state.AutoLocState) error {
	return p.exclusive.Exclusive(ctx, func(ctx context.Context) error {
		if p.actual != value {
			p.actual = value
			if p.OnActualChanged != nil {
				p.OnActualChanged(ctx, value)
			}
			p.SendActualStateChanged()
		}
		return nil
	})
}

// actualLocDirectionProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualLocDirectionProperty struct {
	propertyBase
	actual          state.LocDirection
	OnActualChanged func(context.Context, state.LocDirection)
}

func (p *actualLocDirectionProperty) GetActual(ctx context.Context) state.LocDirection {
	return p.actual
}
func (p *actualLocDirectionProperty) SetActual(ctx context.Context, value state.LocDirection) error {
	return p.exclusive.Exclusive(ctx, func(ctx context.Context) error {
		if p.actual != value {
			p.actual = value
			if p.OnActualChanged != nil {
				p.OnActualChanged(ctx, value)
			}
			p.SendActualStateChanged()
		}
		return nil
	})
}

// actualBlockSideProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualBlockSideProperty struct {
	propertyBase
	actual          model.BlockSide
	OnActualChanged func(context.Context, model.BlockSide)
}

func (p *actualBlockSideProperty) GetActual(ctx context.Context) model.BlockSide {
	return p.actual
}
func (p *actualBlockSideProperty) SetActual(ctx context.Context, value model.BlockSide) error {
	return p.exclusive.Exclusive(ctx, func(ctx context.Context) error {
		if p.actual != value {
			p.actual = value
			if p.OnActualChanged != nil {
				p.OnActualChanged(ctx, value)
			}
			p.SendActualStateChanged()
		}
		return nil
	})
}

// actualBlockProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualBlockProperty struct {
	propertyBase
	actual          state.Block
	OnActualChanged func(context.Context, state.Block)
}

func (p *actualBlockProperty) GetActual(ctx context.Context) state.Block {
	return p.actual
}
func (p *actualBlockProperty) SetActual(ctx context.Context, value state.Block) error {
	return p.exclusive.Exclusive(ctx, func(ctx context.Context) error {
		if p.actual != value {
			p.actual = value
			if p.OnActualChanged != nil {
				p.OnActualChanged(ctx, value)
			}
			p.SendActualStateChanged()
		}
		return nil
	})
}

// actualRouteProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualRouteProperty struct {
	propertyBase
	actual          state.Route
	OnActualChanged func(context.Context, state.Route)
}

func (p *actualRouteProperty) GetActual(ctx context.Context) state.Route {
	return p.actual
}
func (p *actualRouteProperty) SetActual(ctx context.Context, value state.Route) error {
	return p.exclusive.Exclusive(ctx, func(ctx context.Context) error {
		if p.actual != value {
			p.actual = value
			if p.OnActualChanged != nil {
				p.OnActualChanged(ctx, value)
			}
			p.SendActualStateChanged()
		}
		return nil
	})
}

// actualRouteForLocProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualRouteForLocProperty struct {
	propertyBase
	actual          state.RouteForLoc
	OnActualChanged func(context.Context, state.RouteForLoc)
}

func (p *actualRouteForLocProperty) GetActual(ctx context.Context) state.RouteForLoc {
	return p.actual
}
func (p *actualRouteForLocProperty) SetActual(ctx context.Context, value state.RouteForLoc) error {
	return p.exclusive.Exclusive(ctx, func(ctx context.Context) error {
		if p.actual != value {
			p.actual = value
			if p.OnActualChanged != nil {
				p.OnActualChanged(ctx, value)
			}
			p.SendActualStateChanged()
		}
		return nil
	})
}
