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

package common

import (
	"fmt"
	"strings"

	"gioui.org/layout"
	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/app/views"
	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/log"
)

type LogView struct {
	vm     views.ViewManager
	list   layout.List
	events []log.LogEvent
}

// New constructs a new log view
func NewLogView(vm views.ViewManager) *LogView {
	v := &LogView{
		vm: vm,
		list: layout.List{
			Axis: layout.Vertical,
		},
	}
	return v
}

func (v *LogView) OnView(events []log.LogEvent) {
	v.events = events
	v.vm.Invalidate()
}

// Handle events and draw the view
func (v *LogView) Layout(gtx layout.Context) layout.Dimensions {
	th := v.vm.GetTheme()
	events := v.events
	l := len(events)
	return v.list.Layout(gtx, l, func(gtx layout.Context, index int) layout.Dimensions {
		evt := events[l-(1+index)]
		addr := evt.Address
		msg := evt.Message
		if evt.Error != "" {
			msg = msg + " [" + evt.Error + "]"
		}
		kvs := make([]string, 0, len(evt.Fields))
		for _, lf := range evt.Fields {
			kvs = append(kvs, fmt.Sprintf("%s=%v", lf.Key, lf.Value))
		}
		hs := widgets.HorizontalSplit(
			material.Body1(th, addr).Layout,
			func(gtx layout.Context) layout.Dimensions {
				lbl := material.Body1(th, msg+" "+strings.Join(kvs, " "))
				if evt.Error != "" {
					lbl.Color = widgets.ARGB(0xFF880000)
				}
				return lbl.Layout(gtx)
			},
		)
		hs.Start.Weight = 1
		hs.End.Weight = 8
		return hs.Layout(gtx)
	})
}
