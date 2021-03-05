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
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// BuildEditor constructs an editor the given selection.
func BuildEditor(selection interface{}, etx EditorContext, current Editor) Editor {
	switch selection := selection.(type) {
	case model.BinkyNetLocalWorker:
		return newBinkyNetEditor(selection, etx)
	case model.BinkyNetDevice:
		return newBinkyNetEditor(selection, etx)
	case model.BinkyNetObject:
		return newBinkyNetEditor(selection, etx)
	case model.CommandStation:
		return newCommandStationEditor(selection, etx)
	case model.Loc:
		return newLocEditor(selection, etx)
	case model.Module:
		return newModuleEditor(selection, etx)
	case model.Railway:
		return newRailwayEditor(selection, etx)
	case model.ModuleEntity:
		// Re-use existing module editor (if possible)
		module := selection.GetModule()
		if modEditor, ok := current.(ModuleEditor); ok && modEditor.Module() == module {
			// Re-use module editor
			modEditor.OnSelect(selection)
			return modEditor
		}
		// Build new module editor
		modEditor := newModuleEditor(module, etx)
		modEditor.OnSelect(selection)
		return modEditor
	default:
		return nil
	}
}