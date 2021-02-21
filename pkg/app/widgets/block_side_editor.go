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
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// BlockSideEditor is an editor for a BlockSide value
type BlockSideEditor struct {
	widget.Enum
}

// SetValue updates the editor to the current value
func (e *BlockSideEditor) SetValue(value model.BlockSide) {
	e.Enum.Value = string(value)
}

// GetValue returns the current value
func (e *BlockSideEditor) GetValue() (model.BlockSide, error) {
	return model.BlockSide(e.Enum.Value), nil
}

func (e *BlockSideEditor) Layout(gtx C, th *material.Theme) D {
	return layout.Flex{
		Axis: layout.Vertical,
	}.Layout(gtx,
		layout.Rigid(material.RadioButton(th, &e.Enum, string(model.BlockSideFront), "Front").Layout),
		layout.Rigid(material.RadioButton(th, &e.Enum, string(model.BlockSideBack), "Back").Layout),
	)
}