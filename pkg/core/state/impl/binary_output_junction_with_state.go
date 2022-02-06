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

type binaryOutputWithState struct {
	Output BinaryOutput
	Active bool
}

// Is the junction in the correct state?
func (ows *binaryOutputWithState) GetIsPrepared(ctx context.Context) bool {
	return ows.Output.GetActive().GetActual(ctx) == ows.Active
}

// Set the junction in the correct state
func (ows *binaryOutputWithState) Prepare(ctx context.Context) {
	ows.Output.GetActive().SetRequested(ctx, ows.Active)
}

// Does this route contains the given output
func (ows *binaryOutputWithState) Contains(ctx context.Context, j Output) bool {
	return ows.Output == j
}

// Gets all entities that must be locked in order to lock me.
func (ows *binaryOutputWithState) ForEachUnderlyingLockableEntities(ctx context.Context, cb func(state.Lockable) error) error {
	// TODO
	return nil
	//return cb(ows.Output)
}
