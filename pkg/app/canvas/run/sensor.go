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
	"fmt"
	"image"
	"image/color"

	"gioui.org/f32"
	"gioui.org/layout"
	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/app/canvas"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

type sensor struct {
	entity state.Sensor
}

// Return a matrix for drawing the widget in its proper orientation and
// the size of the area it is drawing into.
func (b *sensor) GetAffineAndSize() (f32.Affine2D, f32.Point, float32) {
	return canvas.GetPositionedEntityAffineAndSize(b.entity.GetModel())
}

// Layout must be initialized to a layout function to draw the widget
// and process events.
func (b *sensor) Layout(ctx context.Context, gtx C, size image.Point, th *material.Theme, state canvas.WidgetState) {
	if state.Clicked {
		vm := b.entity.GetRailway().GetVirtualMode()
		fmt.Println("Sensor clicked")
		if vm.GetEnabled() {
			vm.EntityClick(ctx, b.entity)
			fmt.Println("EntityClick returned")
		}
	}

	bg := b.getBackgroundColor(ctx)
	if state.Hovered {
		bg.A = 0xA0
	}

	canvas.DrawSensorShape(gtx, b.entity.GetShape(), bg, layout.FPt(size))
}

// getBackgroundColor returns the color for the sensor based on the current state
func (b *sensor) getBackgroundColor(ctx context.Context) color.NRGBA {
	switch st := b.entity.(type) {
	case state.BinarySensor:
		if st.GetActive().GetActual(ctx) {
			return canvas.ActiveSensorBg
		}
	}
	return canvas.InactiveSensorBg
}
