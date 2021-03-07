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
	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// BinkyNetConnection represents a connection from a BinkyNetObject to a BinkyNetDevice.
type binkyNetConnection struct {
	container *binkyNetConnectionSet

	Key           api.ConnectionName              `xml:"Key,omitempty"`
	Pins          binkyNetConnectionPinList       `xml:"Pins"`
	Configuration binkyNetConnectionConfiguration `xml:"Configuration"`
}

// SetContainer links this instance to its container
func (c *binkyNetConnection) SetContainer(container *binkyNetConnectionSet) {
	c.container = container
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
	c.Pins.onModified = c.OnModified
	return &c.Pins
}

// Gets optional configuration for this connection.
func (c *binkyNetConnection) GetConfiguration() model.BinkyNetConnectionConfiguration {
	c.Configuration.onModified = c.OnModified
	return &c.Configuration
}

// OnModified calls the parents modified callback
func (c *binkyNetConnection) OnModified() {
	if c.container != nil {
		c.container.OnModified()
	}
}
