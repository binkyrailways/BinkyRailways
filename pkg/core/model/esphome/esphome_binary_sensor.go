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
func (fs *DeviceFileSet) createBinarySensor(
	objModel model.BinkyNetObject, namePrefix string,
	connName api.ConnectionName, pinsIndex int) (*BinarySensor, *devicePlatform, error) {

	// Get connection
	conn, err := getConnection(objModel, connName)
	if err != nil {
		return nil, nil, err
	}
	pin, err := getPin(objModel, conn, pinsIndex)
	if err != nil {
		return nil, nil, err
	}

	// Find platform
	platform, err := fs.getPlatform(pin)
	if err != nil {
		return nil, nil, err
	}

	// Create basic sensor
	sensor := BinarySensor{}
	sensor.Id = model.NormalizeName(namePrefix + string(objModel.GetObjectID()))
	sensor.Name = model.NormalizeName(namePrefix + string(objModel.GetObjectID()))
	sensor.StateTopic = objModel.GetMQTTStateTopic(connName)
	sensor.OnState = &Trigger{
		Then: []Action{
			{"switch.turn_on": "led_red"},
			{"delay": "0.2s"},
			{"switch.turn_off": "led_red"},
		},
	}
	sensor.Pin = &Pin{
		Number: fmt.Sprintf("%d", pin.GetIndex()-1),
	}
	platform.ConfigureBinarySensor(&sensor)

	f := platform.deviceFile
	f.BinarySensors = append(f.BinarySensors, sensor)
	return &f.BinarySensors[len(f.BinarySensors)-1], platform, nil
}
