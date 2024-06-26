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
	"github.com/binkyrailways/BinkyRailways/pkg/core/util"
)

// Railway specifies the state of an entire railway.
type Railway interface {
	Entity
	util.Exclusive

	// Unknown sensor has been detected.
	//event EventHandler<PropertyEventArgs<Address>> UnknownSensor;

	// Unknown standard switch has been detected.
	//        event EventHandler<PropertyEventArgs<Address>> UnknownSwitch;

	// Subscribe to events.
	// To cancel the subscription, call the given cancel function.
	Subscribe(context.Context, func(Event)) context.CancelFunc

	// Gets the railway entity (model)
	GetModel() model.Railway

	// Prepare this state for use in a live railway.
	// Make sure all relevant connections to other state objects are resolved.
	// Check the IsReadyForUse property afterwards if it has succeeded.

	//void PrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence);

	// Gets the state of the automatic loc controller?
	GetAutomaticLocController() AutomaticLocController

	// Gets the state dispatcher
	//        IStateDispatcher Dispatcher { get; }

	// Gets the virtual mode.
	// This will never return nil.
	GetVirtualMode() VirtualMode

	// Get the model time
	//        IActualStateProperty<Time> ModelTime { get; }

	// Enable/disable power on all of the command stations of this railway
	GetPower() BoolProperty

	// Gets the states of all blocks in this railway
	ForEachBlock(func(Block))
	// Get the number of blocks
	GetBlockCount() int
	// Gets the state of the block with given ID.
	// Returns nil if not found
	GetBlock(id string) (Block, error)

	// Gets the states of all block groups in this railway
	ForEachBlockGroup(func(BlockGroup))
	// Get the number of block groups
	GetBlockGroupCount() int

	// Gets the states of all command stations in this railway
	ForEachCommandStation(func(CommandStation))
	// Get the number of command stations
	GetCommandStationCount() int

	// Gets the states of all junctions in this railway
	ForEachJunction(func(Junction))
	// Gets the state of the junction with given ID.
	// Returns nil if not found
	GetJunction(id string) (Junction, error)
	// Get the number of junctions
	GetJunctionCount() int

	// Gets the states of all locomotives in this railway
	ForEachLoc(func(Loc))
	// Get the number of locs
	GetLocCount() int
	// Gets the state of the loc with given ID.
	// Returns nil if not found
	GetLoc(id string) (Loc, error)

	// Gets the states of all routes in this railway
	ForEachRoute(func(Route))
	// Get the number of routes
	GetRouteCount() int
	// Gets the state of the route with given ID.
	// Returns nil if not found
	GetRoute(id string) (Route, error)

	// Gets the states of all sensors in this railway
	ForEachSensor(func(Sensor))
	// Gets the state of the sensor with given ID.
	// Returns nil if not found
	GetSensor(id string) (Sensor, error)
	// Get the number of sensors
	GetSensorCount() int

	// Gets the states of all signals in this railway
	ForEachSignal(func(Signal))
	// Gets the state of the signal with given ID.
	// Returns nil if not found
	GetSignal(id string) (Signal, error)
	// Get the number of signals
	GetSignalCount() int

	// Gets the states of all outputs in this railway
	ForEachOutput(func(Output))
	// Gets the state of the output with given ID.
	// Returns nil if not found
	GetOutput(id string) (Output, error)
	// Get the number of outputs
	GetOutputCount() int

	// Get the entity tester
	GetEntityTester() EntityTester

	// Close the railway
	Close(ctx context.Context)
}
