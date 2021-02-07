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

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

type virtualMode struct {
	Enabled bool
	autoRun bool
}

var _ state.VirtualMode = &virtualMode{}

// Is virtual mode enabled?
func (v *virtualMode) GetEnabled() bool {
	return v.Enabled
}

// Automatically run locs?
func (v *virtualMode) GetAutoRun() bool {
	if !v.Enabled {
		return false
	}
	return v.autoRun
}
func (v *virtualMode) SetAutoRun(value bool) error {
	if !v.Enabled {
		return fmt.Errorf("SetAutoRun is not allowed in non virtual mode")
	}
	v.autoRun = value
	return nil
}

// Entity is being clicked on.
func (v *virtualMode) EntityClick(entity state.Entity) {
	// TODO
}
