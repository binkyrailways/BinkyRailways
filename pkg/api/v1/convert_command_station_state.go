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
	"sort"
	"strings"

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// FromState converts a state command station to an API command station
func (dst *CommandStationState) FromState(ctx context.Context, src state.CommandStation) error {
	dst.Model = &CommandStation{}
	if err := dst.Model.FromModel(ctx, src.GetModel(), src.GetModelRef()); err != nil {
		return err
	}
	src.ForEachHardwareModule(func(hm state.HardwareModule) {
		hmDst := &HardwareModule{}
		hmDst.FromState(ctx, hm)
		dst.HardwareModules = append(dst.HardwareModules, hmDst)
	})
	sort.Slice(dst.HardwareModules, func(i, j int) bool {
		return strings.Compare(dst.HardwareModules[i].GetId(), dst.HardwareModules[j].GetId()) < 0
	})
	dst.PowerActual = src.GetPower().GetActual(ctx)
	dst.PowerRequested = src.GetPower().GetRequested(ctx)
	return nil
}
