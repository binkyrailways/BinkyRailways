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

package state

import "time"

// HardwareModule specifies the state of a single hardware module.
type HardwareModule interface {
	// Gets the ID of the module
	GetID() string

	// Gets the uptime of the module
	GetUptime() time.Duration
	// Does this module support uptime data?
	HasUptime() bool

	// Gets the time of last update of the information of this module
	GetLastUpdatedAt() time.Time
	// Does this module support last updated at data?
	HasLastUpdatedAt() bool

	// Gets the version of the module
	GetVersion() string
	// Does this module support version data?
	HasVersion() bool

	// Human readable error messages related to this module
	GetErrorMessages() []string

	// Get IP address of this module (if any)
	GetAddress() string
	// Does this module support address?
	HasAddress() bool

	// URL to get metrics of this module (if any)
	GetMetricsURL() string
	// Does this module support metrics url?
	HasMetricsURL() bool

	// URL to get DCC generator info of this module (if any)
	GetDCCGeneratorURL() string
	// Does this module support DCC generator url?
	HasDCCGeneratorURL() bool

	// URL to open SSH connection to this module (if any)
	GetSSHURL() string
	// Does this module support SSH url?
	HasSSHURL() bool
}
