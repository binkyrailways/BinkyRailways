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

package refs

func NewBool(value bool) *bool {
	return &value
}

func NewInt(value int) *int {
	return &value
}

func NewString(value string) *string {
	return &value
}

func BoolValue(r *bool, defaultValue bool) bool {
	if r == nil {
		return defaultValue
	}
	return *r
}

func IntValue(r *int, defaultValue int) int {
	if r == nil {
		return defaultValue
	}
	return *r
}
func StringValue(r *string, defaultValue string) string {
	if r == nil {
		return defaultValue
	}
	return *r
}
