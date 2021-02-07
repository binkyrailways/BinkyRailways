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
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// Railway adds implementation functions to state.Railway.
type Railway interface {
	Entity
	state.Railway
	state.EventDispatcher

	// Try to resolve the given endpoint into a block state.
	ResolveEndPoint(model.EndPoint) (Block, error)
	// Select a command station that can best drive the given entity
	SelectCommandStation(model.AddressEntity) (CommandStation, error)
}
