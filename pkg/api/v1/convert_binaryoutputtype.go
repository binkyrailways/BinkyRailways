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

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// FromModel converts a binary output type to an API type
func (dst *BinaryOutputType) FromModel(ctx context.Context, src model.BinaryOutputType) error {
	switch src {
	case model.BinaryOutputTypeDefault:
		*dst = BinaryOutputType_BOT_DEFAULT
	case model.BinaryOutputTypeTrackInverter:
		*dst = BinaryOutputType_BOT_TRACKINVERTER
	}
	return nil
}

// ToModel converts a binary output type from an API type
func (src BinaryOutputType) ToModel(ctx context.Context) (model.BinaryOutputType, error) {
	switch src {
	case BinaryOutputType_BOT_DEFAULT:
		return model.BinaryOutputTypeDefault, nil
	case BinaryOutputType_BOT_TRACKINVERTER:
		return model.BinaryOutputTypeTrackInverter, nil
	}
	return model.BinaryOutputTypeDefault, InvalidArgument("Unknown BinaryOutputType: %s", src)
}
