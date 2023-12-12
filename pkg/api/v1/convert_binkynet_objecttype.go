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

package v1

import (
	context "context"

	api "github.com/binkynet/BinkyNet/apis/v1"
)

// FromModel converts a object type to an API type
func (dst *BinkyNetObjectType) FromModel(ctx context.Context, src api.ObjectType) error {
	switch src {
	case api.ObjectTypeBinaryOutput:
		*dst = BinkyNetObjectType_BINARYOUTPUT
	case api.ObjectTypeBinarySensor:
		*dst = BinkyNetObjectType_BINARYSENSOR
	case api.ObjectTypeRelaySwitch:
		*dst = BinkyNetObjectType_RELAYSWITCH
	case api.ObjectTypeServoSwitch:
		*dst = BinkyNetObjectType_SERVOSWITCH
	case api.ObjectTypeTrackInverter:
		*dst = BinkyNetObjectType_TRACKINVERTER
	case api.ObjectTypeMagneticSwitch:
		*dst = BinkyNetObjectType_MAGNETICSWITCH
	}
	return nil
}

// ToModel converts a object type from an API type
func (src BinkyNetObjectType) ToModel(ctx context.Context) (api.ObjectType, error) {
	switch src {
	case BinkyNetObjectType_BINARYOUTPUT:
		return api.ObjectTypeBinaryOutput, nil
	case BinkyNetObjectType_BINARYSENSOR:
		return api.ObjectTypeBinarySensor, nil
	case BinkyNetObjectType_RELAYSWITCH:
		return api.ObjectTypeRelaySwitch, nil
	case BinkyNetObjectType_SERVOSWITCH:
		return api.ObjectTypeServoSwitch, nil
	case BinkyNetObjectType_TRACKINVERTER:
		return api.ObjectTypeTrackInverter, nil
	case BinkyNetObjectType_MAGNETICSWITCH:
		return api.ObjectTypeMagneticSwitch, nil
	}
	return api.ObjectTypeBinaryOutput, InvalidArgument("Unknown BinkyNetObjectType: %s", src)
}
