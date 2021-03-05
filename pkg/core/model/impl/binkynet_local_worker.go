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
	"fmt"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type binkyNetLocalWorker struct {
	onModified func()
	entity

	Alias   string            `xml:"Alias,omitempty"`
	Devices binkyNetDeviceSet `xml:"Devices"`
	Objects binkyNetObjectSet `xml:"Objects"`
}

var _ model.BinkyNetLocalWorker = &binkyNetLocalWorker{}

// Accept a visit by the given visitor
func (lw *binkyNetLocalWorker) Accept(v model.EntityVisitor) interface{} {
	return v.VisitBinkyNetLocalWorker(lw)
}

// Gets the description of the entity
func (lw *binkyNetLocalWorker) GetDescription() string {
	if lw.Alias != "" {
		return fmt.Sprintf("%s (%s)", lw.Alias, lw.GetID())
	}
	return lw.GetID()
}

// Optional alias for the local worker.
func (lw *binkyNetLocalWorker) GetAlias() string {
	return lw.Alias
}
func (lw *binkyNetLocalWorker) SetAlias(value string) error {
	if lw.Alias != value {
		lw.Alias = value
		lw.OnModified()
	}
	return nil
}

// Set of devices that must be configured on this local worker.
func (lw *binkyNetLocalWorker) GetDevices() model.BinkyNetDeviceSet {
	lw.Devices.onModified = lw.OnModified
	return &lw.Devices
}

// Set of real world objects controlled by the local worker
func (lw *binkyNetLocalWorker) GetObjects() model.BinkyNetObjectSet {
	lw.Objects.onModified = lw.OnModified
	return &lw.Objects
}

// OnModified triggers the modified function of the parent (if any)
func (lw *binkyNetLocalWorker) OnModified() {
	if lw.onModified != nil {
		lw.onModified()
	}
	lw.entity.OnModified()
}
