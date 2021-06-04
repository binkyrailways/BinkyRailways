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

package editors

import (
	"context"
	"fmt"
	"image"
	"strings"

	"gioui.org/layout"
	"gioui.org/text"
	"gioui.org/widget"
	"gioui.org/widget/material"

	"github.com/binkyrailways/BinkyRailways/pkg/app/settings"
	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// BinkyLocalWorkerEditor extends the basic Editor interface
type BinkyNetLocalWorkerEditor interface {
	Editor

	// LocalWorker returns the local worker we're editing
	LocalWorker() model.BinkyNetLocalWorker
	// OnSelect is called when the currently selected entity has changed.
	OnSelect(entity interface{})
}

// newBinkyNetLocalWorkerEditor constructs an editor for a local worker.
func newBinkyNetLocalWorkerEditor(localWorker model.BinkyNetLocalWorker, etx EditorContext) BinkyNetLocalWorkerEditor {
	editor := &binkyNetLocalWorkerEditor{
		localWorker:  localWorker,
		etx:          etx,
		devicesTable: newDevicesTables(),
		objectsTable: newObjectsTables(),
	}
	editor.settings = settings.BuildSettings(localWorker)

	return editor
}

// binkyNetLocalWorkerEditor implements an editor for a BinkyNetLocalWorkerEditor.
type binkyNetLocalWorkerEditor struct {
	localWorker model.BinkyNetLocalWorker
	etx         EditorContext

	devicesTable
	objectsTable
	settings  settings.Settings
	selection interface{}
}

// LocalWorker returns the local worker we're editing
func (e *binkyNetLocalWorkerEditor) LocalWorker() model.BinkyNetLocalWorker {
	return e.localWorker
}

// OnSelect is called when the currently selected entity has changed.
func (e *binkyNetLocalWorkerEditor) OnSelect(entity interface{}) {
	e.selection = entity
	e.settings = settings.BuildSettings(entity)
	if e.settings == nil {
		e.settings = settings.BuildSettings(e.localWorker)
	}
	e.etx.Invalidate()
}

// Handle events and draw the editor
func (e *binkyNetLocalWorkerEditor) Layout(gtx C, th *material.Theme) D {
	// Prepare content split
	vs := layout.Flex{
		Axis: layout.Vertical,
	}
	vsLayout := func(gtx C) D {
		return vs.Layout(gtx,
			layout.Rigid(func(gtx C) D {
				return widgets.WithPadding(gtx, material.H5(th, "Devices").Layout, widgets.Zero, widgets.Zero, widgets.Padding, widgets.Zero)
			}),
			layout.Rigid(func(gtx C) D { return e.devicesTable.Layout(gtx, th, e.localWorker.GetDevices(), e.etx.Select) }),
			layout.Rigid(func(gtx C) D {
				return widgets.WithPadding(gtx, material.H5(th, "Objects").Layout, widgets.Padding.Scale(2), widgets.Zero, widgets.Padding, widgets.Zero)
			}),
			layout.Rigid(func(gtx C) D { return e.objectsTable.Layout(gtx, th, e.localWorker.GetObjects(), e.etx.Select) }),
		)
	}

	// Prepare content split
	hs := widgets.HorizontalSplit(
		func(gtx C) D { return vsLayout(gtx) },
		func(gtx C) D { return e.settings.Layout(gtx, th) },
	)
	hs.Start.Weight = 4
	hs.End.Rigid = true
	hs.End.Weight = 1

	return hs.Layout(gtx)
}

// Create the buttons for the "Add resource sheet"
func (e *binkyNetLocalWorkerEditor) CreateAddButtons() []AddButton {
	if e.selection != nil {
		return createAddButtonsFor(e.etx, e.selection)
	}
	return createAddButtonsFor(e.etx, e.localWorker)
}

// Can the currently selected item be deleted?
func (e *binkyNetLocalWorkerEditor) CanDelete() bool {
	onDelete := createOnDelete(e.etx, e.selection)
	return onDelete != nil
}

// Delete the currently selected item
func (e *binkyNetLocalWorkerEditor) Delete(ctx context.Context) error {
	if onDelete := createOnDelete(e.etx, e.selection); onDelete != nil {
		if err := onDelete(ctx); err != nil {
			return err
		}
	}
	return nil
}

type devicesTable struct {
	widgets.SimpleTable
	buttons []widget.Clickable
}

// newDevicesTables constructs a new devices table
func newDevicesTables() devicesTable {
	return devicesTable{
		SimpleTable: widgets.SimpleTable{
			Columns:     make([]widgets.SimpleColumn, 3),
			CellSpacing: image.Pt(8, 4),
		},
	}
}

// Layout renders a devices table
func (t *devicesTable) Layout(gtx C, th *material.Theme, devices model.BinkyNetDeviceSet, onSelect func(entity interface{})) D {
	cnt := devices.GetCount()
	if len(t.buttons) != cnt {
		t.buttons = make([]widget.Clickable, cnt)
	}
	for i, btn := range t.buttons {
		if btn.Clicked() {
			dev, _ := devices.GetAt(i)
			onSelect(dev)
		}
	}

	layoutHeaderCell := func(gtx C, column int) D {
		switch column {
		case 0:
			return material.H6(th, "Device ID").Layout(gtx)
		case 1:
			return material.H6(th, "Device Type").Layout(gtx)
		default:
			return material.H6(th, "Address").Layout(gtx)
		}
	}

	layoutCell := func(gtx C, column int, button *widget.Clickable, dev model.BinkyNetDevice) D {
		switch column {
		case 0:
			lb := material.Body1(th, string(dev.GetDeviceID()))
			if button.Hovered() {
				lb.Font.Weight = text.Bold
			}
			return material.Clickable(gtx, button, lb.Layout)
		case 1:
			return material.Body1(th, string(dev.GetDeviceType())).Layout(gtx)
		default:
			return material.Body1(th, dev.GetAddress()).Layout(gtx)
		}
	}

	return t.SimpleTable.Layout(gtx, 1+devices.GetCount(), func(gtx layout.Context, x, y int) D {
		if y == 0 {
			return layoutHeaderCell(gtx, x)
		} else {
			dev, _ := devices.GetAt(y - 1)
			return layoutCell(gtx, x, &t.buttons[y-1], dev)
		}
	})
}

type objectsTable struct {
	widgets.SimpleTable
	buttons []widget.Clickable
}

// newObjectsTables constructs a new objects table
func newObjectsTables() objectsTable {
	return objectsTable{
		SimpleTable: widgets.SimpleTable{
			Columns:     make([]widgets.SimpleColumn, 3),
			CellSpacing: image.Pt(8, 4),
		},
	}
}

// Layout renders a objects table
func (t *objectsTable) Layout(gtx C, th *material.Theme, objects model.BinkyNetObjectSet, onSelect func(entity interface{})) D {
	cnt := objects.GetCount()
	if len(t.buttons) != cnt {
		t.buttons = make([]widget.Clickable, cnt)
	}
	for i, btn := range t.buttons {
		if btn.Clicked() {
			obj, _ := objects.GetAt(i)
			onSelect(obj)
		}
	}

	layoutHeaderCell := func(gtx C, column int) D {
		switch column {
		case 0:
			return material.H6(th, "Object ID").Layout(gtx)
		case 1:
			return material.H6(th, "Object Type").Layout(gtx)
		default:
			return material.H6(th, "Connections").Layout(gtx)
		}
	}

	layoutCell := func(gtx C, column int, button *widget.Clickable, obj model.BinkyNetObject) D {
		switch column {
		case 0:
			lb := material.Body1(th, string(obj.GetObjectID()))
			if button.Hovered() {
				lb.Font.Weight = text.Bold
			}
			return material.Clickable(gtx, button, lb.Layout)
		case 1:
			return material.Body1(th, string(obj.GetObjectType())).Layout(gtx)
		default:
			return material.Body1(th, formatConnections(obj.GetConnections())).Layout(gtx)
		}
	}

	return t.SimpleTable.Layout(gtx, 1+objects.GetCount(), func(gtx layout.Context, x, y int) D {
		if y == 0 {
			return layoutHeaderCell(gtx, x)
		} else {
			obj, _ := objects.GetAt(y - 1)
			return layoutCell(gtx, x, &t.buttons[y-1], obj)
		}
	})
}

func formatConnections(connections model.BinkyNetConnectionSet) string {
	cnt := connections.GetCount()
	if cnt == 0 {
		return "-"
	}
	lines := make([]string, 0, cnt)
	connections.ForEach(func(conn model.BinkyNetConnection) {
		var line string
		fmtPins := formatConnectionPins(conn.GetPins())
		fmtCfg := formatConnectionConfig(conn.GetConfiguration())
		if fmtCfg == "" {
			line = fmt.Sprintf("%s -> (%s)", conn.GetKey(), fmtPins)
		} else {
			line = fmt.Sprintf("%s -> (%s) with { %s }", conn.GetKey(), fmtPins, fmtCfg)
		}
		lines = append(lines, line)
	})
	return strings.Join(lines, "\n")
}

func formatConnectionPins(pins model.BinkyNetConnectionPinList) string {
	cnt := pins.GetCount()
	if cnt == 0 {
		return "-"
	}
	lines := make([]string, 0, cnt)
	pins.ForEach(func(pin model.BinkyNetDevicePin) {
		line := fmt.Sprintf("%s[%d]", pin.GetDeviceID(), pin.GetIndex())
		lines = append(lines, line)
	})
	return strings.Join(lines, ", ")
}

func formatConnectionConfig(connections model.BinkyNetConnectionConfiguration) string {
	cnt := connections.GetCount()
	if cnt == 0 {
		return ""
	}
	lines := make([]string, 0, cnt)
	connections.ForEach(func(k, v string) {
		line := fmt.Sprintf("%s=%s", k, v)
		lines = append(lines, line)
	})
	return strings.Join(lines, ", ")
}
