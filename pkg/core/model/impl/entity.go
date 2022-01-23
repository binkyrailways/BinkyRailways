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
	"context"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/dchest/uniuri"
)

// Entity adds implementation methods to a model Entity.
type Entity interface {
	model.Entity

	// ForEachAddress iterates all addresses in this entity and any child entities.
	ForEachAddress(cb func(addr model.Address, onUpdate func(context.Context, model.Address) error))
}

type entity struct {
	propertyChanged eventHandler
	ID              string `xml:"Id"`
	Description     string `xml:"Description"`
}

var _ Entity = &entity{}

// NewID creates a new random ID
func NewID() string {
	return uniuri.New()
}

// Accept a visit by the given visitor
func (e *entity) Accept(v model.EntityVisitor) interface{} {
	panic("Override me")
}

// ForEachAddress iterates all addresses in this entity and any child entities.
func (e *entity) ForEachAddress(cb func(addr model.Address, onUpdate func(context.Context, model.Address) error)) {
	// Override me if the entity has one or more addresses
}

// EnsureID sets a unique ID if needed
func (e *entity) EnsureID() {
	if e.ID == "" {
		e.ID = NewID()
	}
}

// OnModified fires the propertychanged callbacks
func (e *entity) OnModified() {
	e.propertyChanged.Invoke(e)
}

// A property of this entity has changed.
func (e *entity) PropertyChanged() model.EventHandler {
	return &e.propertyChanged
}

// Get the Identification value.
func (e *entity) GetID() string {
	if e == nil {
		return ""
	}
	return e.ID
}

// Set the Identification value. Must be unique within it's context.
func (e *entity) SetID(value string) error {
	if e.ID != value {
		e.ID = value
		e.OnModified()
	}
	return nil
}

// Get human readable description
func (e *entity) GetDescription() string {
	if e == nil {
		return ""
	}
	return e.Description
}

// Get human readable description
func (e *entity) SetDescription(value string) error {
	if e.Description != value {
		e.Description = value
		e.OnModified()
	}
	return nil
}

// Does this entity generate it's own description?
func (e *entity) HasAutomaticDescription() bool {
	return false
}

// Human readable name of this type of entity.
func (e *entity) GetTypeName() string {
	panic("Not implemented")
}
