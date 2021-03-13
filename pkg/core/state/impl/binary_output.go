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
}

// Create a new entity
func newBinaryOutput(en model.BinaryOutput, railway Railway) BinaryOutput {
	s := &binaryOutput{
		output: newOutput(en, railway),
	}
	return s
}

// getBinaryOutput returns the entity as BinaryOutput.
func (s *output) getBinaryOutput() model.BinaryOutput {
	return s.GetEntity().(model.BinaryOutput)
}

// Try to prepare the entity for use.
// Returns nil when the entity is successfully prepared,
// returns an error otherwise.
func (s *binaryOutput) TryPrepareForUse(ctx context.Context, _ state.UserInterface, _ state.Persistence) error {
	// Resolve command station
	cs, err := s.railway.SelectCommandStation(ctx, s.getBinaryOutput())
	if err != nil {
		return err
	}
	cs.RegisterOutput(s)

	return nil
}

// Address of the entity
func (s *binaryOutput) GetAddress() model.Address {
	return s.getBinaryOutput().GetAddress()
}
