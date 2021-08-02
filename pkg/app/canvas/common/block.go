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
	"image/color"

	"gioui.org/f32"
	"gioui.org/layout"
	"gioui.org/op/clip"
	"gioui.org/op/paint"
	"gioui.org/unit"
	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/app/canvas"
	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

type Block struct {
	Model model.Block
	State state.Block
}

// Return a matrix for drawing the widget in its proper orientation and
// the size of the area it is drawing into.
func (b *Block) GetAffineAndSize() (f32.Affine2D, f32.Point, float32) {
	return canvas.GetPositionedEntityAffineAndSize(b.Model)
}

// Layout must be initialized to a layout function to draw the widget
// and process events.
func (b *Block) Layout(ctx context.Context, gtx C, size image.Point, th *material.Theme, state canvas.WidgetState) {
	// Get state
	lbl, bg := b.getDescription(ctx)
	if state.Hovered {
		bg.A = 0xFF
	}

	// Draw block background
	rect := f32.Rectangle{Max: layout.FPt(size)}
	paint.FillShape(gtx.Ops, bg, clip.UniformRRect(rect, float32(gtx.Px(unit.Dp(4)))).Op(gtx.Ops))

	// Draw front of block
	if b.Model.GetReverseSides() {
		// Front "right" of block
		rect.Max.X = rect.Max.X / 5
	} else {
		// Front "left" of block
		w := rect.Max.X
		rect.Max.X = rect.Max.X / 5
		rect = rect.Add(f32.Pt(w-rect.Max.X, 0))
	}
	paint.FillShape(gtx.Ops, canvas.BlockFront, clip.UniformRRect(rect, float32(gtx.Px(unit.Dp(4)))).Op(gtx.Ops))

	// Draw label
	widgets.TextCenter(gtx, th, lbl)
}

// getDescription returns text to put in block.
// Returns: label, bgColor
func (b *Block) getDescription(ctx context.Context) (string, color.NRGBA) {
	lbl := b.Model.GetDescription()
	bg := canvas.BlockBg
	if st := b.State; st != nil {
		switch st.GetState(ctx) {
		case state.BlockStateFree:
			return lbl + ": Free", canvas.BlockFreeBg
		case state.BlockStateOccupied:
			return lbl + ": Occupied", canvas.BlockOccupiedBg
		case state.BlockStateOccupiedUnexpected:
			return lbl + ": Unexpected", bg
		case state.BlockStateDestination:
			if l := st.GetLockedBy(ctx); l != nil {
				return lbl + ":" + l.GetDescription(), canvas.BlockDestinationBg
			}
		case state.BlockStateEntering:
			if l := st.GetLockedBy(ctx); l != nil {
				return lbl + ":" + l.GetDescription(), canvas.BlockEnteringBg
			}
		case state.BlockStateLocked:
			if l := st.GetLockedBy(ctx); l != nil {
				return lbl + ":" + l.GetDescription(), bg
			}
		case state.BlockStateClosed:
			return lbl + ": Closed", bg
		}
	}
	return lbl, bg
}
