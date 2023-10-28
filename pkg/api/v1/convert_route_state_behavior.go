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

// FromModel converts a RouteStateBehavior to an API type
func (dst *RouteStateBehavior) FromModel(ctx context.Context, src model.RouteStateBehavior) error {
	switch src {
	case model.RouteStateBehaviorNoChange:
		*dst = RouteStateBehavior_RSB_NOCHANGE
	case model.RouteStateBehaviorEnter:
		*dst = RouteStateBehavior_RSB_ENTER
	case model.RouteStateBehaviorReached:
		*dst = RouteStateBehavior_RSB_REACHED
	}
	return nil
}

// ToModel converts a BlockSide from an API type
func (src RouteStateBehavior) ToModel(ctx context.Context) (model.RouteStateBehavior, error) {
	switch src {
	case RouteStateBehavior_RSB_NOCHANGE:
		return model.RouteStateBehaviorNoChange, nil
	case RouteStateBehavior_RSB_ENTER:
		return model.RouteStateBehaviorEnter, nil
	case RouteStateBehavior_RSB_REACHED:
		return model.RouteStateBehaviorReached, nil
	}
	return model.RouteStateBehaviorNoChange, InvalidArgument("Unknown RouteStateBehavior: %s", src)
}
