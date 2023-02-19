// Copyright 2023 Ewout Prangsma
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

package impl

import (
	"github.com/binkyrailways/BinkyRailways/pkg/metrics"
)

const (
	subSystem = "state"
)

var (
	// requestedOutputValueGauge tracks request output value
	requestedOutputValueGauge = metrics.MustRegisterGaugeVec(subSystem, "requested_output_value", "0, 1", "address")
	// requestedSwitchDirectionGauge tracks request switch direction
	requestedSwitchDirectionGauge = metrics.MustRegisterGaugeVec(subSystem, "requested_switch_direction", "0 is switch is set to straight, 1 if set to off", "address")
	// sendSwitchDirectionCounter tracks the number of switch direction requests send to binkynet
	sendSwitchDirectionCounter = metrics.MustRegisterGaugeVec(subSystem, "send_switch_direction_total", "number of send switch direction requests to binkynet", "address")
)
