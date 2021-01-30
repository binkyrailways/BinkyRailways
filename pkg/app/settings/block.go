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
	w "gioui.org/widget"
	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// NewBlockSettings constructs a settings component for a Block.
func NewBlockSettings(entity model.Block) Settings {
	s := &blockSettings{
		entity: entity,
	}
	s.description.SetText(entity.GetDescription())
	s.x.SetValue(entity.GetX())
	s.y.SetValue(entity.GetY())
	s.width.SetValue(entity.GetWidth())
	s.height.SetValue(entity.GetHeight())
	return s
}

// blockSettings implements an settings grid for a Block.
type blockSettings struct {
	entity model.Block

	description w.Editor
	x           widgets.IntEditor
	y           widgets.IntEditor
	width       widgets.IntEditor
	height      widgets.IntEditor
}

// Handle events and draw the editor
func (e *blockSettings) Layout(gtx C, th *material.Theme) D {
	e.entity.SetDescription(e.description.Text())
	if value, err := e.x.GetValue(); err == nil {
		e.entity.SetX(value)
	}
	if value, err := e.y.GetValue(); err == nil {
		e.entity.SetY(value)
	}
	if value, err := e.width.GetValue(); err == nil {
		e.entity.SetWidth(value)
	}
	if value, err := e.height.GetValue(); err == nil {
		e.entity.SetHeight(value)
	}

	// Prepare settings grid
	grid := widgets.NewSettingsGrid(
		widgets.SettingsGridRow{Title: "Name", Layout: func(gtx C) D {
			return material.Editor(th, &e.description, "Name").Layout(gtx)
		}},
		widgets.SettingsGridRow{Title: "X", Layout: func(gtx C) D {
			return material.Editor(th, &e.x.Editor, "").Layout(gtx)
		}},
		widgets.SettingsGridRow{Title: "Y", Layout: func(gtx C) D {
			return material.Editor(th, &e.y.Editor, "").Layout(gtx)
		}},
		widgets.SettingsGridRow{Title: "Width", Layout: func(gtx C) D {
			return material.Editor(th, &e.width.Editor, "").Layout(gtx)
		}},
		widgets.SettingsGridRow{Title: "Height", Layout: func(gtx C) D {
			return material.Editor(th, &e.height.Editor, "").Layout(gtx)
		}},
	)

	return grid.Layout(gtx, th)
}
