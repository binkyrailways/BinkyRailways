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
	"context"
	"encoding/xml"
	"fmt"
	"strings"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type binkyNetLocalWorker struct {
	container *binkyNetLocalWorkerSet
	binkyNetLocalWorkerFields
}

type binkyNetLocalWorkerFields struct {
	entity
	HardwareID      string                        `xml:"HardwareID,omitempty"`
	Alias           string                        `xml:"Alias,omitempty"`
	LocalWorkerType model.BinkyNetLocalWorkerType `xml:"LocalWorkerType,omitempty"`
	Devices         binkyNetDeviceSet             `xml:"Devices"`
	Objects         binkyNetObjectSet             `xml:"Objects"`
	Routers         binkyNetRouterSet             `xml:"Routers"`
}

var _ model.BinkyNetLocalWorker = &binkyNetLocalWorker{}

// newBinkyNetLocalWorker creates and initializes a new binky local worker.
func newBinkyNetLocalWorker(hardwareID string) *binkyNetLocalWorker {
	lw := &binkyNetLocalWorker{}
	lw.EnsureID()
	lw.SetHardwareID(context.Background(), hardwareID)
	lw.LocalWorkerType = model.BinkynetLocalWorkerTypeLinux
	lw.Devices.SetContainer(lw)
	lw.Objects.SetContainer(lw)
	lw.Routers.SetContainer(lw)
	lw.ensureRouters()
	return lw
}

// UnmarshalXML unmarshals and connects the module.
func (lw *binkyNetLocalWorker) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&lw.binkyNetLocalWorkerFields, &start); err != nil {
		return err
	}
	lw.Devices.SetContainer(lw)
	lw.Objects.SetContainer(lw)
	lw.Routers.SetContainer(lw)
	if lw.LocalWorkerType == "" {
		lw.LocalWorkerType = model.BinkynetLocalWorkerTypeLinux
	}
	lw.ensureRouters()
	return nil
}

// Gets the command station this object belongs to
func (lw *binkyNetLocalWorker) GetCommandStation() model.BinkyNetCommandStation {
	if lw.container != nil {
		return lw.container.GetCommandStation()
	}
	return nil
}

// SetContainer links this instance to its container
func (lw *binkyNetLocalWorker) SetContainer(container *binkyNetLocalWorkerSet) {
	lw.container = container
}

// Accept a visit by the given visitor
func (lw *binkyNetLocalWorker) Accept(v model.EntityVisitor) interface{} {
	return v.VisitBinkyNetLocalWorker(lw)
}

// Gets the description of the entity
func (lw *binkyNetLocalWorker) GetDescription() string {
	if lw.Alias != "" {
		return fmt.Sprintf("%s (%s)", lw.Alias, lw.GetHardwareID())
	}
	return lw.GetHardwareID()
}

// Hardware ID of the local worker.
func (lw *binkyNetLocalWorker) GetHardwareID() string {
	return lw.HardwareID
}
func (lw *binkyNetLocalWorker) SetHardwareID(ctx context.Context, value string) error {
	if lw.HardwareID != value {
		lw.HardwareID = value
		lw.OnModified()
	}
	return nil
}

// What type is the local worker?
func (lw *binkyNetLocalWorker) GetLocalWorkerType() model.BinkyNetLocalWorkerType {
	if lw.LocalWorkerType == "" {
		return model.BinkynetLocalWorkerTypeLinux
	}
	return lw.LocalWorkerType
}

func (lw *binkyNetLocalWorker) SetLocalWorkerType(ctx context.Context, value model.BinkyNetLocalWorkerType) error {
	if lw.LocalWorkerType != value {
		lw.LocalWorkerType = value
		lw.OnModified()
	}
	lw.ensureRouters()
	return nil
}

// Optional alias for the local worker.
func (lw *binkyNetLocalWorker) GetAlias() string {
	return lw.Alias
}
func (lw *binkyNetLocalWorker) SetAlias(ctx context.Context, value string) error {
	if lw.Alias != value {
		oldValue := lw.Alias
		lw.Alias = value
		lw.OnModified()

		// Update addresses using this alias
		if cs := lw.GetCommandStation(); cs != nil {
			if rw := cs.GetPackage().GetRailway(); rw != nil {
				if rw, ok := rw.(Entity); ok {
					oldPrefix := oldValue + "/"
					newPrefix := value + "/"
					rw.ForEachAddress(func(addr model.Address, onUpdate func(context.Context, model.Address) error) {
						if addr.Network.Type == model.AddressTypeBinkyNet {
							if strings.HasPrefix(addr.Value, oldPrefix) {
								addr.Value = newPrefix + addr.Value[len(oldPrefix):]
								onUpdate(ctx, addr)
							}
						}
					})
				}
			}
		}
	}
	return nil
}

// Set of devices that must be configured on this local worker.
func (lw *binkyNetLocalWorker) GetDevices() model.BinkyNetDeviceSet {
	return &lw.Devices
}

// Set of real world objects controlled by the local worker
func (lw *binkyNetLocalWorker) GetObjects() model.BinkyNetObjectSet {
	return &lw.Objects
}

// Set of hardware devices that route commands & state to/from devices belonging to this local worker.
func (lw *binkyNetLocalWorker) GetRouters() model.BinkyNetRouterSet {
	return &lw.Routers
}

// Ensure that there is at least 1 router when type == ESPHOME
func (lw *binkyNetLocalWorker) ensureRouters() {
	if lw.GetLocalWorkerType() == model.BinkynetLocalWorkerTypeEsphome {
		if lw.GetRouters().GetCount() == 0 {
			lw.GetRouters().AddNew()
		}
	}
}

// OnModified triggers the modified function of the parent (if any)
func (lw *binkyNetLocalWorker) OnModified() {
	if lw.container != nil {
		lw.container.OnModified()
	}
	lw.entity.OnModified()
}
