// Copyright 2024 Ewout Prangsma
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

import "context"

// EntityTester automates the testing of changing entity state.
// Once enabled, it will change the state of selected entities
// on a regular basis.
type EntityTester interface {
	// Is the tester enabled?
	GetEnabled() BoolProperty

	// Include the given entity in the test.
	Include(context.Context, Entity)

	// Exclude the given entity from the test.
	Exclude(context.Context, Entity)

	// Is the given entity included in the test?
	IsIncluded(context.Context, Entity) bool
}
