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
	"path/filepath"
	"strings"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model/esphome"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model/refs"
)

// BinkyNetCommandStation extends model BinkyNetCommandStation with implementation methods
type BinkyNetCommandStation interface {
	model.BinkyNetCommandStation
	PersistentEntity
}

const (
	defaultGRPCPort              = 8823
	defaultRequiredWorkerVersion = "1.0.0"
	defaultExcludeUnUsedObjects  = true
)

type binkyNetCommandStation struct {
	commandStation

	ServerHost            *string                `xml:ServerHost,omitempty"`
	GRPCPort              *int                   `xml:"GRPCPort,omitempty"`
	RequiredWorkerVersion *string                `xml:"RequiredWorkerVersion,omitempty"`
	LocalWorkers          binkyNetLocalWorkerSet `xml:"LocalWorkers,omitempty"`
	ExcludeUnUsedObjects  *bool                  `xml:"ExcludeUnUsedObjects,omitempty"`
}

var _ model.BinkyNetCommandStation = &binkyNetCommandStation{}

// NewBinkyNetCommandStation creates a new BinkyNet type command station
func NewBinkyNetCommandStation(p model.Package) BinkyNetCommandStation {
	cs := &binkyNetCommandStation{}
	cs.Initialize()
	cs.EnsureID()
	cs.SetPackage(p)
	cs.SetDescription("New BinkyNet command station")
	cs.LocalWorkers.SetContainer(cs)
	return cs
}

// GetEntityType returns the type of this entity
func (cs *binkyNetCommandStation) GetEntityType() string {
	return TypeBinkyNetCommandStation
}

// Accept a visit by the given visitor
func (cs *binkyNetCommandStation) Accept(v model.EntityVisitor) interface{} {
	return v.VisitBinkyNetCommandStation(cs)
}

// What types of addresses does this command station support?
// The result may vary depending on the type of the optional given entity.
func (cs *binkyNetCommandStation) GetSupportedAddressTypes(entity model.AddressEntity) []model.AddressType {
	if l, ok := entity.(model.Loc); ok && l != nil {
		return []model.AddressType{model.AddressTypeDcc}
	}
	return []model.AddressType{model.AddressTypeBinkyNet}
}

// Network host address (defaults to 0.0.0.0)
func (cs *binkyNetCommandStation) GetServerHost() string {
	return refs.StringValue(cs.ServerHost, "0.0.0.0")
}
func (cs *binkyNetCommandStation) SetServerHost(value string) error {
	if cs.GetServerHost() != value {
		cs.ServerHost = refs.NewString(value)
		cs.OnModified()
	}
	return nil
}

// Network Port of the command station
func (cs *binkyNetCommandStation) GetGRPCPort() int {
	return refs.IntValue(cs.GRPCPort, defaultGRPCPort)
}
func (cs *binkyNetCommandStation) SetGRPCPort(value int) error {
	if cs.GetGRPCPort() != value {
		cs.GRPCPort = refs.NewInt(value)
		cs.OnModified()
	}
	return nil
}

// The required version of local workers
func (cs *binkyNetCommandStation) GetRequiredWorkerVersion() string {
	return refs.StringValue(cs.RequiredWorkerVersion, defaultRequiredWorkerVersion)
}
func (cs *binkyNetCommandStation) SetRequiredWorkerVersion(value string) error {
	if cs.GetRequiredWorkerVersion() != value {
		cs.RequiredWorkerVersion = refs.NewString(value)
		cs.OnModified()
	}
	return nil
}

// If set, do not configure objects that are not used
func (cs *binkyNetCommandStation) GetExcludeUnUsedObjects() bool {
	return refs.BoolValue(cs.ExcludeUnUsedObjects, defaultExcludeUnUsedObjects)
}
func (cs *binkyNetCommandStation) SetExcludeUnUsedObjects(value bool) error {
	if cs.GetExcludeUnUsedObjects() != value {
		cs.ExcludeUnUsedObjects = refs.NewBool(value)
		cs.OnModified()
	}
	return nil
}

// Gets the configuration of local workers on the Binky network
// that this command station is attached to.
func (cs *binkyNetCommandStation) GetLocalWorkers() model.BinkyNetLocalWorkerSet {
	return &cs.LocalWorkers
}

func (cs *binkyNetCommandStation) Upgrade() {
	// Nothing needed
}

// Called after the model has been saved
func (cs *binkyNetCommandStation) AfterSave(path string) error {
	folder := filepath.Dir(path)
	name := filepath.Base(path)
	name = strings.TrimSuffix(name, filepath.Ext(name))
	baseFolder := filepath.Join(folder, "devices", name)
	return esphome.BuildEsphomeConfigs(baseFolder, cs.GetLocalWorkers())
}
