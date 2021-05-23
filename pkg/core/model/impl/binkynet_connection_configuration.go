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

import "sort"

type binkyNetConnectionConfiguration struct {
	container *binkyNetConnection

	Data []binkyNetConnectionConfigurationEntry `xml:"Data,omitempty"`
}

type binkyNetConnectionConfigurationEntry struct {
	Key   string `xml:"Key,omitempty"`
	Value string `xml:"Value,omitempty"`
}

// SetContainer links this instance to its container
func (c *binkyNetConnectionConfiguration) SetContainer(container *binkyNetConnection) {
	c.container = container
}

// Get number of entries
func (c *binkyNetConnectionConfiguration) GetCount() int {
	return len(c.Data)
}

// Get value by key
func (c *binkyNetConnectionConfiguration) Get(key string) (string, bool) {
	for _, e := range c.Data {
		if e.Key == key {
			return e.Value, true
		}
	}
	return "", false
}

// Invoke the callback for configuration key/value pair.
func (c *binkyNetConnectionConfiguration) ForEach(cb func(key, value string)) {
	if len(c.Data) == 0 {
		return
	}
	for _, e := range c.Data {
		cb(e.Key, e.Value)
	}
}

// Remove the value for the given key.
// Returns true if it was removed, false otherwise
func (c *binkyNetConnectionConfiguration) Remove(key string) bool {
	for i, e := range c.Data {
		if e.Key == key {
			c.Data = append(c.Data[:i], c.Data[i+1:]...)
			c.OnModified()
			return true
		}
	}
	return false
}

// Set a given key/value pair
func (c *binkyNetConnectionConfiguration) Set(key, value string) {
	for i, e := range c.Data {
		if e.Key == key {
			c.Data[i].Value = value
			c.OnModified()
			return
		}
	}
	// Not found, append
	c.Data = append(c.Data, binkyNetConnectionConfigurationEntry{
		Key:   key,
		Value: value,
	})
	sort.Slice(c.Data, func(i, j int) bool {
		return c.Data[i].Key < c.Data[j].Key
	})
	c.OnModified()
}

// OnModified triggers the modified function of the parent (if any)
func (c *binkyNetConnectionConfiguration) OnModified() {
	if c.container != nil {
		c.container.OnModified()
	}
}
