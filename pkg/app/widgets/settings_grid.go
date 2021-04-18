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
	"image"
	"math"

	"gioui.org/layout"
	"gioui.org/text"
	"gioui.org/unit"
	"gioui.org/widget/material"
	"gioui.org/x/outlay"
	"golang.org/x/image/math/fixed"
)

func NewSettingsGrid(row ...SettingsGridRow) SettingsGrid {
	return SettingsGrid{
		Rows:   row,
		Gutter: unit.Dp(5),
	}
}

// SettingsGrid shows a vertical align name+widget grid
type SettingsGrid struct {
	Rows   []SettingsGridRow
	Gutter unit.Value
}

type SettingsGridRow struct {
	Title      string
	TitleScale float32
	Layout     func(C) D
}

const defaultTitleScale = 1.4

func (sg SettingsGrid) Layout(gtx C, th *material.Theme) D {
	rowCount := len(sg.Rows)
	vGrid := outlay.Grid{
		Num:  2, //rowCount,
		Axis: layout.Horizontal,
	}
	// Calculate largest label size
	maxTextSize := fixed.Point26_6{}
	for _, row := range sg.Rows {
		thTextSize := th.TextSize
		if row.Layout == nil {
			scale := row.TitleScale
			if scale == 0 {
				scale = defaultTitleScale
			}
			thTextSize = thTextSize.Scale(scale)
		}
		fontSize := fixed.I(gtx.Px(thTextSize))
		lines := th.Shaper.LayoutString(text.Font{}, fontSize, math.MaxInt16, row.Title)
		b := lines[0].Bounds
		textSize := b.Max.Sub(b.Min)
		if textSize.X > maxTextSize.X {
			maxTextSize.X = textSize.X
		}
		if textSize.Y > maxTextSize.Y {
			maxTextSize.Y = textSize.Y
		}
	}
	textDim := D{
		Size: image.Pt(maxTextSize.X.Ceil(), maxTextSize.Y.Ceil()),
	}
	// Layout grid
	return vGrid.Layout(gtx, rowCount*2, func(gtx C, i int) D {
		row := sg.Rows[i/2]
		layoutField := row.Layout
		if i%2 == 0 {
			thTextSize := th.TextSize
			scale := row.TitleScale
			if scale == 0 {
				scale = defaultTitleScale
			}
			lb := material.Label(th, thTextSize, sg.Rows[i/2].Title)
			if layoutField == nil {
				lb.Font.Weight = text.Bold
			}
			layout.Inset{
				Right: sg.Gutter,
			}.Layout(gtx, lb.Layout)
			return textDim
		}
		var dims D
		if layoutField != nil {
			dims = layoutField(gtx)
		}
		if dims.Size.X < textDim.Size.X {
			dims.Size.X = textDim.Size.X
		}
		return dims
	})
}
