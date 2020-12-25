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

package model

// PositionedEntity is an Entity with graphical position
type PositionedEntity interface {
	Entity

	/// The position or size of this entity has changed.
	PositionChanged() EventHandler

	// Get horizontal left position (in pixels) of this entity.
	GetX() int
	// Set horizontal left position (in pixels) of this entity.
	SetX(value int) error

	// Get vertical top position (in pixels) of this entity.
	GetY() int
	// Set vertical top position (in pixels) of this entity.
	SetY(value int) error

	// Get horizontal size (in pixels) of this entity.
	GetWidth() int
	// Set horizontal size (in pixels) of this entity.
	SetWidth(value int) error

	// Get vertical size (in pixels) of this entity.
	GetHeight() int
	// Set vertical size (in pixels) of this entity.
	SetHeight(value int) error

	// Get rotation in degrees of the content of this entity.
	GetRotation() int
	// Set rotation in degrees of the content of this entity.
	SetRotation(value int) error

	// Get locked. If set, the mouse will no longer move and/or resize this entity.
	GetLocked() bool
	// Set locked. If set, the mouse will no longer move and/or resize this entity.
	SetLocked(value bool) error
}
