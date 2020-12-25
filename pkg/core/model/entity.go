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

// Entity is the base interface for railway objects.
type Entity interface {
	// A property of this entity has changed.
	PropertyChanged() EventHandler

	// Get the Identification value.
	GetID() string
	// Set the Identification value. Must be unique within it's context.
	SetID(value string) error

	// Get human readable description
	GetDescription() string
	// Get human readable description
	SetDescription(value string) error

	// Does this entity generate it's own description?
	HasAutomaticDescription() bool

	// Human readable name of this type of entity.
	GetTypeName() string
}
