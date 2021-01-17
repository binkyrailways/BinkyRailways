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

	"github.com/binkyrailways/BinkyRailways/pkg/app/views"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type railwayView struct {
	vm       views.ViewManager
	railway  model.Railway
	runMode  bool
	editView *editView
}

// New constructs a new railway view
func New(vm views.ViewManager, railway model.Railway) views.View {
	v := &railwayView{
		vm:      vm,
		railway: railway,
	}
	v.editView = newEditView(vm, railway, v)
	return v
}

// Return additional text to add to the window title
func (v *railwayView) GetTitleExtension() string {
	return v.railway.GetDescription()
}

// Handle events and draw the view
func (v *railwayView) Layout(gtx layout.Context) layout.Dimensions {
	return v.editView.Layout(gtx)
}

// SetRunMode switch from edit to run mode and back
func (v *railwayView) SetRunMode(runMode bool) {
	v.runMode = runMode
	v.vm.Invalidate()
}
