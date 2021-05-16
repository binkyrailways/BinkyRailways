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

package settings

import (
	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// NewRouteSettings constructs a settings component for a Route.
func NewRouteSettings(entity model.Route) Settings {
	s := &routeSettings{
		entity: entity,
	}
	s.metaSettings.Initialize(entity)
	s.fromBlockSideEditor.SetValue(entity.GetFromBlockSide())
	s.fromBlockEditor.Initialize(entity.GetModule(), entity.GetFrom())
	s.toBlockSideEditor.SetValue(entity.GetToBlockSide())
	s.toBlockEditor.Initialize(entity.GetModule(), entity.GetTo())
	return s
}

// routeSettings implements an settings grid for a Route.
type routeSettings struct {
	entity model.Route

	metaSettings
	fromBlockSideEditor widgets.BlockSideEditor
	fromBlockEditor     widgets.EndpointEditor
	toBlockSideEditor   widgets.BlockSideEditor
	toBlockEditor       widgets.EndpointEditor
}

// Handle events and draw the editor
func (e *routeSettings) Layout(gtx C, th *material.Theme) D {
	e.metaSettings.Update(e.entity)
	if x, err := e.fromBlockSideEditor.GetValue(); err == nil {
		e.entity.SetFromBlockSide(x)
	}
	module := e.entity.GetModule()
	if x, err := e.fromBlockEditor.GetValue(module); err == nil {
		e.entity.SetFrom(x)
	}
	if x, err := e.toBlockSideEditor.GetValue(); err == nil {
		e.entity.SetToBlockSide(x)
	}
	if x, err := e.toBlockEditor.GetValue(module); err == nil {
		e.entity.SetTo(x)
	}

	// Prepare settings grid
	grid := widgets.NewSettingsGrid(
		append(
			e.metaSettings.Rows(th),
			widgets.SettingsGridRow{
				Title: "From endpoint",
				Layout: func(gtx C) D {
					return e.fromBlockEditor.Layout(gtx, th)
				},
			},
			widgets.SettingsGridRow{
				Title: "To endpoint",
				Layout: func(gtx C) D {
					return e.toBlockEditor.Layout(gtx, th)
				},
			},
			widgets.SettingsGridRow{
				Title: "From block side",
				Layout: func(gtx C) D {
					return e.fromBlockSideEditor.Layout(gtx, th)
				},
			},
			widgets.SettingsGridRow{
				Title: "To block side",
				Layout: func(gtx C) D {
					return e.toBlockSideEditor.Layout(gtx, th)
				},
			},
		)...,
	)

	return grid.Layout(gtx, th)
}
