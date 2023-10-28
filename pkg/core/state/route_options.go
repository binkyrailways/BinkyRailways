// Copyright 2022 Ewout Prangsma
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

import (
	"strings"
)

type RouteOptions []RouteOption

func (ros RouteOptions) String() string {
	strs := make([]string, len(ros))
	for i, ro := range ros {
		strs[i] = ro.String()
	}
	return strings.Join(strs, ", ")
}

// Equals returns true if the given route options have the same values.
func (ros RouteOptions) Equals(other RouteOptions) bool {
	if len(ros) != len(other) {
		return false
	}
	for i, ro := range ros {
		if !ro.Equals(other[i]) {
			return false
		}
	}
	return true
}
