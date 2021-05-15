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
	"gioui.org/widget"
	"gioui.org/widget/material"
)

// NewCheckboxList creates a list of checkboxes.
func NewCheckboxList(getChecked func(value string) bool, setChecked func(value string, checked bool), value ...LabeledValue) *CheckboxList {
	cbl := &CheckboxList{
		getChecked: getChecked,
		setChecked: setChecked,
		values:     value,
		checkboxes: make([]widget.Bool, len(value)),
		list: layout.List{
			Axis:      layout.Vertical,
			Alignment: layout.Start,
		},
	}
	for i, lv := range value {
		cbl.checkboxes[i].Value = getChecked(lv.Value)
	}
	return cbl
}

// CheckboxList is a list of checkboxes
type CheckboxList struct {
	checkboxes []widget.Bool
	values     []LabeledValue
	getChecked func(value string) bool
	setChecked func(value string, checked bool)
	list       layout.List
}

// Layout renders the list
func (cbl *CheckboxList) Layout(gtx C, th *material.Theme) D {
	// Update values
	for i, cb := range cbl.checkboxes {
		v := cbl.values[i].Value
		cbl.setChecked(v, cb.Value)
	}

	return cbl.list.Layout(gtx, len(cbl.values), func(gtx layout.Context, index int) layout.Dimensions {
		lbl := cbl.values[index].Label
		return material.CheckBox(th, &cbl.checkboxes[index], lbl).Layout(gtx)
	})
}
