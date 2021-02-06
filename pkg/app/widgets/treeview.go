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
	"gioui.org/widget/material"
)

// TreeView is a UI component that shows a tree of items.
type TreeView struct {
	RootItems []TreeViewItem
	list      layout.List
	OnSelect  func(interface{})
	Selected  interface{}
	widgets   []TreeViewItem
}

// TreeViewItem must be implemented by items that are part of a TreeView.
type TreeViewItem interface {
	// Process events, return an entity if it is clicked.
	ProcessEvents() interface{}
	// Layout the widget
	Layout(gtx C, th *material.Theme, selection interface{}) D
	// Generate sub-widgets
	GenerateWidgets(level int) []TreeViewItem
}

// Layout processes events and redraws the list.
func (v *TreeView) Layout(gtx C, th *material.Theme) D {
	// Process events of widgets
	var newSelection interface{}
	for _, w := range v.widgets {
		if x := w.ProcessEvents(); x != nil {
			if newSelection == nil {
				newSelection = x
			}
		}
	}
	if newSelection != v.Selected && newSelection != nil {
		v.Selected = newSelection
		v.OnSelect(newSelection)
	}

	// Rebuild widgets
	v.list.Axis = layout.Vertical
	selection := v.Selected
	widgets := v.widgets[:0]
	for _, rootItem := range v.RootItems {
		widgets = append(widgets, rootItem)
		widgets = append(widgets, rootItem.GenerateWidgets(0)...)
	}
	v.widgets = widgets
	// Redraw widgets
	return v.list.Layout(gtx, len(widgets), func(gtx C, idx int) D {
		//pointer.PassOp{Pass: true}.Add(gtx.Ops)
		return widgets[idx].Layout(gtx, th, selection)
	})
}

// LayoutWithLevel renders the given widget with given treeview level.
func LayoutWithLevel(gtx C, level int, widget func(C) D) D {
	if level == 0 {
		return widget(gtx)
	}
	return layout.Inset{Left: unit.Dp(float32(level * 20))}.Layout(gtx, widget)
}
