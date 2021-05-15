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

package settings

import (
	"sort"
	"strings"

	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// NewBlockGroupSettings constructs a settings component for a BlockGroup.
func NewBlockGroupSettings(entity model.BlockGroup) Settings {
	s := &blockGroupSettings{
		entity: entity,
	}
	module := entity.GetModule()
	getChecked := func(id string) bool {
		if b, ok := module.GetBlocks().Get(id); ok {
			return b.GetBlockGroup() == entity
		}
		return false
	}
	setChecked := func(id string, checked bool) {
		if b, ok := module.GetBlocks().Get(id); ok {
			if checked {
				b.SetBlockGroup(entity)
			} else {
				b.SetBlockGroup(nil)
			}
		}
	}
	lvs := make([]widgets.LabeledValue, 0, module.GetBlocks().GetCount())
	module.GetBlocks().ForEach(func(b model.Block) {
		lvs = append(lvs, widgets.LV(b.GetID(), b.GetDescription()))
	})
	sort.Slice(lvs, func(i, j int) bool {
		x, y := lvs[i].Label, lvs[j].Label
		return strings.Compare(x, y) < 0
	})
	s.blocks = widgets.NewCheckboxList(getChecked, setChecked, lvs...)
	s.metaSettings.Initialize(entity)
	s.minBlocksInGroup.SetValue(entity.GetMinimumLocsInGroup())
	s.minLocsOnTrackForMinLocsInGroupStart.SetValue(entity.GetMinimumLocsOnTrackForMinimumLocsInGroupStart())
	return s
}

// blockGroupSettings implements an settings grid for a BlockGroup.
type blockGroupSettings struct {
	entity model.BlockGroup

	metaSettings
	blocks                               *widgets.CheckboxList
	minBlocksInGroup                     widgets.IntEditor
	minLocsOnTrackForMinLocsInGroupStart widgets.IntEditor
}

// Handle events and draw the editor
func (e *blockGroupSettings) Layout(gtx C, th *material.Theme) D {
	e.metaSettings.Update(e.entity)
	if value, err := e.minBlocksInGroup.GetValue(); err == nil {
		e.entity.SetMinimumLocsInGroup(value)
	}
	if value, err := e.minLocsOnTrackForMinLocsInGroupStart.GetValue(); err == nil {
		e.entity.SetMinimumLocsOnTrackForMinimumLocsInGroupStart(value)
	}

	// Prepare settings grid
	grid := widgets.NewSettingsGrid(
		append(e.metaSettings.Rows(th),
			widgets.SettingsGridRow{
				Title: "Blocks",
				Layout: func(gtx C) D {
					return e.blocks.Layout(gtx, th)
				},
			},
			widgets.SettingsGridRow{
				Title: "Min. locs in block",
				Layout: func(gtx C) D {
					return material.Editor(th, &e.minBlocksInGroup.Editor, "").Layout(gtx)
				},
			},
			widgets.SettingsGridRow{
				Title: "Min. locs on track",
				Layout: func(gtx C) D {
					return material.Editor(th, &e.minLocsOnTrackForMinLocsInGroupStart.Editor, "").Layout(gtx)
				},
			},
		)...,
	)

	return grid.Layout(gtx, th)
}
