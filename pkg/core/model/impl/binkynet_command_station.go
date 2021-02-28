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
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model/refs"
)

// BinkyNetCommandStation extends model BinkyNetCommandStation with implementation methods
type BinkyNetCommandStation interface {
	model.BinkyNetCommandStation
	PersistentEntity
}

type binkyNetCommandStation struct {
	commandStation

	GRPCPort *int `xml:"GRPCPort,omitempty"`
}

var _ model.BinkyNetCommandStation = &binkyNetCommandStation{}

// NewBinkyNetCommandStation creates a new BinkyNet type command station
func NewBinkyNetCommandStation() BinkyNetCommandStation {
	cs := &binkyNetCommandStation{}
	cs.Initialize()
	cs.EnsureID()
	cs.SetDescription("New BinkyNet command station")
	return cs
}

// GetEntityType returns the type of this entity
func (cs *binkyNetCommandStation) GetEntityType() string {
	return TypeBinkyNetCommandStation
}

// Accept a visit by the given visitor
func (cs *binkyNetCommandStation) Accept(v model.EntityVisitor) interface{} {
	return v.VisitBinkyNetCommandStation(cs)
}

// What types of addresses does this command station support?
// The result may vary depending on the type of the optional given entity.
func (cs *binkyNetCommandStation) GetSupportedAddressTypes(entity model.AddressEntity) []model.AddressType {
	if l, ok := entity.(model.Loc); ok && l != nil {
		return []model.AddressType{model.AddressTypeDcc}
	}
	return []model.AddressType{model.AddressTypeBinkyNet}
}

// Network Port of the command station
func (cs *binkyNetCommandStation) GetGRPCPort() int {
	return refs.IntValue(cs.GRPCPort, 8823)
}
func (cs *binkyNetCommandStation) SetGRPCPort(value int) error {
	if cs.GetGRPCPort() != value {
		cs.GRPCPort = refs.NewInt(value)
		cs.OnModified()
	}
	return nil
}

func (cs *binkyNetCommandStation) Upgrade() {
	// Nothing needed
}
