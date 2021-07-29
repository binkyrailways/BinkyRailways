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

import "github.com/binkyrailways/BinkyRailways/pkg/core/model"

// DccOverRs232CommandStation extends model DccOverRs232CommandStation with implementation methods
type DccOverRs232CommandStation interface {
	model.DccOverRs232CommandStation
	SerialPortCommandStation
}

type dccOverRs232CommandStation struct {
	serialPortCommandStation
}

var _ model.DccOverRs232CommandStation = &dccOverRs232CommandStation{}

// NewDccOverRs232CommandStation creates a new DCC-over-rs232 type command station
func NewDccOverRs232CommandStation(p model.Package) DccOverRs232CommandStation {
	cs := &dccOverRs232CommandStation{}
	cs.Initialize()
	cs.EnsureID()
	cs.SetPackage(p)
	cs.SetDescription("New Dcc over RS232 command station")
	return cs
}

// GetEntityType returns the type of this entity
func (cs *dccOverRs232CommandStation) GetEntityType() string {
	return TypeDccOverRs232CommandStation
}

// Accept a visit by the given visitor
func (cs *dccOverRs232CommandStation) Accept(v model.EntityVisitor) interface{} {
	return v.VisitDccOverRs232CommandStation(cs)
}

// What types of addresses does this command station support?
// The result may vary depending on the type of the optional given entity.
func (cs *dccOverRs232CommandStation) GetSupportedAddressTypes(entity model.AddressEntity) []model.AddressType {
	result := []model.AddressType{model.AddressTypeDcc}
	return result
}
