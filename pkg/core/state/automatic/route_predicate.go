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

// Predicate for a route

type RoutePredicate func(context.Context, state.Route) bool
type routes interface {
	// Get total possible number of routes
	GetRouteCount(context.Context) int
	// Iterate all routes
	ForEachRoute(func(state.Route))
}

// Construct a predicate that requires both predicates to be true
func (p RoutePredicate) And(other RoutePredicate) RoutePredicate {
	return func(ctx context.Context, r state.Route) bool {
		return p(ctx, r) && other(ctx, r)
	}
}

// Get all routes that match the given predicate
func (p RoutePredicate) GetRoutes(ctx context.Context, allRoutes routes) []state.Route {
	result := make([]state.Route, 0, allRoutes.GetRouteCount(ctx))
	allRoutes.ForEachRoute(func(r state.Route) {
		if p(ctx, r) {
			result = append(result, r)
		}
	})
	return result
}
