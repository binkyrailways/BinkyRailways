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

package run

import (
	"fmt"
	"strings"

	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

func buildHardwareModulesTreeViewItems(entity interface{},
	parentKey string,
	itemCache *widgets.EntityTreeViewItemCache,
	groupCache *widgets.EntityTreeGroupCache,
	level int) []widgets.TreeViewItem {
	var result []widgets.TreeViewItem
	switch entity := entity.(type) {
	case state.Railway:
		entity.ForEachCommandStation(func(x state.CommandStation) {
			result = append(result,
				itemCache.CreateItemWithChildren(x, parentKey, level, func(parentKey string, level int) []widgets.TreeViewItem {
					return buildHardwareModulesTreeViewItems(x, parentKey, itemCache, groupCache, level)
				})...,
			)
		})
		return result
	case state.CommandStation:
		entity.ForEachHardwareModule(func(x state.HardwareModule) {
			result = append(result, itemCache.CreateItem(hardwareModuleEntity{x}, parentKey, level))
		})
		return result
	default:
		return nil
	}
}

type hardwareModuleEntity struct{ state.HardwareModule }

func (e hardwareModuleEntity) GetDescription() string {
	fields := []string{e.GetID()}
	if e.HasUptime() {
		fields = append(fields, fmt.Sprintf("uptime=%s", e.GetUptime().String()))
	}
	if e.HasVersion() {
		fields = append(fields, fmt.Sprintf("version=%s", e.GetVersion()))
	}
	return strings.Join(fields, " ")
}
