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
	"time"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	mimpl "github.com/binkyrailways/BinkyRailways/pkg/core/model/impl"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// virtualCommandStation implements the VirtualCommandStation.
type virtualCommandStation struct {
	commandStation

	createdAt time.Time
	power     boolProperty
}

type virtualHardwareModule struct {
	uptime        time.Duration
	lastUpdatedAt time.Time
}

// Create a new entity
func newVirtualCommandStation(railway Railway) CommandStation {
	cs := &virtualCommandStation{
		commandStation: newCommandStation(mimpl.NewVirtualCommandStation(), railway),
		createdAt:      time.Now(),
	}
	cs.power.Configure("power", cs, railway, railway)
	cs.power.SubscribeRequestChanges(func(ctx context.Context, value bool) {
		cs.power.SetActual(ctx, value)
	})
	return cs
}

// getCommandStation returns the entity as VirtualCommandStation.
func (cs *virtualCommandStation) getCommandStation() model.VirtualCommandStation {
	return cs.GetEntity().(model.VirtualCommandStation)
}

// Try to prepare the entity for use.
// Returns nil when the entity is successfully prepared,
// returns an error otherwise.
func (cs *virtualCommandStation) TryPrepareForUse(context.Context, state.UserInterface, state.Persistence) error {
	return nil
}

// Wrap up the preparation fase.
func (cs *virtualCommandStation) FinalizePrepare(ctx context.Context) {
	// TODO
}

// Description of the underlying entity
func (cs *virtualCommandStation) GetDescription() string {
	return "Virtual"
}

// Enable/disable power on the railway
func (cs *virtualCommandStation) GetPower() state.BoolProperty {
	return &cs.power
}

// Has the command station not send or received anything for a while.
func (cs *virtualCommandStation) GetIdle(context.Context) bool {
	return true // TODO
}

// Send the speed and direction of the given loc towards the railway.
func (cs *virtualCommandStation) SendLocSpeedAndDirection(ctx context.Context, l state.Loc) {
	l.GetSpeedInSteps().SetActual(ctx, l.GetSpeedInSteps().GetRequested(ctx))
	l.GetDirection().SetActual(ctx, l.GetDirection().GetRequested(ctx))
	l.GetF0().SetActual(ctx, l.GetF0().GetRequested(ctx))
}

// Send the state of the binary output towards the railway.
func (cs *virtualCommandStation) SendOutputActive(ctx context.Context, bo state.BinaryOutput) {
	bo.GetActive().SetActual(ctx, bo.GetActive().GetRequested(ctx))
}

// Send the direction of the given switch towards the railway.
func (cs *virtualCommandStation) SendSwitchDirection(ctx context.Context, sw state.Switch) {
	sw.GetDirection().SetActual(ctx, sw.GetDirection().GetRequested(ctx))
}

// Send the position of the given turntable towards the railway.
//void SendTurnTablePosition(ITurnTableState turnTable);

// Close the commandstation
func (cs *virtualCommandStation) Close(ctx context.Context) {
	// Nothing to do here
}

// Trigger discovery of attached hardware with given ID.
func (cs *virtualCommandStation) TriggerDiscover(ctx context.Context, hardwareID string) error {
	// Do nothing
	return nil
}

// Iterate over all hardware modules this command station is in control of.
func (cs *virtualCommandStation) ForEachHardwareModule(cb func(state.HardwareModule)) {
	// Only 1
	cb(&virtualHardwareModule{
		uptime:        time.Since(cs.createdAt),
		lastUpdatedAt: time.Now(),
	})
}

// Gets the ID of the module
func (hm *virtualHardwareModule) GetID() string {
	return "virtual"
}

// Gets the uptime of the module
func (hm *virtualHardwareModule) GetUptime() time.Duration {
	return hm.uptime
}

// Does this module support uptime data?
func (hm *virtualHardwareModule) HasUptime() bool {
	return true
}

// Gets the time of last update of the information of this module
func (hm *virtualHardwareModule) GetLastUpdatedAt() time.Time {
	return hm.lastUpdatedAt
}

// Does this module support last updated at data?
func (hm *virtualHardwareModule) HasLastUpdatedAt() bool {
	return true
}

// Gets the version of the module
func (hm *virtualHardwareModule) GetVersion() string {
	return "0.0.0"
}

// Does this module support version data?
func (hm *virtualHardwareModule) HasVersion() bool {
	return true
}

// Get human readable error messages
func (hm *virtualHardwareModule) GetErrorMessages() []string {
	return nil
}

// Does this module support address data?
func (hm *virtualHardwareModule) HasAddress() bool {
	return false
}

// Gets the address of the module (if any)
func (hm *virtualHardwareModule) GetAddress() string {
	return ""
}

// Request a reset of hardware module with given ID
func (cs *virtualCommandStation) ResetHardwareModule(ctx context.Context, id string) error {
	// No modules
	return nil
}
