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

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

type passiveJunctionWithState struct {
	Junction Junction
}

// Is the junction in the correct state?
func (jws *passiveJunctionWithState) GetIsPrepared(ctx context.Context) bool {
	return true
}

// Set the junction in the correct state
func (jws *passiveJunctionWithState) Prepare(context.Context) {
	// Nothing here
}

// Does this route contains the given junction
func (jws *passiveJunctionWithState) Contains(ctx context.Context, j Junction) bool {
	return jws.Junction == j
}

// Gets all entities that must be locked in order to lock me.
func (jws *passiveJunctionWithState) ForEachUnderlyingLockableEntities(ctx context.Context, cb func(state.Lockable)) {
	cb(jws.Junction)
}

// Is this junction for this state in the non-straight position?
func (jws *passiveJunctionWithState) GetIsNonStraight(context.Context) bool {
	return false
}
