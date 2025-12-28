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
	"iter"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model/refs"
)

// BinaryOutput adds implementation methods to model.BinaryOutput
type BinaryOutput interface {
	ModuleEntity
	model.BinaryOutput
}

type binaryOutput struct {
	output

	Address          model.Address          `xml:"Address"`
	BinaryOutputType model.BinaryOutputType `xml:"BinaryOutputType"`
	ActiveText       *string                `xml:"ActiveText,omitempty"`
	InactiveText     *string                `xml:"InactiveText,omitempty"`
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
		if bo.GetDescription() == bo.Address.Value {
			bo.SetDescription(value.Value)
		}
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

// Text displayed when output is in active state
func (bo *binaryOutput) GetActiveText() string {
	return refs.StringValue(bo.ActiveText, "")
}
func (bo *binaryOutput) SetActiveText(ctx context.Context, value string) error {
	if bo.GetActiveText() != value {
		bo.ActiveText = refs.NewString(value)
		bo.OnModified()
	}
	return nil
}

// Text displayed when output is in inactive state
func (bo *binaryOutput) GetInactiveText() string {
	return refs.StringValue(bo.InactiveText, "")
}
func (bo *binaryOutput) SetInactiveText(ctx context.Context, value string) error {
	if bo.GetInactiveText() != value {
		bo.InactiveText = refs.NewString(value)
		bo.OnModified()
	}
	return nil
}

// Return a sequence of all (non-empty) addresses configured in this
// entity with the direction their being used.
func (bo *binaryOutput) AllAddressUsages() iter.Seq[model.AddressUsage] {
	return func(yield func(model.AddressUsage) bool) {
		if !bo.Address.IsEmpty() {
			yield(model.AddressUsage{
				Address:   bo.Address,
				Direction: model.AddressDirectionOutput,
			})
		}
	}
}
