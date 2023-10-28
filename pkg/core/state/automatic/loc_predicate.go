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

package automatic

import (
	"context"

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// Predicate for a loc

type LocPredicate func(context.Context, state.Loc) bool

type locs interface {
	// Get total possible number of locs
	GetLocCount(context.Context) int
	// Iterate all locs
	ForEachLoc(func(state.Loc))
}

// Construct a predicate that requires both predicates to be true
func (p LocPredicate) And(other LocPredicate) LocPredicate {
	return func(ctx context.Context, r state.Loc) bool {
		return p(ctx, r) && other(ctx, r)
	}
}

// Get all locs that match the given predicate
func (p LocPredicate) GetLocs(ctx context.Context, allRoutes locs) []state.Loc {
	result := make([]state.Loc, 0, allRoutes.GetLocCount(ctx))
	allRoutes.ForEachLoc(func(r state.Loc) {
		if p(ctx, r) {
			result = append(result, r)
		}
	})
	return result
}
