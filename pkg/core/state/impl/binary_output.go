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

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// BinaryOutput adds implementation functions to state.BinaryOutput.
type BinaryOutput interface {
	Entity
	state.BinaryOutput
}

type binaryOutput struct {
	output

	commandStation CommandStation
	active         boolProperty
}

// Create a new entity
func newBinaryOutput(en model.BinaryOutput, railway Railway) BinaryOutput {
	bo := &binaryOutput{
		output: newOutput(en, railway),
	}
	bo.active.Configure(bo, railway, railway)
	bo.active.OnRequestedChanged = func(ctx context.Context, value bool) {
		if bo.commandStation != nil {
			bo.commandStation.SendOutputActive(ctx, bo)
		}
	}
	return bo
}

// Unique ID of the module containing this entity
func (b *binaryOutput) GetModuleID() string {
	return b.getBinaryOutput().GetModule().GetID()
}

// getBinaryOutput returns the entity as BinaryOutput.
func (bo *binaryOutput) getBinaryOutput() model.BinaryOutput {
	return bo.GetEntity().(model.BinaryOutput)
}

// Try to prepare the entity for use.
// Returns nil when the entity is successfully prepared,
// returns an error otherwise.
func (bo *binaryOutput) TryPrepareForUse(ctx context.Context, _ state.UserInterface, _ state.Persistence) error {
	// Resolve command station
	var err error
	bo.commandStation, err = bo.railway.SelectCommandStation(ctx, bo.getBinaryOutput())
	if err != nil {
		return err
	}
	if bo.commandStation == nil {
		return fmt.Errorf("Output does not have a commandstation attached.")
	}
	bo.commandStation.RegisterOutput(bo)

	return nil
}

// Address of the entity
func (bo *binaryOutput) GetAddress() model.Address {
	return bo.getBinaryOutput().GetAddress()
}

// Is this output in the 'active' state?
func (bo *binaryOutput) GetActive() state.BoolProperty {
	return &bo.active
}