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
	"image"
	"math"

	"gioui.org/f32"
	"gioui.org/layout"
	"gioui.org/op"
	"gioui.org/op/clip"
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

	// Map of entityID -> widget
	widgets map[string]Widget
	// Current scale
	scale float32
}

// Widget is to be implemented by all positioned entities that
// are to be made visible on the canvas.
type Widget interface {
	// Return the bounds of the widget on the canvas
	GetBounds() f32.Rectangle
	// Returns rotation of entity in degrees
	GetRotation() int
	// Layout must be initialized to a layout function to draw the widget
	// and process events.
	Layout(layout.Context, *material.Theme)
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
		ec.widgets = make(map[string]Widget)
	}
	// Ensure we have all widgets
	maxSize := ec.GetMaxSize()
	foundIDs := make(map[string]struct{})
	ec.Entities(func(entity model.PositionedEntity) {
		id := entity.GetID()
		foundIDs[id] = struct{}{}
		w, found := ec.widgets[id]
		if !found {
			// Build a new widget
			raw := entity.Accept(ec.Builder)
			var ok bool
			w, ok = raw.(Widget)
			if !ok {
				return
			}
			ec.widgets[id] = w
		}
		bounds := w.GetBounds()
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

// Layout generates the UI and processes events.
func (ec *EntityCanvas) Layout(gtx layout.Context, th *material.Theme) layout.Dimensions {
	// Ensure we have all widgets
	max := gtx.Constraints.Max
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
	// Layout all widgets
	for _, w := range ec.widgets {
		// Save state
		state := op.Save(gtx.Ops)
		// Prepare transformation
		bounds := w.GetBounds()
		// Translate, scale & rotate
		rot := w.GetRotation()
		tr := f32.Affine2D{}
		rad := float32(rot) * (math.Pi / 180)
		tr = tr.Offset(bounds.Min)
		tr = tr.Rotate(bounds.Min, rad)
		tr = tr.Scale(f32.Point{}, f32.Point{X: scale, Y: scale})
		op.Affine(tr).Add(gtx.Ops)
		// Set clip rectangle
		clip.Rect{
			Max: image.Point{
				X: int(bounds.Max.X - bounds.Min.X),
				Y: int(bounds.Max.Y - bounds.Min.Y),
			},
		}.Add(gtx.Ops)
		// Layout widget
		w.Layout(gtx, th)
		// Retore previous state
		state.Load()
	}
	return layout.Dimensions{Size: max}
}
