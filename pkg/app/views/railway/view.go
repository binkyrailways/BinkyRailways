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

package railway

import (
	"context"
	"fmt"

	"gioui.org/layout"

	"github.com/binkyrailways/BinkyRailways/pkg/app/views"
	"github.com/binkyrailways/BinkyRailways/pkg/app/views/railway/edit"
	"github.com/binkyrailways/BinkyRailways/pkg/app/views/railway/run"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state/impl"
	"github.com/rs/zerolog"
)

type (
	C = layout.Context
	D = layout.Dimensions
)

type railwayView struct {
	vm      views.ViewManager
	railway model.Railway

	railwayState state.Railway
	runMode      bool
	virtualMode  bool
	currentView  childView
}

// childView is implemented by direct children of this view
type childView interface {
	// Layout the view
	Layout(gtx C) D
}

// New constructs a new railway view
func New(vm views.ViewManager, railway model.Railway) views.View {
	v := &railwayView{
		vm:      vm,
		railway: railway,
	}
	v.currentView = edit.New(vm, railway, v.SetRunMode)
	return v
}

// Return additional text to add to the window title
func (v *railwayView) GetTitleExtension() string {
	return v.railway.GetDescription()
}

// Handle events and draw the view
func (v *railwayView) Layout(gtx layout.Context) layout.Dimensions {
	return v.currentView.Layout(gtx)
}

// SetEditMode switches to edit mode
func (v *railwayView) SetEditMode() {
	if err := v.setRunMode(false, false); err != nil {
		fmt.Printf("Switching to edit mode failed: %s\n", err)
	}
}

// SetRunMode switches to run mode
func (v *railwayView) SetRunMode(virtual bool) {
	if err := v.setRunMode(true, virtual); err != nil {
		fmt.Printf("Switching to run mode failed: %s\n", err)
	}
}

// setRunMode switch from edit to run mode and back
func (v *railwayView) setRunMode(runMode, virtualMode bool) error {
	// Any changes?
	if v.runMode == runMode && v.virtualMode == virtualMode {
		return nil
	}
	// Close any existing state
	ctx := context.Background()
	if v.railwayState != nil {
		v.railwayState.Close(ctx)
		v.railwayState = nil
	}
	v.runMode = runMode
	v.virtualMode = virtualMode
	if runMode {
		var err error
		log := zerolog.New(zerolog.NewConsoleWriter())
		v.railwayState, err = impl.New(ctx, v.railway, log, v, v, virtualMode)
		if err != nil {
			return err
		}
		runView := run.New(v.vm, v.railwayState, v.SetEditMode)
		v.currentView = runView
	} else {
		editView := edit.New(v.vm, v.railway, v.SetRunMode)
		v.currentView = editView
	}
	v.vm.Invalidate()
	return nil
}

// The COM port for the given command station is invalid.
func (v *railwayView) ChooseComPortName(state.CommandStation) (string, error) {
	return "", nil // TODO
}
