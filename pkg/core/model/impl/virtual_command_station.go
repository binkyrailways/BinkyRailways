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
)

// VirtualCommandStation extends model VirtualCommandStation with implementation methods
type VirtualCommandStation interface {
	model.VirtualCommandStation
	PersistentEntity
}

type virtualCommandStation struct {
	commandStation
}

var _ model.VirtualCommandStation = &virtualCommandStation{}

// NewVirtualCommandStation creates a new virtual mode type command station
func NewVirtualCommandStation() VirtualCommandStation {
	cs := &virtualCommandStation{}
	cs.Initialize()
	return cs
}

// Accept a visit by the given visitor
func (cs *virtualCommandStation) Accept(v model.EntityVisitor) interface{} {
	return v.VisitVirtualCommandStation(cs)
}

// What types of addresses does this command station support?
// The result may vary depending on the type of the optional given entity.
func (cs *virtualCommandStation) GetSupportedAddressTypes(entity model.AddressEntity) []model.AddressType {
	return []model.AddressType{model.AddressTypeLocoNet, model.AddressTypeDcc, model.AddressTypeMotorola}
}

func (cs *virtualCommandStation) Upgrade() {
	// Nothing needed
}
