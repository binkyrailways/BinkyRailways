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
	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// Create an WIFI signal strength sensor (measured in decibel-milliwatts (dBm),
// always negative and the closer they are to zero, the better the signal is) and add it to the given DeviceFile.
func (f *DeviceFile) createWifiSignalSensor(moduleID string) (*Sensor, error) {

	// Create basic sensor
	name := model.NormalizeName("wifi_signal_sensor")
	sensor := Sensor{}
	sensor.Id = name
	sensor.Name = name
	sensor.Platform = "wifi_signal"
	sensor.MQTTComponent.StateTopic = api.GetMqttModuleInfoPrefix(moduleID) + api.WifiSignalTopicSuffix

	f.Sensors = append(f.Sensors, sensor)
	return &f.Sensors[len(f.Sensors)-1], nil
}
