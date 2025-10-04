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

// Helper to map binkynet device to esphome platform
type devicePlatform struct {
	Platform        string
	deviceFile      *DeviceFile
	configurePin    func(*Pin)
	configureOutput func(*Output)
}

// Configure the given esphome binary_sensor to use this device platform
func (dp devicePlatform) ConfigureBinarySensor(sensor *BinarySensor) {
	sensor.Platform = dp.Platform
	if sensor.Pin != nil {
		if dp.configurePin != nil {
			dp.configurePin(sensor.Pin)
		}
		sensor.Pin.Mode = &PinMode{
			Input: true,
		}
	}
}

// Configure the given esphome output to use this device platform
func (dp devicePlatform) ConfigureOutput(output *Output) {
	output.Platform = dp.Platform
	if dp.configureOutput != nil {
		dp.configureOutput(output)
	}
}

// Configure the given esphome switch to use this device platform
func (dp devicePlatform) ConfigureSwitch(sw *Switch) {
	sw.Platform = dp.Platform
	if sw.Pin != nil {
		if dp.configurePin != nil {
			dp.configurePin(sw.Pin)
		}
		sw.Pin.Mode = &PinMode{
			Output: true,
		}
	}
}
