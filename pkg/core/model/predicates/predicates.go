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

package predicates

import (
	"fmt"
	"strings"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// ParsePredicate parses a given predicate into a LocPredicate.
// Returns nil if predicate is empty.
func ParsePredicate(predicate string, rw model.Railway) (model.LocPredicate, error) {
	predicate = strings.TrimSpace(predicate)
	if predicate == "" {
		return nil, nil
	}
	result, err := ParseReader("", strings.NewReader(predicate), GlobalStore("railway", rw))
	if err != nil {
		return nil, err
	}
	if lp, ok := result.(model.LocPredicate); ok {
		return lp, nil
	}
	return nil, fmt.Errorf("invalid result type %T", result)
}
