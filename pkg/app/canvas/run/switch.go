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

	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/app/canvas"
	"github.com/binkyrailways/BinkyRailways/pkg/app/canvas/common"
)

type stdSwitch struct {
	common.StdSwitch
}

// Layout must be initialized to a layout function to draw the widget
// and process events.
func (b *stdSwitch) Layout(ctx context.Context, gtx C, size image.Point, th *material.Theme, state canvas.WidgetState) {
	if state.Clicked {
		dirProp := b.State.GetDirection()
		dir := dirProp.GetRequested(ctx)
		dirProp.SetRequested(ctx, dir.Invert())
		fmt.Printf("Set switch direction to %s\n", dir.Invert())
	}

	b.StdSwitch.Layout(ctx, gtx, size, th, state)
}
