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
	"image/color"
	"math"

	"gioui.org/f32"
	"gioui.org/layout"
	"gioui.org/op/clip"
	"gioui.org/op/paint"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// DrawSensorShape draws the shape on a sensor in the given box.
func DrawSensorShape(gtx layout.Context, shape model.Shape, color color.NRGBA, size f32.Point) {
	sz := float32(math.Min(float64(size.X), float64(size.Y)))
	dx := (size.X - sz) / 2
	dy := (size.Y - sz) / 2
	var p clip.Path

	switch shape {
	case model.ShapeCircle:
		p.Begin(gtx.Ops)
		// Top half
		p.MoveTo(f32.Pt(dx, dy+sz/2)) // Left middle
		p.Cube(f32.Pt(dx, dy), f32.Pt(dx+sz, dy), f32.Pt(dx+sz, dy+sz/2))
		// Bottom half
		p.MoveTo(f32.Pt(dx, dy+sz/2)) // Left middle
		p.Cube(f32.Pt(dx, dy+sz), f32.Pt(dx+sz, dy+sz), f32.Pt(dx+sz, dy+sz/2))
		clip.Outline{Path: p.End()}.Op().Push(gtx.Ops).Pop()
		paint.Fill(gtx.Ops, color)
	case model.ShapeSquare:
		p.Begin(gtx.Ops)
		p.MoveTo(f32.Pt(dx, dy))       // Left Top
		p.LineTo(f32.Pt(dx+sz, dy))    // Right Top
		p.LineTo(f32.Pt(dx+sz, dy+sz)) // Right Bottom
		p.LineTo(f32.Pt(dx, dy+sz))    // Left Bottom
		p.Close()
		clip.Outline{Path: p.End()}.Op().Push(gtx.Ops).Pop()
		paint.Fill(gtx.Ops, color)
	case model.ShapeTriangle:
		p.Begin(gtx.Ops)
		p.MoveTo(f32.Pt(dx, dy+sz))    // Bottom left
		p.LineTo(f32.Pt(dx+sz/2, dy))  // Top middle
		p.LineTo(f32.Pt(dx+sz, dy+sz)) // Bottom right
		p.Close()
		clip.Outline{Path: p.End()}.Op().Push(gtx.Ops).Pop()
		paint.Fill(gtx.Ops, color)
	case model.ShapeDiamond:
		mx := dx + sz/2 // middle X
		my := dy + sz/2 // middle Y
		p.Begin(gtx.Ops)
		p.MoveTo(f32.Pt(dx, my))    // Left middle
		p.LineTo(f32.Pt(mx, dy))    // Top middle
		p.LineTo(f32.Pt(dx+sz, my)) // Right middle
		p.LineTo(f32.Pt(mx, dy+sz)) // Bottom middle
		p.Close()
		clip.Outline{Path: p.End()}.Op().Push(gtx.Ops).Pop()
		paint.Fill(gtx.Ops, color)
	default:
		return
	}
}
