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

// FromModel converts a model JunctionWithState to an API JunctionWithState
func (dst *JunctionWithState) FromModel(ctx context.Context, src model.JunctionWithState) error {
	id := JoinParentChildID(src.GetJunction().GetModule().GetID(), src.GetJunction().GetID())
	dst.Junction = &JunctionRef{
		Id: id,
	}
	if sw, ok := src.(model.SwitchWithState); ok {
		dst.SwitchState = &SwitchWithState{}
		if err := dst.SwitchState.FromModel(ctx, sw); err != nil {
			return err
		}
	}
	return nil
}

// ToModel converts an API JunctionWithState to a model JunctionWithState
func (src *JunctionWithState) ToModel(ctx context.Context, dst model.JunctionWithState) error {
	expectedID := JoinParentChildID(dst.GetJunction().GetModule().GetID(), dst.GetJunction().GetID())
	if src.GetJunction().GetId() != expectedID {
		return InvalidArgument("Unexpected junction ID: '%s'", src.GetJunction().GetId())
	}
	if sw, ok := dst.(model.SwitchWithState); ok {
		swSrc := src.GetSwitchState()
		if swSrc == nil {
			return InvalidArgument("Expected switch")
		}
		if err := swSrc.ToModel(ctx, sw); err != nil {
			return err
		}
	}
	return nil
}
