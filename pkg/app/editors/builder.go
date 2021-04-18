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

type moduleEntitySet interface {
	model.EntitySet
	GetModule() model.Module
}

type binkyNetLocalWorkerEntitySet interface {
	GetLocalWorker() model.BinkyNetLocalWorker
}

// BuildEditor constructs an editor the given selection.
func BuildEditor(selection interface{}, etx EditorContext, current Editor) Editor {
	switch selection := selection.(type) {
	case model.Loc:
		return newLocEditor(selection, etx)
	case model.ModuleRef:
		if module := selection.TryResolve(); module != nil {
			return buildModuleEditor(module, nil, etx, current)
		}
		return nil
	case model.Railway:
		return newRailwayEditor(selection, etx)
	case model.ModuleEntity:
		module := selection.GetModule()
		return buildModuleEditor(module, selection, etx, current)
	case moduleEntitySet:
		module := selection.GetModule()
		return buildModuleEditor(module, nil, etx, current)
	case model.BinkyNetLocalWorker:
		lw := selection
		return buildBinkyNetLocalWorkerEditor(lw, selection, etx, current)
	case binkyNetLocalWorkerEntitySet:
		lw := selection.GetLocalWorker()
		return buildBinkyNetLocalWorkerEditor(lw, selection, etx, current)
	default:
		return newGenericEditor(selection, createOnDelete(etx, selection), etx)
	}
}

// buildModuleEditor re-uses or constructs an editor the given selection.
func buildModuleEditor(module model.Module, selection model.ModuleEntity, etx EditorContext, current Editor) Editor {
	// Re-use existing editor (if possible)
	if modEditor, ok := current.(ModuleEditor); ok && modEditor.Module() == module {
		// Re-use module editor
		modEditor.OnSelect(selection)
		return modEditor
	}
	// Build new module editor
	modEditor := newModuleEditor(module, etx)
	modEditor.OnSelect(selection)
	return modEditor
}

// buildBinkyNetLocalWorkerEditor re-uses or constructs an editor the given selection.
func buildBinkyNetLocalWorkerEditor(lw model.BinkyNetLocalWorker, selection interface{}, etx EditorContext, current Editor) Editor {
	// Re-use existing local worker editor (if possible)
	if lwEditor, ok := current.(BinkyNetLocalWorkerEditor); ok && lwEditor.LocalWorker() == lw {
		// Re-use editor
		lwEditor.OnSelect(selection)
		return lwEditor
	}
	// Build new editor
	lwEditor := newBinkyNetLocalWorkerEditor(lw, etx)
	lwEditor.OnSelect(selection)
	return lwEditor
}
