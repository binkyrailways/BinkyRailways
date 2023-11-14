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

package state

import (
	"context"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// CommandStation specifies the state of a command station.
type CommandStation interface {
	Entity

	// Gets the underlying model
	GetModel() model.CommandStation

	// Gets the underlying model reference
	GetModelRef() model.CommandStationRef

	// Junctions driven by this command station
	ForEachJunction(func(Junction))

	// Locomotives driven by this command station
	ForEachLoc(func(Loc))

	// Input signals driven by this command station (overlaps with <see cref="Sensors"/>).
	ForEachInput(func(Input))

	// Sensors driven by this command station
	ForEachSensor(func(Sensor))

	// Signals driven by this command station
	ForEachSignal(func(Signal))

	// Can this command station be used to serve the given network?
	// <param name="entity">The entity being search for.</param>
	// <param name="network">The network in question</param>
	// <param name="exactMatch">Set to true when there is an exact match in address type and address space, false otherwise.</param>
	// Returns: supports, exactMatch
	Supports(entity model.AddressEntity, network model.Network) (bool, bool)

	// Enable/disable power on the railway
	GetPower() BoolProperty

	// Has the command station not send or received anything for a while.
	GetIdle(context.Context) bool

	// Send the speed and direction of the given loc towards the railway.
	SendLocSpeedAndDirection(context.Context, Loc)

	// Send the state of the binary output towards the railway.
	SendOutputActive(context.Context, BinaryOutput)

	// Send the direction of the given switch towards the railway.
	SendSwitchDirection(context.Context, Switch)

	// Send the position of the given turntable towards the railway.
	//void SendTurnTablePosition(ITurnTableState turnTable);

	// Trigger discovery of attached hardware with given ID.
	TriggerDiscover(ctx context.Context, hardwareID string) error

	// Iterate over all hardware modules this command station is in control of.
	ForEachHardwareModule(func(HardwareModule))

	// Request a reset of hardware module with given ID
	ResetHardwareModule(ctx context.Context, id string) error
}
