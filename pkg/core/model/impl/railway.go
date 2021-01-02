// Copyright 2020 Ewout Prangsma
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
	"github.com/binkyrailways/BinkyRailways/pkg/core/model/refs"
)

type Railway interface {
	model.Railway
	PersistentEntity
}

type railway struct {
	entity
	persistentEntity

	p                                 model.Package
	Locs                              railwayLocRefSet            `xml:"Locs"`
	LocGroups                         locGroupSet                 `xml:"LocGroups"`
	Modules                           railwayModuleRefSet         `xml:"Modules"`
	ModuleConnections                 moduleConnectionSet         `xml:"ModuleConnections"`
	CommandStations                   railwayCommandStationRefSet `xml:"CommandStations"`
	ClockSpeedFactor                  *int                        `xml:"ClockSpeedFactor,omitempty"`
	PreferredDccCommandStationID      commandStationRef           `xml:"PreferredDccCommandStationId,omitempty"`
	PreferredLocoNetCommandStationID  commandStationRef           `xml:"PreferredLocoNetCommandStationId,omitempty"`
	PreferredMotorolaCommandStationID commandStationRef           `xml:"PreferredMotorolaCommandStationId,omitempty"`
	PreferredMfxCommandStationID      commandStationRef           `xml:"PreferredMfxCommandStationId,omitempty"`
	PreferredMqttCommandStationID     commandStationRef           `xml:"PreferredMqttCommandStationId,omitempty"`
	locPredicateBuilder
}

var (
	_ Railway = &railway{}
)

// NewRailway initialize a new railway
func NewRailway(p model.Package) Railway {
	r := &railway{
		p: p,
	}
	r.persistentEntity.Initialize(r.entity.OnModified)
	r.Locs.SetContainer(r)
	r.LocGroups.SetContainer(r)
	r.Modules.SetContainer(r)
	r.ModuleConnections.SetContainer(r)
	r.CommandStations.SetContainer(r)
	return r
}

// Accept a visit by the given visitor
func (r *railway) Accept(v model.EntityVisitor) interface{} {
	return v.VisitRailway(r)
}

// Return the containing railway
func (r *railway) GetRailway() model.Railway {
	return r
}

// Gets command stations used in this railway
func (r *railway) GetCommandStations() model.CommandStationRefSet {
	return &r.CommandStations
}

// Gets locomotives used in this railway
func (r *railway) GetLocs() model.LocRefSet {
	return &r.Locs
}

// Gets all groups of locs used in this railway.
func (r *railway) GetLocGroups() model.LocGroupSet {
	return &r.LocGroups
}

// Gets modules used in this railway
func (r *railway) GetModules() model.ModuleRefSet {
	return &r.Modules
}

// Gets the connections between modules used in this railway
func (r *railway) GetModuleConnections() model.ModuleConnectionSet {
	return &r.ModuleConnections
}

// Gets/sets the background image of the this module.
// <value>Null if there is no image.</value>
// <remarks>Image must be png, bmp, gif, jpg, wmf or emf</remarks>
//Stream BackgroundImage { get; set; }

// Gets the number of times human time is speed up to reach model time.
func (r *railway) GetClockSpeedFactor() int {
	return refs.IntValue(r.ClockSpeedFactor, model.DefaultRailwayClockSpeedFactor)
}

// Sets the number of times human time is speed up to reach model time.
func (r *railway) SetClockSpeedFactor(value int) error {
	if r.GetClockSpeedFactor() != value {
		r.ClockSpeedFactor = refs.NewInt(value)
		r.OnModified()
	}
	return nil
}

// Gets the builder used to create predicates.
func (r *railway) GetPredicateBuilder() model.LocPredicateBuilder {
	return &r.locPredicateBuilder
}

// Preferred command station for DCC addresses.
func (r *railway) GetPreferredDccCommandStation() model.CommandStation {
	return r.PreferredDccCommandStationID.TryResolve()
}
func (r *railway) SetPreferredDccCommandStation(value model.CommandStation) error {
	return r.PreferredDccCommandStationID.Set(value, r.OnModified)
}

// Preferred command station for LocoNet addresses.
func (r *railway) GetPreferredLocoNetCommandStation() model.CommandStation {
	return r.PreferredLocoNetCommandStationID.TryResolve()
}
func (r *railway) SetPreferredLocoNetCommandStation(value model.CommandStation) error {
	return r.PreferredLocoNetCommandStationID.Set(value, r.OnModified)
}

// Preferred command station for Motorola addresses.
func (r *railway) GetPreferredMotorolaCommandStation() model.CommandStation {
	return r.PreferredMotorolaCommandStationID.TryResolve()
}
func (r *railway) SetPreferredMotorolaCommandStation(value model.CommandStation) error {
	return r.PreferredMotorolaCommandStationID.Set(value, r.OnModified)
}

// Preferred command station for MFX addresses.
func (r *railway) GetPreferredMfxCommandStation() model.CommandStation {
	return r.PreferredMfxCommandStationID.TryResolve()
}
func (r *railway) SetPreferredMfxCommandStation(value model.CommandStation) error {
	return r.PreferredMfxCommandStationID.Set(value, r.OnModified)
}

// Preferred command station for Mqtt addresses.
func (r *railway) GetPreferredMqttCommandStation() model.CommandStation {
	return r.PreferredMqttCommandStationID.TryResolve()
}
func (r *railway) SetPreferredMqttCommandStation(value model.CommandStation) error {
	return r.PreferredMqttCommandStationID.Set(value, r.OnModified)
}

// Upgrade to latest version
func (r *railway) Upgrade() {
	// Empty on purpose
}
