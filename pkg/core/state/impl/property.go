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

const (
	setRequestTimeout = time.Millisecond
)

// comparableProperty implements a property of generic type.
type comparableProperty[T comparable] struct {
	comparableActualProperty[T]
	requested      T
	requestChanges util.SliceWithIdEntries[func(context.Context, T)]
}

// Configure the values of the property
func (p *comparableProperty[T]) Configure(name string, subject state.Subject, validate func(T) T, dispatcher state.EventDispatcher, exclusive util.Exclusive) {
	p.comparableActualProperty.Configure(p, name, subject, validate, dispatcher, exclusive)
}

func (p *comparableProperty[T]) IsConsistent(ctx context.Context) bool {
	return p.GetActual(ctx) == p.GetRequested(ctx)
}
func (p *comparableProperty[T]) GetRequested(ctx context.Context) T {
	return p.requested
}
func (p *comparableProperty[T]) SetRequested(ctx context.Context, value T) error {
	value = p.validate(value)
	return p.exclusive.Exclusive(ctx, setRequestTimeout, "comparableProperty.SetRequested."+p.Name, func(ctx context.Context) error {
		if p.requested != value {
			p.requested = value
			for _, cb := range p.requestChanges {
				cb.Value(ctx, value)
			}
			p.SendRequestedStateChanged()
		}
		return nil
	})
}

// Subscribe to requested changes
func (p *comparableProperty[T]) SubscribeRequestChanges(cb func(context.Context, T)) context.CancelFunc {
	var cancel context.CancelFunc
	p.exclusive.Exclusive(context.Background(), subscribeTimeout, "bool.SubscribeRequestChanges", func(c context.Context) error {
		cancel = p.requestChanges.Append(cb)
		return nil
	})
	return cancel
}

// BoolProperty contains the value of a property in a state object.
// The value contains a requested value and an actual value.
type boolProperty struct {
	comparableProperty[bool]
}

// intProperty contains the value of a property in a state object.
// The value contains a requested value and an actual value.
type intProperty struct {
	comparableProperty[int]
}

// locDirectionProperty contains the value of a property in a state object.
// The value contains a requested value and an actual value.
type locDirectionProperty struct {
	comparableProperty[state.LocDirection]
}

// locDirectionProperty contains the value of a property in a state object.
// The value contains a requested value and an actual value.
type switchDirectionProperty struct {
	comparableProperty[model.SwitchDirection]
}
