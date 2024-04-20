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

package impl

import (
	"context"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// locoBufferCommandStation implements the LocoBufferCommandStation.
type locoBufferCommandStation struct {
	commandStation

	power boolProperty
}

// Create a new entity
func newLocoBufferCommandStation(en model.LocoBufferCommandStation, railway Railway) CommandStation {
	cs := &locoBufferCommandStation{
		commandStation: newCommandStation(en, railway),
	}
	cs.power.Configure("power", cs, railway, railway)
	return cs
}

// getCommandStation returns the entity as LocoBufferCommandStation.
func (cs *locoBufferCommandStation) getCommandStation() model.LocoBufferCommandStation {
	return cs.GetEntity().(model.LocoBufferCommandStation)
}

// Try to prepare the entity for use.
// Returns nil when the entity is successfully prepared,
// returns an error otherwise.
func (cs *locoBufferCommandStation) TryPrepareForUse(context.Context, state.UserInterface, state.Persistence) error {
	// TODO
	return nil
}

// Wrap up the preparation fase.
func (cs *locoBufferCommandStation) FinalizePrepare(ctx context.Context) {
	// TODO
}

// Enable/disable power on the railway
func (cs *locoBufferCommandStation) GetPower() state.BoolProperty {
	return &cs.power
}

// Has the command station not send or received anything for a while.
func (cs *locoBufferCommandStation) GetIdle(context.Context) bool {
	return true // TODO
}

// Send the speed and direction of the given loc towards the railway.
func (cs *locoBufferCommandStation) SendLocSpeedAndDirection(context.Context, state.Loc) {
	// TODO
}

// Send the state of the binary output towards the railway.
func (cs *locoBufferCommandStation) SendOutputActive(context.Context, state.BinaryOutput) {
	// TODO
}

// Send the direction of the given switch towards the railway.
func (cs *locoBufferCommandStation) SendSwitchDirection(context.Context, state.Switch) {
	// TODO
}

// Send the position of the given turntable towards the railway.
//void SendTurnTablePosition(ITurnTableState turnTable);

// Close the commandstation
func (cs *locoBufferCommandStation) Close(ctx context.Context) {
	// TODO
}

// Trigger discovery of attached hardware with given ID.
func (cs *locoBufferCommandStation) TriggerDiscover(ctx context.Context, hardwareID string) error {
	// Do nothing
	return nil
}

// Iterate over all hardware modules this command station is in control of.
func (cs *locoBufferCommandStation) ForEachHardwareModule(func(state.HardwareModule)) {
	// No modules
}

// Request a reset of hardware module with given ID
func (cs *locoBufferCommandStation) ResetHardwareModule(ctx context.Context, id string) error {
	// No modules
	return nil
}
