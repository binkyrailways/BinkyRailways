// Copyright 2020 Ewout Prangsma
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

package impl

import (
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// ModuleEntityContainer is implemented by containers of ModuleEntities.
type ModuleEntityContainer interface {
	// Return the containing module
	GetModule() model.Module
	// Invoke when anything has changed
	OnModified()
}

type moduleEntityContainer struct {
	container ModuleEntityContainer
}

var (
	_ ModuleEntityContainer = &moduleEntityContainer{}
)

// Gets the containing module
func (me *moduleEntityContainer) GetModule() model.Module {
	if me.container == nil {
		return nil
	}
	return me.container.GetModule()
}

// Gets the containing railway
func (me *moduleEntityContainer) GetRailway() model.Railway {
	if m := me.GetModule(); m != nil {
		if pkg := m.GetPackage(); pkg != nil {
			return pkg.GetRailway()
		}
	}
	return nil
}

// Invoke when anything has changed
func (me *moduleEntityContainer) OnModified() {
	if me.container != nil {
		me.container.OnModified()
	}
}

// SetContainer links this entity to its parent
func (me *moduleEntityContainer) SetContainer(value ModuleEntityContainer) {
	me.container = value
}
