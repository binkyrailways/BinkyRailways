// Copyright 2023 Ewout Prangsma
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
	"fmt"
	"strconv"

	"github.com/binkynet/bidib"
	"github.com/binkynet/bidib/host"
	serialtx "github.com/binkynet/bidib/transport/serial"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// bidibCommandStation implements the BidibCommandStation.
type bidibCommandStation struct {
	commandStation

	host  host.Host
	power boolProperty
}

// Create a new entity
func newBidibCommandStation(en model.BidibCommandStation, railway Railway) CommandStation {
	cs := &bidibCommandStation{
		commandStation: newCommandStation(en, railway),
	}
	cs.power.Configure("power", cs, railway, railway)
	cs.power.SubscribeRequestChanges(cs.sendPower)
	return cs
}

// getCommandStation returns the entity as BidibCommandStation.
func (cs *bidibCommandStation) getCommandStation() model.BidibCommandStation {
	return cs.GetEntity().(model.BidibCommandStation)
}

// Try to prepare the entity for use.
// Returns nil when the entity is successfully prepared,
// returns an error otherwise.
func (cs *bidibCommandStation) TryPrepareForUse(context.Context, state.UserInterface, state.Persistence) error {
	log := cs.log
	portName := cs.getCommandStation().GetComPortName()
	if portName == "" {
		return fmt.Errorf("serial port not set")
	}
	cfg := host.Config{
		Serial: &serialtx.Config{
			PortName: portName,
		},
	}
	h, err := host.New(cfg, log)
	if err != nil {
		return fmt.Errorf("failed to initialize bidib host: %w", err)
	}
	cs.host = h

	return nil
}

// Wrap up the preparation fase.
func (cs *bidibCommandStation) FinalizePrepare(ctx context.Context) {
	// TODO
}

// Enable/disable power on the railway
func (cs *bidibCommandStation) GetPower() state.BoolProperty {
	return &cs.power
}

// Has the command station not send or received anything for a while.
func (cs *bidibCommandStation) GetIdle(context.Context) bool {
	return true // TODO
}

// sendPowerToNode sets the given node and all its children
// in the given power state.
func sendPowerToNode(node *host.Node, enabled bool) {
	if node != nil {
		if cs := node.Cs(); cs != nil {
			if enabled {
				cs.Go()
			} else {
				cs.Off()
			}
		}
		node.ForEachChild(func(child *host.Node) {
			sendPowerToNode(child, enabled)
		})
	}
}

// Send the requested power state.
func (cs *bidibCommandStation) sendPower(ctx context.Context, enabled bool) {
	log := cs.log
	log.Debug().Msg("send power to nodes")
	sendPowerToNode(cs.host.GetRootNode(), enabled)
}

// sendDriveToNode sends a drive command to the given node
// and all its children.
func (cs *bidibCommandStation) sendDriveToNode(node *host.Node, opts host.DriveOptions) {
	if node != nil {
		if cs := node.Cs(); cs != nil {
			cs.Drive(opts)
		}
		node.ForEachChild(func(child *host.Node) {
			cs.sendDriveToNode(child, opts)
		})
	}
}

// Send the speed and direction of the given loc towards the railway.
func (cs *bidibCommandStation) SendLocSpeedAndDirection(ctx context.Context, locState state.Loc) {
	log := cs.log
	// Prepare drive options
	var opts host.DriveOptions

	// Parse address
	addr := locState.GetAddress(ctx)
	if addr.Network.Type != model.AddressTypeDcc {
		log.Error().
			Str("address", addr.String()).
			Msg("loc address is not of type DCC")
		return
	}
	dccAddr, err := strconv.Atoi(addr.Value)
	if err != nil {
		log.Error().
			Err(err).
			Str("address", addr.String()).
			Msg("cannot parse loc address")
		return
	}
	opts.DccAddress = uint16(dccAddr)

	// Detect dcc format
	switch locState.GetSpeedSteps(ctx) {
	default:
		opts.DccFormat = bidib.BIDIB_CS_DRIVE_FORMAT_DCC14
	case 28:
		opts.DccFormat = bidib.BIDIB_CS_DRIVE_FORMAT_DCC28
	case 128:
		opts.DccFormat = bidib.BIDIB_CS_DRIVE_FORMAT_DCC128
	}

	// Get speed
	opts.Speed = uint8(locState.GetSpeedInSteps().GetRequested(ctx))
	opts.OutputSpeed = true

	// Get direction
	opts.DirectionForward = locState.GetDirection().GetRequested(ctx) == state.LocDirectionForward

	// Get flags
	opts.Flags = make(bidib.DccFlags, 1)
	opts.Flags.Set(0, locState.GetF0().GetRequested(ctx))

	log.Debug().Msg("send power to nodes")
	cs.sendDriveToNode(cs.host.GetRootNode(), opts)
}

// Send the state of the binary output towards the railway.
func (cs *bidibCommandStation) SendOutputActive(context.Context, state.BinaryOutput) {
	// TODO
}

// Send the direction of the given switch towards the railway.
func (cs *bidibCommandStation) SendSwitchDirection(context.Context, state.Switch) {
	// TODO
}

// Send the position of the given turntable towards the railway.
//void SendTurnTablePosition(ITurnTableState turnTable);

// Close the commandstation
func (cs *bidibCommandStation) Close(ctx context.Context) {
	cs.sendPower(ctx, false)
	if h := cs.host; h != nil {
		cs.host = nil
		h.Close()
	}
	// TODO
}

// Trigger discovery of attached hardware with given ID.
func (cs *bidibCommandStation) TriggerDiscover(ctx context.Context, hardwareID string) error {
	// Do nothing
	return nil
}

// Iterate over all hardware modules this command station is in control of.
func (cs *bidibCommandStation) ForEachHardwareModule(func(state.HardwareModule)) {
	// No modules
}
