// Copyright 2020 Ewout Prangsma
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
)

// BinaryOutput adds implementation methods to model.BinaryOutput
type BinaryOutput interface {
	ModuleEntity
	model.BinaryOutput
}

type binaryOutput struct {
	output

	Address          model.Address          `xml:"Address"`
	BinaryOutputType model.BinaryOutputType `xml:BinaryOutputType"`
}

var _ BinaryOutput = &binaryOutput{}

func newBinaryOutput() *binaryOutput {
	sw := &binaryOutput{}
	sw.EnsureID()
	sw.SetDescription("New binary output")
	sw.output.Initialize()
	return sw
}

// GetEntityType returns the type of this entity
func (bo *binaryOutput) GetEntityType() string {
	return TypeBinaryOutput
}

// Accept a visit by the given visitor
func (bo *binaryOutput) Accept(v model.EntityVisitor) interface{} {
	return v.VisitBinaryOutput(bo)
}

// ForEachAddress iterates all addresses in this entity and any child entities.
func (bo *binaryOutput) ForEachAddress(cb func(addr model.Address, onUpdate func(context.Context, model.Address) error)) {
	cb(bo.Address, bo.SetAddress)
}

// Get the Address of the entity
func (bo *binaryOutput) GetAddress() model.Address {
	return bo.Address
}

// Set the Address of the entity
func (bo *binaryOutput) SetAddress(ctx context.Context, value model.Address) error {
	if !bo.Address.Equals(value) {
		bo.Address = value
		bo.OnModified()
	}
	return nil
}

// Type of binary output
func (bo *binaryOutput) GetBinaryOutputType() model.BinaryOutputType {
	if bo.BinaryOutputType == "" {
		return model.BinaryOutputTypeDefault
	}
	return bo.BinaryOutputType
}

func (bo *binaryOutput) SetBinaryOutputType(ctx context.Context, value model.BinaryOutputType) error {
	if value == model.BinaryOutputTypeDefault {
		value = ""
	}
	if value != bo.BinaryOutputType {
		bo.BinaryOutputType = value
		bo.OnModified()
	}
	return nil
}

// Call the given callback for all (non-empty) addresses configured in this
// entity with the direction their being used.
func (bo *binaryOutput) ForEachAddressUsage(cb func(model.AddressUsage)) {
	if !bo.Address.IsEmpty() {
		cb(model.AddressUsage{
			Address:   bo.Address,
			Direction: model.AddressDirectionOutput,
		})
	}
}
