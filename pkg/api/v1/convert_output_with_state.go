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

// FromModel converts a model OutputWithState to an API OutputWithState
func (dst *OutputWithState) FromModel(ctx context.Context, src model.OutputWithState) error {
	id := JoinParentChildID(src.GetOutput().GetModule().GetID(), src.GetOutput().GetID())
	dst.Output = &OutputRef{
		Id: id,
	}
	if sw, ok := src.(model.BinaryOutputWithState); ok {
		dst.BinaryOutputState = &BinaryOutputWithState{}
		if err := dst.BinaryOutputState.FromModel(ctx, sw); err != nil {
			return err
		}
	}
	return nil
}

// ToModel converts an API OutputWithState to a model OutputWithState
func (src *OutputWithState) ToModel(ctx context.Context, dst model.OutputWithState) error {
	expectedID := JoinParentChildID(dst.GetOutput().GetModule().GetID(), dst.GetOutput().GetID())
	if src.GetOutput().GetId() != expectedID {
		return InvalidArgument("Unexpected junction ID: '%s'", src.GetOutput().GetId())
	}
	if sw, ok := dst.(model.BinaryOutputWithState); ok {
		swSrc := src.GetBinaryOutputState()
		if swSrc == nil {
			return InvalidArgument("Expected switch")
		}
		if err := swSrc.ToModel(ctx, sw); err != nil {
			return err
		}
	}
	return nil
}
