// Copyright 2025 Ewout Prangsma
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

package esphome

import (
	"fmt"

	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// Set of DeviceFile's
type DeviceFileSet struct {
	deviceFiles map[string]*DeviceFile
	platforms   map[api.DeviceID]*devicePlatform
}

// Gets the platform for the device of the given pin.
func (fs *DeviceFileSet) getPlatform(pin model.BinkyNetDevicePin) (*devicePlatform, error) {
	if platform, ok := fs.platforms[pin.GetDeviceID()]; !ok {
		return nil, fmt.Errorf("Platform not found for device with ID '%s' in %s", pin.GetDeviceID())
	} else {
		return platform, nil
	}
}
