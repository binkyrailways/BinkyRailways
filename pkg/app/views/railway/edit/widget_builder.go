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

	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

func buildTreeViewItems(entity interface{},
	itemCache *widgets.EntityTreeViewItemCache,
	groupCache *widgets.EntityTreeGroupCache,
	level int) []widgets.TreeViewItem {
	var result []widgets.TreeViewItem
	switch entity := entity.(type) {
	case model.Railway:
		return []widgets.TreeViewItem{
			itemCache.CreateItem(entity, level),
		}
	case model.LocRefSet:
		entity.ForEach(func(c model.LocRef) {
			if x := c.TryResolve(); x != nil {
				result = append(result, itemCache.CreateItem(x, level))
			}
		})
		return result
	case model.ModuleRefSet:
		entity.ForEach(func(c model.ModuleRef) {
			if x := c.TryResolve(); x != nil {
				result = append(result,
					itemCache.CreateItem(x, level),
					groupCache.CreateItem("Blocks", func(ctx context.Context, level int) []widgets.TreeViewItem {
						return buildTreeViewItems(x.GetBlocks(), itemCache, groupCache, level)
					}, level+1, x),
					groupCache.CreateItem("Block groups", func(ctx context.Context, level int) []widgets.TreeViewItem {
						return buildTreeViewItems(x.GetBlockGroups(), itemCache, groupCache, level)
					}, level+1, x),
					groupCache.CreateItem("Edges", func(ctx context.Context, level int) []widgets.TreeViewItem {
						return buildTreeViewItems(x.GetEdges(), itemCache, groupCache, level)
					}, level+1, x),
					groupCache.CreateItem("Junctions", func(ctx context.Context, level int) []widgets.TreeViewItem {
						return buildTreeViewItems(x.GetJunctions(), itemCache, groupCache, level)
					}, level+1, x),
					groupCache.CreateItem("Outputs", func(ctx context.Context, level int) []widgets.TreeViewItem {
						return buildTreeViewItems(x.GetOutputs(), itemCache, groupCache, level)
					}, level+1, x),
					groupCache.CreateItem("Routes", func(ctx context.Context, level int) []widgets.TreeViewItem {
						return buildTreeViewItems(x.GetRoutes(), itemCache, groupCache, level)
					}, level+1, x),
					groupCache.CreateItem("Sensors", func(ctx context.Context, level int) []widgets.TreeViewItem {
						return buildTreeViewItems(x.GetSensors(), itemCache, groupCache, level)
					}, level+1, x),
					groupCache.CreateItem("Signals", func(ctx context.Context, level int) []widgets.TreeViewItem {
						return buildTreeViewItems(x.GetSignals(), itemCache, groupCache, level)
					}, level+1, x),
				)
			}
		})
		return result
	case model.BlockSet:
		entity.ForEach(func(entity model.Block) {
			result = append(result, itemCache.CreateItem(entity, level))
		})
		return result
	case model.BlockGroupSet:
		entity.ForEach(func(entity model.BlockGroup) {
			result = append(result, itemCache.CreateItem(entity, level))
		})
		return result
	case model.EdgeSet:
		entity.ForEach(func(entity model.Edge) {
			result = append(result, itemCache.CreateItem(entity, level))
		})
		return result
	case model.JunctionSet:
		entity.ForEach(func(entity model.Junction) {
			result = append(result, itemCache.CreateItem(entity, level))
		})
		return result
	case model.OutputSet:
		entity.ForEach(func(entity model.Output) {
			result = append(result, itemCache.CreateItem(entity, level))
		})
		return result
	case model.RouteSet:
		entity.ForEach(func(entity model.Route) {
			result = append(result, itemCache.CreateItem(entity, level))
		})
		return result
	case model.SensorSet:
		entity.ForEach(func(entity model.Sensor) {
			result = append(result, itemCache.CreateItem(entity, level))
		})
		return result
	case model.SignalSet:
		entity.ForEach(func(entity model.Signal) {
			result = append(result, itemCache.CreateItem(entity, level))
		})
		return result
	case model.CommandStationRefSet:
		entity.ForEach(func(c model.CommandStationRef) {
			if x := c.TryResolve(); x != nil {
				result = append(result,
					itemCache.CreateItem(x, level))
				if cs, ok := x.(model.BinkyNetCommandStation); ok {
					result = append(result,
						groupCache.CreateItem("Local workers", func(ctx context.Context, level int) []widgets.TreeViewItem {
							return buildTreeViewItems(cs.GetLocalWorkers(), itemCache, groupCache, level)
						}, level+1, x),
					)
				}
			}
		})
		return result
	case model.BinkyNetLocalWorkerSet:
		entity.ForEach(func(lw model.BinkyNetLocalWorker) {
			result = append(result,
				itemCache.CreateItem(lw, level),
				groupCache.CreateItem("Devices", func(ctx context.Context, level int) []widgets.TreeViewItem {
					return buildTreeViewItems(lw.GetDevices(), itemCache, groupCache, level)
				}, level+1, lw),
				groupCache.CreateItem("Objects", func(ctx context.Context, level int) []widgets.TreeViewItem {
					return buildTreeViewItems(lw.GetObjects(), itemCache, groupCache, level)
				}, level+1, lw),
			)
		})
		return result
	case model.BinkyNetDeviceSet:
		entity.ForEach(func(entity model.BinkyNetDevice) {
			result = append(result, itemCache.CreateItem(entity, level))
		})
		return result
	case model.BinkyNetObjectSet:
		entity.ForEach(func(entity model.BinkyNetObject) {
			result = append(result, itemCache.CreateItem(entity, level))
		})
		return result
	}
	return nil
}
