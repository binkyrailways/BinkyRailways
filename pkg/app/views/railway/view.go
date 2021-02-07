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
	"gioui.org/layout"
	"gioui.org/widget"
	"golang.org/x/exp/shiny/materialdesign/icons"

	"github.com/binkyrailways/BinkyRailways/pkg/app/views"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state/impl"
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
	currentView  layout.Widget
}

var (
	iconAdd, _        = widget.NewIcon(icons.ContentAdd)
	iconEdit, _       = widget.NewIcon(icons.ContentCreate)
	iconRun, _        = widget.NewIcon(icons.AVPlayArrow)
	iconRunVirtual, _ = widget.NewIcon(icons.AVPlayCircleOutline)
)

// New constructs a new railway view
func New(vm views.ViewManager, railway model.Railway) views.View {
	v := &railwayView{
		vm:      vm,
		railway: railway,
	}
	editView := newEditView(vm, railway, v)
	v.currentView = editView.Layout
	return v
}

// Return additional text to add to the window title
func (v *railwayView) GetTitleExtension() string {
	return v.railway.GetDescription()
}

// Handle events and draw the view
func (v *railwayView) Layout(gtx layout.Context) layout.Dimensions {
	return v.currentView(gtx)
}

// SetRunMode switch from edit to run mode and back
func (v *railwayView) SetRunMode(runMode, virtualMode bool) error {
	// Any changes?
	if v.runMode == runMode && v.virtualMode == virtualMode {
		return nil
	}
	// Close any existing state
	if v.railwayState != nil {
		v.railwayState.Close()
		v.railwayState = nil
	}
	v.runMode = runMode
	v.virtualMode = virtualMode
	if runMode {
		var err error
		v.railwayState, err = impl.New(v.railway, v, v, virtualMode)
		if err != nil {
			return err
		}
		runView := newRunView(v.vm, v.railwayState, v)
		v.currentView = runView.Layout
	} else {
		editView := newEditView(v.vm, v.railway, v)
		v.currentView = editView.Layout
	}
	v.vm.Invalidate()
	return nil
}

// The COM port for the given command station is invalid.
func (v *railwayView) ChooseComPortName(state.CommandStation) (string, error) {
	return "", nil // TODO
}
