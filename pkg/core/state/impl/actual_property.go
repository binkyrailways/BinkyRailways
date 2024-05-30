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

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
	"github.com/binkyrailways/BinkyRailways/pkg/core/util"
)

// propertyBase contains the value of a property in a state object.
// The value contains an actual value.
type propertyBase struct {
	Property         state.ActualProperty  `json:"-"`
	Name             string                `json:"-"`
	Subject          state.Subject         `json:"-"`
	Dispatcher       state.EventDispatcher `json:"-"`
	LastActualChange time.Time             `json:"-"`
	exclusive        util.Exclusive
}

const (
	setActualTimeout          = time.Millisecond
	subscribeTimeout          = time.Millisecond
	cancelSubscriptionTimeout = time.Millisecond * 5
)

// Configure the values of the property
func (p *propertyBase) Configure(property state.ActualProperty, name string, subject state.Subject, dispatcher state.EventDispatcher, exclusive util.Exclusive) {
	p.Property = property
	p.Name = name
	p.Subject = subject
	p.Dispatcher = dispatcher
	p.exclusive = exclusive
}

// Gets the name of the property
func (p *propertyBase) GetName() string {
	return p.Name
}

// SendActualStateChanged dispatches an ActualStateChangedEvent
func (p *propertyBase) SendActualStateChanged() {
	if p.Dispatcher != nil {
		p.Dispatcher.Send(state.ActualStateChangedEvent{
			Subject:  p.Subject,
			Property: p.Property,
		})
	}
}

// SendActualStateChanged dispatches an RequestedStateChangedEvent
func (p *propertyBase) SendRequestedStateChanged() {
	if p.Dispatcher != nil {
		p.Dispatcher.Send(state.RequestedStateChangedEvent{
			Subject:  p.Subject,
			Property: p.Property.(state.Property),
		})
	}
}

// comparableActualProperty implements an actual property for comparable types.
type comparableActualProperty[T comparable] struct {
	propertyBase
	actual        T
	actualChanges util.SliceWithIdEntries[func(context.Context, T)]
	validate      func(T) T
}

// Configure the values of the property
func (p *comparableActualProperty[T]) Configure(property state.ActualProperty, name string, subject state.Subject, validate func(T) T, dispatcher state.EventDispatcher, exclusive util.Exclusive) {
	p.propertyBase.Configure(property, name, subject, dispatcher, exclusive)
	if validate != nil {
		p.validate = validate
	} else {
		p.validate = func(t T) T { return t }
	}
}

// Gets the actual value of the property as string
func (p *comparableActualProperty[T]) GetValueAsString() string {
	return fmt.Sprintf("%v", p.actual)
}

func (p *comparableActualProperty[T]) GetActual(ctx context.Context) T {
	return p.actual
}
func (p *comparableActualProperty[T]) SetActual(ctx context.Context, value T) (bool, error) {
	changed := false
	value = p.validate(value)
	err := p.exclusive.Exclusive(ctx, setActualTimeout, "comparableActualProperty.SetActual", func(ctx context.Context) error {
		if p.actual != value {
			p.LastActualChange = time.Now()
			changed = true
			p.actual = value
			for _, entry := range p.actualChanges {
				entry.Value(ctx, value)
			}
			p.SendActualStateChanged()
		}
		return nil
	})
	return changed, err
}

// Subscribe to actual changes
func (p *comparableActualProperty[T]) SubscribeActualChanges(cb func(context.Context, T)) context.CancelFunc {
	var cancel context.CancelFunc
	p.exclusive.Exclusive(context.Background(), subscribeTimeout, "SubscribeActualChanges", func(context.Context) error {
		cancel = p.actualChanges.Append(cb)
		return nil
	})
	return cancel
}

type equatable[T any] interface {
	Equals(other T) bool
}

// equatableActualProperty implements an actual property for comparable types.
type equatableActualProperty[T equatable[T]] struct {
	propertyBase
	actual        T
	actualChanges util.SliceWithIdEntries[func(context.Context, T)]
}

func (p *equatableActualProperty[T]) GetActual(ctx context.Context) T {
	return p.actual
}
func (p *equatableActualProperty[T]) setActual(ctx context.Context, value T, isEqual func(a, b T) bool) (bool, error) {
	changed := false
	err := p.exclusive.Exclusive(ctx, setActualTimeout, "equatableActualProperty.SetActual", func(ctx context.Context) error {
		if !isEqual(p.actual, value) {
			p.LastActualChange = time.Now()
			changed = true
			p.actual = value
			for _, entry := range p.actualChanges {
				entry.Value(ctx, value)
			}
			p.SendActualStateChanged()
		}
		return nil
	})
	return changed, err
}

// Subscribe to actual changes
func (p *equatableActualProperty[T]) SubscribeActualChanges(cb func(context.Context, T)) context.CancelFunc {
	var cancel context.CancelFunc
	p.exclusive.Exclusive(context.Background(), subscribeTimeout, "SubscribeActualChanges", func(context.Context) error {
		cancel = p.actualChanges.Append(cb)
		return nil
	})
	return cancel
}

// actualBoolProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualBoolProperty struct {
	comparableActualProperty[bool]
}

// actualIntProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualIntProperty struct {
	comparableActualProperty[int]
}

// actualTimeProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualTimeProperty struct {
	comparableActualProperty[time.Time]
}

// actualAutoLocStateProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualAutoLocStateProperty struct {
	comparableActualProperty[state.AutoLocState]
}

// actualLocDirectionProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualLocDirectionProperty struct {
	comparableActualProperty[state.LocDirection]
}

// actualSwitchDirectionProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualSwitchDirectionProperty struct {
	comparableActualProperty[model.SwitchDirection]
}

// actualBlockSideProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualBlockSideProperty struct {
	comparableActualProperty[model.BlockSide]
}

// actualBlockProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualBlockProperty struct {
	equatableActualProperty[state.Block]
}

func (p *actualBlockProperty) SetActual(ctx context.Context, value state.Block) (bool, error) {
	return p.equatableActualProperty.setActual(ctx, value, func(a, b state.Block) bool {
		if a == nil && b == nil {
			return true
		}
		if a == nil || b == nil {
			return false
		}
		return a.Equals(b)
	})
}

// actualRouteProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualRouteProperty struct {
	equatableActualProperty[state.Route]
}

func (p *actualRouteProperty) SetActual(ctx context.Context, value state.Route) (bool, error) {
	return p.equatableActualProperty.setActual(ctx, value, func(a, b state.Route) bool {
		if a == nil && b == nil {
			return true
		}
		if a == nil || b == nil {
			return false
		}
		return a.Equals(b)
	})
}

// actualRouteForLocProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualRouteForLocProperty struct {
	equatableActualProperty[state.RouteForLoc]
}

func (p *actualRouteForLocProperty) SetActual(ctx context.Context, value state.RouteForLoc) (bool, error) {
	return p.equatableActualProperty.setActual(ctx, value, func(a, b state.RouteForLoc) bool {
		if a == nil && b == nil {
			return true
		}
		if a == nil || b == nil {
			return false
		}
		return a.Equals(b)
	})
}

// actualRouteOptionsProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualRouteOptionsProperty struct {
	equatableActualProperty[state.RouteOptions]
}

func (p *actualRouteOptionsProperty) SetActual(ctx context.Context, value state.RouteOptions) (bool, error) {
	return p.equatableActualProperty.setActual(ctx, value, func(a, b state.RouteOptions) bool {
		if a == nil && b == nil {
			return true
		}
		if a == nil || b == nil {
			return false
		}
		return a.Equals(b)
	})
}
