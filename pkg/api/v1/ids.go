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

package v1

import (
	"strings"
)

// JoinParentChildID creates a single ID by joining the module ID and the entity ID
func JoinParentChildID(parentID, childID string) string {
	return parentID + "/" + childID
}

// SplitParentChildID split a single ID into a module ID and an entity ID.
// Returns: parentID, childID, error
func SplitParentChildID(id string) (string, string, error) {
	parts := strings.Split(id, "/")
	if len(parts) != 2 {
		return "", "", InvalidArgument("Invalid id: '%s'", id)
	}
	return parts[0], parts[1], nil
}
