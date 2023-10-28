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

package impl

import (
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// ModuleAction extends implementation methods to model.ModuleAction
type ModuleAction interface {
	model.ModuleAction
	ModuleEntity
}

type moduleAction struct {
	action
	moduleEntity
}

// GetContainer returns the container reference of this action.
func (a *moduleAction) GetModule() model.Module {
	if c := a.GetContainer(); c != nil {
		return c.GetModule()
	}
	return nil
}

// GetContainer returns the container reference of this action.
func (a *moduleAction) GetContainer() ActionTrigger {
	return a.action.GetContainer()
}

// SetContainer initialize the container reference of this action.
func (a *moduleAction) SetContainer(value ActionTrigger) {
	a.action.SetContainer(value)
	a.moduleEntity.SetContainer(a)
}

// Invoke when anything has changed
func (a *moduleAction) OnModified() {
	a.action.OnModified()
	a.moduleEntity.OnModified()
}
