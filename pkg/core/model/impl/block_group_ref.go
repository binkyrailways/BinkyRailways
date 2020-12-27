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

// BlockGroupRef is an ID reference to a block.
type BlockGroupRef string

// Get the block references by this reference
func (bgr BlockGroupRef) Get(m Module) (model.BlockGroup, bool) {
	if bgr == "" || m == nil {
		return nil, false
	}
	return m.GetBlockGroups().Get(string(bgr))
}

// Set the reference
// Returns: changed, error
func (bgr *BlockGroupRef) Set(value model.BlockGroup, m Module, onModified func()) error {
	id := ""
	var vm model.Module
	if value != nil {
		id = value.GetID()
		vm = value.GetModule()
	}
	if id != string(*bgr) {
		if vm != m {
			return fmt.Errorf("Invalid module")
		}
		*bgr = BlockGroupRef(id)
		onModified()
		return nil
	}
	return nil
}
