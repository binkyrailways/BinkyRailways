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

	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// BinkyNetConnection represents a connection from a BinkyNetObject to a BinkyNetDevice.
type binkyNetConnection struct {
	container *binkyNetConnectionSet
	binkyNetConnectionFields
}

type binkyNetConnectionFields struct {
	Key           api.ConnectionName              `xml:"Key,omitempty"`
	Pins          binkyNetConnectionPinList       `xml:"Pins"`
	Configuration binkyNetConnectionConfiguration `xml:"Configuration"`
}

// newBinkyNetConnection creates a new BinkyNetConnection
func newBinkyNetConnection(key api.ConnectionName) *binkyNetConnection {
	c := &binkyNetConnection{}
	c.Key = key
	c.Pins.SetContainer(c)
	c.Configuration.SetContainer(c)
	req, opt := key.ExpectedConfigurations()
	c.ensureConfiguration(append(req, opt...))
	return c
}

// UnmarshalXML unmarshals and connects the module.
func (c *binkyNetConnection) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&c.binkyNetConnectionFields, &start); err != nil {
		return err
	}
	c.Pins.SetContainer(c)
	c.Configuration.SetContainer(c)
	req, opt := c.Key.ExpectedConfigurations()
	c.ensureConfiguration(append(req, opt...))
	return nil
}

// SetContainer links this instance to its container
func (c *binkyNetConnection) SetContainer(container *binkyNetConnectionSet) {
	c.container = container
}

// Gets the containing local worker
func (c *binkyNetConnection) GetLocalWorker() model.BinkyNetLocalWorker {
	if c.container != nil {
		return c.container.GetLocalWorker()
	}
	return nil
}

// Gets the object this connection belongs to
func (c *binkyNetConnection) GetObject() model.BinkyNetObject {
	if c.container != nil {
		return c.container.GetObject()
	}
	return nil
}

// Key is specific to the type of device.
func (c *binkyNetConnection) GetKey() api.ConnectionName {
	return c.Key
}
func (c *binkyNetConnection) SetKey(value api.ConnectionName) error {
	c.Key = value
	return nil
}

// The pins of devices to connect to.
func (c *binkyNetConnection) GetPins() model.BinkyNetConnectionPinList {
	return &c.Pins
}

// Gets optional configuration for this connection.
func (c *binkyNetConnection) GetConfiguration() model.BinkyNetConnectionConfiguration {
	return &c.Configuration
}

// OnModified calls the parents modified callback
func (c *binkyNetConnection) OnModified() {
	if c.container != nil {
		c.container.OnModified()
	}
}

// ensureConfiguration ensures that a configuration entry exists for all of the given keys.
// Also remove all other keys with a default value.
func (c *binkyNetConnection) ensureConfiguration(keys []api.ConfigKey) {
	// Ensure values
	for _, key := range keys {
		if _, found := c.Configuration.Get(string(key)); !found {
			c.Configuration.Set(string(key), key.DefaultValue())
		}
	}
	// Remove empty unexpected configurations
	isExpected := func(key api.ConfigKey) bool {
		for _, x := range keys {
			if x == key {
				return true
			}
		}
		return false
	}
	for _, e := range c.Configuration.Data {
		key := api.ConfigKey(e.Key)
		if !isExpected(key) && key.DefaultValue() == e.Value {
			c.Configuration.Remove(e.Key)
		}
	}
}
