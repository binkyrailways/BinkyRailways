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

// FromModel converts a LocSpeedBehavior to an API type
func (dst *LocSpeedBehavior) FromModel(ctx context.Context, src model.LocSpeedBehavior) error {
	switch src {
	case model.LocSpeedBehaviorDefault:
		*dst = LocSpeedBehavior_LSB_DEFAULT
	case model.LocSpeedBehaviorNoChange:
		*dst = LocSpeedBehavior_LSB_NOCHANGE
	case model.LocSpeedBehaviorMedium:
		*dst = LocSpeedBehavior_LSB_MEDIUM
	case model.LocSpeedBehaviorMinimum:
		*dst = LocSpeedBehavior_LSB_MINIMUM
	case model.LocSpeedBehaviorMaximum:
		*dst = LocSpeedBehavior_LSB_MAXIMUM
	}
	return nil
}

// ToModel converts a LocSpeedBehavior from an API type
func (src LocSpeedBehavior) ToModel(ctx context.Context) (model.LocSpeedBehavior, error) {
	switch src {
	case LocSpeedBehavior_LSB_DEFAULT:
		return model.LocSpeedBehaviorDefault, nil
	case LocSpeedBehavior_LSB_NOCHANGE:
		return model.LocSpeedBehaviorNoChange, nil
	case LocSpeedBehavior_LSB_MEDIUM:
		return model.LocSpeedBehaviorMedium, nil
	case LocSpeedBehavior_LSB_MINIMUM:
		return model.LocSpeedBehaviorMinimum, nil
	case LocSpeedBehavior_LSB_MAXIMUM:
		return model.LocSpeedBehaviorMaximum, nil
	}
	return model.LocSpeedBehaviorDefault, InvalidArgument("Unknown LocSpeedBehavior: %s", src)
}
