// Copyright 2024 Ewout Prangsma
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

// FromModel converts a BlockSignalType to an API type
func (dst *BlockSignalType) FromModel(ctx context.Context, src model.BlockSignalType) error {
	switch src {
	case model.BlockSignalTypeEntry:
		*dst = BlockSignalType_ENTRY
	case model.BlockSignalTypeExit:
		*dst = BlockSignalType_EXIT
	}
	return nil
}

// ToModel converts a BlockSignalType from an API type
func (src BlockSignalType) ToModel(ctx context.Context) (model.BlockSignalType, error) {
	switch src {
	case BlockSignalType_ENTRY:
		return model.BlockSignalTypeEntry, nil
	case BlockSignalType_EXIT:
		return model.BlockSignalTypeExit, nil
	}
	return model.BlockSignalTypeEntry, InvalidArgument("Unknown BlockSignalType: %s", src)
}
