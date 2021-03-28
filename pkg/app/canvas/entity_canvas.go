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
	"context"
	"fmt"
	"image"
	"math"

	"gioui.org/f32"
	"gioui.org/gesture"
	"gioui.org/io/pointer"
	"gioui.org/layout"
	"gioui.org/op"
	"gioui.org/op/clip"
	"gioui.org/widget"
	"gioui.org/widget/material"
	"github.com/binkyrailways/BinkyRailways/pkg/core/util"
)

// Entity represents a distinct resource on the canvas.
type Entity interface {
	// Get the ID of the resource
	GetID() string
}

// MoveableEntity represents an entity that can be moved.
type MoveableEntity interface {
	Entity

	// Gets the X coordinate
	GetX() int
	// Gets the Y coordinate
	GetY() int
	// Sets the X coordinate
	SetX(int) error
	// Sets the Y coordinate
	SetY(int) error
}

// WidgetBuilder abstracts the creation of a widget for an Entity.
type WidgetBuilder interface {
	// Create a widget for the given entity.
	// A return value of nil means no widget.
	CreateWidget(Entity) Widget
}

// EntityIterator defines an iterator function for positioned entities
type EntityIterator = func(func(entity Entity))

// WidgetTransformer is used to transform widgets
type WidgetTransformer = func(entity Entity, tx f32.Affine2D) f32.Affine2D

// EntityCanvas shows positioned entities in a 2D view.
type EntityCanvas struct {
	// Exclusive to claim when painting
	Exclusive util.Exclusive
	// Return the maximum size of the canvas
	GetMaxSize func() f32.Point
	// Entities must be set to an iterator function that yields
	// all entities that must be displayed in the canvas.
	Entities EntityIterator
	// Transformer allows widgets to be replaced.
	Transformer WidgetTransformer
	// Builder must be set to an entity visitor that builds Widgets.
	Builder WidgetBuilder
	// If set, OnSelect is called when the given entity is selected.
	OnSelect func(Entity)

	// Background widget
	background []Widget
	// Map of entityID -> widget
	widgets map[string]*canvasWidget
	// Current scale
	scale float32
	// Current selection
	selection Entity
	// Click on background
	widget.Clickable
}

// Widget is to be implemented by all positioned entities that
// are to be made visible on the canvas.
type Widget interface {
	// Return a matrix for drawing the widget in its proper orientation and
	// the size of the area it is drawing into.
	// Returns: Matrix, Size, Rotation (in radials, already applied in Matrix)
	GetAffineAndSize() (f32.Affine2D, f32.Point, float32)
	// Layout draws the widget and process events.
	Layout(ctx context.Context, gtx layout.Context, size image.Point, th *material.Theme, state WidgetState)
}

// WidgetState describes the current state of the widget
type WidgetState struct {
	// Mouse hovers over the widget
	Hovered bool
	// Widget has been clicked on
	Clicked bool
	// Widget is currently pressed on
	Pressed bool
}

// canvasWidget is a widget representation of the object on the canvas.
type canvasWidget struct {
	entity Entity
	gesture.Drag
	gesture.Click
	widget        Widget
	origEntityPos image.Point
	startPt       f32.Point
	clicked       bool
}

// Clicked returns true if the widget has been clicked on and reset the clicked flag.
func (cw *canvasWidget) Clicked() bool {
	result := cw.clicked
	cw.clicked = false
	return result
}

// Clicked returns true if the widget has been clicked on and reset the clicked flag.
func (cw *canvasWidget) Pressed() bool {
	result := cw.Click.Pressed()
	return result
}

// Layout draws the widget and process events.
func (cw *canvasWidget) layout(ctx context.Context, gtx layout.Context, th *material.Theme, size image.Point, rad float32) {
	// Process events
	for _, evt := range cw.Click.Events(gtx) {
		switch evt.Type {
		case gesture.TypeClick:
			cw.clicked = true
		}
	}
	for _, evt := range cw.Drag.Events(gtx.Metric, gtx, gesture.Both) {
		pt := f32.Affine2D{}.Rotate(f32.Point{}, -rad).Transform(evt.Position)
		switch evt.Type {
		case pointer.Press:
			if mentity, ok := cw.entity.(MoveableEntity); ok {
				cw.origEntityPos = image.Point{X: mentity.GetX(), Y: mentity.GetY()}
				cw.startPt = pt
			}
		case pointer.Drag:
			if mentity, ok := cw.entity.(MoveableEntity); ok {
				delta := pt.Sub(cw.startPt)
				newEntityPos := f32.Pt(float32(cw.origEntityPos.X), float32(cw.origEntityPos.Y)).Add(delta)
				fmt.Println(pt, gtx.Constraints.Min, gtx.Constraints.Max, cw.origEntityPos, delta, newEntityPos)
				mentity.SetX(int(newEntityPos.X))
				mentity.SetY(int(newEntityPos.Y))
			}
		case pointer.Cancel:
			if mentity, ok := cw.entity.(MoveableEntity); ok {
				mentity.SetX(cw.origEntityPos.X)
				mentity.SetY(cw.origEntityPos.Y)
			}
		}
	}

	// Constraints paint area
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
	wState := WidgetState{
		Hovered: cw.Click.Hovered(),
		Clicked: cw.Clicked(),
		Pressed: cw.Pressed(),
	}

	// Layout actual widget
	state := op.Save(gtx.Ops)
	clip.Rect{
		Max: size,
	}.Add(gtx.Ops)
	cw.widget.Layout(ctx, gtx, size, th, wState)
	state.Load()

	// Draw selection overlay (if needed)
	if wState.Hovered {
		clip.Stroke{
			Path:  clip.UniformRRect(f32.Rectangle{Max: layout.FPt(size)}, 0).Path(gtx.Ops),
			Style: clip.StrokeStyle{Width: 1},
		}.Op().Add(gtx.Ops)
	}
}

// SetBackground stores a background widget
func (ec *EntityCanvas) SetBackground(background ...Widget) {
	ec.background = background
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
func (ec *EntityCanvas) rebuildWidgets(tranformer WidgetTransformer) ([]*canvasWidget, f32.Point) {
	if ec.widgets == nil {
		ec.widgets = make(map[string]*canvasWidget)
	}
	// Ensure we have all widgets
	maxSize := ec.GetMaxSize()
	foundIDs := make(map[string]struct{})
	minCap := len(ec.widgets) + 64 + len(ec.background)
	list := make([]*canvasWidget, 0, minCap)
	for _, bg := range ec.background {
		list = append(list, &canvasWidget{
			entity: nil,
			widget: bg,
		})
	}
	ec.Entities(func(entity Entity) {
		id := entity.GetID()
		foundIDs[id] = struct{}{}
		entry, found := ec.widgets[id]
		if !found {
			// Build a new widget
			w := ec.Builder.CreateWidget(entity)
			if w == nil {
				return
			}
			entry = &canvasWidget{
				entity: entity,
				widget: w,
			}
			ec.widgets[id] = entry
		}
		_, sz, _ := entry.widget.GetAffineAndSize()
		if sz.X > maxSize.X {
			maxSize.X = sz.X
		}
		if sz.Y > maxSize.Y {
			maxSize.Y = sz.Y
		}
		list = append(list, entry)
	})
	// Ensure we do not have too much widgets
	for id := range ec.widgets {
		if _, found := foundIDs[id]; !found {
			delete(ec.widgets, id)
		}
	}
	return list, maxSize
}

// GetSelection returns the current selection (if any)
func (ec *EntityCanvas) GetSelection() Entity {
	return ec.selection
}

// Select sets the selected entity
func (ec *EntityCanvas) Select(entity Entity) {
	if ec.selection != entity {
		ec.selection = entity
		if ec.OnSelect != nil {
			ec.OnSelect(entity)
		}
	}
}

// Layout generates the UI and processes events.
func (ec *EntityCanvas) Layout(gtx layout.Context, th *material.Theme) layout.Dimensions {
	if ec.Exclusive == nil {
		// No exclusive configured
		return ec.layout(context.Background(), gtx, th)
	}
	// Run exclusively
	var result layout.Dimensions
	ec.Exclusive.Exclusive(context.Background(), func(ctx context.Context) error {
		result = ec.layout(ctx, gtx, th)
		return nil
	})
	return result
}

// Layout generates the UI and processes events.
func (ec *EntityCanvas) layout(ctx context.Context, gtx layout.Context, th *material.Theme) layout.Dimensions {
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
	transformer := ec.Transformer
	if transformer == nil {
		transformer = func(entity Entity, tx f32.Affine2D) f32.Affine2D {
			return tx
		}
	}
	widgets, maxSize := ec.rebuildWidgets(transformer)
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
	// Layout all widgets
	for _, w := range widgets {
		// Save state
		state := op.Save(gtx.Ops)
		// Prepare transformation & size
		tr, size, rad := w.widget.GetAffineAndSize()
		tr = transformer(w.entity, tr)
		tr = tr.Scale(f32.Point{}, f32.Point{X: scale, Y: scale})
		op.Affine(tr).Add(gtx.Ops)
		// Layout widget
		sz := image.Point{
			X: int(math.Ceil(float64(size.X))),
			Y: int(math.Ceil(float64(size.Y))),
		}
		w.layout(ctx, gtx, th, sz, rad)
		// Retore previous state
		state.Load()
	}
	return layout.Dimensions{Size: max}
}
