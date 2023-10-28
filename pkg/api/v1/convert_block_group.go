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

// FromModel converts a model BlockGroup to an API BlockGroup
func (dst *BlockGroup) FromModel(ctx context.Context, src model.BlockGroup) error {
	dst.Id = JoinParentChildID(src.GetModule().GetID(), src.GetID())
	dst.Description = src.GetDescription()
	dst.ModuleId = src.GetModule().GetID()
	dst.MinimumLocsInGroup = int32(src.GetMinimumLocsInGroup())
	dst.MinimumLocsOnTrack = int32(src.GetMinimumLocsOnTrackForMinimumLocsInGroupStart())
	return nil
}

// ToModel converts an API BlockGroup to a model BlockGroup
func (src *BlockGroup) ToModel(ctx context.Context, dst model.BlockGroup) error {
	expectedID := JoinParentChildID(dst.GetModule().GetID(), dst.GetID())
	if src.GetId() != expectedID {
		return InvalidArgument("Unexpected block group ID: '%s'", src.GetId())
	}
	if err := dst.SetDescription(src.GetDescription()); err != nil {
		return err
	}
	if err := dst.SetMinimumLocsInGroup(int(src.GetMinimumLocsInGroup())); err != nil {
		return err
	}
	if err := dst.SetMinimumLocsOnTrackForMinimumLocsInGroupStart(int(src.GetMinimumLocsOnTrack())); err != nil {
		return err
	}
	return nil
}
