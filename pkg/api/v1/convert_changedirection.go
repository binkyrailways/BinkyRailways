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

// FromModel converts a change direction to an API type
func (dst *ChangeDirection) FromModel(ctx context.Context, src model.ChangeDirection) error {
	switch src {
	case model.ChangeDirectionAllow:
		*dst = ChangeDirection_ALLOW
	case model.ChangeDirectionAvoid:
		*dst = ChangeDirection_AVOID
	}
	return nil
}

// ToModel converts a change direction from an API type
func (src ChangeDirection) ToModel(ctx context.Context) (model.ChangeDirection, error) {
	switch src {
	case ChangeDirection_ALLOW:
		return model.ChangeDirectionAllow, nil
	case ChangeDirection_AVOID:
		return model.ChangeDirectionAvoid, nil
	}
	return model.ChangeDirectionAllow, InvalidArgument("Unknown ChangeDirection: %s", src)
}
