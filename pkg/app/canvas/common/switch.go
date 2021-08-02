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
	"gioui.org/layout"
	"gioui.org/op"
	"gioui.org/op/clip"
	"gioui.org/op/paint"
	"gioui.org/unit"
	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/app/canvas"
	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

type StdSwitch struct {
	Model model.Switch
	State state.Switch
}

// Return a matrix for drawing the widget in its proper orientation and
// the size of the area it is drawing into.
func (b *StdSwitch) GetAffineAndSize() (f32.Affine2D, f32.Point, float32) {
	tr, sz, rad := canvas.GetPositionedEntityAffineAndSize(b.Model)
	sz.Y = sz.Y * 2
	return tr, sz, rad
}

// Layout must be initialized to a layout function to draw the widget
// and process events.
func (b *StdSwitch) Layout(ctx context.Context, gtx C, size image.Point, th *material.Theme, state canvas.WidgetState) {
	bg := canvas.SwitchBg
	if state.Hovered {
		bg = canvas.HoverBg
	}

	// Draw background
	size.Y = size.Y / 2
	rect := f32.Rectangle{Max: layout.FPt(size)}
	center := f32.Pt((rect.Min.X+rect.Max.X)/2, (rect.Min.Y+rect.Max.Y)/2)
	bgShape := clip.UniformRRect(rect, float32(gtx.Px(unit.Dp(2))))
	paint.FillShape(gtx.Ops, bg, bgShape.Op(gtx.Ops))

	// Draw switch indicator
	dir, consistent := b.getDirection(ctx)
	indicatorColor := canvas.SwitchIndicator
	if !consistent {
		indicatorColor = canvas.SwitchIndicatorInconsistent
	}
	if dir != model.SwitchDirectionOff {
		// Straight
		h := rect.Max.Y
		rect.Max.Y = h / 5
		rect = rect.Add(f32.Pt(0, 2*(h/5)))
		paint.FillShape(gtx.Ops, indicatorColor, clip.UniformRRect(rect, float32(gtx.Px(unit.Dp(0)))).Op(gtx.Ops))
	} else {
		// Off
		// Clip area
		st := op.Save(gtx.Ops)
		bgShape.Op(gtx.Ops).Add(gtx.Ops)
		// Rotate
		tr := f32.Affine2D{}.Rotate(center, 60.0/180.0)
		op.Affine(tr).Add(gtx.Ops)
		h := rect.Max.Y
		rect.Max.Y = h / 5
		rect = rect.Add(f32.Pt(0, 2*(h/5)))
		paint.FillShape(gtx.Ops, indicatorColor, clip.UniformRRect(rect, float32(gtx.Px(unit.Dp(0)))).Op(gtx.Ops))
		// Retore previous state
		st.Load()
	}

	// Draw label below switch
	st := op.Save(gtx.Ops)
	gtx.Constraints.Max.Y = gtx.Constraints.Max.Y / 2
	tr := f32.Affine2D{}.Offset(f32.Pt(0, float32(size.Y/2)))
	op.Affine(tr).Add(gtx.Ops)
	widgets.TextCenter(gtx, th, b.Model.GetDescription())
	// Retore previous state
	st.Load()
}

// getDirection returns the direction of the switch to draw.
// Returns: direction, consistent
func (b *StdSwitch) getDirection(ctx context.Context) (model.SwitchDirection, bool) {
	if b.State != nil {
		dirProp := b.State.GetDirection()
		actual := dirProp.GetActual(ctx)
		req := dirProp.GetRequested(ctx)
		return actual, actual == req
	}
	return model.SwitchDirectionStraight, true
}
