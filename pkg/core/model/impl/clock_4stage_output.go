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
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model/refs"
)

// Clock4StageOutput adds implementation methods to model.Clock4StageOutput
type Clock4StageOutput interface {
	ModuleEntity
	model.Clock4StageOutput
}

type clock4StageOutput struct {
	output

	Address1         model.Address `xml:"Address1"`
	Address2         model.Address `xml:"Address2"`
	MorningPattern   *int          `xml:"MorningPattern,omitempty"`
	AfternoonPattern *int          `xml:"AfternoonPattern,omitempty"`
	EveningPattern   *int          `xml:"EveningPattern,omitempty"`
	NightPattern     *int          `xml:"NightPattern,omitempty"`
}

var _ Clock4StageOutput = &clock4StageOutput{}

func newClock4StageOutput() *clock4StageOutput {
	cso := &clock4StageOutput{}
	cso.output.Initialize()
	return cso
}

// Accept a visit by the given visitor
func (cso *clock4StageOutput) Accept(v model.EntityVisitor) interface{} {
	return v.VisitClock4StageOutput(cso)
}

// Get the Address of the entity
func (cso *clock4StageOutput) GetAddress() model.Address {
	return cso.GetAddress1()
}

// Set the Address of the entity
func (cso *clock4StageOutput) SetAddress(value model.Address) error {
	return cso.SetAddress1(value)
}

// Call the given callback for all (non-empty) addresses configured in this
// entity with the direction their being used.
func (cso *clock4StageOutput) ForEachAddressUsage(cb func(model.AddressUsage)) {
	if !cso.Address1.IsEmpty() {
		cb(model.AddressUsage{
			Address:   cso.Address1,
			Direction: model.AddressDirectionOutput,
		})
	}
	if !cso.Address2.IsEmpty() {
		cb(model.AddressUsage{
			Address:   cso.Address2,
			Direction: model.AddressDirectionOutput,
		})
	}
}

// Address of first clock bit.
// This is an output signal.
func (cso *clock4StageOutput) GetAddress1() model.Address {
	return cso.Address1
}
func (cso *clock4StageOutput) SetAddress1(value model.Address) error {
	if !cso.Address1.Equals(value) {
		cso.Address1 = value
		cso.OnModified()
	}
	return nil
}

// Address of second clock bit.
// This is an output signal.
func (cso *clock4StageOutput) GetAddress2() model.Address {
	return cso.Address2
}
func (cso *clock4StageOutput) SetAddress2(value model.Address) error {
	if !cso.Address2.Equals(value) {
		cso.Address2 = value
		cso.OnModified()
	}
	return nil
}

// Bit pattern used for "morning".
func (cso *clock4StageOutput) GetMorningPattern() int {
	return refs.IntValue(cso.MorningPattern, model.DefaultClock4StageOutputMorningPattern)
}
func (cso *clock4StageOutput) SetMorningPattern(value int) error {
	if cso.GetMorningPattern() != value {
		cso.MorningPattern = refs.NewInt(value)
		cso.OnModified()
	}
	return nil
}

// Bit pattern used for "afternoon".
func (cso *clock4StageOutput) GetAfternoonPattern() int {
	return refs.IntValue(cso.AfternoonPattern, model.DefaultClock4StageOutputAfternoonPattern)
}
func (cso *clock4StageOutput) SetAfternoonPattern(value int) error {
	if cso.GetAfternoonPattern() != value {
		cso.AfternoonPattern = refs.NewInt(value)
		cso.OnModified()
	}
	return nil
}

// Bit pattern used for "evening".
func (cso *clock4StageOutput) GetEveningPattern() int {
	return refs.IntValue(cso.EveningPattern, model.DefaultClock4StageOutputEveningPattern)
}
func (cso *clock4StageOutput) SetEveningPattern(value int) error {
	if cso.GetEveningPattern() != value {
		cso.EveningPattern = refs.NewInt(value)
		cso.OnModified()
	}
	return nil
}

// Bit pattern used for "night".
func (cso *clock4StageOutput) GetNightPattern() int {
	return refs.IntValue(cso.NightPattern, model.DefaultClock4StageOutputNightPattern)
}
func (cso *clock4StageOutput) SetNightPattern(value int) error {
	if cso.GetNightPattern() != value {
		cso.NightPattern = refs.NewInt(value)
		cso.OnModified()
	}
	return nil
}
