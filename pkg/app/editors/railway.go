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

package editors

import (
	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/app/settings"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// NewRailwayEditor constructs an editor for a Railway.
func NewRailwayEditor(railway model.Railway, etx EditorContext) Editor {
	editor := &railwayEditor{
		settings: settings.NewRailwaySettings(railway),
	}
	return editor
}

// railwayEditor implements an editor for a Railway.
type railwayEditor struct {
	settings settings.Settings
}

// Handle events and draw the editor
func (e *railwayEditor) Layout(gtx C, th *material.Theme) D {
	return e.settings.Layout(gtx, th)
}
