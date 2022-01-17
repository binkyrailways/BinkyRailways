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

// FromState converts a state railway to an API railway
func (dst *RailwayState) FromState(ctx context.Context, src state.Railway) error {
	if src == nil {
		dst.IsRunModeEnabled = false
		dst.IsVirtualModeEnabled = false
		return nil
	}
	dst.IsRunModeEnabled = true
	dst.IsVirtualModeEnabled = src.GetVirtualMode().GetEnabled()
	dst.Model = &Railway{}
	if err := dst.Model.FromModel(ctx, src.GetModel()); err != nil {
		return err
	}
	dst.PowerActual = src.GetPower().GetActual(ctx)
	dst.PowerRequested = src.GetPower().GetRequested(ctx)
	dst.AutomaticControlActual = src.GetAutomaticLocController().GetEnabled().GetActual(ctx)
	dst.AutomaticControlRequested = src.GetAutomaticLocController().GetEnabled().GetRequested(ctx)

	src.ForEachBlock(func(b state.Block) {
		r := &BlockRef{Id: JoinParentChildID(b.GetModuleID(), b.GetID())}
		dst.Blocks = append(dst.Blocks, r)
	})
	src.ForEachBlockGroup(func(b state.BlockGroup) {
		r := &BlockGroupRef{Id: JoinParentChildID(b.GetModel().GetModule().GetID(), b.GetModel().GetID())}
		dst.BlockGroups = append(dst.BlockGroups, r)
	})
	src.ForEachCommandStation(func(b state.CommandStation) {
		r := &CommandStationRef{Id: b.GetID()}
		dst.CommandStations = append(dst.CommandStations, r)
	})
	src.ForEachJunction(func(b state.Junction) {
		r := &JunctionRef{Id: JoinParentChildID(b.GetModuleID(), b.GetID())}
		dst.Junctions = append(dst.Junctions, r)
	})
	src.ForEachLoc(func(b state.Loc) {
		r := &LocRef{Id: b.GetID()}
		dst.Locs = append(dst.Locs, r)
	})
	src.ForEachOutput(func(b state.Output) {
		r := &OutputRef{Id: JoinParentChildID(b.GetModuleID(), b.GetID())}
		dst.Outputs = append(dst.Outputs, r)
	})
	src.ForEachRoute(func(b state.Route) {
		r := &RouteRef{Id: JoinParentChildID(b.GetModuleID(), b.GetID())}
		dst.Routes = append(dst.Routes, r)
	})
	src.ForEachSensor(func(b state.Sensor) {
		r := &SensorRef{Id: JoinParentChildID(b.GetModuleID(), b.GetID())}
		dst.Sensors = append(dst.Sensors, r)
	})
	src.ForEachSignal(func(b state.Signal) {
		r := &SignalRef{Id: JoinParentChildID(b.GetModuleID(), b.GetID())}
		dst.Signals = append(dst.Signals, r)
	})

	return nil
}
