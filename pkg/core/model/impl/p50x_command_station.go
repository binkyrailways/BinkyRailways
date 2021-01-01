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

// P50xCommandStation extends model P50xCommandStation with implementation methods
type P50xCommandStation interface {
	model.P50xCommandStation
	SerialPortCommandStation
}

type p50xCommandStation struct {
	serialPortCommandStation
}

var _ model.P50xCommandStation = &p50xCommandStation{}

// NewP50xCommandStation creates a new P50X type command station
func NewP50xCommandStation() P50xCommandStation {
	cs := &p50xCommandStation{}
	cs.Initialize()
	return cs
}

// What types of addresses does this command station support?
// The result may vary depending on the type of the optional given entity.
func (cs *p50xCommandStation) GetSupportedAddressTypes(entity model.AddressEntity) []model.AddressType {
	if l, ok := entity.(model.Loc); ok && l != nil {
		return []model.AddressType{model.AddressTypeDcc}
	}
	return nil
}
