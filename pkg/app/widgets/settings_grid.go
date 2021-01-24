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
	"gioui.org/layout"
	"gioui.org/widget/material"
	"gioui.org/x/outlay"
)

func NewSettingsGrid(row ...SettingsGridRow) SettingsGrid {
	return SettingsGrid{Rows: row}
}

// SettingsGrid shows a vertical align name+widget grid
type SettingsGrid struct {
	Rows []SettingsGridRow
}

type SettingsGridRow struct {
	Title  string
	Layout func(C) D
}

func (sg SettingsGrid) Layout(gtx C, th *material.Theme) D {
	rowCount := len(sg.Rows)
	vGrid := outlay.Grid{
		Num:  rowCount,
		Axis: layout.Vertical,
	}
	return vGrid.Layout(gtx, rowCount*2, func(gtx C, i int) D {
		if i < rowCount {
			return material.Label(th, th.TextSize, sg.Rows[i].Title).Layout(gtx)
		}
		return sg.Rows[i-rowCount].Layout(gtx)
	})
}
