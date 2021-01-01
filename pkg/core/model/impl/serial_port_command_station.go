// Copyright 2020 Ewout Prangsma
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

package impl

import (
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// SerialPortCommandStation extends model SerialPortCommandStation with implementation methods
type SerialPortCommandStation interface {
	model.SerialPortCommandStation
	PersistentEntity
}

type serialPortCommandStation struct {
	commandStation

	ComPortName string `xml:"ComPortName,omitempty"`
}

// Name of COM port used to communicate with the locobuffer.
func (cs *serialPortCommandStation) GetComPortName() string {
	return cs.ComPortName
}
func (cs *serialPortCommandStation) SetComPortName(value string) error {
	cs.ComPortName = value
	return nil
}

func (cs *serialPortCommandStation) Upgrade() {
	// Nothing needed
}
