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

	// Add a new LocoBuffer type command station.
	//ILocoBufferCommandStation AddNewLocoBufferCommandStation();

	// Add a new DCC over RS232 type command station.
	//IDccOverRs232CommandStation AddNewDccOverRs232CommandStation();

	// Add a new Ecos command station.
	//IEcosCommandStation AddNewEcosCommandStation();

	// Add a new MQTT command station.
	//IMqttCommandStation AddNewMqttCommandStation();

	// Add a new P50x command station.
	//IP50xCommandStation AddNewP50xCommandStation();

	// Load a command station by it's id.
	// <returns>Null if not found</returns>
	//ICommandStation GetCommandStation(string id);

	// Get all command stations
	//IEnumerable<ICommandStation> GetCommandStations();

	// Add a new loc.
	AddNewLoc() (Loc, error)

	// Load a loc by it's id.
	// <returns>Null if not found</returns>
	GetLoc(id string) Loc

	// Get all locs
	ForEachLoc(func(Loc))

	// Add a new module.
	AddNewModule() (Module, error)

	// Load a module by it's id.
	// <returns>Null if not found</returns>
	GetModule(id string) Module

	// Get all modules
	ForEachModule(func(Module))

	// Gets the ID's of all generic parts that belong to the given entity.
	//IEnumerable<string> GetGenericPartIDs(IPersistentEntity entity);

	// Load a generic file part that belongs to the given entity by it's id.
	// <returns>Null if not found</returns>
	//Stream GetGenericPart(IPersistentEntity entity, string id);

	// Store a generic file part that belongs to the given entity by it's id.
	//void SetGenericPart(IPersistentEntity entity, string id, Stream source);

	// Remove a generic file part that belongs to the given entity by it's id.
	//void RemoveGenericPart(IPersistentEntity entity, string id);

	// Save to disk.
	Save(path string) error

	// Has this package been changed since the last save?
	GetIsDirty() bool
}