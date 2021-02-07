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
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// CommandStation adds implementation functions to state.CommandStation.
type CommandStation interface {
	Entity
	state.CommandStation

	// Register the given junction as controlled by this command station
	RegisterJunction(state.Junction)
	// Register the given loc as controlled by this command station
	RegisterLoc(state.Loc)
	// Register the given input as controlled by this command station
	RegisterInput(state.Input)
	// Register the given sensor as controlled by this command station
	RegisterSensor(state.Sensor)
	// Register the given signal as controlled by this command station
	RegisterSignal(state.Signal)
}

// commandStation provides implementations for shared command station functions.
type commandStation struct {
	entity

	junctions []state.Junction
	locs      []state.Loc
	inputs    []state.Input
	sensors   []state.Sensor
	signals   []state.Signal
}

// getCommandStation returns the entity as CommandStation.
func (cs *commandStation) getCommandStation() model.CommandStation {
	return cs.GetEntity().(model.CommandStation)
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

// Register the given sensor as controlled by this command station
func (cs *commandStation) RegisterSensor(x state.Sensor) {
	cs.sensors = append(cs.sensors, x)
}

// Register the given signal as controlled by this command station
func (cs *commandStation) RegisterSignal(x state.Signal) {
	cs.signals = append(cs.signals, x)
}
