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
)

type Railway interface {
	model.Railway
	PersistentEntity
}

type railway struct {
	entity
	persistentEntity

	p                model.Package
	locs             locSet
	modules          moduleSet
	clockSpeedFactor int
}

var (
	_ Railway = &railway{}
)

// NewRailway initialize a new railway
func NewRailway(p model.Package) Railway {
	r := &railway{
		p:                p,
		clockSpeedFactor: model.DefaultRailwayClockSpeedFactor,
	}
	r.persistentEntity.Initialize(r.entity.OnModified)
	r.locs.Initialize(r.entity.OnModified, p.GetLoc)
	r.modules.Initialize(r.entity.OnModified, p.GetModule)
	return r
}

// Gets command stations used in this railway
//IPersistentEntityRefSet<ICommandStationRef, ICommandStation> CommandStations { get; }

// Gets locomotives used in this railway
func (r *railway) GetLocs() model.LocSet {
	return &r.locs
}

// Gets all groups of locs used in this railway.
//IEntitySet2<ILocGroup> LocGroups { get; }

// Gets modules used in this railway
func (r *railway) GetModules() model.ModuleSet {
	return &r.modules
}

// Gets the connections between modules used in this railway
//IEntitySet2<IModuleConnection> ModuleConnections { get; }

// Gets/sets the background image of the this module.
// <value>Null if there is no image.</value>
// <remarks>Image must be png, bmp, gif, jpg, wmf or emf</remarks>
//Stream BackgroundImage { get; set; }

// Gets the number of times human time is speed up to reach model time.
func (r *railway) GetClockSpeedFactor() int {
	return r.clockSpeedFactor
}

// Sets the number of times human time is speed up to reach model time.
func (r *railway) SetClockSpeedFactor(value int) error {
	if r.clockSpeedFactor != value {
		r.clockSpeedFactor = value
		r.OnModified()
	}
	return nil
}

// Gets the builder used to create predicates.
//ILocPredicateBuilder PredicateBuilder { get; }

// Preferred command station for DCC addresses.
//ICommandStation PreferredDccCommandStation { get; set; }

// Preferred command station for LocoNet addresses.
//ICommandStation PreferredLocoNetCommandStation { get; set; }

// Preferred command station for Motorola addresses.
//ICommandStation PreferredMotorolaCommandStation { get; set; }

// Preferred command station for MFX addresses.
//ICommandStation PreferredMfxCommandStation { get; set; }

// Preferred command station for Mqtt addresses.
//        ICommandStation PreferredMqttCommandStation { get; set; }

// Upgrade to latest version
func (r *railway) Upgrade() {
	// Empty on purpose
}
