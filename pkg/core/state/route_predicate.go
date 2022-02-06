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
	"context"
)

// Predicate for a route

type RoutePredicate func(context.Context, Route) bool
type routes interface {
	// Get total possible number of routes
	GetRouteCount(context.Context) int
	// Iterate all routes
	ForEachRoute(func(Route))
}

// Construct a predicate that requires both predicates to be true
func (p RoutePredicate) And(other RoutePredicate) RoutePredicate {
	return func(ctx context.Context, r Route) bool {
		return p(ctx, r) && other(ctx, r)
	}
}

// Get all routes that match the given predicate
func (p RoutePredicate) GetRoutes(ctx context.Context, allRoutes routes) []Route {
	result := make([]Route, 0, allRoutes.GetRouteCount(ctx))
	allRoutes.ForEachRoute(func(r Route) {
		if p(ctx, r) {
			result = append(result, r)
		}
	})
	return result
}

// Is there at least one route that matches the given predicate?
func (p RoutePredicate) AnyRoutes(ctx context.Context, allRoutes routes) bool {
	result := false
	allRoutes.ForEachRoute(func(r Route) {
		if p(ctx, r) {
			result = true
		}
	})
	return result
}
