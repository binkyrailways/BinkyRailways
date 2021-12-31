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

// FromModel converts a switch direction to an API type
func (dst *SwitchDirection) FromModel(ctx context.Context, src model.SwitchDirection) error {
	switch src {
	case model.SwitchDirectionStraight:
		*dst = SwitchDirection_STRAIGHT
	case model.SwitchDirectionOff:
		*dst = SwitchDirection_OFF
	}
	return nil
}

// ToModel converts a switch direction from an API type
func (src SwitchDirection) ToModel(ctx context.Context) (model.SwitchDirection, error) {
	switch src {
	case SwitchDirection_STRAIGHT:
		return model.SwitchDirectionStraight, nil
	case SwitchDirection_OFF:
		return model.SwitchDirectionOff, nil
	}
	return model.SwitchDirectionStraight, InvalidArgument("Unknown SwitchDirection: %s", src)
}
