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
	"encoding/xml"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// RouteEvent adds implementation methods to model.RouteEvent
type RouteEvent interface {
	ModuleEntity
	model.RouteEvent
}

type routeEvent struct {
	routeEventFields
}

type routeEventFields struct {
	moduleEntity

	SensorID  SensorRef              `xml:"Junction,omitempty"` // Not the wrong element name. This is for historical reasons
	Behaviors routeEventBehaviorList `xml:"Behaviors"`
}

// SetModule initialize the entity
func (re *routeEventFields) SetRouteEvent(value *routeEvent) {
	re.moduleEntity.SetContainer(value)
	re.Behaviors.SetContainer(value)
}

var _ RouteEvent = &routeEvent{}

// newRouteEvent creates a new routeEvent instance.
func newRouteEvent(sensor model.Sensor) *routeEvent {
	re := &routeEvent{}
	re.routeEventFields.SetContainer(re)
	re.SensorID = SensorRef(sensor.GetID())
	return re
}

// Accept a visit by the given visitor
func (re *routeEvent) Accept(v model.EntityVisitor) interface{} {
	return v.VisitRouteEvent(re)
}

// UnmarshalXML unmarshals any persistent entity.
func (re *routeEvent) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&re.routeEventFields, &start); err != nil {
		return err
	}
	re.routeEventFields.SetContainer(re)
	return nil
}

// Sensor that triggers this event
func (re *routeEvent) GetSensor() model.Sensor {
	result, _ := re.SensorID.Get(re.GetModule())
	return result
}

// Gets the list of behaviors to choose from.
// The first matching behavior is used.
func (re *routeEvent) GetBehaviors() model.RouteEventBehaviorList {
	return &re.Behaviors
}
