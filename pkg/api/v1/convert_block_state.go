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
	context "context"

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// FromState converts a state block to an API block
func (dst *BlockState) FromState(ctx context.Context, src state.Block) error {
	dst.Model = &Block{}
	if err := dst.Model.FromModel(ctx, src.GetModel()); err != nil {
		return err
	}
	if loc := src.GetLockedBy(ctx); loc != nil {
		dst.LockedBy = &LocRef{Id: loc.GetID()}
	}
	if err := dst.State.FromState(ctx, src.GetState(ctx)); err != nil {
		return err
	}
	dst.ClosedActual = src.GetClosed().GetActual(ctx)
	dst.ClosedRequested = src.GetClosed().GetRequested(ctx)
	dst.IsDeadend = src.GetIsDeadEnd(ctx)
	dst.IsStation = src.GetIsStation(ctx)
	dst.HasWaitingLoc = src.GetHasWaitingLoc(ctx)
	return nil
}
