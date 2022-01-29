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
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// FromState converts a state command station to an API command station
func (dst *BinkyNetCommandStationState) FromState(ctx context.Context, src state.BinkyNetCommandStation) error {
	if bnModel, ok := src.GetModel().(model.BinkyNetCommandStation); ok {
		bnModel.GetLocalWorkers().ForEach(func(lw model.BinkyNetLocalWorker) {
			lwState := &BinkyNetLocalWorkerState{
				Model: &BinkyNetLocalWorker{},
			}
			lwState.Model.FromModel(ctx, lw)
			info, found := src.GetLocalWorkerInfo(ctx, lw.GetAlias())
			if !found {
				info, found = src.GetLocalWorkerInfo(ctx, lw.GetHardwareID())
			}
			if found {
				lwState.Uptime = info.GetUptime()
			}
			dst.LocalWorkers = append(dst.LocalWorkers, lwState)
		})
	}
	return nil
}
