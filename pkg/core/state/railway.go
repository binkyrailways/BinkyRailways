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

import "github.com/binkyrailways/BinkyRailways/pkg/core/model"

// Railway specifies the state of an entire railway.
type Railway interface {
	Entity

	// Unknown sensor has been detected.
	//event EventHandler<PropertyEventArgs<Address>> UnknownSensor;

	// Unknown standard switch has been detected.
	//        event EventHandler<PropertyEventArgs<Address>> UnknownSwitch;

	// Gets the railway entity (model)
	GetModel() model.Railway

	// Prepare this state for use in a live railway.
	// Make sure all relevant connections to other state objects are resolved.
	// Check the IsReadyForUse property afterwards if it has succeeded.

	//void PrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence);

	// Gets the state of the automatic loc controller?
	//IAutomaticLocController AutomaticLocController { get; }

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

	// Gets the states of all block groups in this railway
	ForEachBlockGroup(func(BlockGroup))

	// Gets the states of all command stations in this railway
	ForEachCommandStation(func(CommandStation))

	// Gets the states of all junctions in this railway
	ForEachJunction(func(Junction))

	// Gets the states of all locomotives in this railway
	ForEachLoc(func(Loc))

	// Gets the states of all routes in this railway
	ForEachRoute(func(Route))

	// Gets the states of all sensors in this railway
	ForEachSensor(func(Sensor))

	// Gets the states of all signals in this railway
	ForEachSignal(func(Signal))

	// Gets the states of all outputs in this railway
	ForEachOutput(func(Output))
}
