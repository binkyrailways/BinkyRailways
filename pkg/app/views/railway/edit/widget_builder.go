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

package edit

import (
	"context"
	"fmt"
	"strconv"

	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

func buildTreeViewItems(entity interface{},
	parentKey string,
	itemCache *widgets.EntityTreeViewItemCache,
	groupCache *widgets.EntityTreeGroupCache,
	level int) []widgets.TreeViewItem {
	var result []widgets.TreeViewItem
	switch entity := entity.(type) {
	case model.Railway:
		return []widgets.TreeViewItem{
			itemCache.CreateItem(entity, parentKey, level),
		}
	case model.LocRefSet:
		entity.ForEach(func(c model.LocRef) {
			if x := c.TryResolve(); x != nil {
				result = append(result, itemCache.CreateItem(x, parentKey, level))
			}
		})
		return result
	case model.ModuleRefSet:
		entity.ForEach(func(c model.ModuleRef) {
			if x := c.TryResolve(); x != nil {
				result = append(result,
					itemCache.CreateItemWithChildren(c, parentKey, level, func(parentKey string, level int) []widgets.TreeViewItem {
						return buildTreeViewItems(x, parentKey, itemCache, groupCache, level)
					})...,
				)
			}
		})
		return result
	case model.Module:
		result = append(result,
			groupCache.CreateItem("Blocks", parentKey, func(ctx context.Context, parentKey string, level int) []widgets.TreeViewItem {
				return buildTreeViewItems(entity.GetBlocks(), parentKey, itemCache, groupCache, level)
			}, level+1, moduleBlockSet{entity}),
			groupCache.CreateItem("Block groups", parentKey, func(ctx context.Context, parentKey string, level int) []widgets.TreeViewItem {
				return buildTreeViewItems(entity.GetBlockGroups(), parentKey, itemCache, groupCache, level)
			}, level+1, moduleBlockGroupSet{entity}),
			groupCache.CreateItem("Edges", parentKey, func(ctx context.Context, parentKey string, level int) []widgets.TreeViewItem {
				return buildTreeViewItems(entity.GetEdges(), parentKey, itemCache, groupCache, level)
			}, level+1, moduleEdgeSet{entity}),
			groupCache.CreateItem("Junctions", parentKey, func(ctx context.Context, parentKey string, level int) []widgets.TreeViewItem {
				return buildTreeViewItems(entity.GetJunctions(), parentKey, itemCache, groupCache, level)
			}, level+1, moduleJunctionSet{entity}),
			groupCache.CreateItem("Outputs", parentKey, func(ctx context.Context, parentKey string, level int) []widgets.TreeViewItem {
				return buildTreeViewItems(entity.GetOutputs(), parentKey, itemCache, groupCache, level)
			}, level+1, moduleOutputSet{entity}),
			groupCache.CreateItem("Routes", parentKey, func(ctx context.Context, parentKey string, level int) []widgets.TreeViewItem {
				return buildTreeViewItems(entity.GetRoutes(), parentKey, itemCache, groupCache, level)
			}, level+1, moduleRouteSet{entity}),
			groupCache.CreateItem("Sensors", parentKey, func(ctx context.Context, parentKey string, level int) []widgets.TreeViewItem {
				return buildTreeViewItems(entity.GetSensors(), parentKey, itemCache, groupCache, level)
			}, level+1, moduleSensorSet{entity}),
			groupCache.CreateItem("Signals", parentKey, func(ctx context.Context, parentKey string, level int) []widgets.TreeViewItem {
				return buildTreeViewItems(entity.GetSignals(), parentKey, itemCache, groupCache, level)
			}, level+1, moduleSignalSet{entity}),
		)
		return result
	case model.BlockSet:
		entity.ForEach(func(entity model.Block) {
			result = append(result, itemCache.CreateItem(entity, parentKey, level))
		})
		return result
	case model.BlockGroupSet:
		entity.ForEach(func(entity model.BlockGroup) {
			result = append(result, itemCache.CreateItem(entity, parentKey, level))
		})
		return result
	case model.EdgeSet:
		entity.ForEach(func(entity model.Edge) {
			result = append(result, itemCache.CreateItem(entity, parentKey, level))
		})
		return result
	case model.JunctionSet:
		entity.ForEach(func(entity model.Junction) {
			result = append(result, itemCache.CreateItem(entity, parentKey, level))
		})
		return result
	case model.OutputSet:
		entity.ForEach(func(entity model.Output) {
			result = append(result, itemCache.CreateItem(entity, parentKey, level))
		})
		return result
	case model.RouteSet:
		entity.ForEach(func(entity model.Route) {
			result = append(result, itemCache.CreateItem(entity, parentKey, level))
		})
		return result
	case model.SensorSet:
		entity.ForEach(func(entity model.Sensor) {
			result = append(result, itemCache.CreateItem(entity, parentKey, level))
		})
		return result
	case model.SignalSet:
		entity.ForEach(func(entity model.Signal) {
			result = append(result, itemCache.CreateItem(entity, parentKey, level))
		})
		return result
	case model.CommandStationRefSet:
		entity.ForEach(func(c model.CommandStationRef) {
			if x := c.TryResolve(); x != nil {
				result = append(result,
					itemCache.CreateItemWithChildren(x, parentKey, level, func(parentKey string, level int) []widgets.TreeViewItem {
						return buildTreeViewItems(x, parentKey, itemCache, groupCache, level)
					})...)
			}
		})
		return result
	case model.BinkyNetCommandStation:
		result = append(result,
			groupCache.CreateItem("Local workers", parentKey, func(ctx context.Context, parentKey string, level int) []widgets.TreeViewItem {
				return buildTreeViewItems(entity.GetLocalWorkers(), parentKey, itemCache, groupCache, level)
			}, level+1, binkyNetCommandStationLocalWorkerSet{entity}),
		)
		return result
	case model.BinkyNetLocalWorkerSet:
		entity.ForEach(func(lw model.BinkyNetLocalWorker) {
			result = append(result,
				itemCache.CreateItemWithChildren(lw, parentKey, level, func(parentKey string, level int) []widgets.TreeViewItem {
					return buildTreeViewItems(lw, parentKey, itemCache, groupCache, level)
				})...,
			)
		})
		return result
	case model.BinkyNetLocalWorker:
		result = append(result,
			groupCache.CreateItem("Devices", parentKey, func(ctx context.Context, parentKey string, level int) []widgets.TreeViewItem {
				return buildTreeViewItems(entity.GetDevices(), parentKey, itemCache, groupCache, level)
			}, level, binkyNetLocalWorkerDeviceSet{entity}),
			groupCache.CreateItem("Objects", parentKey, func(ctx context.Context, parentKey string, level int) []widgets.TreeViewItem {
				return buildTreeViewItems(entity.GetObjects(), parentKey, itemCache, groupCache, level)
			}, level, binkyNetLocalWorkerObjectSet{entity}),
		)
		return result
	case model.BinkyNetDeviceSet:
		entity.ForEach(func(entity model.BinkyNetDevice) {
			result = append(result, itemCache.CreateItem(entity, parentKey, level))
		})
		return result
	case model.BinkyNetObjectSet:
		entity.ForEach(func(entity model.BinkyNetObject) {
			result = append(result,
				itemCache.CreateItemWithChildren(entity, parentKey, level, func(parentKey string, level int) []widgets.TreeViewItem {
					return buildTreeViewItems(entity, parentKey, itemCache, groupCache, level)
				})...,
			)
		})
		return result
	case model.BinkyNetObject:
		result = append(result,
			groupCache.CreateItem("Connections", parentKey, func(ctx context.Context, parentKey string, level int) []widgets.TreeViewItem {
				return buildTreeViewItems(entity.GetConnections(), parentKey, itemCache, groupCache, level)
			}, level, binkyNetObjectConnectionSet{entity}),
		)
		return result
	case model.BinkyNetConnectionSet:
		entity.ForEach(func(entity model.BinkyNetConnection) {
			result = append(result,
				itemCache.CreateItem(binkyNetConnectionEntity{entity}, parentKey, level),
				groupCache.CreateItem("Pins", parentKey, func(ctx context.Context, parentKey string, level int) []widgets.TreeViewItem {
					return buildTreeViewItems(entity.GetPins(), parentKey, itemCache, groupCache, level)
				}, level+1, binkyNetObjectConnectionPinSet{entity}),
			)
		})
		return result
	case model.BinkyNetConnectionPinList:
		i := 0
		entity.ForEach(func(entity model.BinkyNetDevicePin) {
			result = append(result,
				itemCache.CreateItem(binkyNetDevicePinEntity{i, entity}, parentKey, level),
			)
			i++
		})
		return result
	}
	return nil
}

// Identifyable & Selectable implementation for BlockSet
type moduleBlockSet struct {
	model.Module
}

func (e moduleBlockSet) GetID() string       { return "blocks" }
func (e moduleBlockSet) Select() interface{} { return e.Module.GetBlocks() }

// Identifyable & Selectable implementation for BlockGroupSet
type moduleBlockGroupSet struct {
	model.Module
}

func (e moduleBlockGroupSet) GetID() string       { return "blockGroups" }
func (e moduleBlockGroupSet) Select() interface{} { return e.Module.GetBlockGroups() }

// Identifyable & Selectable implementation for EdgeSet
type moduleEdgeSet struct {
	model.Module
}

func (e moduleEdgeSet) GetID() string       { return "edges" }
func (e moduleEdgeSet) Select() interface{} { return e.Module.GetEdges() }

// Identifyable & Selectable implementation for JunctionSet
type moduleJunctionSet struct {
	model.Module
}

func (e moduleJunctionSet) GetID() string       { return "junctions" }
func (e moduleJunctionSet) Select() interface{} { return e.Module.GetJunctions() }

// Identifyable & Selectable implementation for OutputSet
type moduleOutputSet struct {
	model.Module
}

func (e moduleOutputSet) GetID() string       { return "outputs" }
func (e moduleOutputSet) Select() interface{} { return e.Module.GetOutputs() }

// Identifyable & Selectable implementation for RouteSet
type moduleRouteSet struct {
	model.Module
}

func (e moduleRouteSet) GetID() string       { return "routes" }
func (e moduleRouteSet) Select() interface{} { return e.Module.GetRoutes() }

// Identifyable & Selectable implementation for SensorSet
type moduleSensorSet struct {
	model.Module
}

func (e moduleSensorSet) GetID() string       { return "sensors" }
func (e moduleSensorSet) Select() interface{} { return e.Module.GetSensors() }

// Identifyable & Selectable implementation for SignalSet
type moduleSignalSet struct {
	model.Module
}

func (e moduleSignalSet) GetID() string       { return "signals" }
func (e moduleSignalSet) Select() interface{} { return e.Module.GetSignals() }

// Identifyable & Selectable implementation for BinkyNetLocalWorkerSet
type binkyNetCommandStationLocalWorkerSet struct {
	model.BinkyNetCommandStation
}

func (e binkyNetCommandStationLocalWorkerSet) GetID() string {
	return "localworkers"
}
func (e binkyNetCommandStationLocalWorkerSet) Select() interface{} {
	return e.BinkyNetCommandStation.GetLocalWorkers()
}

// Identifyable & Selectable implementation for BinkyNetDeviceSet
type binkyNetLocalWorkerDeviceSet struct {
	model.BinkyNetLocalWorker
}

func (e binkyNetLocalWorkerDeviceSet) GetID() string {
	return "devices"
}
func (e binkyNetLocalWorkerDeviceSet) Select() interface{} { return e.BinkyNetLocalWorker.GetDevices() }

// Identifyable & Selectable implementation for BinkyNetObjectSet
type binkyNetLocalWorkerObjectSet struct {
	model.BinkyNetLocalWorker
}

func (e binkyNetLocalWorkerObjectSet) GetID() string {
	return "objects"
}
func (e binkyNetLocalWorkerObjectSet) Select() interface{} { return e.BinkyNetLocalWorker.GetObjects() }

// Identifyable & Selectable implementation for BinkyNetConnectionSet
type binkyNetObjectConnectionSet struct {
	model.BinkyNetObject
}

func (e binkyNetObjectConnectionSet) GetID() string       { return "connections" }
func (e binkyNetObjectConnectionSet) Select() interface{} { return e.BinkyNetObject.GetConnections() }

// Identifyable & Selectable implementation for BinkyNetConnectionSet
type binkyNetObjectConnectionPinSet struct {
	model.BinkyNetConnection
}

func (e binkyNetObjectConnectionPinSet) GetID() string {
	return "pins"
}
func (e binkyNetObjectConnectionPinSet) Select() interface{} { return e.BinkyNetConnection.GetPins() }

// Entity implementation for BinkyNetConnection
type binkyNetConnectionEntity struct {
	model.BinkyNetConnection
}

func (e binkyNetConnectionEntity) GetID() string {
	return string(e.GetKey())
}
func (e binkyNetConnectionEntity) GetDescription() string { return string(e.GetKey()) }
func (e binkyNetConnectionEntity) Select() interface{}    { return e.BinkyNetConnection }

// Entity implementation for BinkyNetDevicePin
type binkyNetDevicePinEntity struct {
	index int
	model.BinkyNetDevicePin
}

func (e binkyNetDevicePinEntity) GetID() string {
	return strconv.Itoa(e.index)
}
func (e binkyNetDevicePinEntity) GetDescription() string {
	return fmt.Sprintf("%s[%d]", e.BinkyNetDevicePin.GetDeviceID(), e.BinkyNetDevicePin.GetIndex())
}
func (e binkyNetDevicePinEntity) Select() interface{} { return e.BinkyNetDevicePin }
