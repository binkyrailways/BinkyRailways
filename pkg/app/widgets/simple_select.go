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
	"gioui.org/io/key"
	"gioui.org/op/clip"
	"gioui.org/op/paint"
	"gioui.org/widget"
	"gioui.org/widget/material"
)

// LV creates a LabeledValue.
// If the label argument is set, it can only have 1 element.
// If not set, the value is used as label
func LV(value string, label ...string) LabeledValue {
	if len(label) >= 1 {
		return LabeledValue{
			Value: value,
			Label: label[0],
		}
	}
	return LabeledValue{
		Value: value,
		Label: value,
	}
}

// LabeledValue defines a label with a value
type LabeledValue struct {
	Value string
	Label string
}

// NewSimpleSelect creates a new simple select.
func NewSimpleSelect(enum *widget.Enum, value ...LabeledValue) *SimpleSelect {
	return &SimpleSelect{
		enum:   enum,
		values: value,
	}
}

// SimpleSelect shows a poor mans select widget
type SimpleSelect struct {
	clickable    widget.Clickable
	enum         *widget.Enum
	values       []LabeledValue
	requestFocus bool
	focused      bool
}

// SetValues changes the possible values.
// If the current value in the enum is no longer possible,
// it is set to an empty string.
func (ssel *SimpleSelect) SetValues(value ...LabeledValue) {
	ssel.values = value
	current := ssel.enum.Value
	if current != "" {
		for _, lv := range value {
			if lv.Value == current {
				return
			}
		}
		// Current value no longer possible
		ssel.enum.Value = ""
	}
}

// Layout renders the select
func (ssel *SimpleSelect) Layout(gtx C, th *material.Theme) D {
	// Handle events
	if ssel.clickable.Clicked() {
		ssel.requestFocus = true
	}
	for _, evt := range gtx.Events(ssel) {
		switch evt := evt.(type) {
		case key.FocusEvent:
			ssel.focused = evt.Focus
		case key.Event:
			if evt.State == key.Press {
				switch evt.Name {
				case key.NameUpArrow:
					ssel.selectPrevious()
				case key.NameDownArrow:
					ssel.selectNext()
				}
			}
		}
	}

	// Request focus (if needed)
	key.InputOp{Tag: ssel}.Add(gtx.Ops)
	if ssel.requestFocus {
		key.FocusOp{
			Tag: ssel,
		}.Add(gtx.Ops)
		ssel.requestFocus = false
	}

	// Draw the current value
	txt, _ := ssel.getCurrentLabel()
	focused := ssel.focused

	lb := material.Label(th, th.TextSize, txt)
	return material.Clickable(gtx, &ssel.clickable, func(gtx C) D {
		if focused {
			fg := th.ContrastFg
			bg := th.ContrastBg
			lb.Color = fg
			clip.Rect{
				Max: gtx.Constraints.Max,
			}.Push(gtx.Ops).Pop()
			paint.Fill(gtx.Ops, bg)
		}
		return lb.Layout(gtx)
	})
}

// Gets the label for the selected value.
// Returns: label, hasSelection
func (ssel *SimpleSelect) getCurrentLabel() (string, bool) {
	value := ssel.enum.Value
	for _, lv := range ssel.values {
		if lv.Value == value {
			return lv.Label, true
		}
	}
	return "...", false
}

// Select the previous value
func (ssel *SimpleSelect) selectPrevious() {
	value := ssel.enum.Value
	for idx, lv := range ssel.values {
		if lv.Value == value {
			if idx > 0 {
				ssel.enum.Value = ssel.values[idx-1].Value
			}
			return
		}
	}
	// No current selection, select last value
	if len(ssel.values) > 0 {
		ssel.enum.Value = ssel.values[len(ssel.values)-1].Value
	}
}

// Select the next value
func (ssel *SimpleSelect) selectNext() {
	value := ssel.enum.Value
	for idx, lv := range ssel.values {
		if lv.Value == value {
			if idx+1 < len(ssel.values) {
				ssel.enum.Value = ssel.values[idx+1].Value
			}
			return
		}
	}
	// No current selection, select first value
	if len(ssel.values) > 0 {
		ssel.enum.Value = ssel.values[0].Value
	}
}
