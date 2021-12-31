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

// FromState converts a loc direction to an API type
func (dst *LocDirection) FromState(ctx context.Context, src state.LocDirection) error {
	switch src {
	case state.LocDirectionForward:
		*dst = LocDirection_FORWARD
	case state.LocDirectionReverse:
		*dst = LocDirection_REVERSE
	}
	return nil
}

// ToState converts a loc direction from an API type
func (src LocDirection) ToState(ctx context.Context) (state.LocDirection, error) {
	switch src {
	case LocDirection_FORWARD:
		return state.LocDirectionForward, nil
	case LocDirection_REVERSE:
		return state.LocDirectionReverse, nil
	}
	return state.LocDirectionForward, InvalidArgument("Unknown LocDirection: %s", src)
}
