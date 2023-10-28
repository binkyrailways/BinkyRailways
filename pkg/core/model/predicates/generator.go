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

// GeneratePredicate generates a string representation of the given
// predicate that can then be parsed by ParsePredicate.
func GeneratePredicate(p model.LocPredicate) string {
	if p == nil {
		return "all"
	}
	switch p := p.(type) {
	case model.LocEqualsPredicate:
		if l, err := p.GetLoc(); err == nil {
			return fmt.Sprintf(`"%s"`, l.GetDescription())
		}
		return fmt.Sprintf(`"%s"`, p.GetLocID())
	case model.LocGroupEqualsPredicate:
		if g := p.GetGroup(); g != nil {
			return fmt.Sprintf(`memberOf("%s")`, g.GetDescription())
		}
		return fmt.Sprintf(`memberOf("%s")`, p.GetGroupID())
	case model.LocCanChangeDirectionPredicate:
		return `canChangeDirection`
	case model.LocAndPredicate:
		return generatePredicates(p.GetPredicates(), "and", false)
	case model.LocOrPredicate:
		return generatePredicates(p.GetPredicates(), "or", false)
	case model.LocStandardPredicate:
		includes := p.GetIncludes().GetPredicates().GetCount()
		excludes := p.GetExcludes().GetPredicates().GetCount()
		if includes == 0 && excludes == 0 {
			return "all"
		}
		if includes == 0 {
			return "except(" + generatePredicates(p.GetExcludes().GetPredicates(), "or", true) + ")"
		}
		if excludes == 0 {
			return "only(" + generatePredicates(p.GetIncludes().GetPredicates(), "or", true) + ")"
		}
		return "only(" + generatePredicates(p.GetIncludes().GetPredicates(), "or", true) + ") " +
			"except(" + generatePredicates(p.GetExcludes().GetPredicates(), "or", true) + ")"
	default:
		panic(fmt.Sprintf("unknown type of predicate %T", p))
	}
}

// generatePredicates generates a string representation of the given
// predicate set.
func generatePredicates(p model.LocPredicateSet, op string, excludeBrackets bool) string {
	if p == nil {
		return "all"
	}
	list := make([]string, 0, p.GetCount())
	p.ForEach(func(lp model.LocPredicate) {
		list = append(list, GeneratePredicate(lp))
	})
	if len(list) == 1 {
		return list[0]
	}
	inner := strings.Join(list, " "+op+" ")
	if excludeBrackets {
		return inner
	}
	return "(" + inner + ")"
}
