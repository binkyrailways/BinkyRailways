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

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// BoolProperty contains the value of a property in a state object.
// The value contains a requested value and an actual value.
type boolProperty struct {
	actualBoolProperty
	requested          bool
	OnRequestedChanged func(context.Context, bool)
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
			if p.OnRequestedChanged != nil {
				p.OnRequestedChanged(ctx, value)
			}
			p.SendRequestedStateChanged(p)
		}
		return nil
	})
}

// intProperty contains the value of a property in a state object.
// The value contains a requested value and an actual value.
type intProperty struct {
	actualIntProperty
	requested          int
	OnRequestedChanged func(context.Context, int)
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
			if p.OnRequestedChanged != nil {
				p.OnRequestedChanged(ctx, value)
			}
			p.SendRequestedStateChanged(p)
		}
		return nil
	})
}

// locDirectionProperty contains the value of a property in a state object.
// The value contains a requested value and an actual value.
type locDirectionProperty struct {
	actualLocDirectionProperty
	requested          state.LocDirection
	OnRequestedChanged func(context.Context, state.LocDirection)
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
			if p.OnRequestedChanged != nil {
				p.OnRequestedChanged(ctx, value)
			}
			p.SendRequestedStateChanged(p)
		}
		return nil
	})
}