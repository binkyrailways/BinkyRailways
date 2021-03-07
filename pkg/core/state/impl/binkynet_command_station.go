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

// Send the speed and direction of the given loc towards the railway.
func (cs *binkyNetCommandStation) SendLocSpeedAndDirection(context.Context, state.Loc) {
	// TODO
}

// Send the direction of the given switch towards the railway.
func (cs *binkyNetCommandStation) SendSwitchDirection(context.Context, state.Switch) {
	// TODO
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
