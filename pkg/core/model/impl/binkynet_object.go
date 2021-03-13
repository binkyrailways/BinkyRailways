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

package impl

import (
	"encoding/xml"
	"fmt"

	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type binkyNetObject struct {
	container *binkyNetObjectSet
	binkyNetObjectFields
}

type binkyNetObjectFields struct {
	entity
	ObjectID    api.ObjectID          `xml:"ObjectID,omitempty"`
	Type        api.ObjectType        `xml:"Type,omitempty"`
	Connections binkyNetConnectionSet `xml:"Connections"`
}

var _ model.BinkyNetObject = &binkyNetObject{}

// newBinkyNetObject creates and initializes a new binky object.
func newBinkyNetObject() *binkyNetObject {
	o := &binkyNetObject{}
	o.EnsureID()
	o.Connections.SetContainer(o)
	return o
}

// UnmarshalXML unmarshals and connects the module.
func (o *binkyNetObject) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&o.binkyNetObjectFields, &start); err != nil {
		return err
	}
	o.Connections.SetContainer(o)
	return nil
}

// Gets the local worker this object belongs to
func (o *binkyNetObject) GetLocalWorker() model.BinkyNetLocalWorker {
	if o.container != nil {
		return o.container.GetLocalWorker()
	}
	return nil
}

// SetContainer links this instance to its container
func (o *binkyNetObject) SetContainer(container *binkyNetObjectSet) {
	o.container = container
}

// Accept a visit by the given visitor
func (o *binkyNetObject) Accept(v model.EntityVisitor) interface{} {
	return v.VisitBinkyNetObject(o)
}

// Gets the description of the entity
func (o *binkyNetObject) GetDescription() string {
	return fmt.Sprintf("%s, %s (%s)", o.GetObjectID(), o.GetObjectType(), o.GetID())
}

// ID of the object (equal to entity ID)
func (o *binkyNetObject) GetObjectID() api.ObjectID {
	return o.ObjectID
}
func (o *binkyNetObject) SetObjectID(value api.ObjectID) error {
	if o.ObjectID != value {
		o.ObjectID = value
		o.OnModified()
	}
	return nil
}

// Type of the object
func (o *binkyNetObject) GetObjectType() api.ObjectType {
	return o.Type
}
func (o *binkyNetObject) SetObjectType(value api.ObjectType) error {
	if o.Type != value {
		o.Type = value
		o.ensureConnectionsForType()
		o.OnModified()
	}
	return nil
}

// ensureConnectionsForType ensures that all expected connections exists.
func (o *binkyNetObject) ensureConnectionsForType() {
	// Ensure all expected connections are there and remove all unexpected (empty) connections.
	req, opt := o.GetObjectType().ExpectedConnections()
	o.Connections.ensureConnections(append(req, opt...))
}

// Connections to devices used by this object
// The keys used in this map are specific to the type of object.
func (o *binkyNetObject) GetConnections() model.BinkyNetConnectionSet {
	return &o.Connections
}

// OnModified triggers the modified function of the parent (if any)
func (o *binkyNetObject) OnModified() {
	if o.container != nil {
		o.container.OnModified()
	}
	o.entity.OnModified()
}
