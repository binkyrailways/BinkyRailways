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
	"go.uber.org/multierr"
)

// FromModel converts a model Route to an API Route
func (dst *Route) FromModel(ctx context.Context, src model.Route) error {
	dst.Id = JoinParentChildID(src.GetModule().GetID(), src.GetID())
	dst.Description = src.GetDescription()
	dst.ModuleId = src.GetModule().GetID()
	dst.From = &Endpoint{}
	dst.From.FromModel(ctx, src.GetFrom(), src.GetFromBlockSide())
	dst.To = &Endpoint{}
	dst.To.FromModel(ctx, src.GetTo(), src.GetToBlockSide())
	dst.Speed = int32(src.GetSpeed())
	dst.ChooseProbability = int32(src.GetChooseProbability())
	dst.Closed = src.GetClosed()
	dst.MaxDuration = int32(src.GetMaxDuration())
	return nil
}

// ToModel converts an API Route to a model Route
func (src *Route) ToModel(ctx context.Context, dst model.Route) error {
	expectedID := JoinParentChildID(dst.GetModule().GetID(), dst.GetID())
	if src.GetId() != expectedID {
		return InvalidArgument("Unexpected block ID: '%s'", src.GetId())
	}
	epFrom, bSideFrom, err := src.GetFrom().ToModel(ctx, dst.GetModule())
	if err != nil {
		return err
	}
	epTo, bSideTo, err := src.GetTo().ToModel(ctx, dst.GetModule())
	if err != nil {
		return err
	}
	multierr.AppendInto(&err, dst.SetDescription(src.GetDescription()))
	multierr.AppendInto(&err, dst.SetFrom(epFrom))
	multierr.AppendInto(&err, dst.SetFromBlockSide(bSideFrom))
	multierr.AppendInto(&err, dst.SetTo(epTo))
	multierr.AppendInto(&err, dst.SetToBlockSide(bSideTo))
	multierr.AppendInto(&err, dst.SetSpeed(int(src.GetSpeed())))
	multierr.AppendInto(&err, dst.SetChooseProbability(int(src.GetChooseProbability())))
	multierr.AppendInto(&err, dst.SetClosed(src.GetClosed()))
	multierr.AppendInto(&err, dst.SetMaxDuration(int(src.GetMaxDuration())))
	return nil
}
