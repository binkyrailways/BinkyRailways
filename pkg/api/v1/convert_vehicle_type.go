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

package v1

import (
	context "context"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// FromModel converts a VehicleType to an API type
func (dst *VehicleType) FromModel(ctx context.Context, src model.VehicleType) error {
	switch src {
	case model.VehicleTypeLoc:
		*dst = VehicleType_LOC
	case model.VehicleTypeCar:
		*dst = VehicleType_CAR
	}
	return nil
}

// ToModel converts a VehicleType from an API type
func (src VehicleType) ToModel(ctx context.Context) (model.VehicleType, error) {
	switch src {
	case VehicleType_LOC:
		return model.VehicleTypeLoc, nil
	case VehicleType_CAR:
		return model.VehicleTypeCar, nil
	}
	return model.VehicleTypeLoc, InvalidArgument("Unknown VehicleType: %s", src)
}
