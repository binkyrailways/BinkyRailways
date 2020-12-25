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

package storage

import (
	"archive/zip"
	"bytes"
	"encoding/xml"
	"fmt"
	"io"
	"io/ioutil"
	"path"
	"strings"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model/impl"
	"golang.org/x/net/html/charset"
)

type packageImpl struct {
	railway model.Railway

	onError        impl.EventHandler
	parts          map[string][]byte
	loadedEntities map[string]model.PersistentEntity
	dirty          bool
}

var _ model.Package = &packageImpl{}

// IValidationSubject

func newPackage() *packageImpl {
	return &packageImpl{
		onError:        impl.NewEventHandler(),
		parts:          make(map[string][]byte),
		loadedEntities: make(map[string]model.PersistentEntity),
	}
}

// NewPackage creates a new empty package
func NewPackage() model.Package {
	p := newPackage()
	p.railway = impl.NewRailway(p)
	return p
}

// NewPackageFromFile loads a package from file
func NewPackageFromFile(path string) (model.Package, error) {
	p := newPackage()

	// Open a zip archive for reading.
	r, err := zip.OpenReader(path)
	if err != nil {
		return nil, err
	}
	defer r.Close()

	// Iterate through the files in the archive,
	for _, f := range r.File {
		rc, err := f.Open()
		if err != nil {
			return nil, err
		}
		data, err := ioutil.ReadAll(rc)
		if err != nil {
			return nil, err
		}
		p.parts[f.Name] = data

	}
	// Resolve shared objects
	for uri, data := range p.parts {
		if !isSharedObject(uri) {
			hexID := fmt.Sprintf("%x", data)
			sharedURI := createSharedObjectPartURI(hexID)
			if sharedData, found := p.parts[sharedURI]; found {
				p.parts[uri] = sharedData
			} else {
				fmt.Printf("%s %s\n", uri, sharedURI)
			}
		}
	}

	// Read railway
	entry, err := p.ReadEntity(impl.PackageFolderRailway, "_default", impl.NewRailway(p))
	if err != nil {
		return nil, err
	}
	if entry == nil {
		return nil, fmt.Errorf("Railway entry not found")
	}
	p.railway = entry.(model.Railway)

	return p, nil
}

// Event handler called when errors are detected
func (p *packageImpl) OnError() model.EventHandler {
	return p.onError
}

// Gets the default railway contained in this package.
func (p *packageImpl) GetRailway() model.Railway {
	return p.railway
}

// Remove the given entity from this package
func (p *packageImpl) Remove(model.PersistentEntity) error {
	// TODO
	return nil
}

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
func (p *packageImpl) AddNewLoc() (model.Loc, error) {
	result := impl.NewLoc()
	uri := createPartURI(impl.PackageFolderLoc, result.GetID())
	p.loadedEntities[uri] = result
	p.dirty = true
	// TODO add to parts
	return result, nil
}

// Load a loc by it's id.
// <returns>Null if not found</returns>
func (p *packageImpl) GetLoc(id string) model.Loc {
	entity, err := p.ReadEntity(impl.PackageFolderLoc, id)
	if err != nil {
		p.onError.Invoke(err)
		return nil
	}
	if entity == nil {
		return nil
	}
	if result, ok := entity.(model.Loc); ok {
		return result
	}
	p.onError.Invoke("Entity is not of type Loc")
	return nil
}

// Get all locs
func (p *packageImpl) ForEachLoc(cb func(model.Loc)) {
	for uri := range p.parts {
		if result, id := uriHasFolder(uri, impl.PackageFolderLoc); result {
			loc := p.GetLoc(id)
			if loc != nil {
				cb(loc)
			}
		}
	}
}

// Add a new module.
func (p *packageImpl) AddNewModule() (model.Module, error) {
	result := impl.NewModule()
	uri := createPartURI(impl.PackageFolderModule, result.GetID())
	p.loadedEntities[uri] = result
	p.dirty = true
	return result, nil
}

// Load a module by it's id.
// <returns>Null if not found</returns>
func (p *packageImpl) GetModule(id string) model.Module {
	entity, err := p.ReadEntity(impl.PackageFolderModule, id)
	if err != nil {
		p.onError.Invoke(fmt.Sprintf("ReadEntity: %v", err))
		return nil
	}
	if entity == nil {
		return nil
	}
	if result, ok := entity.(model.Module); ok {
		return result
	}
	p.onError.Invoke("Entity is not of type Module")
	return nil
}

// Get all modules
func (p *packageImpl) ForEachModule(cb func(model.Module)) {
	for uri := range p.parts {
		if result, id := uriHasFolder(uri, impl.PackageFolderModule); result {
			module := p.GetModule(id)
			if module != nil {
				cb(module)
			}
		}
	}
}

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
func (p *packageImpl) Save(path string) error {
	return fmt.Errorf("Not implemented")
}

// Has this package been changed since the last save?
func (p *packageImpl) GetIsDirty() bool {
	return p.dirty
}

/// <summary>
/// Read an entity in a given folder.
/// </summary>
/// <returns>Null if not found</returns>
func (p *packageImpl) ReadEntity(folder, id string, template ...model.PersistentEntity) (model.PersistentEntity, error) {
	uri := createPartURI(folder, id)
	// Already loaded?
	if result, found := p.loadedEntities[uri]; found {
		return result, nil
	}
	// Get raw data
	data, found := p.parts[uri]
	if !found {
		return nil, nil
	}

	nr, err := charset.NewReader(bytes.NewReader(data), "utf-16")
	if err != nil {
		return nil, err
	}
	decoder := xml.NewDecoder(nr)
	decoder.CharsetReader = func(charset string, input io.Reader) (io.Reader, error) { return input, nil }

	container := persistentEntityContainer{}
	if len(template) == 1 {
		container.Entity.PersistentEntity = template[0]
	}
	if err := decoder.Decode(&container); err != nil {
		p.onError.Invoke(fmt.Sprintf("Unmarshal: %v %v", err, container))
		return nil, err
	}

	result := container.Entity.PersistentEntity.(impl.PersistentEntity)
	result.SetPackage(p)
	p.loadedEntities[uri] = result
	result.Upgrade()
	return result, nil
}

// createPartURI creates an uri for a part with given id in given folder.
func createPartURI(folder, id string) string {
	return path.Join(folder, id)
}

// Create an uri for a generic part with given id in given folder.
func createGenericPartURI(entity model.PersistentEntity, id string) string {
	return path.Join(impl.PackageFolderGenericParts, entity.GetID(), id)
}

// Create an uri for a shared object with a given hash.
func createSharedObjectPartURI(objectHash string) string {
	return path.Join(impl.PackageFolderObjects, objectHash)
}

// isSharedObject returns true if the given Uri an Uri of a shared object?
func isSharedObject(uri string) bool {
	result, _ := uriHasFolder(uri, impl.PackageFolderObjects)
	return result
}

// uriHasFolder returns true if the given Uri is contained in the given folder
// Returns: result, id
func uriHasFolder(uri, folder string) (bool, string) {
	uri = strings.TrimPrefix(uri, "/")
	splitted := strings.SplitN(uri, "/", 2)
	var remaining string
	if len(splitted) > 1 {
		remaining = splitted[1]
	}
	return splitted[0] == folder, remaining
}
