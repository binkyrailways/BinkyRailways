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
	src.GetCrossingJunctions().ForEach(func(jws model.JunctionWithState) {
		jwsDst := &JunctionWithState{}
		jwsDst.FromModel(ctx, jws)
		dst.CrossingJunctions = append(dst.CrossingJunctions, jwsDst)
	})
	src.GetOutputs().ForEach(func(ows model.OutputWithState) {
		owsDst := &OutputWithState{}
		owsDst.FromModel(ctx, ows)
		dst.Outputs = append(dst.Outputs, owsDst)
	})
	src.GetEvents().ForEach(func(re model.RouteEvent) {
		reDst := &RouteEvent{}
		reDst.FromModel(ctx, re)
		dst.Events = append(dst.Events, reDst)
	})
	dst.Speed = int32(src.GetSpeed())
	dst.ChooseProbability = int32(src.GetChooseProbability())
	if !src.GetPermissions().IsEmpty() {
		perm, err := (&LocPredicate{}).FromModel(ctx, src.GetPermissions())
		if err != nil {
			return err
		}
		dst.Permissions = perm.GetStandard()
	} else {
		dst.Permissions = &LocStandardPredicate{}
	}
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
	if src.GetPermissions() != nil {
		src := LocPredicate{Standard: src.GetPermissions()}
		railway := dst.GetModule().GetPackage().GetRailway()
		lp, err := src.ToModel(ctx, railway)
		if err != nil {
			return err
		}
		if err := dst.SetPermissions(lp.(model.LocStandardPredicate)); err != nil {
			return err
		}
	}
	if len(src.GetCrossingJunctions()) != dst.GetCrossingJunctions().GetCount() {
		return InvalidArgument("Unexpected number of crossing junctions (got %d, expected %d)", len(src.GetCrossingJunctions()), dst.GetCrossingJunctions().GetCount())
	}
	if len(src.GetOutputs()) != dst.GetOutputs().GetCount() {
		return InvalidArgument("Unexpected number of outputs (got %d, expected %d)", len(src.GetOutputs()), dst.GetOutputs().GetCount())
	}
	if len(src.GetEvents()) != dst.GetEvents().GetCount() {
		return InvalidArgument("Unexpected number of events (got %d, expected %d)", len(src.GetEvents()), dst.GetEvents().GetCount())
	}
	multierr.AppendInto(&err, dst.SetDescription(src.GetDescription()))
	multierr.AppendInto(&err, dst.SetFrom(epFrom))
	multierr.AppendInto(&err, dst.SetFromBlockSide(bSideFrom))
	multierr.AppendInto(&err, dst.SetTo(epTo))
	multierr.AppendInto(&err, dst.SetToBlockSide(bSideTo))
	for _, x := range src.GetCrossingJunctions() {
		_, junctionID, err := SplitParentChildID(x.GetJunction().GetId())
		if err != nil {
			return err
		}
		dstJws, ok := dst.GetCrossingJunctions().Get(junctionID)
		if !ok {
			return InvalidArgument("Unknown junction ID: '%s'", junctionID)
		}
		multierr.AppendInto(&err, x.ToModel(ctx, dstJws))
	}
	for _, x := range src.GetOutputs() {
		_, outputID, err := SplitParentChildID(x.GetOutput().GetId())
		if err != nil {
			return err
		}
		dstOws, ok := dst.GetOutputs().Get(outputID)
		if !ok {
			return InvalidArgument("Unknown output ID: '%s'", outputID)
		}
		multierr.AppendInto(&err, x.ToModel(ctx, dstOws))
	}
	for _, x := range src.GetEvents() {
		_, sensorID, err := SplitParentChildID(x.GetSensor().GetId())
		if err != nil {
			return err
		}
		dstRe, ok := dst.GetEvents().Get(sensorID)
		if !ok {
			return InvalidArgument("Unknown sensor ID: '%s'", sensorID)
		}
		multierr.AppendInto(&err, x.ToModel(ctx, dstRe))
	}
	multierr.AppendInto(&err, dst.SetSpeed(int(src.GetSpeed())))
	multierr.AppendInto(&err, dst.SetChooseProbability(int(src.GetChooseProbability())))
	multierr.AppendInto(&err, dst.SetClosed(src.GetClosed()))
	multierr.AppendInto(&err, dst.SetMaxDuration(int(src.GetMaxDuration())))
	return nil
}
