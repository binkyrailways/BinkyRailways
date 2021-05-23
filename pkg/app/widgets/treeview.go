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
	"context"
	"sort"

	"gioui.org/io/key"
	"gioui.org/layout"
	"gioui.org/unit"
	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/core/util"
)

// TreeView is a UI component that shows a tree of items.
type TreeView struct {
	Exclusive    util.Exclusive
	RootItems    []TreeViewItem
	list         layout.List
	OnSelect     func(interface{})
	Selected     interface{}
	widgets      []TreeViewItem
	requestFocus bool
	focused      bool
}

// TreeViewItem must be implemented by items that are part of a TreeView.
type TreeViewItem interface {
	// Return a key used to sort the items
	SortKey() string
	// Return true if the given selection is contained in this item
	Contains(selection interface{}) bool
	// Select returns the selection that this item represents (can be nil)
	Select() interface{}
	// Process events, return an entity if it is clicked.
	ProcessEvents() interface{}
	// Layout the widget
	Layout(ctx context.Context, gtx C, th *material.Theme, selection interface{}, focused bool) D
	// Generate sub-widgets
	GenerateWidgets(ctx context.Context) TreeViewItems
}

// Expandable is implemented by widgets that can expand & collapse
type Expandable interface {
	Expand()
	Collapse()
}

// TreeViewItems is a list of TreeViewItem's
type TreeViewItems []TreeViewItem

// Sort by SortKey
func (l TreeViewItems) Sort() {
	sort.SliceStable(l, func(i, j int) bool {
		ii, ij := l[i], l[j]
		return ii.SortKey() < ij.SortKey()
	})
}

// onSelectWidget ensures the given selection is becoming the new selection
// and the widget with given index is visible
func (v *TreeView) onSelectWidget(index int, selection interface{}) {
	// Save selection
	v.Selected = selection
	if v.OnSelect != nil {
		v.OnSelect(selection)
	}
	// Scroll widget into view
	if index < v.list.Position.First {
		// We have to scroll up
		v.list.Position.First = index
	} else if index >= v.list.Position.First+v.list.Position.Count {
		// We have to scroll down
		v.list.Position.First = index - (v.list.Position.Count - 1)
	}
}

// SelectFirst selects the first entry in the list.
func (v *TreeView) SelectFirst() {
	for i, w := range v.widgets {
		if x := w.Select(); x != nil {
			v.onSelectWidget(i, x)
			break
		}
	}
}

// SelectLast selects the last entry in the list.
func (v *TreeView) SelectLast() {
	for i := len(v.widgets) - 1; i >= 0; i-- {
		w := v.widgets[i]
		if x := w.Select(); x != nil {
			v.onSelectWidget(i, x)
			break
		}
	}
}

// SelectNext selects the next entry in the list.
func (v *TreeView) SelectNext() {
	if v.Selected == nil {
		v.SelectFirst()
		return
	}
	foundSelection := false
	for i, w := range v.widgets {
		if foundSelection {
			if x := w.Select(); x != nil {
				v.onSelectWidget(i, x)
				break
			}
		} else if w.Contains(v.Selected) {
			foundSelection = true
		}
	}
}

// SelectPrevious selects the previous entry in the list.
func (v *TreeView) SelectPrevious() {
	if v.Selected == nil {
		v.SelectLast()
		return
	}
	foundSelection := false
	for i := len(v.widgets) - 1; i >= 0; i-- {
		w := v.widgets[i]
		if foundSelection {
			if x := w.Select(); x != nil {
				v.onSelectWidget(i, x)
				break
			}
		} else if w.Contains(v.Selected) {
			foundSelection = true
		}
	}
}

// ExpandSelection tries to expand the currently selected item.
func (v *TreeView) ExpandSelection() {
	if v.Selected == nil {
		return
	}
	for _, w := range v.widgets {
		if w.Contains(v.Selected) {
			if exp, ok := w.(Expandable); ok {
				exp.Expand()
			}
			return
		}
	}
}

// CollapseSelection tries to collapse the currently selected item.
func (v *TreeView) CollapseSelection() {
	if v.Selected == nil {
		return
	}
	for _, w := range v.widgets {
		if w.Contains(v.Selected) {
			if exp, ok := w.(Expandable); ok {
				exp.Collapse()
			}
			return
		}
	}
}

// Layout processes events and redraws the list.
func (v *TreeView) Layout(gtx C, th *material.Theme) D {
	if v.Exclusive == nil {
		return v.layout(context.Background(), gtx, th)
	}
	var result D
	v.Exclusive.Exclusive(context.Background(), func(ctx context.Context) error {
		result = v.layout(ctx, gtx, th)
		return nil
	})
	return result
}

// Layout processes events and redraws the list.
func (v *TreeView) layout(ctx context.Context, gtx C, th *material.Theme) D {

	for _, evt := range gtx.Events(v) {
		switch evt := evt.(type) {
		case key.FocusEvent:
			v.focused = evt.Focus
		case key.Event:
			if evt.State == key.Press {
				switch evt.Name {
				case key.NameUpArrow:
					v.SelectPrevious()
				case key.NameDownArrow:
					v.SelectNext()
				case key.NameRightArrow:
					v.ExpandSelection()
				case key.NameLeftArrow:
					v.CollapseSelection()
				}
			}
		}
	}

	// Process events of widgets
	var newSelection interface{}
	for _, w := range v.widgets {
		if x := w.ProcessEvents(); x != nil {
			if newSelection == nil {
				v.requestFocus = true
				newSelection = x
			}
		}
	}
	if newSelection != v.Selected && newSelection != nil {
		v.Selected = newSelection
		if v.OnSelect != nil {
			v.OnSelect(newSelection)
		}
	}

	// Rebuild widgets
	v.list.Axis = layout.Vertical
	selection := v.Selected
	widgets := v.widgets[:0]
	for _, rootItem := range v.RootItems {
		widgets = append(widgets, rootItem)
		widgets = append(widgets, rootItem.GenerateWidgets(ctx)...)
	}
	v.widgets = widgets
	// Register for input
	key.InputOp{Tag: v}.Add(gtx.Ops)
	if v.requestFocus {
		key.FocusOp{Tag: v}.Add(gtx.Ops)
		v.requestFocus = false
	}
	// Redraw widgets
	return v.list.Layout(gtx, len(widgets), func(gtx C, idx int) D {
		//pointer.PassOp{Pass: true}.Add(gtx.Ops)
		return widgets[idx].Layout(ctx, gtx, th, selection, v.focused)
	})
}

// LayoutWithLevel renders the given widget with given treeview level.
func LayoutWithLevel(gtx C, level int, widget func(C) D) D {
	if level == 0 {
		return widget(gtx)
	}
	return layout.Inset{Left: unit.Dp(float32(level * 10))}.Layout(gtx, widget)
}
