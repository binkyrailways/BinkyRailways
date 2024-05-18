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
	"fmt"
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

// URL to get metrics of this module (if any)
func (lw *binkyNetLocalWorkerModule) GetMetricsURL() string {
	if info, host, _, found := lw.Manager.GetLocalWorkerInfo(lw.ID); found {
		port := info.GetMetricsPort()
		secure := info.GetMetricsSecure()
		if port > 0 {
			scheme := "http"
			if secure {
				scheme = "https"
			}
			return fmt.Sprintf("%s://%s:%d/metrics", scheme, host, port)
		}
	}
	return ""
}

// Does this module support metrics url?
func (lw *binkyNetLocalWorkerModule) HasMetricsURL() bool {
	return true
}

// URL to get DCC generator info of this module (if any)
func (lw *binkyNetLocalWorkerModule) GetDCCGeneratorURL() string {
	if lw.ID == dccModuleID {
		if info, host, _, found := lw.Manager.GetLocalWorkerInfo(lw.ID); found {
			port := info.GetMetricsPort()
			secure := info.GetMetricsSecure()
			if port > 0 {
				scheme := "http"
				if secure {
					scheme = "https"
				}
				return fmt.Sprintf("%s://%s:%d/dcc", scheme, host, port)
			}
		}
	}
	return ""
}

// Does this module support DCC generator url?
func (lw *binkyNetLocalWorkerModule) HasDCCGeneratorURL() bool {
	return lw.GetID() == dccModuleID
}

// URL to open SSH connection to this module (if any)
func (lw *binkyNetLocalWorkerModule) GetSSHURL() string {
	if lw.ID == dccModuleID {
		if _, host, _, found := lw.Manager.GetLocalWorkerInfo(lw.ID); found {
			return fmt.Sprintf("ssh://%s:1515", host)
		}
	}
	return ""
}

// Does this module support SSH url?
func (lw *binkyNetLocalWorkerModule) HasSSHURL() bool {
	return lw.GetID() == dccModuleID
}
