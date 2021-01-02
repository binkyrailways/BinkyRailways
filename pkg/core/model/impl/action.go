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

// Action extends implementation methods to model.Action
type Action interface {
	model.Action

	// GetContainer returns the container reference of this action.
	GetContainer() ActionTrigger
	// SetContainer initialize the container reference of this action.
	SetContainer(value ActionTrigger)
}

type action struct {
	container ActionTrigger
}

// GetContainer returns the container reference of this action.
func (a *action) GetContainer() ActionTrigger {
	return a.container
}

// SetContainer initialize the container reference of this action.
func (a *action) SetContainer(value ActionTrigger) {
	a.container = value
}

// Invoke when anything has changed
func (a *action) OnModified() {
	if a.container != nil {
		a.container.OnModified()
	}
}
