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

	"gioui.org/layout"
	"gioui.org/op"
)

type SimpleTable struct {
	Columns     []SimpleColumn
	CellSpacing image.Point

	colWidths  []int
	rowHeights []int
}

type SimpleColumn struct {
}

func fillWithZeros(list []int) {
	for i := range list {
		list[i] = 0
	}
}

func (t *SimpleTable) Layout(gtx C, rows int, cell func(gtx C, x, y int) D) D {
	// Prepare internals
	nCols := len(t.Columns)
	if len(t.colWidths) != nCols {
		t.colWidths = make([]int, nCols)
	} else {
		fillWithZeros(t.colWidths)
	}
	if len(t.rowHeights) != rows {
		t.rowHeights = make([]int, rows)
	} else {
		fillWithZeros(t.rowHeights)
	}

	// Make first pass to calculate column width
	cgtx := gtx
	cgtx.Ops = new(op.Ops)
	cgtx.Constraints.Min = image.Point{}
	for y := 0; y < rows; y++ {
		for x := 0; x < nCols; x++ {
			macro := op.Record(cgtx.Ops)
			dim := cell(cgtx, x, y)
			macro.Stop()
			if dim.Size.X > t.colWidths[x] {
				t.colWidths[x] = dim.Size.X
			}
			if dim.Size.Y > t.rowHeights[y] {
				t.rowHeights[y] = dim.Size.Y
			}
		}
	}
	// Second pass to perform actual layout
	yOffset := 0
	xOffset := 0
	for y := 0; y < rows; y++ {
		xOffset = 0
		for x := 0; x < nCols; x++ {
			//stack := op.Save(gtx.Ops)
			state := op.Offset(layout.FPt(image.Pt(xOffset, yOffset))).Push(gtx.Ops)
			sz := image.Pt(t.colWidths[x], t.rowHeights[y])
			gtx.Constraints.Max = sz
			gtx.Constraints.Min = sz
			cell(gtx, x, y)
			state.Pop()
			xOffset += sz.X + t.CellSpacing.X
		}
		yOffset += t.rowHeights[y] + t.CellSpacing.Y
	}
	return layout.Dimensions{
		Size: image.Pt(xOffset, yOffset),
	}
}
