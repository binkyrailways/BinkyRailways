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

// FromState converts a state loc to an API loc
func (dst *LocState) FromState(ctx context.Context, src state.Loc, httpHost string, httpSecure bool) error {
	dst.Model = &Loc{}
	if err := dst.Model.FromModel(ctx, src.GetModel(), httpHost, httpSecure); err != nil {
		return err
	}
	dst.ControlledAutomaticallyActual = src.GetControlledAutomatically().GetActual(ctx)
	dst.ControlledAutomaticallyRequested = src.GetControlledAutomatically().GetRequested(ctx)
	dst.CanBeControlledAutomatically = src.GetCanSetAutomaticControl(ctx)
	dst.AutomaticState.FromState(ctx, src.GetAutomaticState().GetActual(ctx))
	if x := src.GetCurrentRoute().GetActual(ctx); x != nil {
		dst.CurrentRoute = &RouteRef{
			Id: JoinParentChildID(x.GetRoute().GetModuleID(), x.GetRoute().GetID()),
		}
	}
	dst.WaitAfterCurrentRoute = src.GetWaitAfterCurrentRoute().GetActual(ctx)
	dst.IsCurrentRouteDurationExceeded = src.GetIsCurrentRouteDurationExceeded(ctx)
	if x := src.GetNextRoute().GetActual(ctx); x != nil {
		dst.NextRoute = &RouteRef{
			Id: JoinParentChildID(x.GetModuleID(), x.GetID()),
		}
	}
	if x := src.GetCurrentBlock().GetActual(ctx); x != nil {
		dst.CurrentBlock = &BlockRef{
			Id: JoinParentChildID(x.GetModuleID(), x.GetID()),
		}
	}
	dst.CurrentBlockEnterSide.FromModel(ctx, src.GetCurrentBlockEnterSide().GetActual(ctx))
	dst.SpeedActual = int32(src.GetSpeed().GetActual(ctx))
	dst.SpeedRequested = int32(src.GetSpeed().GetRequested(ctx))
	dst.SpeedText = src.GetSpeedText(ctx)
	dst.StateText = src.GetStateText(ctx)
	dst.SpeedInStepsActual = int32(src.GetSpeedInSteps().GetActual(ctx))
	dst.SpeedInStepsRequested = int32(src.GetSpeedInSteps().GetRequested(ctx))
	dst.DirectionActual.FromState(ctx, src.GetDirection().GetActual(ctx))
	dst.DirectionRequested.FromState(ctx, src.GetDirection().GetRequested(ctx))
	dst.IsReversing = src.GetReversing().GetActual(ctx)
	dst.F0Actual = src.GetF0().GetActual(ctx)
	dst.F0Requested = src.GetF0().GetRequested(ctx)
	dst.IsEnabled = src.GetEnabled()
	dst.HasBatteryLevel = src.HasBatteryLevel().GetActual(ctx)
	dst.BatteryLevel = int32(src.GetBatteryLevel().GetActual(ctx))
	return nil
}
