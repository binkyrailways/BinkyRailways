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
	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/app/canvas"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type sensor struct {
	entity model.Sensor
}

// Return a matrix for drawing the widget in its proper orientation and
// the size of the area it is drawing into.
func (b *sensor) GetAffineAndSize() (f32.Affine2D, f32.Point, float32) {
	return canvas.GetPositionedEntityAffineAndSize(b.entity)
}

// Layout must be initialized to a layout function to draw the widget
// and process events.
func (b *sensor) Layout(ctx context.Context, gtx C, size image.Point, th *material.Theme, state canvas.WidgetState) {
	bg := canvas.SensorBg
	if state.Hovered {
		bg = canvas.HoverBg
	}
	canvas.DrawSensorShape(gtx, b.entity.GetShape(), bg, layout.FPt(size))
}
