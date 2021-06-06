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
	"image/color"

	"gioui.org/layout"
	"gioui.org/x/component"
)

type SimpleIconActionWithTooltip struct {
	component.AppBarAction
	actionLayout func(gtx layout.Context, bg, fg color.NRGBA) layout.Dimensions
	area         *component.TipArea
	tooltip      component.Tooltip
}

func NewSimpleIconActionWithTooltip(action component.AppBarAction, area *component.TipArea, tooltip component.Tooltip) SimpleIconActionWithTooltip {
	result := SimpleIconActionWithTooltip{
		AppBarAction: action,
		actionLayout: action.Layout,
		area:         area,
		tooltip:      tooltip,
	}
	result.Layout = result.layout
	return result
}

func (c SimpleIconActionWithTooltip) layout(gtx layout.Context, bg, fg color.NRGBA) layout.Dimensions {
	return c.area.Layout(gtx, c.tooltip, func(gtx layout.Context) layout.Dimensions {
		return c.actionLayout(gtx, bg, fg)
	})
}
