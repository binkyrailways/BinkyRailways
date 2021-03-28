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

	"gioui.org/f32"
	"gioui.org/layout"
	"gioui.org/op/paint"
	"gioui.org/widget"
	"gioui.org/widget/material"
)

type ImageWidget struct {
	bounds  f32.Rectangle
	image   image.Image
	imageOp paint.ImageOp
}

// NewImageWidget constructs a new image widget.
func NewImageWidget(bounds f32.Rectangle, image image.Image) Widget {
	w := ImageWidget{
		bounds: bounds,
		image:  image,
	}
	if image != nil {
		w.imageOp = paint.NewImageOp(image)
	} else {
		w.imageOp = paint.ImageOp{}
	}
	return &w
}

// Return a matrix for drawing the widget in its proper orientation and
// the size of the area it is drawing into.
// Returns: Matrix, Size, Rotation (in radials, already applied in Matrix)
func (w *ImageWidget) GetAffineAndSize() (f32.Affine2D, f32.Point, float32) {
	tr := f32.Affine2D{}.
		Offset(w.bounds.Min)
	size := w.bounds.Size()
	return tr, size, 0
}

// Layout draws the widget and process events.
func (w *ImageWidget) Layout(ctx context.Context, gtx layout.Context, size image.Point, th *material.Theme, state WidgetState) {
	// Draw image (if any)
	if w.image != nil {
		prevMax := gtx.Constraints.Max
		gtx.Constraints.Max = size
		widget.Image{
			Src: w.imageOp,
			Fit: widget.Contain,
		}.Layout(gtx)
		gtx.Constraints.Max = prevMax
	}
}
