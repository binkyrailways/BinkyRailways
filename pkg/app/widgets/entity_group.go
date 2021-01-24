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
	"sort"

	"gioui.org/layout"
	"gioui.org/op/paint"
	"gioui.org/unit"
	w "gioui.org/widget"
	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type EntityGroup struct {
	Name       string
	Collection func() []model.Entity
	items      []entityItem
}

type entityItem struct {
	entity model.Entity
	w.Clickable
}

type entityGroupWidget func(C, *material.Theme, model.Entity, func(entity model.Entity)) D

// generateWidgets generates all widgets for this group
func (v *EntityGroup) generateWidgets() []entityGroupWidget {
	// Get entities
	entities := v.Collection()
	sort.Slice(entities, func(i, j int) bool {
		return entities[i].GetDescription() < entities[j].GetDescription()
	})
	// Prepare result list
	result := make([]entityGroupWidget, 1+len(entities))
	result[0] = func(gtx C, th *material.Theme, selection model.Entity, onSelect func(entity model.Entity)) D {
		return material.Label(th, th.TextSize.Scale(1.4), v.Name).Layout(gtx)
	}
	// Rebuild items if needed
	if len(v.items) != len(entities) {
		fmt.Println("Rebuild items")
		v.items = make([]entityItem, len(entities))
	}
	for idx, entity := range entities {
		v.items[idx].entity = entity
		result[idx+1] = v.items[idx].Layout
	}
	return result
}

func (item *entityItem) Layout(gtx C, th *material.Theme, selected model.Entity, onSelect func(entity model.Entity)) D {
	// Process events
	if item.Clicked() {
		fmt.Println("click " + item.entity.GetID())
		onSelect(item.entity)
	}

	lb := material.Label(th, th.TextSize, item.entity.GetDescription())
	return material.Clickable(gtx, &item.Clickable, func(gtx C) D {
		if selected == item.entity {
			lb.Color = th.ContrastFg
			paint.Fill(gtx.Ops, th.ContrastBg)
		}
		return layout.Inset{Left: unit.Dp(20)}.Layout(gtx, lb.Layout)
	})
}
