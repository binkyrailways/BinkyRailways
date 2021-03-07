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

package editors

import (
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/gen2brain/dlgs"
)

// createAddButtonsFor creates "Add" buttons for the given entity
func createAddButtonsFor(etx EditorContext, entity interface{}) []AddButton {
	if entity == nil {
		return nil
	}
	switch entity := entity.(type) {
	case model.BinkyNetCommandStation:
		prefix := []AddButton{
			{
				Title: "Add Local Worker",
				OnClick: func() {
					if id, ok, err := dlgs.Entry("Add Local Worker", "Hardware ID", ""); err == nil && ok {
						if lw, err := entity.GetLocalWorkers().AddNew(id); err == nil {
							etx.Select(lw)
						} else {
							dlgs.Error("Failed to add Local Worker", err.Error())
						}
					}
				},
			},
			{
				Separator: true,
			},
		}
		return append(prefix, createPersistentEntityAddButtons(entity, etx)...)
	case model.BinkyNetLocalWorker:
		prefix := []AddButton{
			{
				Title: "Add Device",
				OnClick: func() {
					dev := entity.GetDevices().AddNew()
					etx.Select(dev)
				},
			}, {
				Title: "Add Object",
				OnClick: func() {
					obj := entity.GetObjects().AddNew()
					etx.Select(obj)
				},
			},
			{
				Separator: true,
			},
		}
		return append(prefix, createAddButtonsFor(etx, entity.GetCommandStation())...)
	case model.BinkyNetDevice:
		return createAddButtonsFor(etx, entity.GetLocalWorker())
	case model.BinkyNetObject:
		return createAddButtonsFor(etx, entity.GetLocalWorker())
	case model.BinkyNetConnection:
		prefix := []AddButton{
			{
				Title: "Add Device Pin",
				OnClick: func() {
					dev := entity.GetPins().AddNew()
					etx.Select(dev)
				},
			}, {
				Separator: true,
			},
		}
		return append(prefix, createAddButtonsFor(etx, entity.GetObject())...)
	case model.Module:
		prefix := []AddButton{
			{
				Title: "Add block",
				OnClick: func() {
					x := entity.GetBlocks().AddNew()
					etx.Select(x)
				},
			},
			{
				Title: "Add block group",
				OnClick: func() {
					x := entity.GetBlockGroups().AddNew()
					etx.Select(x)
				},
			},
			{
				Title: "Add edge",
				OnClick: func() {
					x := entity.GetEdges().AddNew()
					etx.Select(x)
				},
			},
			{
				Title: "Add binary output",
				OnClick: func() {
					x := entity.GetOutputs().AddNewBinaryOutput()
					etx.Select(x)
				},
			},
			{
				Title: "Add click 4 stage output",
				OnClick: func() {
					x := entity.GetOutputs().AddNewClock4StageOutput()
					etx.Select(x)
				},
			},
			{
				Title: "Add route",
				OnClick: func() {
					x := entity.GetRoutes().AddNew()
					etx.Select(x)
				},
			},
			{
				Title: "Add passive junction",
				OnClick: func() {
					x := entity.GetJunctions().AddPassiveJunction()
					etx.Select(x)
				},
			},
			{
				Title: "Add switch",
				OnClick: func() {
					x := entity.GetJunctions().AddSwitch()
					etx.Select(x)
				},
			},
			{
				Title: "Add turntable",
				OnClick: func() {
					x := entity.GetJunctions().AddTurnTable()
					etx.Select(x)
				},
			},
			{
				Title: "Add binary sensor",
				OnClick: func() {
					x := entity.GetSensors().AddNewBinarySensor()
					etx.Select(x)
				},
			},
			{
				Title: "Add block signal",
				OnClick: func() {
					x := entity.GetSignals().AddNewBlockSignal()
					etx.Select(x)
				},
			},
			{
				Separator: true,
			},
		}
		return append(prefix, createPersistentEntityAddButtons(entity, etx)...)

		// Sets in module
	case model.BlockSet:
		return createAddButtonsFor(etx, entity.GetModule())
	case model.BlockGroupSet:
		return createAddButtonsFor(etx, entity.GetModule())
	case model.EdgeSet:
		return createAddButtonsFor(etx, entity.GetModule())
	case model.JunctionSet:
		return createAddButtonsFor(etx, entity.GetModule())
	case model.OutputSet:
		return createAddButtonsFor(etx, entity.GetModule())
	case model.RouteSet:
		return createAddButtonsFor(etx, entity.GetModule())
	case model.SensorSet:
		return createAddButtonsFor(etx, entity.GetModule())
	case model.SignalSet:
		return createAddButtonsFor(etx, entity.GetModule())

	// Sets in railway
	case model.LocRefSet:
		return createPersistentEntityAddButtons(entity.GetRailway(), etx)
	case model.LocGroupSet:
		return createPersistentEntityAddButtons(entity.GetRailway(), etx)
	case model.ModuleRefSet:
		return createPersistentEntityAddButtons(entity.GetRailway(), etx)
	case model.CommandStationRefSet:
		return createPersistentEntityAddButtons(entity.GetRailway(), etx)

	// Keep this generic entity last
	case model.PersistentEntity:
		return createPersistentEntityAddButtons(entity, etx)
	default:
		return nil
	}
}

// createPersistentEntityAddButtons creates the buttons for the "Add resource sheet" that apply to persistent entities
func createPersistentEntityAddButtons(entity model.PersistentEntity, etx EditorContext) []AddButton {
	if entity == nil {
		return nil
	}
	pkg := entity.GetPackage()
	if pkg == nil {
		return nil
	}
	return []AddButton{
		{
			Title: "Add loc",
			OnClick: func() {
				if loc, err := pkg.AddNewLoc(); err == nil {
					pkg.GetRailway().GetLocs().Add(loc)
					etx.Select(loc)
				}
			},
		},
		{
			Title: "Add module",
			OnClick: func() {
				if module, err := pkg.AddNewModule(); err == nil {
					pkg.GetRailway().GetModules().Add(module)
					etx.Select(module)
				}
			},
		},
		{
			Title: "Add BinkyNet Command station",
			OnClick: func() {
				if cs, err := pkg.AddNewBinkyNetCommandStation(); err == nil {
					pkg.GetRailway().GetCommandStations().Add(cs)
					etx.Select(cs)
				}
			},
		},
		{
			Title: "Add LocoBuffer Command station",
			OnClick: func() {
				if cs, err := pkg.AddNewLocoBufferCommandStation(); err == nil {
					pkg.GetRailway().GetCommandStations().Add(cs)
					etx.Select(cs)
				}
			},
		},
	}
}