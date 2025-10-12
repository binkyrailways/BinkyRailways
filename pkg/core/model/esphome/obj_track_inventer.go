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

// Add an object of type TrackInverter
func addTrackInverter(fs *DeviceFileSet, objModel model.BinkyNetObject) error {
	if _, err := fs.createSwitch(objModel, "out_a_in_a_", api.ConnectionNameRelayOutAInA, 0); err != nil {
		return err
	}
	if _, err := fs.createSwitch(objModel, "out_a_in_b_", api.ConnectionNameRelayOutAInB, 0); err != nil {
		return err
	}
	if _, err := fs.createSwitch(objModel, "out_b_in_a_", api.ConnectionNameRelayOutBInA, 0); err != nil {
		return err
	}
	if _, err := fs.createSwitch(objModel, "out_b_in_b_", api.ConnectionNameRelayOutBInB, 0); err != nil {
		return err
	}

	return nil
}
