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

// Entity specifies the state of a single entity.
type Entity interface {
	// Unique ID of the underlying entity
	GetID() string

	// Description of the underlying entity
	GetDescription() string

	// Gets the railway state this object is a part of.
	GetRailway() Railway

	// Is this entity fully resolved such that is can be used in the live railway?
	GetIsReadyForUse() bool
}
