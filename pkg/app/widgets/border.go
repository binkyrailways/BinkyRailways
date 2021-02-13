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
	"gioui.org/unit"
	"gioui.org/widget"
	"gioui.org/widget/material"
)

// WithBorder adds a border around the given widget.
func WithBorder(gtx C, th *material.Theme, w func(C) D) D {
	return widget.Border{
		Color:        th.Fg,
		CornerRadius: unit.Dp(5),
		Width:        unit.Dp(1),
	}.Layout(gtx, w)
}
