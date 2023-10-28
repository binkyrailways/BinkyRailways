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

// RailwayEntityContainer is implemented by containers of RailwayEntities.
type RailwayEntityContainer interface {
	// Return the containing railway
	GetRailway() model.Railway
	// Invoke when anything has changed
	OnModified()
}

type railwayEntityContainer struct {
	container RailwayEntityContainer
}

var (
	_ RailwayEntityContainer = &railwayEntityContainer{}
)

// Gets the containing railway
func (me *railwayEntityContainer) GetRailway() model.Railway {
	if me.container == nil {
		return nil
	}
	return me.container.GetRailway()
}

// Invoke when anything has changed
func (me *railwayEntityContainer) OnModified() {
	if me.container != nil {
		me.container.OnModified()
	}
}

// SetContainer links this entity to its parent
func (me *railwayEntityContainer) SetContainer(value RailwayEntityContainer) {
	me.container = value
}
