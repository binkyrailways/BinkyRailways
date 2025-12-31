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
	"context"
	"encoding/xml"
	"fmt"
	"strings"

	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type binkyNetObject struct {
	container *binkyNetObjectSet
	binkyNetObjectFields
}

type binkyNetObjectFields struct {
	entity
	ObjectID         api.ObjectID                `xml:"ObjectID,omitempty"`
	Type             api.ObjectType              `xml:"Type,omitempty"`
	Connections      binkyNetConnectionSet       `xml:"Connections"`
	Configuration    binkyNetObjectConfiguration `xml:"Configuration"`
	UseGlobalAddress bool                        `xml:"UseGlobalAddress,omitempty"`
}

var _ model.BinkyNetObject = &binkyNetObject{}

// newBinkyNetObject creates and initializes a new binky object.
func newBinkyNetObject() *binkyNetObject {
	o := &binkyNetObject{}
	o.EnsureID()
	o.Connections.SetContainer(o)
	o.Configuration.SetContainer(o)
	return o
}

// UnmarshalXML unmarshals and connects the module.
func (o *binkyNetObject) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&o.binkyNetObjectFields, &start); err != nil {
		return err
	}
	o.Connections.SetContainer(o)
	o.Configuration.SetContainer(o)
	opt := o.Type.ExpectedConfigurations()
	o.ensureConfiguration(opt)
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
	if o.GetObjectID() == "" && o.GetObjectType() == "" {
		return o.GetID()
	}
	return fmt.Sprintf("%s, %s", o.GetObjectID(), o.GetObjectType())
}

// ID of the object (equal to entity ID)
func (o *binkyNetObject) GetObjectID() api.ObjectID {
	return o.ObjectID
}
func (o *binkyNetObject) SetObjectID(ctx context.Context, value api.ObjectID) error {
	if o.ObjectID != value {
		oldValue := o.ObjectID
		o.ObjectID = value
		o.OnModified()

		// Update addresses using this object
		if lw := o.GetLocalWorker(); lw != nil {
			if cs := lw.GetCommandStation(); cs != nil {
				if rw := cs.GetPackage().GetRailway(); rw != nil {
					if rw, ok := rw.(Entity); ok {
						oldAddr := lw.GetAlias() + "/" + string(oldValue)
						newAddr := lw.GetAlias() + "/" + string(value)
						fmt.Printf("bnObject: %s -> %s\n", oldAddr, newAddr)
						rw.ForEachAddress(func(addr model.Address, onUpdate func(context.Context, model.Address) error) {
							if addr.Network.Type == model.AddressTypeBinkyNet {
								if addr.Value == oldAddr {
									fmt.Printf("bnObject updating: %s -> %s\n", oldAddr, newAddr)
									addr.Value = newAddr
									onUpdate(ctx, addr)
								}
							}
						})
					}
				}
			}
		}
	}
	return nil
}

// Type of the object
func (o *binkyNetObject) GetObjectType() api.ObjectType {
	return o.Type
}
func (o *binkyNetObject) SetObjectType(ctx context.Context, value api.ObjectType) error {
	if o.Type != value {
		o.Type = value
		opt := o.Type.ExpectedConfigurations()
		o.ensureConfiguration(opt)
		o.ensureConnectionsForType()
		o.OnModified()
	}
	return nil
}

// If set, use global address instead of module local address.
func (o *binkyNetObject) GetUseGlobalAddress() bool {
	return o.UseGlobalAddress
}
func (o *binkyNetObject) SetUseGlobalAddress(ctx context.Context, value bool) error {
	if o.UseGlobalAddress != value {
		o.UseGlobalAddress = value
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

// Gets optional configuration for this object.
func (o *binkyNetObject) GetConfiguration() model.BinkyNetObjectConfiguration {
	return &o.Configuration
}

// ensureConfiguration ensures that a configuration entry exists for all of the given keys.
// Also remove all other keys with a default value.
func (o *binkyNetObject) ensureConfiguration(keys []api.ObjectConfigKey) {
	// Ensure values
	for _, key := range keys {
		if _, found := o.Configuration.Get(string(key)); !found {
			o.Configuration.Set(string(key), key.DefaultValue())
		}
	}
	// Remove empty unexpected configurations
	isExpected := func(key api.ObjectConfigKey) bool {
		for _, x := range keys {
			if x == key {
				return true
			}
		}
		return false
	}
	for _, e := range o.Configuration.Data {
		key := api.ObjectConfigKey(e.Key)
		if !isExpected(key) && key.DefaultValue() == e.Value {
			o.Configuration.Remove(e.Key)
		}
	}
}

// OnModified triggers the modified function of the parent (if any)
func (o *binkyNetObject) OnModified() {
	if o.container != nil {
		o.container.OnModified()
	}
	o.entity.OnModified()
}

// Gets the MQTT state topic to use for the connection with given name on this object
func (o *binkyNetObject) getMQTTPrefix() string {
	alias := strings.ToLower(o.GetLocalWorker().GetAlias())
	if o.UseGlobalAddress {
		alias = "GLOBAL"
	}
	return "/binky/" + alias + "/" + strings.ToLower(string(o.GetObjectID())) + "/"
}

// Gets the MQTT state topic to use for the connection with given name on this object
func (o *binkyNetObject) GetMQTTStateTopic(connName api.ConnectionName) string {
	return o.getMQTTPrefix() + strings.ToLower(string(connName)+"/state")
}

// Gets the MQTT command topic to use for the connection with given name on this object
func (o *binkyNetObject) GetMQTTCommandTopic(connName api.ConnectionName) string {
	return o.getMQTTPrefix() + strings.ToLower(string(connName)+"/command")
}
