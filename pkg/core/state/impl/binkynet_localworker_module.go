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

	bn "github.com/binkynet/BinkyNet/apis/v1"

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

type binkyNetLocalWorkerModule bn.LocalWorkerInfo

var _ state.HardwareModule = &binkyNetLocalWorkerModule{}

// Gets the ID of the module
func (lw *binkyNetLocalWorkerModule) GetID() string {
	return lw.Id
}

// Gets the uptime of the module
func (lw *binkyNetLocalWorkerModule) GetUptime() time.Duration {
	return time.Duration(lw.Uptime) * time.Second
}

// Does this module support uptime data?
func (lw *binkyNetLocalWorkerModule) HasUptime() bool {
	return true
}

// Gets the version of the module
func (lw *binkyNetLocalWorkerModule) GetVersion() string {
	return lw.Version
}

// Does this module support version data?
func (lw *binkyNetLocalWorkerModule) HasVersion() bool {
	return true
}
