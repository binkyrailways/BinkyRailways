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
	"fmt"

	"go.uber.org/multierr"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// Railway adds implementation functions to state.Railway.
type Railway interface {
	Entity
	state.Railway
	state.EventDispatcher

	// Try to resolve the given endpoint into a block state.
	ResolveEndPoint(model.EndPoint) (Block, error)
	// Select a command station that can best drive the given entity
	SelectCommandStation(model.AddressEntity) (CommandStation, error)
}

// railway implements the Railway state
type railway struct {
	entity

	power           powerProperty
	blocks          []Block
	blockGroups     []BlockGroup
	commandStations []CommandStation
	junctions       []Junction
	locs            []Loc
	routes          []Route
	sensors         []Sensor
	signals         []Signal
	outputs         []Output
}

// New constructs and initializes state for the given railway.
func New(entity model.Railway, ui state.UserInterface, persistence state.Persistence) (state.Railway, error) {
	// Create
	r := &railway{}
	r.entity = newEntity(entity, r)
	r.power = powerProperty{
		Railway: r,
	}

	// Construct children
	builder := &builder{Railway: r}
	// Create module entities
	entity.GetModules().ForEach(func(modRef model.ModuleRef) {
		if module := modRef.TryResolve(); module != nil {
			// Create blocks
			module.GetBlocks().ForEach(func(me model.Block) {
				if st, ok := me.Accept(builder).(Block); ok {
					r.blocks = append(r.blocks, st)
				}
			})
			// Create block groups
			module.GetBlockGroups().ForEach(func(me model.BlockGroup) {
				if st, ok := me.Accept(builder).(BlockGroup); ok {
					r.blockGroups = append(r.blockGroups, st)
				}
			})
			// Create junctions
			module.GetJunctions().ForEach(func(me model.Junction) {
				if st, ok := me.Accept(builder).(Junction); ok {
					r.junctions = append(r.junctions, st)
				}
			})
			// Create routes
			module.GetRoutes().ForEach(func(me model.Route) {
				if st, ok := me.Accept(builder).(Route); ok {
					r.routes = append(r.routes, st)
				}
			})
			// Create sensors
			module.GetSensors().ForEach(func(me model.Sensor) {
				if st, ok := me.Accept(builder).(Sensor); ok {
					r.sensors = append(r.sensors, st)
				}
			})
			// Create signals
			module.GetSignals().ForEach(func(me model.Signal) {
				if st, ok := me.Accept(builder).(Signal); ok {
					r.signals = append(r.signals, st)
				}
			})
			// Create outputs
			module.GetOutputs().ForEach(func(me model.Output) {
				if st, ok := me.Accept(builder).(Output); ok {
					r.outputs = append(r.outputs, st)
				}
			})
		}
	})
	// Create command stations
	entity.GetCommandStations().ForEach(func(csRef model.CommandStationRef) {
		if cs := csRef.TryResolve(); cs != nil {
			if st, ok := cs.Accept(builder).(CommandStation); ok {
				r.commandStations = append(r.commandStations, st)
			}
		}
	})
	// Create locs
	entity.GetLocs().ForEach(func(locRef model.LocRef) {
		if loc := locRef.TryResolve(); loc != nil {
			if st, ok := loc.Accept(builder).(Loc); ok {
				r.locs = append(r.locs, st)
			}
		}
	})

	// Prepare
	if err := prepareForUse(r, ui, persistence); err != nil {
		return nil, err
	}

	return r, nil
}

// Try to prepare the entity for use.
// Returns nil when the entity is successfully prepared,
// returns an error otherwise.
func (r *railway) TryPrepareForUse(ui state.UserInterface, persistence state.Persistence) error {
	var err error
	// Note that the order of preparation is important
	r.ForEachCommandStation(func(x state.CommandStation) {
		ix := x.(CommandStation)
		multierr.AppendInto(&err, prepareForUse(ix, ui, persistence))
	})
	r.ForEachBlock(func(x state.Block) {
		ix := x.(Block)
		multierr.AppendInto(&err, prepareForUse(ix, ui, persistence))
	})
	r.ForEachBlockGroup(func(x state.BlockGroup) {
		ix := x.(BlockGroup)
		multierr.AppendInto(&err, prepareForUse(ix, ui, persistence))
	})
	r.ForEachJunction(func(x state.Junction) {
		ix := x.(Junction)
		multierr.AppendInto(&err, prepareForUse(ix, ui, persistence))
	})
	r.ForEachSensor(func(x state.Sensor) {
		ix := x.(Sensor)
		multierr.AppendInto(&err, prepareForUse(ix, ui, persistence))
	})
	r.ForEachSignal(func(x state.Signal) {
		ix := x.(Signal)
		multierr.AppendInto(&err, prepareForUse(ix, ui, persistence))
	})
	r.ForEachOutput(func(x state.Output) {
		ix := x.(Output)
		multierr.AppendInto(&err, prepareForUse(ix, ui, persistence))
	})
	r.ForEachRoute(func(x state.Route) {
		ix := x.(Route)
		multierr.AppendInto(&err, prepareForUse(ix, ui, persistence))
	})
	r.ForEachLoc(func(x state.Loc) {
		ix := x.(Loc)
		multierr.AppendInto(&err, prepareForUse(ix, ui, persistence))
	})
	return err
}

// Send the given event to all interested receivers.
func (r *railway) Send(state.Event) {}

// Try to resolve the given endpoint into a block state.
func (r *railway) ResolveEndPoint(endpoint model.EndPoint) (Block, error) {
	id := endpoint.GetID()
	if _, ok := endpoint.(model.Block); ok {
		for _, x := range r.blocks {
			if x.GetID() == id {
				return x, nil
			}
		}
	}
	return nil, fmt.Errorf("Non block not implemented") // TODO
}

// Try to resolve the given commandstatin into a commandstation state.
func (r *railway) ResolveCommandStation(cs model.CommandStation) (CommandStation, error) {
	id := cs.GetID()
	for _, x := range r.commandStations {
		if x.GetID() == id {
			return x, nil
		}
	}
	return nil, fmt.Errorf("Command station '%s' not found", id)
}

// Select a command station that can best drive the given entity
func (r *railway) SelectCommandStation(entity model.AddressEntity) (CommandStation, error) {
	addr := entity.GetAddress()
	if addr.IsEmpty() {
		// No address
		return nil, nil
	}
	// Lookup preferred command station
	var prefCS model.CommandStation
	switch addr.Network.Type {
	case model.AddressTypeDcc:
		prefCS = r.GetModel().GetPreferredDccCommandStation()
	case model.AddressTypeLocoNet:
		prefCS = r.GetModel().GetPreferredLocoNetCommandStation()
	case model.AddressTypeMotorola:
		prefCS = r.GetModel().GetPreferredMotorolaCommandStation()
	case model.AddressTypeMfx:
		prefCS = r.GetModel().GetPreferredMfxCommandStation()
	case model.AddressTypeMqtt:
		prefCS = r.GetModel().GetPreferredMqttCommandStation()
	default:
		return nil, fmt.Errorf("Unknown network type: '%s'", addr.Network.Type)
	}
	if prefCS != nil {
		if cs, err := r.ResolveCommandStation(prefCS); err == nil {
			if supports, _ := cs.Supports(entity, addr.Network); supports {
				// Preferred command station suppports the entity, use it.
				return cs, nil
			}
		}
	}

	// Go over all command stations
	var supportsCS []CommandStation
	var exactCS []CommandStation
	for _, cs := range r.commandStations {
		supports, exact := cs.Supports(entity, addr.Network)
		if supports {
			supportsCS = append(supportsCS, cs)
		}
		if exact {
			exactCS = append(exactCS, cs)
		}
	}
	// Choose command station
	if len(exactCS) == 1 {
		// Found exactly 1 command station to support it (exactly)
		return exactCS[0], nil
	}
	if len(exactCS) > 1 {
		return nil, fmt.Errorf("Found multiple command stations that supports '%s' exactly", entity.GetDescription())
	}
	if len(supportsCS) == 1 {
		// Found exactly 1 command station to support it
		return supportsCS[0], nil
	}
	if len(supportsCS) > 1 {
		return nil, fmt.Errorf("Found multiple command stations that supports '%s'", entity.GetDescription())
	}
	return nil, fmt.Errorf("Found no command station that supports '%s'", entity.GetDescription())
}

// Unknown sensor has been detected.
//event EventHandler<PropertyEventArgs<Address>> UnknownSensor;

// Unknown standard switch has been detected.
//        event EventHandler<PropertyEventArgs<Address>> UnknownSwitch;

// Gets the railway entity (model)
func (r *railway) GetModel() model.Railway {
	return r.GetEntity().(model.Railway)
}

// Prepare this state for use in a live railway.
// Make sure all relevant connections to other state objects are resolved.
// Check the IsReadyForUse property afterwards if it has succeeded.

//void PrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence);

// Gets the state of the automatic loc controller?
//IAutomaticLocController AutomaticLocController { get; }

// Gets the state dispatcher
//        IStateDispatcher Dispatcher { get; }

// Gets the virtual mode.
// This will never return null.
//        IVirtualMode VirtualMode { get; }

// Get the model time
//        IActualStateProperty<Time> ModelTime { get; }

// Enable/disable power on all of the command stations of this railway
func (r *railway) GetPower() state.BoolProperty {
	return &r.power
}

// Gets the states of all blocks in this railway
func (r *railway) ForEachBlock(cb func(state.Block)) {
	for _, x := range r.blocks {
		cb(x)
	}
}

// Gets the states of all block groups in this railway
func (r *railway) ForEachBlockGroup(cb func(state.BlockGroup)) {
	for _, x := range r.blockGroups {
		cb(x)
	}
}

// Gets the states of all command stations in this railway
func (r *railway) ForEachCommandStation(cb func(state.CommandStation)) {
	for _, x := range r.commandStations {
		cb(x)
	}
}

// Gets the states of all junctions in this railway
func (r *railway) ForEachJunction(cb func(state.Junction)) {
	for _, x := range r.junctions {
		cb(x)
	}
}

// Gets the states of all locomotives in this railway
func (r *railway) ForEachLoc(cb func(state.Loc)) {
	for _, x := range r.locs {
		cb(x)
	}
}

// Gets the states of all routes in this railway
func (r *railway) ForEachRoute(cb func(state.Route)) {
	for _, x := range r.routes {
		cb(x)
	}
}

// Gets the states of all sensors in this railway
func (r *railway) ForEachSensor(cb func(state.Sensor)) {
	for _, x := range r.sensors {
		cb(x)
	}
}

// Gets the states of all signals in this railway
func (r *railway) ForEachSignal(cb func(state.Signal)) {
	for _, x := range r.signals {
		cb(x)
	}
}

// Gets the states of all outputs in this railway
func (r *railway) ForEachOutput(cb func(state.Output)) {
	for _, x := range r.outputs {
		cb(x)
	}
}
