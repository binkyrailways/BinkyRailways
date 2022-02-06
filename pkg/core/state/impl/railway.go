// Copyright 2021-2022 Ewout Prangsma
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

	"go.uber.org/multierr"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state/automatic"
	"github.com/binkyrailways/BinkyRailways/pkg/core/util"
	"github.com/rs/zerolog"
)

// Railway adds implementation functions to state.Railway.
type Railway interface {
	Entity
	state.Railway
	state.EventDispatcher
	util.Exclusive

	// Get logger
	Logger() zerolog.Logger

	// Try to resolve the given block into a block state.
	ResolveBlock(context.Context, model.Block) (Block, error)
	// Try to resolve the given block group into a block group state.
	ResolveBlockGroup(context.Context, model.BlockGroup) (BlockGroup, error)
	// Try to resolve the given endpoint into a block state.
	ResolveEndPoint(context.Context, model.EndPoint) (Block, error)
	// Try to resolve the given junction into a junction state.
	ResolveJunction(context.Context, model.Junction) (Junction, error)
	// Try to resolve the given sensor (model) into a sensor state.
	ResolveSensor(context.Context, model.Sensor) (Sensor, error)
	// Select a command station that can best drive the given entity
	SelectCommandStation(context.Context, model.AddressEntity) (CommandStation, error)
}

// railway implements the Railway state
type railway struct {
	entity
	eventDispatcher
	log zerolog.Logger

	exclusive              util.Exclusive
	virtualMode            VirtualMode
	power                  powerProperty
	blocks                 []Block
	blockGroups            []BlockGroup
	commandStations        []CommandStation
	junctions              []Junction
	locs                   []Loc
	routes                 []Route
	sensors                []Sensor
	signals                []Signal
	outputs                []Output
	automaticLocController state.AutomaticLocController
}

// New constructs and initializes state for the given railway.
func New(ctx context.Context, entity model.Railway, log zerolog.Logger, ui state.UserInterface, persistence state.Persistence, virtual bool) (state.Railway, error) {
	// Create
	r := &railway{
		exclusive: util.NewExclusive(),
		log:       log,
	}
	r.entity = newEntity(log, entity, r)
	r.power = powerProperty{
		Railway: r,
	}
	r.virtualMode = newVirtualMode(virtual, r)

	// Construct children
	builder := &builder{Railway: r}
	// Create module entities
	entity.GetModules().ForEach(func(modRef model.ModuleRef) {
		if module, _ := modRef.TryResolve(); module != nil {
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
	if virtual {
		st := newVirtualCommandStation(r)
		r.commandStations = append(r.commandStations, st)
	} else {
		entity.GetCommandStations().ForEach(func(csRef model.CommandStationRef) {
			if cs, _ := csRef.TryResolve(); cs != nil {
				if st, ok := cs.Accept(builder).(CommandStation); ok {
					st.SetAddressSpaces(csRef.GetAddressSpaces())
					r.commandStations = append(r.commandStations, st)
				}
			}
		})
	}
	// Create locs
	entity.GetLocs().ForEach(func(locRef model.LocRef) {
		if loc, _ := locRef.TryResolve(); loc != nil {
			if st, ok := loc.Accept(builder).(Loc); ok {
				r.locs = append(r.locs, st)
			}
		}
	})

	// Prepare
	if err := prepareForUse(ctx, r, ui, persistence); err != nil {
		return nil, err
	}
	// Wrap up preparation
	finalizePrepare(ctx, r)

	return r, nil
}

// Exclusive runs the given function while holding an exclusive lock.
// This function allows nesting.
func (r *railway) Exclusive(ctx context.Context, cb func(context.Context) error) error {
	return r.exclusive.Exclusive(ctx, cb)
}

// Get logger
func (r *railway) Logger() zerolog.Logger {
	return r.log
}

// Try to prepare the entity for use.
// Returns nil when the entity is successfully prepared,
// returns an error otherwise.
func (r *railway) TryPrepareForUse(ctx context.Context, ui state.UserInterface, persistence state.Persistence) error {
	var err error
	// Note that the order of preparation is important
	r.ForEachCommandStation(func(x state.CommandStation) {
		ix := x.(CommandStation)
		multierr.AppendInto(&err, prepareForUse(ctx, ix, ui, persistence))
	})
	r.ForEachBlock(func(x state.Block) {
		ix := x.(Block)
		multierr.AppendInto(&err, prepareForUse(ctx, ix, ui, persistence))
	})
	r.ForEachBlockGroup(func(x state.BlockGroup) {
		ix := x.(BlockGroup)
		multierr.AppendInto(&err, prepareForUse(ctx, ix, ui, persistence))
	})
	r.ForEachJunction(func(x state.Junction) {
		ix := x.(Junction)
		multierr.AppendInto(&err, prepareForUse(ctx, ix, ui, persistence))
	})
	r.ForEachSensor(func(x state.Sensor) {
		ix := x.(Sensor)
		multierr.AppendInto(&err, prepareForUse(ctx, ix, ui, persistence))
	})
	r.ForEachSignal(func(x state.Signal) {
		ix := x.(Signal)
		multierr.AppendInto(&err, prepareForUse(ctx, ix, ui, persistence))
	})
	r.ForEachOutput(func(x state.Output) {
		ix := x.(Output)
		multierr.AppendInto(&err, prepareForUse(ctx, ix, ui, persistence))
	})
	r.ForEachRoute(func(x state.Route) {
		ix := x.(Route)
		multierr.AppendInto(&err, prepareForUse(ctx, ix, ui, persistence))
	})
	r.ForEachLoc(func(x state.Loc) {
		ix := x.(Loc)
		multierr.AppendInto(&err, prepareForUse(ctx, ix, ui, persistence))
	})

	if alc, xerr := automatic.NewAutomaticLocController(r, r.log); xerr != nil {
		multierr.AppendInto(&err, xerr)
	} else {
		r.automaticLocController = alc
	}
	return err
}

// Wrap up the preparation fase.
func (r *railway) FinalizePrepare(ctx context.Context) {
	// Note that the order of preparation is important
	r.ForEachCommandStation(func(x state.CommandStation) {
		ix := x.(CommandStation)
		finalizePrepare(ctx, ix)
	})
	r.ForEachBlock(func(x state.Block) {
		ix := x.(Block)
		finalizePrepare(ctx, ix)
	})
	r.ForEachBlockGroup(func(x state.BlockGroup) {
		ix := x.(BlockGroup)
		finalizePrepare(ctx, ix)
	})
	r.ForEachJunction(func(x state.Junction) {
		ix := x.(Junction)
		finalizePrepare(ctx, ix)
	})
	r.ForEachSensor(func(x state.Sensor) {
		ix := x.(Sensor)
		finalizePrepare(ctx, ix)
	})
	r.ForEachSignal(func(x state.Signal) {
		ix := x.(Signal)
		finalizePrepare(ctx, ix)
	})
	r.ForEachOutput(func(x state.Output) {
		ix := x.(Output)
		finalizePrepare(ctx, ix)
	})
	r.ForEachRoute(func(x state.Route) {
		ix := x.(Route)
		finalizePrepare(ctx, ix)
	})
	r.ForEachLoc(func(x state.Loc) {
		ix := x.(Loc)
		finalizePrepare(ctx, ix)
	})
}

// Try to resolve the given block (model) into a block state.
func (r *railway) ResolveBlock(ctx context.Context, block model.Block) (Block, error) {
	id := block.GetID()
	for _, x := range r.blocks {
		if x.GetID() == id {
			return x, nil
		}
	}
	return nil, fmt.Errorf("Block '%s' not found", id)
}

// Try to resolve the given block group into a block group state.
func (r *railway) ResolveBlockGroup(ctx context.Context, blockGroup model.BlockGroup) (BlockGroup, error) {
	id := blockGroup.GetID()
	for _, x := range r.blockGroups {
		if x.GetID() == id {
			return x, nil
		}
	}
	return nil, fmt.Errorf("BlockGroup '%s' not found", id)
}

// Try to resolve the given endpoint into a block state.
func (r *railway) ResolveEndPoint(ctx context.Context, endpoint model.EndPoint) (Block, error) {
	if block, ok := endpoint.(model.Block); ok {
		return r.ResolveBlock(ctx, block)
	}
	return nil, fmt.Errorf("Non block not implemented") // TODO
}

// Try to resolve the given junction (model) into a junction state.
func (r *railway) ResolveJunction(ctx context.Context, junction model.Junction) (Junction, error) {
	id := junction.GetID()
	for _, x := range r.junctions {
		if x.GetID() == id {
			return x, nil
		}
	}
	return nil, fmt.Errorf("Junction '%s' not found", id)
}

// Try to resolve the given sensor (model) into a sensor state.
func (r *railway) ResolveSensor(ctx context.Context, sensor model.Sensor) (Sensor, error) {
	id := sensor.GetID()
	for _, x := range r.sensors {
		if x.GetID() == id {
			return x, nil
		}
	}
	return nil, fmt.Errorf("Sensor '%s' not found", id)
}

// Try to resolve the given commandstatin into a commandstation state.
func (r *railway) ResolveCommandStation(ctx context.Context, cs model.CommandStation) (CommandStation, error) {
	id := cs.GetID()
	for _, x := range r.commandStations {
		if x.GetID() == id {
			return x, nil
		}
	}
	return nil, fmt.Errorf("Command station '%s' not found", id)
}

// Select a command station that can best drive the given entity
func (r *railway) SelectCommandStation(ctx context.Context, entity model.AddressEntity) (CommandStation, error) {
	addr := entity.GetAddress()
	if addr.IsEmpty() {
		// No address
		return nil, nil
	}
	// Lookup preferred command station
	var prefCS model.CommandStation
	var err error
	switch addr.Network.Type {
	case model.AddressTypeBinkyNet:
		prefCS, err = r.GetModel().GetPreferredBinkyNetCommandStation()
	case model.AddressTypeDcc:
		prefCS, err = r.GetModel().GetPreferredDccCommandStation()
	case model.AddressTypeLocoNet:
		prefCS, err = r.GetModel().GetPreferredLocoNetCommandStation()
	case model.AddressTypeMotorola:
		prefCS, err = r.GetModel().GetPreferredMotorolaCommandStation()
	case model.AddressTypeMfx:
		prefCS, err = r.GetModel().GetPreferredMfxCommandStation()
	case model.AddressTypeMqtt:
		prefCS, err = r.GetModel().GetPreferredMqttCommandStation()
	default:
		return nil, fmt.Errorf("Unknown network type: '%s'", addr.Network.Type)
	}
	if err != nil {
		return nil, err
	}
	if prefCS != nil {
		if cs, err := r.ResolveCommandStation(ctx, prefCS); err == nil {
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
func (r *railway) GetAutomaticLocController() state.AutomaticLocController {
	return r.automaticLocController
}

// Gets the state dispatcher
//        IStateDispatcher Dispatcher { get; }

// Gets the virtual mode.
// This will never return null.
func (r *railway) GetVirtualMode() state.VirtualMode {
	return r.virtualMode
}

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

// Gets the number of blocks in this railway
func (r *railway) GetBlockCount(context.Context) int {
	return len(r.blocks)
}

// Gets the state of the block with given ID.
// Returns nil if not found
func (r *railway) GetBlock(id string) (state.Block, error) {
	for _, x := range r.blocks {
		if x.GetID() == id {
			return x, nil
		}
	}
	return nil, nil
}

// Gets the states of all block groups in this railway
func (r *railway) ForEachBlockGroup(cb func(state.BlockGroup)) {
	for _, x := range r.blockGroups {
		cb(x)
	}
}

// Gets the number of block groups in this railway
func (r *railway) GetBlockGroupCount(context.Context) int {
	return len(r.blockGroups)
}

// Gets the states of all command stations in this railway
func (r *railway) ForEachCommandStation(cb func(state.CommandStation)) {
	for _, x := range r.commandStations {
		cb(x)
	}
}

// Gets the number of command stations in this railway
func (r *railway) GetCommandStationCount(context.Context) int {
	return len(r.commandStations)
}

// Gets the states of all junctions in this railway
func (r *railway) ForEachJunction(cb func(state.Junction)) {
	for _, x := range r.junctions {
		cb(x)
	}
}

// Gets the number of junctions in this railway
func (r *railway) GetJunctionCount(context.Context) int {
	return len(r.junctions)
}

// Gets the state of the junction with given ID.
// Returns nil if not found
func (r *railway) GetJunction(id string) (state.Junction, error) {
	for _, x := range r.junctions {
		if x.GetID() == id {
			return x, nil
		}
	}
	return nil, nil
}

// Gets the states of all locomotives in this railway
func (r *railway) ForEachLoc(cb func(state.Loc)) {
	for _, x := range r.locs {
		cb(x)
	}
}

// Gets the number of locomotives in this railway
func (r *railway) GetLocCount(context.Context) int {
	return len(r.locs)
}

// Gets the state of the loc with given ID.
// Returns nil if not found
func (r *railway) GetLoc(id string) (state.Loc, error) {
	for _, x := range r.locs {
		if x.GetID() == id {
			return x, nil
		}
	}
	return nil, nil
}

// Gets the states of all routes in this railway
func (r *railway) ForEachRoute(cb func(state.Route)) {
	for _, x := range r.routes {
		cb(x)
	}
}

// Gets the number of routes in this railway
func (r *railway) GetRouteCount(context.Context) int {
	return len(r.routes)
}

// Gets the state of the route with given ID.
// Returns nil if not found
func (r *railway) GetRoute(id string) (state.Route, error) {
	for _, x := range r.routes {
		if x.GetID() == id {
			return x, nil
		}
	}
	return nil, nil
}

// Gets the states of all sensors in this railway
func (r *railway) ForEachSensor(cb func(state.Sensor)) {
	for _, x := range r.sensors {
		cb(x)
	}
}

// Gets the number of sensors in this railway
func (r *railway) GetSensorCount(context.Context) int {
	return len(r.sensors)
}

// Gets the state of the sensor with given ID.
// Returns nil if not found
func (r *railway) GetSensor(id string) (state.Sensor, error) {
	for _, x := range r.sensors {
		if x.GetID() == id {
			return x, nil
		}
	}
	return nil, nil
}

// Gets the states of all signals in this railway
func (r *railway) ForEachSignal(cb func(state.Signal)) {
	for _, x := range r.signals {
		cb(x)
	}
}

// Gets the number of signals in this railway
func (r *railway) GetSignalCount(context.Context) int {
	return len(r.signals)
}

// Gets the state of the signal with given ID.
// Returns nil if not found
func (r *railway) GetSignal(id string) (state.Signal, error) {
	for _, x := range r.signals {
		if x.GetID() == id {
			return x, nil
		}
	}
	return nil, nil
}

// Gets the states of all outputs in this railway
func (r *railway) ForEachOutput(cb func(state.Output)) {
	for _, x := range r.outputs {
		cb(x)
	}
}

// Gets the number of outputs in this railway
func (r *railway) GetOutputCount(context.Context) int {
	return len(r.outputs)
}

// Gets the state of the output with given ID.
// Returns nil if not found
func (r *railway) GetOutput(id string) (state.Output, error) {
	for _, x := range r.outputs {
		if x.GetID() == id {
			return x, nil
		}
	}
	return nil, nil
}

// Close the railway
func (r *railway) Close(ctx context.Context) {
	// Stop virtual mode
	r.virtualMode.Close()
	// Stop automatic loc controller
	if r.automaticLocController != nil {
		r.automaticLocController.Close(ctx)
	}
	// Close all commandstations
	for _, cs := range r.commandStations {
		cs.Close(ctx)
	}
	// Stop event dispatching
	r.eventDispatcher.CancelAll()
	// TODO
}
