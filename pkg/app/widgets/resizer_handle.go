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

	"gioui.org/f32"
	"gioui.org/layout"
	"gioui.org/op/clip"
	"gioui.org/op/paint"
)

// HorizontalResizerHandle draws a resize handle for a horizontal resizer.
func HorizontalResizerHandle(gtx C) D {
	return ResizerHandle(gtx, layout.Horizontal)
}

// VerticalResizerHandle draws a resize handle for a vertical resizer.
func VerticalResizerHandle(gtx C) D {
	return ResizerHandle(gtx, layout.Horizontal)
}

// ResizerHandle draws a resize handle for the given axis.
func ResizerHandle(gtx C, axis layout.Axis) D {
	sz := axis.Convert(image.Point{X: 10, Y: gtx.Constraints.Max.Y})
	rect := clip.UniformRRect(f32.Rect(0, 0, float32(sz.X), float32(sz.Y)), 3)
	paint.FillShape(gtx.Ops, BlueGrey, rect.Op(gtx.Ops))
	return layout.Dimensions{Size: sz}
}
