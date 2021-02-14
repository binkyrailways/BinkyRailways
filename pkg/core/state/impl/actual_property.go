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
	"time"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// propertyBase contains the value of a property in a state object.
// The value contains an actual value.
type propertyBase struct {
	Subject    state.Entity
	Dispatcher state.EventDispatcher
}

// Configure the values of the property
func (p *propertyBase) Configure(subject state.Entity, dispatcher state.EventDispatcher) {
	p.Subject = subject
	p.Dispatcher = dispatcher
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
	OnActualChanged func(bool)
}

func (p *actualBoolProperty) GetActual() bool {
	return p.actual
}
func (p *actualBoolProperty) SetActual(value bool) error {
	if p.actual != value {
		p.actual = value
		if p.OnActualChanged != nil {
			p.OnActualChanged(value)
		}
		p.SendActualStateChanged()
	}
	return nil
}

// actualIntProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualIntProperty struct {
	propertyBase
	actual          int
	OnActualChanged func(int)
}

func (p *actualIntProperty) GetActual() int {
	return p.actual
}
func (p *actualIntProperty) SetActual(value int) error {
	if p.actual != value {
		p.actual = value
		if p.OnActualChanged != nil {
			p.OnActualChanged(value)
		}
		p.SendActualStateChanged()
	}
	return nil
}

// actualTimeProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualTimeProperty struct {
	propertyBase
	actual          time.Time
	OnActualChanged func(time.Time)
}

func (p *actualTimeProperty) GetActual() time.Time {
	return p.actual
}
func (p *actualTimeProperty) SetActual(value time.Time) error {
	if !p.actual.Equal(value) {
		p.actual = value
		if p.OnActualChanged != nil {
			p.OnActualChanged(value)
		}
		p.SendActualStateChanged()
	}
	return nil
}

// actualAutoLocStateProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualAutoLocStateProperty struct {
	propertyBase
	actual          state.AutoLocState
	OnActualChanged func(state.AutoLocState)
}

func (p *actualAutoLocStateProperty) GetActual() state.AutoLocState {
	return p.actual
}
func (p *actualAutoLocStateProperty) SetActual(value state.AutoLocState) error {
	if p.actual != value {
		p.actual = value
		if p.OnActualChanged != nil {
			p.OnActualChanged(value)
		}
		p.SendActualStateChanged()
	}
	return nil
}

// actualLocDirectionProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualLocDirectionProperty struct {
	propertyBase
	actual          state.LocDirection
	OnActualChanged func(state.LocDirection)
}

func (p *actualLocDirectionProperty) GetActual() state.LocDirection {
	return p.actual
}
func (p *actualLocDirectionProperty) SetActual(value state.LocDirection) error {
	if p.actual != value {
		p.actual = value
		if p.OnActualChanged != nil {
			p.OnActualChanged(value)
		}
		p.SendActualStateChanged()
	}
	return nil
}

// actualBlockSideProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualBlockSideProperty struct {
	propertyBase
	actual          model.BlockSide
	OnActualChanged func(model.BlockSide)
}

func (p *actualBlockSideProperty) GetActual() model.BlockSide {
	return p.actual
}
func (p *actualBlockSideProperty) SetActual(value model.BlockSide) error {
	if p.actual != value {
		p.actual = value
		if p.OnActualChanged != nil {
			p.OnActualChanged(value)
		}
		p.SendActualStateChanged()
	}
	return nil
}

// actualBlockProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualBlockProperty struct {
	propertyBase
	actual          state.Block
	OnActualChanged func(state.Block)
}

func (p *actualBlockProperty) GetActual() state.Block {
	return p.actual
}
func (p *actualBlockProperty) SetActual(value state.Block) error {
	if p.actual != value {
		p.actual = value
		if p.OnActualChanged != nil {
			p.OnActualChanged(value)
		}
		p.SendActualStateChanged()
	}
	return nil
}

// actualRouteProperty contains the value of a property in a state object.
// The value contains an actual value.
type actualRouteProperty struct {
	propertyBase
	actual          state.Route
	OnActualChanged func(state.Route)
}

func (p *actualRouteProperty) GetActual() state.Route {
	return p.actual
}
func (p *actualRouteProperty) SetActual(value state.Route) error {
	if p.actual != value {
		p.actual = value
		if p.OnActualChanged != nil {
			p.OnActualChanged(value)
		}
		p.SendActualStateChanged()
	}
	return nil
}
