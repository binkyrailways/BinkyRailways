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

package state

import "github.com/binkyrailways/BinkyRailways/pkg/core/model"

// RouteEventBehavior specifies the state of a single route event behavior.
type RouteEventBehavior interface {
	// Does this behavior apply to the given loc?
	AppliesTo(loc Loc) bool

	// How is the state of the route changed.
	GetStateBehavior() model.RouteStateBehavior

	// How is the speed of the occupying loc changed.
	GetSpeedBehavior() model.LocSpeedBehavior
}
