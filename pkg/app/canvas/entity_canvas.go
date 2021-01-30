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

package canvas

import (
	"fmt"
	"image"
	"math"

	"gioui.org/f32"
	"gioui.org/gesture"
	"gioui.org/io/pointer"
	"gioui.org/layout"
	"gioui.org/op"
	"gioui.org/op/clip"
	"gioui.org/op/paint"
	"gioui.org/widget"
	"gioui.org/widget/material"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// EntityIterator defines an iterator function for positioned entities
type EntityIterator = func(func(entity model.PositionedEntity))

// EntityCanvas shows positioned entities in a 2D view.
type EntityCanvas struct {
	// Return the maximum size of the canvas
	GetMaxSize func() f32.Point
	// Entities must be set to an iterator function that yields
	// all entities that must be displayed in the canvas.
	Entities EntityIterator
	// Builder must be set to an entity visitor that builds Widgets.
	Builder model.EntityVisitor
	// If set, OnSelect is called when the given entity is selected.
	OnSelect func(model.PositionedEntity)

	// Background image
	background   image.Image
	backgroundOp paint.ImageOp
	// Map of entityID -> widget
	widgets map[string]*canvasWidget
	// Current scale
	scale float32
	// Current selection
	selection model.PositionedEntity
	// Click on background
	widget.Clickable
}

// Widget is to be implemented by all positioned entities that
// are to be made visible on the canvas.
type Widget interface {
	// Return the bounds of the widget on the canvas
	GetBounds() f32.Rectangle
	// Returns rotation of entity in degrees
	GetRotation() int
	// Layout draws the widget and process events.
	Layout(layout.Context, *material.Theme, WidgetState)
}

// WidgetState describes the current state of the widget
type WidgetState struct {
	// Mouse hovers over the widget
	Hovered bool
}

// canvasWidget is a widget representation of the object on the canvas.
type canvasWidget struct {
	entity model.PositionedEntity
	gesture.Drag
	gesture.Click
	widget        Widget
	origEntityPos image.Point
	startPt       f32.Point
}

// Clicked returns true if the widget has been clicked on and reset the clicked flag.
func (cw *canvasWidget) Clicked() bool {
	result := cw.Click.Pressed()
	return result
}

// Layout draws the widget and process events.
func (cw *canvasWidget) layout(gtx layout.Context, th *material.Theme, size image.Point, rad float32) {
	// Process events
	for range cw.Click.Events(gtx) {
		// Nothing here
	}
	for _, evt := range cw.Drag.Events(gtx.Metric, gtx, gesture.Both) {
		pt := f32.Affine2D{}.Rotate(f32.Point{}, -rad).Transform(evt.Position)
		switch evt.Type {
		case pointer.Press:
			cw.origEntityPos = image.Point{X: cw.entity.GetX(), Y: cw.entity.GetY()}
			cw.startPt = pt
		case pointer.Drag:
			delta := pt.Sub(cw.startPt)
			newEntityPos := f32.Pt(float32(cw.origEntityPos.X), float32(cw.origEntityPos.Y)).Add(delta)
			fmt.Println(pt, gtx.Constraints.Min, gtx.Constraints.Max, cw.origEntityPos, delta, newEntityPos)
			cw.entity.SetX(int(newEntityPos.X))
			cw.entity.SetY(int(newEntityPos.Y))
		case pointer.Cancel:
			cw.entity.SetX(cw.origEntityPos.X)
			cw.entity.SetY(cw.origEntityPos.Y)
		}
	}

	// First as clickable
	gtx.Constraints.Min = size
	gtx.Constraints.Max = size
	// Add clicking & hover detection
	pointer.Rect(image.Rectangle{Max: size}).Add(gtx.Ops)
	cw.Click.Add(gtx.Ops)
	pointer.PassOp{Pass: true}.Add(gtx.Ops)
	// Add dragging
	pointer.Rect(image.Rectangle{Max: size}).Add(gtx.Ops)
	cw.Drag.Add(gtx.Ops)
	pointer.PassOp{Pass: true}.Add(gtx.Ops)
	// Now layout actual widget
	state := WidgetState{
		Hovered: cw.Click.Hovered(),
	}
	clip.Rect{
		Max: size,
	}.Add(gtx.Ops)
	cw.widget.Layout(gtx, th, state)
}

// SetBackground sets the background image
func (ec *EntityCanvas) SetBackground(img image.Image) {
	if ec.background != img {
		ec.background = img
		if img != nil {
			ec.backgroundOp = paint.NewImageOp(img)
		} else {
			ec.backgroundOp = paint.ImageOp{}
		}
	}
}

// SetScale adjusts the current scale
// Returns: scaleChanged
func (ec *EntityCanvas) SetScale(scale float32) bool {
	if ec.scale != scale {
		ec.scale = scale
		return true
	}
	return false
}

// rebuildWidgets ensures that the map of widgets contains
// exactly those widgets that we currently need.
// Returns: maxSize
func (ec *EntityCanvas) rebuildWidgets() f32.Point {
	if ec.widgets == nil {
		ec.widgets = make(map[string]*canvasWidget)
	}
	// Ensure we have all widgets
	maxSize := ec.GetMaxSize()
	foundIDs := make(map[string]struct{})
	ec.Entities(func(entity model.PositionedEntity) {
		id := entity.GetID()
		foundIDs[id] = struct{}{}
		entry, found := ec.widgets[id]
		if !found {
			// Build a new widget
			raw := entity.Accept(ec.Builder)
			w, ok := raw.(Widget)
			if !ok {
				return
			}
			entry = &canvasWidget{
				entity: entity,
				widget: w,
			}
			ec.widgets[id] = entry
		}
		bounds := entry.widget.GetBounds()
		if bounds.Max.X > maxSize.X {
			maxSize.X = bounds.Max.X
		}
		if bounds.Max.Y > maxSize.Y {
			maxSize.Y = bounds.Max.Y
		}
	})
	// Ensure we do not have too much widgets
	for id := range ec.widgets {
		if _, found := foundIDs[id]; !found {
			delete(ec.widgets, id)
		}
	}
	return maxSize
}

// GetSelection returns the current selection (if any)
func (ec *EntityCanvas) GetSelection() model.PositionedEntity {
	return ec.selection
}

// Select sets the selected entity
func (ec *EntityCanvas) Select(entity model.PositionedEntity) {
	if ec.selection != entity {
		ec.selection = entity
		if ec.OnSelect != nil {
			ec.OnSelect(entity)
		}
	}
}

// Layout generates the UI and processes events.
func (ec *EntityCanvas) Layout(gtx layout.Context, th *material.Theme) layout.Dimensions {
	defer op.Save(gtx.Ops).Load()

	// Process events
	if ec.Clickable.Clicked() {
		ec.Select(nil)
	}
	for _, w := range ec.widgets {
		if w.Clicked() {
			ec.Select(w.entity)
		}
	}

	// Add background click
	ec.Clickable.Layout(gtx)
	// Ensure we have all widgets
	max := gtx.Constraints.Max
	clip.Rect{Max: max}.Add(gtx.Ops)
	maxSize := ec.rebuildWidgets()
	// Prepare overall scaling
	scale := ec.scale
	if maxSize.X > 0 && maxSize.Y > 0 {
		// Scale to fit as much as possible on the view
		sx := float32(max.X) / maxSize.X
		sy := float32(max.Y) / maxSize.Y
		minScale := sx
		if sy < minScale {
			minScale = sy
		}
		scale *= minScale
	}
	// Draw background (if any)
	if ec.background != nil {
		state := op.Save(gtx.Ops)
		tr := f32.Affine2D{}.
			Scale(f32.Point{}, f32.Point{X: scale, Y: scale})
		op.Affine(tr).Add(gtx.Ops)
		widget.Image{Src: ec.backgroundOp}.Layout(gtx)
		state.Load()
	}
	// Layout all widgets
	for _, w := range ec.widgets {
		// Save state
		state := op.Save(gtx.Ops)
		// Prepare transformation
		bounds := w.widget.GetBounds()
		// Translate, scale & rotate
		rot := w.widget.GetRotation()
		rad := float32(rot%360) * (math.Pi / 180)
		tr := f32.Affine2D{}.
			Offset(bounds.Min).
			Rotate(bounds.Min, rad).
			Scale(f32.Point{}, f32.Point{X: scale, Y: scale})
		op.Affine(tr).Add(gtx.Ops)
		// Set clip rectangle
		size := image.Point{
			X: int(bounds.Max.X - bounds.Min.X),
			Y: int(bounds.Max.Y - bounds.Min.Y),
		}
		// Layout widget
		w.layout(gtx, th, size, rad)
		// Retore previous state
		state.Load()
	}
	return layout.Dimensions{Size: max}
}
