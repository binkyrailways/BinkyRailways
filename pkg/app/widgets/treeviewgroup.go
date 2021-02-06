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

package widgets

import (
	"fmt"

	w "gioui.org/widget"
	"gioui.org/widget/material"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// EntityTreeGroupCache is a cache for creating Entity TreeViewGroup's
type EntityTreeGroupCache struct {
	cache map[string]*TreeViewGroup
}

// CreateItem creates a TreeView item for the given entity, using the cache
// when possible.
func (c *EntityTreeGroupCache) CreateItem(name string, collection func(level int) []TreeViewItem,
	level int, entity model.Entity) *TreeViewGroup {
	key := fmt.Sprintf("%s/%d/%s", entity.GetID(), level, name)
	if c.cache == nil {
		c.cache = make(map[string]*TreeViewGroup)
	}
	if result, found := c.cache[key]; found {
		result.Collection = collection
		return result
	}
	w := &TreeViewGroup{
		Name:       name,
		Collection: collection,
		Level:      level,
	}
	w.SetCollapsed(true)
	c.cache[key] = w
	return w
}

// TreeViewGroup is a collapsable group of treeview items
type TreeViewGroup struct {
	Name       string
	Collection func(level int) []TreeViewItem
	Level      int

	collapsed bool
	widgets   []TreeViewItem
	clickable w.Clickable
}

// Collapsed returns the collapsed state of the group.
func (v *TreeViewGroup) Collapsed() bool {
	return v.collapsed
}

// SetCollapsed sets the collapsed state of the group.
func (v *TreeViewGroup) SetCollapsed(value bool) {
	v.collapsed = value
}

// ProcessEvents processes events of this item, return an entity if it is clicked.
func (v *TreeViewGroup) ProcessEvents() interface{} {
	if v.clickable.Clicked() {
		v.SetCollapsed(!v.Collapsed())
	}
	return nil
}

// Layout the widget
func (v *TreeViewGroup) Layout(gtx C, th *material.Theme, selection interface{}) D {
	return material.Clickable(gtx, &v.clickable, func(gtx C) D {
		postfix := ""
		if v.Collapsed() {
			postfix = "..."
		}
		return LayoutWithLevel(gtx, v.Level, func(gtx C) D {
			scale := 1.4 - (float32(v.Level) / 5.0)
			if scale < 1 {
				scale = 1
			}
			return material.Label(th, th.TextSize.Scale(scale), v.Name+postfix).Layout(gtx)
		})
	})
}

// GenerateWidgets generates all widgets for this group
func (v *TreeViewGroup) GenerateWidgets(level int) []TreeViewItem {
	if v.collapsed {
		return nil
	}
	result := v.Collection(level + 1)
	for idx := 0; idx < len(result); idx = idx + 1 {
		if subItems := result[idx].GenerateWidgets(level + 1); len(subItems) > 0 {
			// Insert items
			prefix := result[:idx+1]
			remaining := result[idx+1:]
			result = append(append(prefix, subItems...), remaining...)
		}
	}
	return result
}
