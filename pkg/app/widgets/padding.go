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
	"gioui.org/layout"
	"gioui.org/unit"
)

// WithPadding adds a standard padding around the given widget.
// If not padding is given, the default padding is applied.
// When 2 padding values are given, they are taken as top/bottom, left/right
// When 4 padding values are given, they are taken as top, right, bottom, left
func WithPadding(gtx C, w func(C) D, padding ...unit.Value) D {
	inset := layout.Inset{
		Top:    Padding,
		Bottom: Padding,
		Left:   Padding,
		Right:  Padding,
	}
	switch len(padding) {
	case 2:
		inset.Top = padding[0]
		inset.Bottom = padding[0]
		inset.Left = padding[1]
		inset.Right = padding[1]
	case 4:
		inset.Top = padding[0]
		inset.Right = padding[1]
		inset.Bottom = padding[2]
		inset.Left = padding[3]
	}

	return inset.Layout(gtx, w)
}
