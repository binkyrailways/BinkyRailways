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
	"strings"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// CommandStation adds implementation functions to state.CommandStation.
type CommandStation interface {
	Entity
	state.CommandStation

	// Set the address spaces configured for this command station
	SetAddressSpaces(value []string)
	// Register the given junction as controlled by this command station
	RegisterJunction(state.Junction)
	// Register the given loc as controlled by this command station
	RegisterLoc(state.Loc)
	// Register the given input as controlled by this command station
	RegisterInput(state.Input)
	// Register the given output as controlled by this command station
	RegisterOutput(state.Output)
	// Register the given sensor as controlled by this command station
	RegisterSensor(state.Sensor)
	// Register the given signal as controlled by this command station
	RegisterSignal(state.Signal)

	// Close the commandstation
	Close(ctx context.Context)
}

// commandStation provides implementations for shared command station functions.
type commandStation struct {
	entity

	csRef         model.CommandStationRef
	addressSpaces []string
	junctions     []state.Junction
	locs          []state.Loc
	inputs        []state.Input
	outputs       []state.Output
	sensors       []state.Sensor
	signals       []state.Signal
}

// Create a new entity
func newCommandStation(en model.CommandStation, railway Railway, isVirtual bool) commandStation {
	csRef, found := railway.GetModel().GetCommandStations().Get(en.GetID())
	if !found && !isVirtual {
		panic("Command station ref not found")
	}
	var addressSpaces []string
	if found {
		addressSpaces = csRef.GetAddressSpaces()
	}
	return commandStation{
		csRef:         csRef,
		entity:        newEntity(railway.Logger().With().Str("cs", en.GetDescription()).Logger(), en, railway),
		addressSpaces: addressSpaces,
	}
}

// getCommandStation returns the entity as CommandStation.
func (cs *commandStation) getCommandStation() model.CommandStation {
	return cs.GetEntity().(model.CommandStation)
}

// Gets the underlying model
func (cs *commandStation) GetModel() model.CommandStation {
	return cs.getCommandStation()
}

// Gets the underlying model
func (cs *commandStation) GetModelRef() model.CommandStationRef {
	return cs.csRef
}

// Can this command station be used to serve the given network?
// <param name="entity">The entity being search for.</param>
// <param name="network">The network in question</param>
// <param name="exactMatch">Set to true when there is an exact match in address type and address space, false otherwise.</param>
// Returns: supports, exactMatch
func (cs *commandStation) Supports(entity model.AddressEntity, network model.Network) (bool, bool) {
	supportedTypes := cs.getCommandStation().GetSupportedAddressTypes(entity)
	typeSupported := false
	for _, x := range supportedTypes {
		if x == network.Type {
			typeSupported = true
			break
		}
	}
	if !typeSupported {
		return false, false
	}
	// There is a match in type, look for an exact match
	addressSpace := strings.ToLower(network.AddressSpace)
	if addressSpace == "" {
		// No address space specified, so no exact match unless cs has no address spaces specified
		return true, len(cs.addressSpaces) == 0
	}
	cs.log.Debug().
		Str("locAddrSpace", addressSpace).
		Strs("csAddrSpaces", cs.addressSpaces).
		Msg("Trying address space match")
	exactMatch := false
	for _, x := range cs.addressSpaces {
		if strings.ToLower(x) == addressSpace {
			exactMatch = true
		}
	}
	if len(cs.addressSpaces) > 0 && !exactMatch {
		// We have specific address spaces we support, the network has a different specific
		// address space, so we do not support it
		return false, false
	}
	return true, exactMatch
}

// Junctions driven by this command station
func (cs *commandStation) ForEachJunction(cb func(state.Junction)) {
	for _, x := range cs.junctions {
		cb(x)
	}
}

// Locomotives driven by this command station
func (cs *commandStation) ForEachLoc(cb func(state.Loc)) {
	for _, x := range cs.locs {
		cb(x)
	}
}

// Input signals driven by this command station (overlaps with <see cref="Sensors"/>).
func (cs *commandStation) ForEachInput(cb func(state.Input)) {
	for _, x := range cs.inputs {
		cb(x)
	}
}

// Outputs driven by this command station.
func (cs *commandStation) ForEachOutput(cb func(state.Output)) {
	for _, x := range cs.outputs {
		cb(x)
	}
}

// Sensors driven by this command station
func (cs *commandStation) ForEachSensor(cb func(state.Sensor)) {
	for _, x := range cs.sensors {
		cb(x)
	}
}

// Signals driven by this command station
func (cs *commandStation) ForEachSignal(cb func(state.Signal)) {
	for _, x := range cs.signals {
		cb(x)
	}
}

// Set the address spaces configured for this command station
func (cs *commandStation) SetAddressSpaces(value []string) {
	cs.addressSpaces = value
}

// Register the given junction as controlled by this command station
func (cs *commandStation) RegisterJunction(x state.Junction) {
	cs.junctions = append(cs.junctions, x)
}

// Register the given loc as controlled by this command station
func (cs *commandStation) RegisterLoc(x state.Loc) {
	cs.locs = append(cs.locs, x)
}

// Register the given input as controlled by this command station
func (cs *commandStation) RegisterInput(x state.Input) {
	cs.inputs = append(cs.inputs, x)
}

// Register the given output as controlled by this command station
func (cs *commandStation) RegisterOutput(x state.Output) {
	cs.outputs = append(cs.outputs, x)
}

// Register the given sensor as controlled by this command station
func (cs *commandStation) RegisterSensor(x state.Sensor) {
	cs.sensors = append(cs.sensors, x)
}

// Register the given signal as controlled by this command station
func (cs *commandStation) RegisterSignal(x state.Signal) {
	cs.signals = append(cs.signals, x)
}
