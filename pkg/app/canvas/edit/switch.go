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

package edit

import (
	"context"
	"image"

	"gioui.org/f32"
	"gioui.org/layout"
	"gioui.org/op/clip"
	"gioui.org/op/paint"
	"gioui.org/unit"
	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/app/canvas"
	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type stdSwitch struct {
	entity model.Switch
}

// Return a matrix for drawing the widget in its proper orientation and
// the size of the area it is drawing into.
func (b *stdSwitch) GetAffineAndSize() (f32.Affine2D, f32.Point, float32) {
	return canvas.GetPositionedEntityAffineAndSize(b.entity)
}

// Layout must be initialized to a layout function to draw the widget
// and process events.
func (b *stdSwitch) Layout(ctx context.Context, gtx C, size image.Point, th *material.Theme, state canvas.WidgetState) {
	bg := canvas.JunctionBg
	if state.Hovered {
		bg = canvas.HoverBg
	}

	rect := f32.Rectangle{Max: layout.FPt(size)}
	paint.FillShape(gtx.Ops, bg, clip.UniformRRect(rect, float32(gtx.Px(unit.Dp(4)))).Op(gtx.Ops))

	widgets.TextCenter(gtx, th, b.entity.GetDescription())
}
