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
	"context"
	"fmt"
	"strings"

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
func (c *EntityTreeGroupCache) CreateItem(name string, collection func(ctx context.Context, level int) []TreeViewItem,
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
	Collection func(ctx context.Context, level int) []TreeViewItem
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

// SortKey returns a key used to sort the items
func (v *TreeViewGroup) SortKey() string {
	return "1." + strings.ToLower(v.Name)
}

// Select returns the selection that this item represents (can be nil)
func (v *TreeViewGroup) Select() interface{} {
	return nil
}

// Contains returns true if the given selection is contained in this item
func (v *TreeViewGroup) Contains(selection interface{}) bool {
	return false
}

// ProcessEvents processes events of this item, return an entity if it is clicked.
func (v *TreeViewGroup) ProcessEvents() interface{} {
	if v.clickable.Clicked() {
		v.SetCollapsed(!v.Collapsed())
	}
	return nil
}

// Layout the widget
func (v *TreeViewGroup) Layout(ctx context.Context, gtx C, th *material.Theme, selection interface{}, focused bool) D {
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
func (v *TreeViewGroup) GenerateWidgets(ctx context.Context, level int) TreeViewItems {
	if v.collapsed {
		return nil
	}
	var result TreeViewItems
	collected := TreeViewItems(v.Collection(ctx, level+1))
	collected.Sort()
	for _, item := range collected {
		result = append(result, item)
		if subItems := item.GenerateWidgets(ctx, level+1); len(subItems) > 0 {
			// Insert items
			result = append(result, subItems...)
		}
	}
	return result
}
