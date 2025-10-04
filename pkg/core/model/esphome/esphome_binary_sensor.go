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

// Create a binary sensor for the connection with given name of the given object and add it to the given DeviceFile.
func (f *DeviceFile) createBinarySensor(
	objModel model.BinkyNetObject, namePrefix string,
	connName api.ConnectionName, pinsIndex int) (*BinarySensor, error) {

	// Create basic sensor
	sensor := BinarySensor{}
	sensor.Id = name(namePrefix + string(objModel.GetObjectID()))
	sensor.Name = name(namePrefix + string(objModel.GetObjectID()))
	sensor.StateTopic = objModel.GetMQTTStateTopic(connName)
	sensor.OnState = &Trigger{
		Then: []Action{
			{"switch.turn_on": "led_red"},
			{"delay": "0.2s"},
			{"switch.turn_off": "led_red"},
		},
	}
	conn, err := getConnection(objModel, connName)
	if err != nil {
		return nil, err
	}
	pin, err := getPin(objModel, conn, pinsIndex)
	if err != nil {
		return nil, err
	}
	sensor.Pin = &Pin{
		Number: fmt.Sprintf("%d", pin.GetIndex()-1),
	}
	if err := sensor.Pin.applyInvert(conn); err != nil {
		return nil, err
	}
	if err := sensor.applyPlatformConfiguration(f, objModel, pin); err != nil {
		return nil, err
	}
	f.BinarySensors = append(f.BinarySensors, sensor)
	return &f.BinarySensors[len(f.BinarySensors)-1], nil
}

// Apply platform specific configuration on this sensor
func (sensor *BinarySensor) applyPlatformConfiguration(f *DeviceFile, objModel model.BinkyNetObject, pin model.BinkyNetDevicePin) error {
	if platform, ok := f.platforms[pin.GetDeviceID()]; !ok {
		return fmt.Errorf("Platform not found for device with ID '%s' in %s", pin.GetDeviceID(), objModel.GetDescription())
	} else {
		platform.ConfigureBinarySensor(sensor)
	}
	return nil
}
