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

	"gioui.org/op/clip"
	"gioui.org/op/paint"
	w "gioui.org/widget"
	"gioui.org/widget/material"
)

// EntityTreeGroupCache is a cache for creating Entity TreeViewGroup's
type EntityTreeGroupCache struct {
	cache map[string]*TreeViewGroup
}

// CreateItem creates a TreeView item for the given entity, using the cache
// when possible.
func (c *EntityTreeGroupCache) CreateItem(name, parentKey string, collection func(ctx context.Context, parentKey string, level int) []TreeViewItem,
	level int, entity Identifyable) *TreeViewGroup {
	key := fmt.Sprintf("%s/%s/%d/%s", parentKey, entity.GetID(), level, name)
	if c.cache == nil {
		c.cache = make(map[string]*TreeViewGroup)
	}
	if result, found := c.cache[key]; found {
		result.Collection = collection
		return result
	}
	w := &TreeViewGroup{
		key:        key,
		Name:       name,
		Collection: collection,
		Level:      level,
		Entity:     entity,
	}
	w.SetCollapsed(true)
	c.cache[key] = w
	return w
}

// TreeViewGroup is a collapsable group of treeview items
type TreeViewGroup struct {
	key        string
	Name       string
	Collection func(ctx context.Context, parentKey string, level int) []TreeViewItem
	Level      int
	Entity     Identifyable

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

// Expand sets the collapsed state of the group to false
func (v *TreeViewGroup) Expand() {
	v.SetCollapsed(false)
}

// Collapse sets the collapsed state of the group to true
func (v *TreeViewGroup) Collapse() {
	v.SetCollapsed(true)
}

// SortKey returns a key used to sort the items
func (v *TreeViewGroup) SortKey() string {
	return "1." + strings.ToLower(v.Name)
}

// Select returns the selection that this item represents (can be nil)
func (v *TreeViewGroup) Select() interface{} {
	if sEntity, ok := v.Entity.(Selectable); ok {
		return sEntity.Select()
	}
	return v.Entity
}

// Contains returns true if the given selection is contained in this item
func (v *TreeViewGroup) Contains(selection interface{}) bool {
	if v.Entity == selection {
		return true
	}
	if sEntity, ok := v.Entity.(Selectable); ok {
		if sEntity.Select() == selection {
			return true
		}
	}
	return false
}

// ProcessEvents processes events of this item, return an entity if it is clicked.
func (v *TreeViewGroup) ProcessEvents() interface{} {
	if v.clickable.Clicked() {
		v.SetCollapsed(!v.Collapsed())
		if sEntity, ok := v.Entity.(Selectable); ok {
			return sEntity.Select()
		}
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
		scale := 1.4 - (float32(v.Level) / 5.0)
		if scale < 1 {
			scale = 1
		}
		lb := material.Label(th, th.TextSize.Scale(scale), v.Name+postfix)
		return LayoutWithLevel(gtx, v.Level, func(gtx C) D {
			if v.Contains(selection) {
				fg := th.ContrastFg
				bg := th.ContrastBg
				if !focused {
					bg.A = bg.A / 2
				}
				lb.Color = fg
				clip.Rect{
					Max: gtx.Constraints.Max,
				}.Push(gtx.Ops).Pop()
				paint.Fill(gtx.Ops, bg)
			}
			return lb.Layout(gtx)
		})
	})
}

// GenerateWidgets generates all widgets for this group
func (v *TreeViewGroup) GenerateWidgets(ctx context.Context) TreeViewItems {
	if v.collapsed {
		return nil
	}
	var result TreeViewItems
	collected := TreeViewItems(v.Collection(ctx, v.key, v.Level+1))
	//collected.Sort()
	for _, item := range collected {
		result = append(result, item)
		if subItems := item.GenerateWidgets(ctx); len(subItems) > 0 {
			// Insert items
			result = append(result, subItems...)
		}
	}
	return result
}
