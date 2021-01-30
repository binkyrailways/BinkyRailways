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
	"math"

	"gioui.org/layout"
	"gioui.org/text"
	"gioui.org/widget"
	"gioui.org/widget/material"
	"golang.org/x/image/math/fixed"
)

// TextCenter prints the given string centered in the constrains of the given context,
// scaling to the largest possible size that is less or equal to the text size
// of the theme.
func TextCenter(gtx C, th *material.Theme, str string) D {
	lb := widget.Label{
		Alignment: text.Middle,
		MaxLines:  1,
	}

	// Calculate string size
	fontSize := fixed.I(gtx.Px(th.TextSize))
	lines := th.Shaper.LayoutString(text.Font{}, fontSize, math.MaxInt16, str)
	b := lines[0].Bounds
	textSize := b.Max.Sub(b.Min)
	scaleX := float32(gtx.Constraints.Max.X) / float32(textSize.X.Ceil())
	scaleY := float32(gtx.Constraints.Max.Y) / float32(textSize.Y.Ceil())
	scale := scaleX
	if scaleY < scale {
		scale = scaleY
	}
	if scale > 1.0 {
		scale = 1.0
	}

	return layout.Center.Layout(gtx, func(gtx C) D {
		return lb.Layout(gtx, th.Shaper, text.Font{}, th.TextSize.Scale(scale), str)
	})
}
