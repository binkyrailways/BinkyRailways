// Copyright 2023 Ewout Prangsma
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

package util

// SliceExcept return a copy of the given slice that has all elements of
// the given slice, except those values where the given predicate returns true.
func SliceExcept[T any](slice []T, excludePredicate func(T) bool) []T {
	result := make([]T, 0, len(slice))
	for _, x := range slice {
		if excludePredicate(x) {
			result = append(result, x)
		}
	}
	return result
}
