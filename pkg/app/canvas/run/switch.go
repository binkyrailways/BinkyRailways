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

package run

import (
	"context"

	"gioui.org/f32"
	"gioui.org/op/paint"
	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/app/canvas"
	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

type stdSwitch struct {
	entity state.Switch
}

// Return the bounds of the widget on the canvas
func (b *stdSwitch) GetBounds() f32.Rectangle {
	return canvas.GetPositionedEntityBounds(b.entity.GetModel())
}

// Returns rotation of entity in degrees
func (b *stdSwitch) GetRotation() int {
	return b.entity.GetModel().GetRotation()
}

// Layout must be initialized to a layout function to draw the widget
// and process events.
func (b *stdSwitch) Layout(ctx context.Context, gtx C, th *material.Theme, state canvas.WidgetState) {
	bg := canvas.BlockBg
	if state.Hovered {
		bg = canvas.HoverBg
	}
	paint.Fill(gtx.Ops, bg)
	//lb := material.Label(th, th.TextSize, b.entity.GetDescription())
	//lb.Alignment = text.Middle
	//lb.Layout(gtx)
	widgets.TextCenter(gtx, th, b.entity.GetDescription())
}
