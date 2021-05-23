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

package run

import (
	"context"

	"gioui.org/layout"

	"github.com/binkyrailways/BinkyRailways/pkg/app/views"
	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

type hardwareModulesView struct {
	vm         views.ViewManager
	entityList widgets.TreeView
}

// New constructs a new locs view
func newHardwareModuleView(vm views.ViewManager, railway state.Railway) *hardwareModulesView {
	itemCache := widgets.EntityTreeViewItemCache{}
	groupCache := widgets.EntityTreeGroupCache{}
	v := &hardwareModulesView{
		vm: vm,
		entityList: widgets.TreeView{
			Exclusive: railway,
			RootItems: []widgets.TreeViewItem{
				&widgets.TreeViewGroup{
					Name: "Command stations",
					Collection: func(ctx context.Context, parentKey string, level int) []widgets.TreeViewItem {
						return buildHardwareModulesTreeViewItems(railway, parentKey, &itemCache, &groupCache, level)
					},
				},
			},
		},
	}
	return v
}

// Handle events and draw the view
func (v *hardwareModulesView) Layout(gtx layout.Context) layout.Dimensions {
	th := v.vm.GetTheme()
	return v.entityList.Layout(gtx, th)
}
