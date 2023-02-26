// Copyright 2023 Ewout Prangsma
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
	"fmt"

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// FromState converts a block state to an API type
func (dst *BlockStateState) FromState(ctx context.Context, src state.BlockState) error {
	switch src {
	case state.BlockStateFree:
		*dst = BlockStateState_FREE
	case state.BlockStateOccupied:
		*dst = BlockStateState_OCCUPIED
	case state.BlockStateOccupiedUnexpected:
		*dst = BlockStateState_OCCUPIEDUNEXPECTED
	case state.BlockStateDestination:
		*dst = BlockStateState_DESTINATION
	case state.BlockStateEntering:
		*dst = BlockStateState_ENTERING
	case state.BlockStateLocked:
		*dst = BlockStateState_LOCKED
	case state.BlockStateClosed:
		*dst = BlockStateState_CLOSED
	default:
		return fmt.Errorf("invalid block state: %d", src)
	}
	return nil
}
