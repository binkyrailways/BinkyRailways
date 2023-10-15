// Copyright 2023 Ewout Prangsma
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

// BidibCommandStation extends model BidibCommandStation with implementation methods
type BidibCommandStation interface {
	model.BidibCommandStation
	SerialPortCommandStation
}

type bidibCommandStation struct {
	serialPortCommandStation
}

var _ model.BidibCommandStation = &bidibCommandStation{}

// NewBidibCommandStation creates a new bidib type command station
func NewBidibCommandStation(p model.Package) BidibCommandStation {
	cs := &bidibCommandStation{}
	cs.Initialize()
	cs.EnsureID()
	cs.SetPackage(p)
	cs.SetDescription("New Bidib command station")
	return cs
}

// GetEntityType returns the type of this entity
func (cs *bidibCommandStation) GetEntityType() string {
	return TypeBidibCommandStation
}

// Accept a visit by the given visitor
func (cs *bidibCommandStation) Accept(v model.EntityVisitor) interface{} {
	return v.VisitBidibNetCommandStation(cs)
}

// What types of addresses does this command station support?
// The result may vary depending on the type of the optional given entity.
func (cs *bidibCommandStation) GetSupportedAddressTypes(entity model.AddressEntity) []model.AddressType {
	result := []model.AddressType{model.AddressTypeDcc}
	return result
}
