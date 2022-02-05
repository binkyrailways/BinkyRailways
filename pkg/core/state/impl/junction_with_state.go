// Copyright 2022 Ewout Prangsma
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

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// JunctionWithState abstracts state of JunctionWithState implementations
type JunctionWithState interface {
	// Is the junction in the correct state?
	GetIsPrepared(context.Context) bool

	// Set the junction in the correct state
	Prepare(context.Context)

	// Does this route contains the given junction
	Contains(context.Context, Junction) bool

	// Gets all entities that must be locked in order to lock me.
	ForEachUnderlyingLockableEntities(context.Context, func(state.Lockable))

	// Is this junction for this state in the non-straight position?
	GetIsNonStraight(context.Context) bool
}

// newJunctionWithState constructs a state implementation for the given junction with state.
func newJunctionWithState(ctx context.Context, jws model.JunctionWithState, rw Railway) (JunctionWithState, error) {
	j, err := rw.GetJunction(jws.GetJunction().GetID())
	if err != nil {
		return nil, err
	}

	switch tjws := jws.(type) {
	case model.SwitchWithState:
		jImpl, ok := j.(Switch)
		if !ok {
			return nil, fmt.Errorf("Junction %s (%T) does not implement Switch", jws, jws)
		}
		return &switchJunctionWithState{
			Junction:  jImpl,
			Direction: tjws.GetDirection(),
		}, nil
	case model.PassiveJunctionWithState:
		jImpl, ok := j.(Switch)
		if !ok {
			return nil, fmt.Errorf("Junction %s (%T) does not implement Junction", jws, jws)
		}
		return &passiveJunctionWithState{Junction: jImpl}, nil
	default:
		return nil, fmt.Errorf("Unsupport JunctionWithState %T", jws)
	}
}
