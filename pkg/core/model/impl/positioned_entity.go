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

type positionedEntity struct {
	moduleEntity
	positionChanged eventHandler
	X               int  `xml:"X"`
	Y               int  `xml:"Y"`
	Width           int  `xml:"Width"`
	Height          int  `xml:"Height"`
	Rotation        int  `xml:"Rotation"`
	Locked          bool `xml:"Locked"`
}

const (
	defaultX  = 0
	defaultY  = 0
	minWidth  = 5
	minHeight = 5
)

// Initialize the default position & size.
func (pe *positionedEntity) Initialize(defaultWidth, defaultHeight int) {
	pe.X = defaultX
	pe.Y = defaultY
	pe.Width = defaultWidth
	pe.Height = defaultHeight
}

/// The position or size of this entity has changed.
func (pe *positionedEntity) PositionChanged() model.EventHandler {
	return &pe.positionChanged
}

// Get horizontal left position (in pixels) of this entity.
func (pe *positionedEntity) GetX() int {
	return pe.X
}

// Set horizontal left position (in pixels) of this entity.
func (pe *positionedEntity) SetX(value int) error {
	if pe.X != value {
		pe.X = value
		pe.positionChanged.Invoke(pe)
		pe.OnModified()
	}
	return nil
}

// Get vertical top position (in pixels) of this entity.
func (pe *positionedEntity) GetY() int {
	return pe.Y
}

// Set vertical top position (in pixels) of this entity.
func (pe *positionedEntity) SetY(value int) error {
	if pe.Y != value {
		pe.Y = value
		pe.positionChanged.Invoke(pe)
		pe.OnModified()
	}
	return nil
}

// Get horizontal size (in pixels) of this entity.
func (pe *positionedEntity) GetWidth() int {
	return pe.Width
}

// Set horizontal size (in pixels) of this entity.
func (pe *positionedEntity) SetWidth(value int) error {
	if pe.Width != value {
		pe.Width = value
		pe.positionChanged.Invoke(pe)
		pe.OnModified()
	}
	return nil
}

// Get vertical size (in pixels) of this entity.
func (pe *positionedEntity) GetHeight() int {
	return pe.Height
}

// Set vertical size (in pixels) of this entity.
func (pe *positionedEntity) SetHeight(value int) error {
	if pe.Height != value {
		pe.Height = value
		pe.positionChanged.Invoke(pe)
		pe.OnModified()
	}
	return nil
}

// Get rotation in degrees of the content of this entity.
func (pe *positionedEntity) GetRotation() int {
	return pe.Rotation
}

// Set rotation in degrees of the content of this entity.
func (pe *positionedEntity) SetRotation(value int) error {
	if pe.Rotation != value {
		pe.Rotation = value
		pe.positionChanged.Invoke(pe)
		pe.OnModified()
	}
	return nil
}

// Get locked. If set, the mouse will no longer move and/or resize this entity.
func (pe *positionedEntity) GetLocked() bool {
	return pe.Locked
}

// Set locked. If set, the mouse will no longer move and/or resize this entity.
func (pe *positionedEntity) SetLocked(value bool) error {
	if pe.Locked != value {
		pe.Locked = value
		pe.OnModified()
	}
	return nil
}
