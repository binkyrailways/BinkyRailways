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

// Package abstracts a single file containing zero or more persistent entities.
type Package interface {
	// IValidationSubject

	// Event handler called when errors are detected
	OnError() EventHandler

	// Gets the default railway contained in this package.
	GetRailway() Railway

	// Remove the given entity from this package
	Remove(PersistentEntity) error

	// Add a new Bidib command station.
	AddNewBidibCommandStation() (BidibCommandStation, error)

	// Add a new BinkyNet command station.
	AddNewBinkyNetCommandStation() (BinkyNetCommandStation, error)

	// Add a new LocoBuffer type command station.
	AddNewLocoBufferCommandStation() (LocoBufferCommandStation, error)

	// Add a new DCC over RS232 type command station.
	AddNewDccOverRs232CommandStation() (DccOverRs232CommandStation, error)

	// Add a new Ecos command station.
	AddNewEcosCommandStation() (EcosCommandStation, error)

	// Add a new MQTT command station.
	AddNewMqttCommandStation() (MqttCommandStation, error)

	// Add a new P50x command station.
	AddNewP50xCommandStation() (P50xCommandStation, error)

	// Load a command station by it's id.
	// <returns>Null if not found</returns>
	GetCommandStation(id string) (CommandStation, error)

	// Get all command stations
	ForEachCommandStation(func(CommandStation)) error

	// Add a new loc.
	AddNewLoc() (Loc, error)

	// Load a loc by it's id.
	// <returns>Null if not found</returns>
	GetLoc(id string) (Loc, error)

	// Get all locs
	ForEachLoc(func(Loc)) error

	// Add a new module.
	AddNewModule() (Module, error)

	// Load a module by it's id.
	// <returns>Null if not found</returns>
	GetModule(id string) (Module, error)

	// Get all modules
	ForEachModule(func(Module)) error

	// Gets the ID's of all generic parts that belong to the given entity.
	GetGenericPartIDs(entity PersistentEntity) []string

	// Load a generic file part that belongs to the given entity by it's id.
	// Returns: nil if not found
	GetGenericPart(entity PersistentEntity, id string) ([]byte, error)

	// Store a generic file part that belongs to the given entity by it's id.
	SetGenericPart(entity PersistentEntity, id string, data []byte) error

	// Remove a generic file part that belongs to the given entity by it's id.
	RemoveGenericPart(entity PersistentEntity, id string) error

	// Save to disk.
	Save() error

	// Save to disk.
	SaveAs(path string) error

	// Has this package been changed since the last save?
	GetIsDirty() bool
}
