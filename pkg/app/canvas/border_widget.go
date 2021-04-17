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

package canvas

import (
	"context"
	"image"
	"image/color"

	"gioui.org/f32"
	"gioui.org/layout"
	"gioui.org/op"
	"gioui.org/op/clip"
	"gioui.org/op/paint"
	"gioui.org/widget/material"
)

type BorderWidget struct {
	bounds f32.Rectangle
	color  color.NRGBA
}

// NewBorderWidget constructs a new border widget.
func NewBorderWidget(bounds f32.Rectangle, color color.NRGBA) Widget {
	w := BorderWidget{
		bounds: bounds,
		color:  color,
	}
	return &w
}

// Return a matrix for drawing the widget in its proper orientation and
// the size of the area it is drawing into.
// Returns: Matrix, Size, Rotation (in radials, already applied in Matrix)
func (w *BorderWidget) GetAffineAndSize() (f32.Affine2D, f32.Point, float32) {
	tr := f32.Affine2D{}.
		Offset(w.bounds.Min)
	size := w.bounds.Size()
	return tr, size, 0
}

// Layout draws the widget and process events.
func (w *BorderWidget) Layout(ctx context.Context, gtx layout.Context, size image.Point, th *material.Theme, state WidgetState) {
	defer op.Save(gtx.Ops).Load()
	paint.ColorOp{Color: w.color}.Add(gtx.Ops)
	clip.Stroke{
		Path:  clip.UniformRRect(f32.Rectangle{Max: layout.FPt(size)}, 0).Path(gtx.Ops),
		Style: clip.StrokeStyle{Width: 1},
	}.Op().Add(gtx.Ops)
}
