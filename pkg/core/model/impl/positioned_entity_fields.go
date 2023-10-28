// Copyright 2020 Ewout Prangsma
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

package impl

import (
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type positionedEntityFields struct {
	onModified      func()
	positionChanged eventHandler
	X               int    `xml:"X"`
	Y               int    `xml:"Y"`
	Width           int    `xml:"Width"`
	Height          int    `xml:"Height"`
	Rotation        int    `xml:"Rotation"`
	Layer           string `xml:"Layer"`
	Locked          bool   `xml:"Locked"`
}

// Initialize the default position & size.
func (pe *positionedEntityFields) Initialize(defaultX, defaultY, defaultWidth, defaultHeight int, onModified func()) {
	pe.X = defaultX
	pe.Y = defaultY
	pe.Width = defaultWidth
	pe.Height = defaultHeight
}

/// The position or size of this entity has changed.
func (pe *positionedEntityFields) PositionChanged() model.EventHandler {
	return &pe.positionChanged
}

// Get horizontal left position (in pixels) of this entity.
func (pe *positionedEntityFields) GetX() int {
	return pe.X
}

// Set horizontal left position (in pixels) of this entity.
func (pe *positionedEntityFields) SetX(value int) error {
	if pe.X != value {
		pe.X = value
		pe.positionChanged.Invoke(pe)
		pe.OnModified()
	}
	return nil
}

// Get vertical top position (in pixels) of this entity.
func (pe *positionedEntityFields) GetY() int {
	return pe.Y
}

// Set vertical top position (in pixels) of this entity.
func (pe *positionedEntityFields) SetY(value int) error {
	if pe.Y != value {
		pe.Y = value
		pe.positionChanged.Invoke(pe)
		pe.OnModified()
	}
	return nil
}

// Get horizontal size (in pixels) of this entity.
func (pe *positionedEntityFields) GetWidth() int {
	return pe.Width
}

// Set horizontal size (in pixels) of this entity.
func (pe *positionedEntityFields) SetWidth(value int) error {
	if pe.Width != value {
		pe.Width = value
		pe.positionChanged.Invoke(pe)
		pe.OnModified()
	}
	return nil
}

// Get vertical size (in pixels) of this entity.
func (pe *positionedEntityFields) GetHeight() int {
	return pe.Height
}

// Set vertical size (in pixels) of this entity.
func (pe *positionedEntityFields) SetHeight(value int) error {
	if pe.Height != value {
		pe.Height = value
		pe.positionChanged.Invoke(pe)
		pe.OnModified()
	}
	return nil
}

// Get rotation in degrees of the content of this entity.
func (pe *positionedEntityFields) GetRotation() int {
	return pe.Rotation
}

// Set rotation in degrees of the content of this entity.
func (pe *positionedEntityFields) SetRotation(value int) error {
	if pe.Rotation != value {
		pe.Rotation = value
		pe.positionChanged.Invoke(pe)
		pe.OnModified()
	}
	return nil
}

// Get the layer this entity is on (if any).
func (pe *positionedEntityFields) GetLayer() string {
	return pe.Layer
}

// Set the layer this entity is on (empty for no layer).
func (pe *positionedEntityFields) SetLayer(value string) error {
	if pe.Layer != value {
		pe.Layer = value
		pe.positionChanged.Invoke(pe)
		pe.OnModified()
	}
	return nil
}

// Get locked. If set, the mouse will no longer move and/or resize this entity.
func (pe *positionedEntityFields) GetLocked() bool {
	return pe.Locked
}

// Set locked. If set, the mouse will no longer move and/or resize this entity.
func (pe *positionedEntityFields) SetLocked(value bool) error {
	if pe.Locked != value {
		pe.Locked = value
		pe.OnModified()
	}
	return nil
}

func (pe *positionedEntityFields) OnModified() {
	if pe.onModified != nil {
		pe.onModified()
	}
}
