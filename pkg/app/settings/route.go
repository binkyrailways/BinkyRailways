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
	"gioui.org/unit"
	"gioui.org/widget"
	"gioui.org/widget/material"
	"gioui.org/x/component"

	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// NewRouteSettings constructs a settings component for a Route.
func NewRouteSettings(entity model.Route, modal *component.ModalLayer) Settings {
	s := &routeSettings{
		entity:           entity,
		modal:            modal,
		eventEditorSheet: component.NewModalSheet(modal),
		eventEditor:      NewRouteEventSetSettings(entity.GetEvents(), entity.GetModule()),
	}
	s.eventEditorSheet.MaxWidth = unit.Dp(600)
	s.metaSettings.Initialize(entity)
	s.fromBlockSideEditor.SetValue(entity.GetFromBlockSide())
	s.fromBlockEditor.Initialize(entity.GetModule(), entity.GetFrom())
	s.toBlockSideEditor.SetValue(entity.GetToBlockSide())
	s.toBlockEditor.Initialize(entity.GetModule(), entity.GetTo())
	s.speedEditor.Validator = widgets.MinMaxIntValidator(0, 100)
	s.speedEditor.SetValue(entity.GetSpeed())
	s.chooseProbabilityEditor.Validator = widgets.MinMaxIntValidator(0, 100)
	s.chooseProbabilityEditor.SetValue(entity.GetChooseProbability())
	s.closedEditor.SetValue(entity.GetClosed())
	s.maxDurationEditor.Validator = widgets.MinMaxIntValidator(0, 3600)
	s.maxDurationEditor.SetValue(entity.GetMaxDuration())
	return s
}

// routeSettings implements an settings grid for a Route.
type routeSettings struct {
	entity model.Route
	modal  *component.ModalLayer

	metaSettings
	fromBlockSideEditor     widgets.BlockSideEditor
	fromBlockEditor         widgets.EndpointEditor
	toBlockSideEditor       widgets.BlockSideEditor
	toBlockEditor           widgets.EndpointEditor
	speedEditor             widgets.IntEditor
	chooseProbabilityEditor widgets.IntEditor
	closedEditor            widgets.BoolEditor
	maxDurationEditor       widgets.IntEditor
	editEvents              widget.Clickable
	eventEditorSheet        *component.ModalSheet
	eventEditor             Settings
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
	if x, err := e.speedEditor.GetValue(); err == nil {
		e.entity.SetSpeed(x)
	}
	if x, err := e.chooseProbabilityEditor.GetValue(); err == nil {
		e.entity.SetChooseProbability(x)
	}
	if x, err := e.closedEditor.GetValue(); err == nil {
		e.entity.SetClosed(x)
	}
	if x, err := e.maxDurationEditor.GetValue(); err == nil {
		e.entity.SetMaxDuration(x)
	}
	if e.editEvents.Clicked() {
		e.eventEditorSheet.LayoutModal(func(gtx C, th *material.Theme, anim *component.VisibilityAnimation) D {
			return e.eventEditor.Layout(gtx, th)
		})
		e.eventEditorSheet.Appear(gtx.Now)
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
			widgets.SettingsGridRow{
				Title: "Speed",
				Layout: func(gtx C) D {
					return e.speedEditor.Layout(gtx, th)
				},
			},
			widgets.SettingsGridRow{
				Title: "Choose probability",
				Layout: func(gtx C) D {
					return e.chooseProbabilityEditor.Layout(gtx, th)
				},
			},
			widgets.SettingsGridRow{
				Title: "Max duration (sec)",
				Layout: func(gtx C) D {
					return e.maxDurationEditor.Layout(gtx, th)
				},
			},
			widgets.SettingsGridRow{
				Title: "Closed",
				Layout: func(gtx C) D {
					return e.closedEditor.Layout(gtx, th)
				},
			},
			widgets.SettingsGridRow{
				Title: "Events",
				Layout: func(gtx C) D {
					return material.Button(th, &e.editEvents, "...").Layout(gtx)
				},
			},
		)...,
	)

	return grid.Layout(gtx, th)
}
