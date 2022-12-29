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

import (
	"context"
	"fmt"
)

// Event is implemented by all Event types.
type Event interface {
	// Just to ensure we can identify events.
	implementsEvent()
	// Format the event for event logger
	LogFormat() []KeyValue
}

type KeyValue struct {
	Key   string
	Value string
}

// EventDispatcher is responsible for distributing events.
type EventDispatcher interface {
	// Send the given event to all interested receivers.
	Send(Event)
	// Subscribe to events.
	// To cancel the subscription, call the given cancel function.
	Subscribe(context.Context, func(Event)) context.CancelFunc
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

// Format the event for event logger
func (e ActualStateChangedEvent) LogFormat() []KeyValue {
	value := ""
	if x, ok := e.Property.(TypedActualProperty[any]); ok {
		value = fmt.Sprintf("%v", x.GetActual(context.Background()))
	}
	return []KeyValue{
		{"event", "actual-state-changed"},
		{"property", e.Property.GetName()},
		{"value", value},
		{"subject-id", e.Subject.GetID()},
		{"subject-description", e.Subject.GetDescription()},
	}
}

// RequestedStateChangedEvent is raised when a requested state of a property of an
// entity has changed.
type RequestedStateChangedEvent struct {
	// Subject holds the entity for which a requested state has changed
	Subject Entity
	// Property that has changed
	Property Property
}

// Implement Event interface
func (e RequestedStateChangedEvent) implementsEvent() {}

// Format the event for event logger
func (e RequestedStateChangedEvent) LogFormat() []KeyValue {
	return []KeyValue{
		{"event", "requested-state-changed"},
		{"property", e.Property.GetName()},
		{"subject-id", e.Subject.GetID()},
		{"subject-description", e.Subject.GetDescription()},
	}
}

// IdleChangedEvent is raised when the idle state of a commandstation has changed.
type IdleChangedEvent struct {
	// Subject holds the commandstation that has its idle state changed.
	Subject CommandStation
}

// Implement Event interface
func (e IdleChangedEvent) implementsEvent() {}

// Format the event for event logger
func (e IdleChangedEvent) LogFormat() []KeyValue {
	return []KeyValue{
		{"event", "idle-changed"},
		{"subject-id", e.Subject.GetID()},
		{"subject-description", e.Subject.GetDescription()},
	}
}

// UnexpectedSensorActivatedEvent is raised when a sensor is activated that wa not expected
// from the automatic control of the locs.
type UnexpectedSensorActivatedEvent struct {
	// Subject holds the sensor that was unexpectedly activated
	Subject Sensor
}

// Implement Event interface
func (e UnexpectedSensorActivatedEvent) implementsEvent() {}

// Format the event for event logger
func (e UnexpectedSensorActivatedEvent) LogFormat() []KeyValue {
	return []KeyValue{
		{"event", "unexpected-sensor-activated"},
		{"subject-id", e.Subject.GetID()},
		{"subject-description", e.Subject.GetDescription()},
	}
}

// UnknownBinkyNetLocalWorkerEvent is raised when a request is made for configuration
// of an unknown local worker on the Binky Net.
type UnknownBinkyNetLocalWorkerEvent struct {
	// Hardware ID of the unknown local worker
	HardwareID string
}

// Implement Event interface
func (e UnknownBinkyNetLocalWorkerEvent) implementsEvent() {}

// Format the event for event logger
func (e UnknownBinkyNetLocalWorkerEvent) LogFormat() []KeyValue {
	return []KeyValue{
		{"event", "unknown-binkynet-localworker"},
		{"hardward-id", e.HardwareID},
	}
}
