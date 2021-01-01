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

// LocoBufferCommandStation extends model LocoBufferCommandStation with implementation methods
type LocoBufferCommandStation interface {
	model.LocoBufferCommandStation
	SerialPortCommandStation
}

type locoBufferCommandStation struct {
	serialPortCommandStation
}

var _ model.LocoBufferCommandStation = &locoBufferCommandStation{}

// NewLocoBufferCommandStation creates a new LB type command station
func NewLocoBufferCommandStation() LocoBufferCommandStation {
	cs := &locoBufferCommandStation{}
	cs.Initialize()
	return cs
}

// What types of addresses does this command station support?
// The result may vary depending on the type of the optional given entity.
func (cs *locoBufferCommandStation) GetSupportedAddressTypes(entity model.AddressEntity) []model.AddressType {
	result := []model.AddressType{model.AddressTypeLocoNet}
	if l, ok := entity.(model.Loc); ok && l != nil {
		result = append(result, model.AddressTypeDcc, model.AddressTypeMotorola)
	}
	return result
}
