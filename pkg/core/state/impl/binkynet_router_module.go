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

	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkynet/NetManager/service/manager"

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

type binkyNetRouterModule struct {
	LocalWorkerID string
	RouterID      string
	Manager       manager.Manager
	ErrorMessages []string
}

var _ state.HardwareModule = &binkyNetRouterModule{}

// Gets the ID of the module
func (rm *binkyNetRouterModule) GetID() string {
	return rm.RouterID
}

// Gets RouterInfo if online.
func (rm *binkyNetRouterModule) getRouterInfo() *api.RouterInfo {
	if info, _, _, found := rm.Manager.GetLocalWorkerInfo(rm.LocalWorkerID); found {
		for _, ri := range info.GetOnlineRouters() {
			if ri.GetId() == rm.RouterID {
				return ri
			}
		}
	}
	return nil
}

// Gets the uptime of the module
func (rm *binkyNetRouterModule) GetUptime() time.Duration {
	if info := rm.getRouterInfo(); info != nil {
		return time.Duration(info.GetUptime()) * time.Second
	}
	return 0
}

// Does this module support uptime data?
func (rm *binkyNetRouterModule) HasUptime() bool {
	return true
}

// Gets the time of last update of the information of this module
func (rm *binkyNetRouterModule) GetLastUpdatedAt() time.Time {
	if _, _, lastUpdateAt, found := rm.Manager.GetLocalWorkerInfo(rm.LocalWorkerID); found {
		return lastUpdateAt
	}
	return time.Time{}
}

// Does this module support last updated at data?
func (rm *binkyNetRouterModule) HasLastUpdatedAt() bool {
	return true
}

// Gets the version of the module
func (rm *binkyNetRouterModule) GetVersion() string {
	if info := rm.getRouterInfo(); info != nil {
		return info.GetVersion()
	}
	return ""
}

// Does this module support version data?
func (rm *binkyNetRouterModule) HasVersion() bool {
	return true
}

// Get human readable error messages related to this module
func (rm *binkyNetRouterModule) GetErrorMessages() []string {
	return rm.ErrorMessages
}

// Get the address of the module (if any)
func (rm *binkyNetRouterModule) GetAddress() string {
	if info := rm.getRouterInfo(); info != nil {
		return info.GetIpAddress()
	}
	return ""
}

// Does this module support address data?
func (rm *binkyNetRouterModule) HasAddress() bool {
	return true
}

// URL to get metrics of this module (if any)
func (rm *binkyNetRouterModule) GetMetricsURL() string {
	if info := rm.getRouterInfo(); info != nil {
		port := info.GetMetricsPort()
		secure := info.GetMetricsSecure()
		ipAddress := info.GetIpAddress()
		if port > 0 {
			scheme := "http"
			if secure {
				scheme = "https"
			}
			return fmt.Sprintf("%s://%s:%d/metrics", scheme, ipAddress, port)
		}
	}
	return ""
}

// Does this module support metrics url?
func (rm *binkyNetRouterModule) HasMetricsURL() bool {
	return true
}

// URL to get DCC generator info of this module (if any)
func (rm *binkyNetRouterModule) GetDCCGeneratorURL() string {
	return ""
}

// Does this module support DCC generator url?
func (rm *binkyNetRouterModule) HasDCCGeneratorURL() bool {
	return false
}

// URL to open SSH connection to this module (if any)
func (rm *binkyNetRouterModule) GetSSHURL() string {
	return ""
}

// Does this module support SSH url?
func (rm *binkyNetRouterModule) HasSSHURL() bool {
	return false
}
