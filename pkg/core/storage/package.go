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
	"sync"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model/impl"
	"golang.org/x/net/html/charset"
)

type packageImpl struct {
	railway model.Railway

	onError        impl.EventHandler
	mutex          sync.Mutex
	parts          map[string][]byte                 // uri -> data
	loadedEntities map[string]model.PersistentEntity // uri -> entity
	dirty          bool
	path           string
}

var _ model.Package = &packageImpl{}

const (
	railwayID             = "_default"
	currentPackageVersion = "2.0.0"
)

// IValidationSubject

func newPackage(path string) *packageImpl {
	return &packageImpl{
		onError:        impl.NewEventHandler(),
		parts:          make(map[string][]byte),
		loadedEntities: make(map[string]model.PersistentEntity),
		path:           path,
	}
}

// NewPackage creates a new empty package
func NewPackage(path string) model.Package {
	p := newPackage(path)
	p.railway = impl.NewRailway(p)
	p.railway.SetDescription("New railway")
	uri := createPartURI(impl.PackageFolderRailway, railwayID)
	p.loadedEntities[uri] = p.railway
	p.dirty = true
	return p
}

// NewPackageFromFile loads a package from file
func NewPackageFromFile(path string) (model.Package, error) {
	p := newPackage(path)

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
			}
		}
	}
	// Remove shared objects from memory
	for uri := range p.parts {
		if isSharedObject(uri) {
			delete(p.parts, uri)
		}
	}

	// Read railway
	entry, err := p.ReadEntity(impl.PackageFolderRailway, railwayID, impl.NewRailway(p))
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

// Add a new Bidib command station.
func (p *packageImpl) AddNewBidibCommandStation() (model.BidibCommandStation, error) {
	cs := impl.NewBidibCommandStation(p)
	cs.SetPackage(p)
	uri := createPartURI(impl.PackageFolderCommandStation, cs.GetID())

	p.mutex.Lock()
	defer p.mutex.Unlock()
	p.loadedEntities[uri] = cs
	p.dirty = true
	return cs, nil
}

// Add a new BinkyNet command station.
func (p *packageImpl) AddNewBinkyNetCommandStation() (model.BinkyNetCommandStation, error) {
	cs := impl.NewBinkyNetCommandStation(p)
	cs.SetPackage(p)
	uri := createPartURI(impl.PackageFolderCommandStation, cs.GetID())

	p.mutex.Lock()
	defer p.mutex.Unlock()
	p.loadedEntities[uri] = cs
	p.dirty = true
	return cs, nil
}

// Add a new LocoBuffer type command station.
func (p *packageImpl) AddNewLocoBufferCommandStation() (model.LocoBufferCommandStation, error) {
	cs := impl.NewLocoBufferCommandStation(p)
	cs.SetPackage(p)
	uri := createPartURI(impl.PackageFolderCommandStation, cs.GetID())

	p.mutex.Lock()
	defer p.mutex.Unlock()
	p.loadedEntities[uri] = cs
	p.dirty = true
	return cs, nil
}

// Add a new DCC over RS232 type command station.
func (p *packageImpl) AddNewDccOverRs232CommandStation() (model.DccOverRs232CommandStation, error) {
	cs := impl.NewDccOverRs232CommandStation(p)
	cs.SetPackage(p)
	uri := createPartURI(impl.PackageFolderCommandStation, cs.GetID())

	p.mutex.Lock()
	defer p.mutex.Unlock()
	p.loadedEntities[uri] = cs
	p.dirty = true
	return cs, nil
}

// Add a new Ecos command station.
func (p *packageImpl) AddNewEcosCommandStation() (model.EcosCommandStation, error) {
	cs := impl.NewEcosCommandStation(p)
	cs.SetPackage(p)
	uri := createPartURI(impl.PackageFolderCommandStation, cs.GetID())

	p.mutex.Lock()
	defer p.mutex.Unlock()
	p.loadedEntities[uri] = cs
	p.dirty = true
	return cs, nil
}

// Add a new MQTT command station.
func (p *packageImpl) AddNewMqttCommandStation() (model.MqttCommandStation, error) {
	cs := impl.NewMqttCommandStation(p)
	cs.SetPackage(p)
	uri := createPartURI(impl.PackageFolderCommandStation, cs.GetID())

	p.mutex.Lock()
	defer p.mutex.Unlock()
	p.loadedEntities[uri] = cs
	p.dirty = true
	return cs, nil
}

// Add a new P50x command station.
func (p *packageImpl) AddNewP50xCommandStation() (model.P50xCommandStation, error) {
	cs := impl.NewP50xCommandStation(p)
	cs.SetPackage(p)
	uri := createPartURI(impl.PackageFolderCommandStation, cs.GetID())

	p.mutex.Lock()
	defer p.mutex.Unlock()
	p.loadedEntities[uri] = cs
	p.dirty = true
	return cs, nil
}

// Load a command station by it's id.
// <returns>Null if not found</returns>
func (p *packageImpl) GetCommandStation(id string) (model.CommandStation, error) {
	entity, err := p.ReadEntity(impl.PackageFolderCommandStation, id)
	if err != nil {
		p.onError.Invoke(err)
		return nil, err
	}
	if entity == nil {
		return nil, fmt.Errorf("entity is nil")
	}
	if result, ok := entity.(model.CommandStation); ok {
		return result, nil
	}
	p.onError.Invoke("Entity is not of type CommandStation")
	return nil, fmt.Errorf("Entity is not of type CommandStation")

}

// Get all command stations
func (p *packageImpl) ForEachCommandStation(cb func(model.CommandStation)) error {
	for uri := range p.parts {
		if result, id := uriHasFolder(uri, impl.PackageFolderCommandStation); result {
			if cs, err := p.GetCommandStation(id); err != nil {
				return err
			} else if cs != nil {
				cb(cs)
			}
		}
	}
	return nil
}

// Add a new loc.
func (p *packageImpl) AddNewLoc() (model.Loc, error) {
	result := impl.NewLoc(p)
	uri := createPartURI(impl.PackageFolderLoc, result.GetID())

	p.mutex.Lock()
	defer p.mutex.Unlock()
	p.loadedEntities[uri] = result
	p.dirty = true
	return result, nil
}

// Load a loc by it's id.
// <returns>Null if not found</returns>
func (p *packageImpl) GetLoc(id string) (model.Loc, error) {
	entity, err := p.ReadEntity(impl.PackageFolderLoc, id)
	if err != nil {
		p.onError.Invoke(err)
		return nil, err
	}
	if entity == nil {
		return nil, fmt.Errorf("entity is nil")
	}
	if result, ok := entity.(model.Loc); ok {
		return result, nil
	}
	p.onError.Invoke("Entity is not of type Loc")
	return nil, fmt.Errorf("Entity is not of type Loc")
}

// Get all locs
func (p *packageImpl) ForEachLoc(cb func(model.Loc)) error {
	for uri := range p.parts {
		if result, id := uriHasFolder(uri, impl.PackageFolderLoc); result {
			loc, err := p.GetLoc(id)
			if err != nil {
				return err
			} else if loc != nil {
				cb(loc)
			}
		}
	}
	return nil
}

// Add a new module.
func (p *packageImpl) AddNewModule() (model.Module, error) {
	result := impl.NewModule(p)
	uri := createPartURI(impl.PackageFolderModule, result.GetID())

	p.mutex.Lock()
	defer p.mutex.Unlock()
	p.loadedEntities[uri] = result
	p.dirty = true
	return result, nil
}

// Load a module by it's id.
// <returns>Null if not found</returns>
func (p *packageImpl) GetModule(id string) (model.Module, error) {
	entity, err := p.ReadEntity(impl.PackageFolderModule, id)
	if err != nil {
		p.onError.Invoke(fmt.Sprintf("ReadEntity: %v", err))
		return nil, err
	}
	if entity == nil {
		return nil, fmt.Errorf("Entity not found")
	}
	if result, ok := entity.(model.Module); ok {
		return result, nil
	}
	p.onError.Invoke("Entity is not of type Module")
	return nil, fmt.Errorf("Entity not of type Module")
}

// Get all modules
func (p *packageImpl) ForEachModule(cb func(model.Module)) error {
	for uri := range p.parts {
		if result, id := uriHasFolder(uri, impl.PackageFolderModule); result {
			if module, err := p.GetModule(id); err != nil {
				return err
			} else if module != nil {
				cb(module)
			}
		}
	}
	return nil
}

// Gets the ID's of all generic parts that belong to the given entity.
func (p *packageImpl) GetGenericPartIDs(entity model.PersistentEntity) []string {
	uriPrefix := createPartURI(impl.PackageFolderGenericParts, entity.GetID()) + "/"
	var result []string
	for uri := range p.parts {
		if strings.HasPrefix(uri, uriPrefix) {
			id := uri[len(uriPrefix):]
			result = append(result, id)
		}
	}
	return result
}

// Load a generic file part that belongs to the given entity by it's id.
// Returns: nil if not found
func (p *packageImpl) GetGenericPart(entity model.PersistentEntity, id string) ([]byte, error) {
	uri := createPartURI(impl.PackageFolderGenericParts, path.Join(entity.GetID(), id))
	// Already loaded?
	p.mutex.Lock()
	defer p.mutex.Unlock()
	if result, found := p.parts[uri]; found {
		return result, nil
	}
	return nil, nil
}

// Store a generic file part that belongs to the given entity by it's id.
func (p *packageImpl) SetGenericPart(entity model.PersistentEntity, id string, data []byte) error {
	uri := createPartURI(impl.PackageFolderGenericParts, path.Join(entity.GetID(), id))

	p.mutex.Lock()
	defer p.mutex.Unlock()
	p.parts[uri] = data
	p.dirty = true
	return nil
}

// Remove a generic file part that belongs to the given entity by it's id.
func (p *packageImpl) RemoveGenericPart(entity model.PersistentEntity, id string) error {
	uri := createPartURI(impl.PackageFolderGenericParts, path.Join(entity.GetID(), id))
	p.mutex.Lock()
	defer p.mutex.Unlock()
	if _, found := p.parts[uri]; found {
		delete(p.parts, uri)
		p.dirty = true
	}
	return nil
}

// Save to disk.
func (p *packageImpl) Save() error {
	if err := p.SaveAs(p.path); err != nil {
		return err
	}
	p.dirty = false
	return nil
}

// Save to disk.
func (p *packageImpl) SaveAs(path string) error {
	p.mutex.Lock()
	defer p.mutex.Unlock()

	// Update all parts
	if err := p.updateEntityParts(); err != nil {
		return err
	}

	// Create archive
	buf := bytes.Buffer{}
	zf := zip.NewWriter(&buf)

	// Add all parts
	for uri, data := range p.parts {
		w, err := zf.Create(uri)
		if err != nil {
			return err
		}
		if _, err := io.Copy(w, bytes.NewReader(data)); err != nil {
			return err
		}
	}

	// Close archive
	if err := zf.Close(); err != nil {
		return err
	}

	// Write to disk
	if err := ioutil.WriteFile(path, buf.Bytes(), 0644); err != nil {
		return err
	}

	return nil
}

// Has this package been changed since the last save?
func (p *packageImpl) GetIsDirty() bool {
	return p.dirty
}

// Read an entity in a given folder.
// Returns: nil if not found
func (p *packageImpl) ReadEntity(folder, id string, template ...model.PersistentEntity) (model.PersistentEntity, error) {
	p.mutex.Lock()
	defer p.mutex.Unlock()

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

	container := PersistentEntityContainer{}
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

// updateEntityParts marshals all loaded entities and updates their parts.
func (p *packageImpl) updateEntityParts() error {
	for uri, entity := range p.loadedEntities {
		// Prepare encoder
		buf := bytes.Buffer{}
		encoder := xml.NewEncoder(&buf)

		// Prepare container
		container := PersistentEntityContainer{
			Entity: impl.PersistentEntityContainer{
				PersistentEntity: entity,
			},
			Version: currentPackageVersion,
		}

		// Encode container
		if err := encoder.Encode(container); err != nil {
			return err
		}

		// Update part
		if entity == p.railway {
			uri = createPartURI(impl.PackageFolderRailway, railwayID)
		}
		p.parts[uri] = buf.Bytes()
		fmt.Println(uri)
	}
	return nil
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
