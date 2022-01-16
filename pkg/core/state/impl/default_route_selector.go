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

package impl

import (
	"context"
	"math/rand"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

type defaultRouteSelector struct{}

var defaultRouteSelectorInstance state.RouteSelector = &defaultRouteSelector{}

/// Select one of the given possible routes.
/// Returns null if no route should be taken.
/// <param name="possibleRoutes">A list of routes to choose from</param>
/// <param name="loc">The loc to choose for</param>
/// <param name="fromBlock">The block from which the next route will leave</param>
/// <param name="locDirection">The direction the loc is facing in the <see cref="fromBlock"/>.</param>
func (rs *defaultRouteSelector) SelectRoute(ctx context.Context, possibleRoutes []state.Route, loc state.Loc, fromBlock state.Block, locDirection model.BlockSide) state.Route {
	// TODO port from C#
	if len(possibleRoutes) == 0 {
		return nil
	}
	return possibleRoutes[rand.Intn(len(possibleRoutes))]
}

/// Called when the loc has entered the given to-block of the current route.
func (rs *defaultRouteSelector) BlockEntered(ctx context.Context, loc state.Loc, block state.Block) {
	// Do nothing
}

/// Called when the loc has reached the given to-block of the current route.
func (rs *defaultRouteSelector) BlockReached(ctx context.Context, loc state.Loc, block state.Block) {
	// Do nothing
}
