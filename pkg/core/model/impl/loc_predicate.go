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

import "github.com/binkyrailways/BinkyRailways/pkg/core/model"

// LocPredicate extends implementation methods to model.LocPredicate
type LocPredicate interface {
	ModuleEntity
	model.LocPredicate
}

type locPredicate struct {
	moduleEntity
}

//var _ model.LocPredicate = &locPredicate{}

// SetContainer links this entity to its parent
func (lp *locPredicate) SetContainer(value ModuleEntityContainer) {
	lp.moduleEntityContainer.SetContainer(value)
}
