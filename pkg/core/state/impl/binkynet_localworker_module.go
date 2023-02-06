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

package impl

import (
	"time"

	"github.com/binkynet/NetManager/service/manager"

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

type binkyNetLocalWorkerModule struct {
	ID            string
	Manager       manager.Manager
	ErrorMessages []string
}

var _ state.HardwareModule = &binkyNetLocalWorkerModule{}

// Gets the ID of the module
func (lw *binkyNetLocalWorkerModule) GetID() string {
	return lw.ID
}

// Gets the uptime of the module
func (lw *binkyNetLocalWorkerModule) GetUptime() time.Duration {
	if info, _, _, found := lw.Manager.GetLocalWorkerInfo(lw.ID); found {
		return time.Duration(info.Uptime) * time.Second
	}
	return 0
}

// Does this module support uptime data?
func (lw *binkyNetLocalWorkerModule) HasUptime() bool {
	return true
}

// Gets the time of last update of the information of this module
func (lw *binkyNetLocalWorkerModule) GetLastUpdatedAt() time.Time {
	if _, _, lastUpdateAt, found := lw.Manager.GetLocalWorkerInfo(lw.ID); found {
		return lastUpdateAt
	}
	return time.Time{}
}

// Does this module support last updated at data?
func (lw *binkyNetLocalWorkerModule) HasLastUpdatedAt() bool {
	return true
}

// Gets the version of the module
func (lw *binkyNetLocalWorkerModule) GetVersion() string {
	if info, _, _, found := lw.Manager.GetLocalWorkerInfo(lw.ID); found {
		return info.Version
	}
	return ""
}

// Does this module support version data?
func (lw *binkyNetLocalWorkerModule) HasVersion() bool {
	return true
}

// Get human readable error messages related to this module
func (lw *binkyNetLocalWorkerModule) GetErrorMessages() []string {
	return lw.ErrorMessages
}

// Get the address of the module (if any)
func (lw *binkyNetLocalWorkerModule) GetAddress() string {
	if _, remoteAddr, _, found := lw.Manager.GetLocalWorkerInfo(lw.ID); found {
		return remoteAddr
	}
	return ""
}

// Does this module support address data?
func (lw *binkyNetLocalWorkerModule) HasAddress() bool {
	return true
}
