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

// FromModel converts a device type to an API type
func (dst *BinkyNetDeviceType) FromModel(ctx context.Context, src api.DeviceType) error {
	switch src {
	case api.DeviceTypeMCP23008:
		*dst = BinkyNetDeviceType_MCP23008
	case api.DeviceTypeMCP23017:
		*dst = BinkyNetDeviceType_MCP23017
	case api.DeviceTypePCA9685:
		*dst = BinkyNetDeviceType_PCA9685
	case api.DeviceTypePCF8574:
		*dst = BinkyNetDeviceType_PCF8574
	case api.DeviceTypeADS1115:
		*dst = BinkyNetDeviceType_ADS1115
	case api.DeviceTypeBinkyCarSensor:
		*dst = BinkyNetDeviceType_BINKYCARSENSOR
	case api.DeviceTypeMQTT:
		*dst = BinkyNetDeviceType_MQTT
	}
	return nil
}

// ToModel converts a device type from an API type
func (src BinkyNetDeviceType) ToModel(ctx context.Context) (api.DeviceType, error) {
	switch src {
	case BinkyNetDeviceType_MCP23008:
		return api.DeviceTypeMCP23008, nil
	case BinkyNetDeviceType_MCP23017:
		return api.DeviceTypeMCP23017, nil
	case BinkyNetDeviceType_PCA9685:
		return api.DeviceTypePCA9685, nil
	case BinkyNetDeviceType_PCF8574:
		return api.DeviceTypePCF8574, nil
	case BinkyNetDeviceType_ADS1115:
		return api.DeviceTypeADS1115, nil
	case BinkyNetDeviceType_BINKYCARSENSOR:
		return api.DeviceTypeBinkyCarSensor, nil
	case BinkyNetDeviceType_MQTT:
		return api.DeviceTypeMQTT, nil
	}
	return api.DeviceTypeMCP23008, InvalidArgument("Unknown BinkyNetDeviceType: %s", src)
}
