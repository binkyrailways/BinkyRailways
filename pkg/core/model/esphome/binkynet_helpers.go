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
	"strconv"

	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// Try to find the connection with given name in the given object.
// Returns true if connection exists, false otherwise.
func hasConnection(objModel model.BinkyNetObject, connName api.ConnectionName) bool {
	_, ok := objModel.GetConnections().Get(connName)
	return ok
}

// Try to find the connection with given name in the given object.
func getConnection(objModel model.BinkyNetObject, connName api.ConnectionName) (model.BinkyNetConnection, error) {
	conn, ok := objModel.GetConnections().Get(connName)
	if !ok {
		return nil, fmt.Errorf("%s connection not found in %s", connName, objModel.GetDescription())
	}
	return conn, nil
}

// Try to get a pin at given index in the given connection.
func getPin(objModel model.BinkyNetObject, conn model.BinkyNetConnection, index int) (model.BinkyNetDevicePin, error) {
	pin, ok := conn.GetPins().Get(index)
	if !ok {
		return nil, fmt.Errorf("%s pin at index %d not found in %s", conn.GetKey(), index, objModel.GetDescription())
	}
	return pin, nil
}

// Is the Invert configuration set to true?
func getInvert(conn model.BinkyNetConnection) (bool, error) {
	if value, ok := conn.GetConfiguration().Get(string(api.ConfigKeyInvert)); ok {
		if b, err := strconv.ParseBool(value); err != nil {
			return false, err
		} else {
			return b, nil
		}
	}
	return false, nil
}

// Apply any inverted setting on the pin.
func (pin *Pin) applyInvert(conn model.BinkyNetConnection) error {
	inverted, err := getInvert(conn)
	if err != nil {
		return err
	}
	pin.Inverted = inverted
	return nil
}
