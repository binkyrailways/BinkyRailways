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

// Create a switch for the connection with given name of the given object and add it to the given DeviceFile.
func (fs *DeviceFileSet) createSwitch(
	objModel model.BinkyNetObject, namePrefix string,
	connName api.ConnectionName, pinsIndex int) (*Switch, error) {

	// Fetch connection & pin
	conn, err := getConnection(objModel, connName)
	if err != nil {
		return nil, err
	}
	pin, err := getPin(objModel, conn, pinsIndex)
	if err != nil {
		return nil, err
	}

	// Find platform
	platform, err := fs.getPlatform(pin)
	if err != nil {
		return nil, err
	}

	// Create basic switch
	sw := Switch{}
	sw.Id = model.NormalizeName(namePrefix + string(objModel.GetObjectID()))
	sw.Name = model.NormalizeName(namePrefix + string(objModel.GetObjectID()))
	sw.StateTopic = objModel.GetMQTTStateTopic(connName)
	sw.CommandTopic = objModel.GetMQTTCommandTopic(connName)
	sw.Pin = &Pin{
		Number: fmt.Sprintf("%d", pin.GetIndex()-1),
	}
	if err := sw.Pin.applyInvert(conn); err != nil {
		return nil, err
	}
	platform.ConfigureSwitch(&sw)

	// Add to file
	f := platform.deviceFile
	f.Switches = append(f.Switches, sw)
	return &f.Switches[len(f.Switches)-1], nil
}
