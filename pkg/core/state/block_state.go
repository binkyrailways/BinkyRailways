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

package state

// BlockState describes the current state of a block.
type BlockState int

const (

	// BlockStateFree means the block is available, but not occupied, not claimed
	BlockStateFree BlockState = 0

	// BlockStateOccupied means the block is occupied by a loc, which is expected
	BlockStateOccupied BlockState = 1

	// BlockStateOccupiedUnexpected means a sensor of this block is active, which is unexpected
	BlockStateOccupiedUnexpected BlockState = 2

	// BlockStateDestination means the block is locked by a coming loc.
	// That loc is now taking a route that leads to this block.
	BlockStateDestination BlockState = 3

	// BlockStateEntering means the block is locked by a coming loc.
	// That loc is now entering this block.
	BlockStateEntering BlockState = 4

	// BlockStateLocked means the block is locked by a coming loc.
	// That loc's next route will lead to this block.
	BlockStateLocked BlockState = 5

	// BlockStateClosed means the block has been taken out of use
	BlockStateClosed BlockState = 6
)
