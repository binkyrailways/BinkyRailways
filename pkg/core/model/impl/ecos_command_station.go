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

// EcosCommandStation extends model EcosCommandStation with implementation methods
type EcosCommandStation interface {
	model.EcosCommandStation
	PersistentEntity
}

type ecosCommandStation struct {
	commandStation

	HostName *string `xml:"HostName,omitempty"`
}

var _ model.EcosCommandStation = &ecosCommandStation{}

// NewEcosCommandStation creates a new ECOS type command station
func NewEcosCommandStation() EcosCommandStation {
	cs := &ecosCommandStation{}
	cs.Initialize()
	return cs
}

// Accept a visit by the given visitor
func (cs *ecosCommandStation) Accept(v model.EntityVisitor) interface{} {
	return v.VisitEcosCommandStation(cs)
}

// What types of addresses does this command station support?
// The result may vary depending on the type of the optional given entity.
func (cs *ecosCommandStation) GetSupportedAddressTypes(entity model.AddressEntity) []model.AddressType {
	if l, ok := entity.(model.Loc); ok && l != nil {
		return []model.AddressType{model.AddressTypeDcc, model.AddressTypeMotorola, model.AddressTypeMfx}
	}
	return []model.AddressType{model.AddressTypeLocoNet}
}

// Network hostname of the command station
func (cs *ecosCommandStation) GetHostName() string {
	return refs.StringValue(cs.HostName, "ecos")
}
func (cs *ecosCommandStation) SetHostName(value string) error {
	if cs.GetHostName() != value {
		cs.HostName = refs.NewString(value)
		cs.OnModified()
	}
	return nil
}

func (cs *ecosCommandStation) Upgrade() {
	// Nothing needed
}
