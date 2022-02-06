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

// OutputWithState abstracts state of OutputWithState implementations
type OutputWithState interface {
	// Is the junction in the correct state?
	GetIsPrepared(context.Context) bool

	// Set the junction in the correct state
	Prepare(context.Context)

	// Does this route contains the given output
	Contains(context.Context, Output) bool

	// Gets all entities that must be locked in order to lock me.
	ForEachUnderlyingLockableEntities(context.Context, func(state.Lockable) error) error
}

// newOutputWithState constructs a state implementation for the given output with state.
func newOutputWithState(ctx context.Context, ows model.OutputWithState, rw Railway) (OutputWithState, error) {
	o, err := rw.GetOutput(ows.GetOutput().GetID())
	if err != nil {
		return nil, err
	}

	switch tows := ows.(type) {
	case model.BinaryOutputWithState:
		oImpl, ok := o.(BinaryOutput)
		if !ok {
			return nil, fmt.Errorf("Output %s (%T) does not implement BinaryOutput", ows, ows)
		}
		return &binaryOutputWithState{
			Output: oImpl,
			Active: tows.GetActive(),
		}, nil
	default:
		return nil, fmt.Errorf("Unsupport OutputWithState %T", ows)
	}
}
