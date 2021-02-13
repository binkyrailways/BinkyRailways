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

package railway

import (
	"gioui.org/layout"
	"gioui.org/unit"
	"gioui.org/widget"

	"github.com/binkyrailways/BinkyRailways/pkg/app/views"
	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

type runLocsView struct {
	vm         views.ViewManager
	entityList widgets.TreeView
	locView    *runLocView
}

// New constructs a new locs view
func newRunLocsView(vm views.ViewManager, railway state.Railway) *runLocsView {
	itemCache := widgets.EntityTreeViewItemCache{}
	v := &runLocsView{
		vm: vm,
		entityList: widgets.TreeView{
			RootItems: []widgets.TreeViewItem{
				&widgets.TreeViewGroup{
					Name: "Locs",
					Collection: func(level int) []widgets.TreeViewItem {
						var result widgets.TreeViewItems
						railway.ForEachLoc(func(x state.Loc) {
							result = append(result, itemCache.CreateItem(x, level))
						})
						result.Sort()
						return result
					},
				},
			},
		},
		locView: newRunLocView(vm),
	}
	v.entityList.OnSelect = v.onSelect
	return v
}

// Invalidate the UI
func (v *runLocsView) Invalidate() {
	v.vm.Invalidate()
}

// onSelect ensures that the given object is selected in the view
func (v *runLocsView) onSelect(selection interface{}) {
	switch selection := selection.(type) {
	case state.Loc:
		v.locView.Select(selection)
	default:
		v.locView.Select(nil)
	}
	v.vm.Invalidate()
}

// Handle events and draw the view
func (v *runLocsView) Layout(gtx layout.Context) layout.Dimensions {
	th := v.vm.GetTheme()

	vs := widgets.VerticalSplit(
		func(gtx C) D { return v.entityList.Layout(gtx, th) },
		func(gtx C) D {
			return widget.Border{
				Color:        th.Fg,
				CornerRadius: unit.Dp(5),
				Width:        unit.Dp(1),
			}.Layout(gtx, func(gtx C) D { return layout.UniformInset(unit.Dp(5)).Layout(gtx, v.locView.Layout) })
		},
	)
	vs.End.Rigid = true

	return vs.Layout(gtx)
}
