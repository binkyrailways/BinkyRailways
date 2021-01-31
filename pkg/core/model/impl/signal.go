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

// Signal adds implementation methods to model.Signal
type Signal interface {
	ModuleEntity
	model.Signal
	TypedEntity
}

type signal struct {
	positionedModuleEntity
}

//var _ model.Signal = &signal{}

// Initialize the signal after construction
func (j *signal) Initialize() {
	j.positionedModuleEntity.Initialize(16, 8)
}
