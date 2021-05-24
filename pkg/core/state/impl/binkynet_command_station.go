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
	"fmt"
	"time"

	bn "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkynet/NetManager/service"
	"github.com/binkynet/NetManager/service/manager"
	"github.com/binkynet/NetManager/service/server"
	"golang.org/x/sync/errgroup"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// binkyNetCommandStation implements the BinkyNetCommandStation.
type binkyNetCommandStation struct {
	commandStation

	power boolProperty

	reconfigureQueue chan string
	manager          manager.Manager
	service          service.Service
	server           server.Server
	cancel           context.CancelFunc
}

// Create a new entity
func newBinkyNetCommandStation(en model.BinkyNetCommandStation, railway Railway) CommandStation {
	cs := &binkyNetCommandStation{
		commandStation: newCommandStation(en, railway),
	}
	cs.power.Configure(cs, railway, railway)
	cs.power.OnRequestedChanged = cs.sendPower
	return cs
}

// getCommandStation returns the entity as LocoBufferCommandStation.
func (cs *binkyNetCommandStation) getCommandStation() model.BinkyNetCommandStation {
	return cs.GetEntity().(model.BinkyNetCommandStation)
}

// Try to prepare the entity for use.
// Returns nil when the entity is successfully prepared,
// returns an error otherwise.
func (cs *binkyNetCommandStation) TryPrepareForUse(ctx context.Context, _ state.UserInterface, _ state.Persistence) error {
	var err error
	serverHost := cs.getCommandStation().GetServerHost()
	cs.reconfigureQueue = make(chan string, 64)
	registry := newBinkyNetConfigRegistry(cs.getCommandStation().GetLocalWorkers(), cs.onUnknownLocalWorker)
	cs.manager, err = manager.New(manager.Dependencies{
		Log:              cs.log,
		ConfigRegistry:   registry,
		ReconfigureQueue: cs.reconfigureQueue,
	})
	if err != nil {
		return err
	}
	cs.service, err = service.NewService(service.Config{
		RequiredWorkerVersion: cs.getCommandStation().GetRequiredWorkerVersion(),
	}, service.Dependencies{
		Log:            cs.log,
		Manager:        cs.manager,
		ConfigRegistry: registry,
	})
	if err != nil {
		return err
	}
	cs.server, err = server.NewServer(server.Config{
		Host:     serverHost,
		GRPCPort: cs.getCommandStation().GetGRPCPort(),
	}, cs.service, cs.log)
	if err != nil {
		return err
	}
	ctx, cancel := context.WithCancel(context.Background())
	cs.cancel = cancel
	g, ctx := errgroup.WithContext(ctx)
	ctx = bn.WithServiceInfoHost(ctx, serverHost)
	g.Go(func() error { return cs.manager.Run(ctx) })
	g.Go(func() error { return cs.server.Run(ctx) })
	g.Go(func() error {
		updates, cancel := cs.manager.SubscribeLocalWorkerUpdates()
		defer cancel()
		for {
			select {
			case update := <-updates:
				cs.log.Debug().
					Str("id", update.Id).
					Int64("uptime", update.Uptime).
					Msg("local worker update")
			case <-ctx.Done():
				return nil
			}
		}
	})
	g.Go(func() error {
		actuals, cancel := cs.manager.SubscribePowerActuals()
		defer cancel()
		for {
			select {
			case actual := <-actuals:
				cs.onPowerActual(ctx, actual)
			case <-ctx.Done():
				return nil
			}
		}
	})
	g.Go(func() error {
		actuals, cancel := cs.manager.SubscribeOutputActuals()
		defer cancel()
		for {
			select {
			case actual := <-actuals:
				cs.onOutputActual(ctx, actual)
			case <-ctx.Done():
				return nil
			}
		}
	})

	return nil
}

// Enable/disable power on the railway
func (cs *binkyNetCommandStation) GetPower() state.BoolProperty {
	return &cs.power
}

// Has the command station not send or received anything for a while.
func (cs *binkyNetCommandStation) GetIdle(context.Context) bool {
	return true // TODO
}

// Send the requested power state.
func (cs *binkyNetCommandStation) sendPower(ctx context.Context, enabled bool) {
	cs.manager.SetPowerRequest(bn.PowerState{
		Enabled: enabled,
	})
}

// Send the state of the binary output towards the railway.
func (cs *binkyNetCommandStation) onPowerActual(ctx context.Context, actual bn.Power) {
	cs.power.SetActual(ctx, actual.GetActual().GetEnabled())
}

// Send the speed and direction of the given loc towards the railway.
func (cs *binkyNetCommandStation) SendLocSpeedAndDirection(ctx context.Context, loc state.Loc) {
	fmt.Println("SendLocSpeedAndDirection")
	addr := cs.createObjectAddress(loc.GetAddress(ctx))
	direction := bn.LocDirection_FORWARD
	if loc.GetDirection().GetRequested(ctx) == state.LocDirectionReverse {
		direction = bn.LocDirection_REVERSE
	}
	cs.manager.SetLocRequest(bn.Loc{
		Address: addr,
		Request: &bn.LocState{
			Speed:      int32(loc.GetSpeedInSteps().GetRequested(ctx)),
			SpeedSteps: int32(loc.GetSpeedSteps(ctx)),
			Direction:  direction,
			// TODO functions
		},
	})
}

// Send the state of the binary output towards the railway.
func (cs *binkyNetCommandStation) SendOutputActive(ctx context.Context, bo state.BinaryOutput) {
	addr := cs.createObjectAddress(bo.GetAddress())
	value := int32(0)
	if bo.GetActive().GetRequested(ctx) {
		value = 1
	}
	cs.manager.SetOutputRequest(bn.Output{
		Address: addr,
		Request: &bn.OutputState{
			Value: value,
		},
	})
}

// Send the state of the binary output towards the railway.
func (cs *binkyNetCommandStation) onOutputActual(ctx context.Context, actual bn.Output) {
	objAddr := actual.GetAddress()
	cs.ForEachOutput(func(output state.Output) {
		if bo, ok := output.(state.BinaryOutput); ok {
			if isAddressEqual(bo.GetAddress(), objAddr) {
				bo.GetActive().SetActual(ctx, actual.GetActual().GetValue() != 0)
			}
		}
	})
}

// Send the direction of the given switch towards the railway.
func (cs *binkyNetCommandStation) SendSwitchDirection(ctx context.Context, sw state.Switch) {
	addr := cs.createObjectAddress(sw.GetAddress())
	var direction bn.SwitchDirection
	switch sw.GetDirection().GetRequested(ctx) {
	case model.SwitchDirectionStraight:
		direction = bn.SwitchDirection_STRAIGHT
	case model.SwitchDirectionOff:
		direction = bn.SwitchDirection_OFF
	default:
		// Unknown direction
		return
	}
	cs.manager.SetSwitchRequest(bn.Switch{
		Address: addr,
		Request: &bn.SwitchState{
			Direction: direction,
		},
	})
}

// Send the position of the given turntable towards the railway.
//void SendTurnTablePosition(ITurnTableState turnTable);

// Send an event about the given unknown worker
func (cs *binkyNetCommandStation) onUnknownLocalWorker(hardwareID string) {
	if sender := cs.GetRailwayImpl(); sender != nil {
		sender.Send(state.UnknownBinkyNetLocalWorkerEvent{
			HardwareID: hardwareID,
		})
	}
}

// Trigger discovery of locally attached devices on local worker
func (cs *binkyNetCommandStation) TriggerDiscover(ctx context.Context, hardwareID string) error {
	go func() {
		log := cs.log.With().Str("hardware_id", hardwareID).Logger()
		ctx, cancel := context.WithTimeout(context.Background(), time.Minute)
		defer cancel()
		log.Info().Msg("Trigger discover...")
		result, err := cs.manager.Discover(ctx, hardwareID)
		if err != nil {
			log.Warn().Err(err).Msg("Discover failed")
		} else {
			log.Info().Strs("addresses", result.GetAddresses()).Msg("Discover result")
		}
	}()
	return nil
}

// Close the commandstation
func (cs *binkyNetCommandStation) Close(ctx context.Context) {
	cancel, reconfigureQueue := cs.cancel, cs.reconfigureQueue
	cs.cancel = nil
	cs.reconfigureQueue = nil
	if cancel != nil {
		cancel()
	}
	if reconfigureQueue != nil {
		close(reconfigureQueue)
	}
	// TODO
}

// Iterate over all hardware modules this command station is in control of.
func (cs *binkyNetCommandStation) ForEachHardwareModule(cb func(state.HardwareModule)) {
	// Return all local workers
	for _, info := range cs.manager.GetAllLocalWorkers() {
		cb(binkyNetLocalWorkerModule(info))
	}
}

// createObjectAddress converts a model address into a BinkyNet object address.
func (cs *binkyNetCommandStation) createObjectAddress(addr model.Address) bn.ObjectAddress {
	return bn.ObjectAddress(addr.Value)
}

// isAddressEqual returns true if the given addresses are the same
func isAddressEqual(modelAddr model.Address, objAddr bn.ObjectAddress) bool {
	return modelAddr.Value == string(objAddr)
}
