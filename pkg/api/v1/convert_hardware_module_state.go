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
	"time"

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// FromState converts a state hardware module to an API hardware module
func (dst *HardwareModule) FromState(ctx context.Context, src state.HardwareModule) error {
	lastUpdatedAt := src.GetLastUpdatedAt()
	dst.Id = src.GetID()
	dst.Uptime = int64(src.GetUptime().Seconds())
	dst.LastUpdatedAt = lastUpdatedAt.Format(time.RFC3339)
	dst.ErrorMessages = src.GetErrorMessages()
	dst.Address = src.GetAddress()
	if lastUpdatedAt.IsZero() {
		dst.SecondsSinceLastUpdated = 0
	} else {
		dst.SecondsSinceLastUpdated = int64(time.Since(lastUpdatedAt).Seconds())
	}
	dst.MetricsUrl = src.GetMetricsURL()
	return nil
}
