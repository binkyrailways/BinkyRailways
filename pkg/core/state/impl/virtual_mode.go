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
	"context"
	"fmt"

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// VirtualMode adds implementation methods to state.VirtualMode
type VirtualMode interface {
	state.VirtualMode

	// Close all virtual activities
	Close()
}

type virtualMode struct {
	enabled bool
	autoRun *autoRunState
}

var _ VirtualMode = &virtualMode{}

// newVirtualMode constructs a new VirtualMode
func newVirtualMode(enabled bool, railway Railway) VirtualMode {
	return &virtualMode{
		enabled: enabled,
		autoRun: newAutoRunState(railway),
	}
}

// Is virtual mode enabled?
func (v *virtualMode) GetEnabled() bool {
	return v.enabled
}

// Automatically run locs?
func (v *virtualMode) GetAutoRun() bool {
	if !v.enabled {
		return false
	}
	return v.autoRun.GetEnabled()
}
func (v *virtualMode) SetAutoRun(value bool) error {
	if !v.enabled {
		return fmt.Errorf("SetAutoRun is not allowed in non virtual mode")
	}
	return v.autoRun.SetEnabled(value)
}

// Entity is being clicked on.
func (v *virtualMode) EntityClick(ctx context.Context, entity state.Entity) {
	switch st := entity.(type) {
	case state.Sensor:
		st.GetActive().SetActual(ctx, !st.GetActive().GetActual(ctx))
		fmt.Println("Change sensor active to ", st.GetActive().GetActual(ctx))
	}
}

// Close all virtual activities
func (v *virtualMode) Close() {
	if v.GetAutoRun() {
		v.SetAutoRun(false)
	}
}
