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

// Entity provides enough of the core Entity interface to display it.
type Entity interface {
	GetID() string
	GetDescription() string
}

// IndirectEntity is used to allow for customization of the selection.
type IndirectEntity interface {
	Entity
	Select() interface{}
}

// EntityTreeViewItemCache is a cache for creating Entity TreeViewItem's
type EntityTreeViewItemCache struct {
	cache map[string]TreeViewItem
}

// CreateItem creates a TreeView item for the given entity, using the cache
// when possible.
func (c *EntityTreeViewItemCache) CreateItem(entity Entity, level int) TreeViewItem {
	key := fmt.Sprintf("%s/%d", entity.GetID(), level)
	if c.cache == nil {
		c.cache = make(map[string]TreeViewItem)
	}
	if result, found := c.cache[key]; found {
		return result
	}
	w := &entityItem{
		level:  level,
		entity: entity,
	}
	c.cache[key] = w
	return w
}

type entityItem struct {
	entity Entity
	level  int
	w.Clickable
}

// Return a key used to sort the items
func (item *entityItem) SortKey() string {
	return "0." + strings.ToLower(item.entity.GetDescription())
}

// Return true if the given selection is contained in this item
func (item *entityItem) Select() interface{} {
	if iEntity, ok := item.entity.(IndirectEntity); ok {
		return iEntity.Select()
	}
	return item.entity
}

// Contains returns true if the given selection is contained in this item
func (item *entityItem) Contains(selection interface{}) bool {
	if item.entity == selection {
		return true
	}
	if iEntity, ok := item.entity.(IndirectEntity); ok {
		if iEntity.Select() == selection {
			return true
		}
	}
	return false
}

func (item *entityItem) ProcessEvents() interface{} {
	if item.Clicked() {
		return item.entity
	}
	return nil
}

func (item *entityItem) Layout(ctx context.Context, gtx C, th *material.Theme, selected interface{}, focused bool) D {
	lb := material.Label(th, th.TextSize, item.entity.GetDescription())
	return material.Clickable(gtx, &item.Clickable, func(gtx C) D {
		if selected == item.entity {
			fg := th.ContrastFg
			bg := th.ContrastBg
			if !focused {
				bg.A = bg.A / 2
			}
			lb.Color = fg
			clip.Rect{
				Max: gtx.Constraints.Max,
			}.Add(gtx.Ops)
			paint.Fill(gtx.Ops, bg)
		}
		return LayoutWithLevel(gtx, item.level, lb.Layout)
	})
}

// Generate sub-widgets
func (item entityItem) GenerateWidgets(ctx context.Context, level int) TreeViewItems {
	return nil
}
