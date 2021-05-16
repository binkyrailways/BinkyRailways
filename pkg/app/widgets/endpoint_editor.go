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
	"fmt"
	"strings"

	"gioui.org/widget"
	"gioui.org/widget/material"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

const (
	blockIDPrefix = "b:"
	edgeIDPrefix  = "e:"
)

// EndpointEditor is an editor for a EndPoint value
type EndpointEditor struct {
	editor *SimpleSelect
	enum   widget.Enum
	module model.Module
}

// Initialize the editor
func (e *EndpointEditor) Initialize(module model.Module, value model.EndPoint) {
	e.module = module
	e.SetValue(value)
	lvs := make([]LabeledValue, 0, module.GetBlocks().GetCount()+module.GetEdges().GetCount())
	module.GetBlocks().ForEach(func(x model.Block) {
		lvs = append(lvs, LV(blockIDPrefix+x.GetID(), x.GetDescription()))
	})
	module.GetEdges().ForEach(func(x model.Edge) {
		lvs = append(lvs, LV(edgeIDPrefix+x.GetID(), x.GetDescription()))
	})
	e.editor = NewSimpleSelect(&e.enum, lvs...)
}

// SetValue updates the editor to the current value
func (e *EndpointEditor) SetValue(value model.EndPoint) {
	if x, ok := value.(model.Block); ok {
		e.enum.Value = blockIDPrefix + x.GetID()
	} else if x, ok := value.(model.Edge); ok {
		e.enum.Value = edgeIDPrefix + x.GetID()
	} else {
		e.enum.Value = ""
	}
}

// GetValue returns the current value
func (e *EndpointEditor) GetValue(module model.Module) (model.EndPoint, error) {
	id := e.enum.Value
	if id == "" {
		return nil, nil
	}
	if e.module == nil {
		return nil, fmt.Errorf("Module not set")
	}
	if strings.HasPrefix(id, blockIDPrefix) {
		if b, ok := e.module.GetBlocks().Get(id[len(blockIDPrefix):]); ok {
			return b, nil
		}
	} else if strings.HasPrefix(id, edgeIDPrefix) {
		if b, ok := e.module.GetEdges().Get(id[len(edgeIDPrefix):]); ok {
			return b, nil
		}
	}
	return nil, fmt.Errorf("Unknown endpoint with id '%s'", id)
}

func (e *EndpointEditor) Layout(gtx C, th *material.Theme) D {
	return e.editor.Layout(gtx, th)
}
