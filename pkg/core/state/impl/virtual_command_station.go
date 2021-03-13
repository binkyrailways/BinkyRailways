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
	mimpl "github.com/binkyrailways/BinkyRailways/pkg/core/model/impl"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// virtualCommandStation implements the VirtualCommandStation.
type virtualCommandStation struct {
	commandStation

	power boolProperty
}

// Create a new entity
func newVirtualCommandStation(railway Railway) CommandStation {
	cs := &virtualCommandStation{
		commandStation: newCommandStation(mimpl.NewVirtualCommandStation(), railway),
	}
	cs.power.Configure(cs, railway, railway)
	cs.power.OnRequestedChanged = func(ctx context.Context, value bool) {
		cs.power.SetActual(ctx, value)
	}
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

// Send the direction of the given switch towards the railway.
func (cs *virtualCommandStation) SendSwitchDirection(ctx context.Context, sw state.Switch) {
	sw.GetDirection().SetActual(ctx, sw.GetDirection().GetRequest(ctx))
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
