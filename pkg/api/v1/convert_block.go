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

// FromModel converts a model block to an API block
func (dst *Block) FromModel(ctx context.Context, src model.Block) error {
	dst.Id = JoinParentChildID(src.GetModule().GetID(), src.GetID())
	dst.Description = src.GetDescription()
	dst.ModuleId = src.GetModule().GetID()
	dst.Position = &Position{}
	if err := dst.Position.FromModel(ctx, src); err != nil {
		return err
	}
	dst.WaitProbability = int32(src.GetWaitProbability())
	dst.MinimumWaitTime = int32(src.GetMinimumWaitTime())
	dst.MaximumWaitTime = int32(src.GetMaximumWaitTime())
	//	GetWaitPermissions() LocStandardPredicate
	dst.ReverseSides = src.GetReverseSides()
	dst.ChangeDirection.FromModel(ctx, src.GetChangeDirection())
	dst.ChangeDirectionReversingLocs = src.GetChangeDirectionReversingLocs()
	//	GetStationMode() StationMode
	dst.IsStation = src.GetIsStation()
	if x := src.GetBlockGroup(); x != nil {
		dst.BlockGroup = &BlockGroupRef{
			Id: x.GetID(),
		}
	}
	if !src.GetWaitPermissions().IsEmpty() {
		perm, err := (&LocPredicate{}).FromModel(ctx, src.GetWaitPermissions())
		if err != nil {
			return err
		}
		dst.WaitPermissions = perm.GetStandard()
	} else {
		dst.WaitPermissions = &LocStandardPredicate{}
	}

	return nil
}

// ToModel converts an API block to a model block
func (src *Block) ToModel(ctx context.Context, dst model.Block) error {
	expectedID := JoinParentChildID(dst.GetModule().GetID(), dst.GetID())
	if src.GetId() != expectedID {
		return InvalidArgument("Unexpected block ID: '%s'", src.GetId())
	}
	if err := dst.SetDescription(src.GetDescription()); err != nil {
		return err
	}
	if err := src.GetPosition().ToModel(ctx, dst); err != nil {
		return err
	}
	if err := dst.SetWaitProbability(int(src.GetWaitProbability())); err != nil {
		return err
	}
	if err := dst.SetMinimumWaitTime(int(src.GetMinimumWaitTime())); err != nil {
		return err
	}
	if err := dst.SetMaximumWaitTime(int(src.GetMaximumWaitTime())); err != nil {
		return err
	}
	if src.GetWaitPermissions() != nil {
		src := LocPredicate{Standard: src.GetWaitPermissions()}
		railway := dst.GetModule().GetPackage().GetRailway()
		lp, err := src.ToModel(ctx, railway)
		if err != nil {
			return err
		}
		if err := dst.SetWaitPermissions(lp.(model.LocStandardPredicate)); err != nil {
			return err
		}
	}
	//	GetWaitPermissions() LocStandardPredicate
	dst.SetReverseSides(src.GetReverseSides())
	cd, err := src.GetChangeDirection().ToModel(ctx)
	if err != nil {
		return err
	}
	if err := dst.SetChangeDirection(cd); err != nil {
		return err
	}
	if err := dst.SetChangeDirectionReversingLocs(src.GetChangeDirectionReversingLocs()); err != nil {
		return err
	}
	//	GetStationMode() StationMode
	var bg model.BlockGroup
	if id := src.GetBlockGroup().GetId(); id != "" {
		var ok bool
		bg, ok = dst.GetModule().GetBlockGroups().Get(id)
		if !ok {
			return InvalidArgument("Unknown block group '%s'", id)
		}
	}
	if err := dst.SetBlockGroup(bg); err != nil {
		return err
	}

	return nil
}
