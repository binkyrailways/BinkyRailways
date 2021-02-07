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

package state

// Event is implemented by all Event types.
type Event interface {
	// Just to ensure we can identify events.
	implementsEvent()
}

// EventDispatcher is responsible for distributing events.
type EventDispatcher interface {
	// Send the given event to all interested receivers.
	Send(Event)
}

// ActualStateChangedEvent is raised when an actual state of a property of an
// entity has changed.
type ActualStateChangedEvent struct {
	// Subject holds the entity for which an actual state has changed
	Subject Entity
	// Property that has changed
	Property ActualProperty
}

// Implement Event interface
func (e ActualStateChangedEvent) implementsEvent() {}

// RequestStateChangedEvent is raised when a requested state of a property of an
// entity has changed.
type RequestStateChangedEvent struct {
	// Subject holds the entity for which a requested state has changed
	Subject Entity
	// Property that has changed
	Property Property
}

// Implement Event interface
func (e RequestStateChangedEvent) implementsEvent() {}

// IdleChangedEvent is raised when the idle state of a commandstation has changed.
type IdleChangedEvent struct {
	// Subject holds the commandstation that has its idle state changed.
	Subject CommandStation
}

// Implement Event interface
func (e IdleChangedEvent) implementsEvent() {}
