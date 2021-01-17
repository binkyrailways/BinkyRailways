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
	"sort"

	"gioui.org/layout"
	"gioui.org/unit"
	"gioui.org/widget/material"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type EntityGroup struct {
	Name       string
	Collection func() []model.Entity
}

func (v EntityGroup) Layout(gtx C, th *material.Theme) D {
	entities := v.Collection()
	sort.Slice(entities, func(i, j int) bool {
		return entities[i].GetDescription() < entities[j].GetDescription()
	})
	items := make([]layout.FlexChild, 1, 1+len(entities))
	items[0] = layout.Rigid(material.Label(th, th.TextSize.Scale(1.4), v.Name).Layout)
	for _, entity := range entities {
		descr := entity.GetDescription()
		items = append(items, layout.Rigid(func(gtx C) D {
			return layout.Inset{Left: unit.Dp(20)}.Layout(gtx, material.Label(th, th.TextSize, descr).Layout)
		}))
	}
	return layout.Flex{Axis: layout.Vertical}.Layout(gtx, items...)
}
