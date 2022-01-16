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

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// Choose a next route for a loc.
type RouteSelector interface {
	/// Select one of the given possible routes.
	/// Returns null if no route should be taken.
	/// <param name="possibleRoutes">A list of routes to choose from</param>
	/// <param name="loc">The loc to choose for</param>
	/// <param name="fromBlock">The block from which the next route will leave</param>
	/// <param name="locDirection">The direction the loc is facing in the <see cref="fromBlock"/>.</param>
	SelectRoute(ctx context.Context, possibleRoutes []Route, loc Loc, fromBlock Block, locDirection model.BlockSide) Route

	/// Called when the loc has entered the given to-block of the current route.
	BlockEntered(ctx context.Context, loc Loc, block Block)

	/// Called when the loc has reached the given to-block of the current route.
	BlockReached(ctx context.Context, loc Loc, block Block)
}
