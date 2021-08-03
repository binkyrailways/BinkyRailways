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

package common

import (
	"context"
	"image"

	"gioui.org/f32"
	"gioui.org/op/clip"
	"gioui.org/op/paint"
	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/app/canvas"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

type Route struct {
	Model model.Route
	State state.Route
}

// Return a matrix for drawing the widget in its proper orientation and
// the size of the area it is drawing into.
func (b *Route) GetAffineAndSize() (f32.Affine2D, f32.Point, float32) {
	from1, from2 := b.getPoint(b.Model.GetFrom(), b.Model.GetFromBlockSide())
	to1, to2 := b.getPoint(b.Model.GetTo(), b.Model.GetToBlockSide())
	ctrl1, ctrl2 := b.getControlPoints(from1, from2, to1, to2)

	rect := f32.Rectangle{
		Min: from1,
		Max: to1,
	}.Canon().Union(f32.Rectangle{
		Min: ctrl1,
		Max: ctrl2,
	}.Canon()).Canon()

	tr := f32.Affine2D{}
	return tr, rect.Max, 0
}

// Layout must be initialized to a layout function to draw the widget
// and process events.
func (b *Route) Layout(ctx context.Context, gtx C, size image.Point, th *material.Theme, state canvas.WidgetState) {
	from1, from2 := b.getPoint(b.Model.GetFrom(), b.Model.GetFromBlockSide())
	to1, to2 := b.getPoint(b.Model.GetTo(), b.Model.GetToBlockSide())
	ctrl1, ctrl2 := b.getControlPoints(from1, from2, to1, to2)

	var p clip.Path
	p.Begin(gtx.Ops)
	p.MoveTo(from1)
	p.CubeTo(ctrl1, ctrl2, to1)

	paint.ColorOp{Color: canvas.BlockDestinationBg}.Add(gtx.Ops)
	clip.Stroke{
		Path:  p.End(),
		Style: clip.StrokeStyle{Width: 1},
	}.Op().Add(gtx.Ops)
	paint.PaintOp{}.Add(gtx.Ops)
}

// getPoints returns the point (in graphics space) of the given endpoint.
// Returns: sidePt, otherSidePt
func (b *Route) getPoint(endpoint model.EndPoint, side model.BlockSide) (f32.Point, f32.Point) {
	if endpoint == nil {
		return f32.Pt(0, 0), f32.Pt(0, 0)
	}
	tr, sz, _ := canvas.GetPositionedEntityAffineAndSize(endpoint)
	switch ep := endpoint.(type) {
	case model.Block:
		frontPt := f32.Pt(sz.X, sz.Y/2)
		backPt := f32.Pt(0, sz.Y/2)
		if ep.GetReverseSides() {
			// Swap front, back
			frontPt, backPt = backPt, frontPt
		}
		if side == model.BlockSideBack {
			// Swap front, back
			frontPt, backPt = backPt, frontPt
		}
		return tr.Transform(frontPt), tr.Transform(backPt)
	}
	// Take center
	center := tr.Transform(f32.Pt(sz.X/2, sz.Y/2))
	return center, center
}

// getControlPoints returns the control points for drawing.
func (b *Route) getControlPoints(from1, from2, to1, to2 f32.Point) (f32.Point, f32.Point) {
	fromDiff := from1.Sub(from2)
	toDiff := to2.Sub(to1)
	ctrl1 := from1.Add(fromDiff.Mul(2))
	ctrl2 := to1.Sub(toDiff.Mul(2))

	if ctrl1.X < 0 {
		ctrl1.X = 0
	}
	if ctrl1.Y < 0 {
		ctrl1.Y = 0
	}
	if ctrl2.X < 0 {
		ctrl2.X = 0
	}
	if ctrl2.Y < 0 {
		ctrl2.Y = 0
	}

	return ctrl1, ctrl2
}
