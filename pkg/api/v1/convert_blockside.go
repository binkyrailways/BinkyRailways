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

package v1

import (
	context "context"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// FromModel converts a BlockSide to an API type
func (dst *BlockSide) FromModel(ctx context.Context, src model.BlockSide) error {
	switch src {
	case model.BlockSideFront:
		*dst = BlockSide_FRONT
	case model.BlockSideBack:
		*dst = BlockSide_BACK
	}
	return nil
}

// ToModel converts a BlockSide from an API type
func (src BlockSide) ToModel(ctx context.Context) (model.BlockSide, error) {
	switch src {
	case BlockSide_FRONT:
		return model.BlockSideFront, nil
	case BlockSide_BACK:
		return model.BlockSideBack, nil
	}
	return model.BlockSideFront, InvalidArgument("Unknown BlockSide: %s", src)
}
