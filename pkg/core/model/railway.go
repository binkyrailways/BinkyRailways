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

package model

// Railway is the combined description of entire railway.
type Railway interface {
	PersistentEntity

	// Gets command stations used in this railway
	GetCommandStations() CommandStationRefSet

	// Gets locomotives used in this railway
	GetLocs() LocRefSet

	// Gets all groups of locs used in this railway.
	GetLocGroups() LocGroupSet

	// Gets modules used in this railway
	GetModules() ModuleRefSet

	// Gets the connections between modules used in this railway
	GetModuleConnections() ModuleConnectionSet

	// Gets/sets the background image of the this module.
	// <value>Null if there is no image.</value>
	// <remarks>Image must be png, bmp, gif, jpg, wmf or emf</remarks>
	//Stream BackgroundImage { get; set; }

	// Gets the number of times human time is speed up to reach model time.
	GetClockSpeedFactor() int
	// Sets the number of times human time is speed up to reach model time.
	SetClockSpeedFactor(value int) error

	// Gets the builder used to create predicates.
	GetPredicateBuilder() LocPredicateBuilder

	// Preferred command station for BinkyNet addresses.
	GetPreferredBinkyNetCommandStation() (CommandStation, error)
	SetPreferredBinkyNetCommandStation(value CommandStation) error

	// Preferred command station for DCC addresses.
	GetPreferredDccCommandStation() (CommandStation, error)
	SetPreferredDccCommandStation(value CommandStation) error

	// Preferred command station for LocoNet addresses.
	GetPreferredLocoNetCommandStation() (CommandStation, error)
	SetPreferredLocoNetCommandStation(value CommandStation) error

	// Preferred command station for Motorola addresses.
	GetPreferredMotorolaCommandStation() (CommandStation, error)
	SetPreferredMotorolaCommandStation(value CommandStation) error

	// Preferred command station for MFX addresses.
	GetPreferredMfxCommandStation() (CommandStation, error)
	SetPreferredMfxCommandStation(value CommandStation) error

	// Preferred command station for Mqtt addresses.
	GetPreferredMqttCommandStation() (CommandStation, error)
	SetPreferredMqttCommandStation(value CommandStation) error

	// Validate the railway, returning all findings
	Validate() []Finding
}
