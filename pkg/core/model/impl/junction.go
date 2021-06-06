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
	"fmt"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// Junction adds implementation methods to model.Junction
type Junction interface {
	ModuleEntity
	model.Junction
}

type junction struct {
	positionedModuleEntity

	BlockID string `xml:"Block,omitempty"`
}

var _ model.Junction = &junction{}

// Initialize the junction after construction
func (j *junction) Initialize(w, h int) {
	j.positionedModuleEntity.Initialize(w, h)
}

// The block that this junction belongs to.
// When set, this junction is considered lock if the block is locked.
func (j *junction) GetBlock() model.Block {
	m := j.GetModule()
	if m == nil || j.BlockID == "" {
		return nil
	}
	b, _ := m.GetBlocks().Get(j.BlockID)
	return b
}
func (j *junction) SetBlock(value model.Block) error {
	id := ""
	var module model.Module
	if value != nil {
		id = value.GetID()
		module = value.GetModule()
	}
	if j.BlockID != id {
		if module != j.GetModule() {
			return fmt.Errorf("Invalid module")
		}
		j.BlockID = id
		j.OnModified()
	}
	return nil
}

// Ensure implementation implements Junction
func (j *junction) ImplementsJunction() {
	// Nothing here
}
