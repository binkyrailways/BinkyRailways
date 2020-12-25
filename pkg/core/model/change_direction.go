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

package model

// ChangeDirection specifies if it is allowed / should be avoided to change direction in a block,
// or is it allowed / should is be avoided that a loc changes direction?
type ChangeDirection string

const (
	// ChangeDirectionAllow indicates that changing direction is allowed
	ChangeDirectionAllow ChangeDirection = "Allow"
	// ChangeDirectionAvoid indicates that changing direction should be avoided
	ChangeDirectionAvoid ChangeDirection = "Avoid"
)
