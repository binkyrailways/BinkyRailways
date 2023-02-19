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

package impl

import (
	"context"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

type criticalSectionRoutes struct {
	routes []state.Route
}

var _ state.CriticalSectionRoutes = &criticalSectionRoutes{}

// Build the set of critical routes from the given route.
func (csr *criticalSectionRoutes) Build(ctx context.Context, route state.Route, rw state.Railway) error {
	routes, err := buildCriticalSectionRoutes(ctx, route, rw)
	if err != nil {
		return nil
	}
	csr.routes = routes
	return nil
}

// Iterate all the routes
func (csr *criticalSectionRoutes) ForEach(cb func(state.Route)) {
	for _, r := range csr.routes {
		cb(r)
	}
}

// Are all routes not being used by any other loc than the given loc?
func (csr *criticalSectionRoutes) AllFree(ctx context.Context, allowedLoc state.Loc) bool {
	// Check routes
	for _, route := range csr.routes {
		if state.IsLocked(ctx, route) {
			return false
		}
	}
	// Check blocks in the routes
	for _, route := range csr.routes {
		toBlock := route.GetTo(ctx)
		loc := toBlock.GetLockedBy(ctx)
		if (loc != nil) && (loc != allowedLoc) && (loc.GetCurrentBlockEnterSide().GetActual(ctx) == route.GetToBlockSide(ctx)) {
			// If the loc occupying the blokc cannot change direction, we may not enter this critical section.
			if loc.GetChangeDirection(ctx) == model.ChangeDirectionAvoid {
				return false
			}
			// If changing direction in the blockis not allowed, we may not enter this critical section.
			if toBlock.GetChangeDirection(ctx) == model.ChangeDirectionAvoid {
				return false
			}
		}
	}
	return true

}
