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

type binkyNetConnectionConfiguration struct {
	container *binkyNetConnection

	Data map[string]string `xml:"Data,omitempty"`
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
	result, found := c.Data[key]
	return result, found
}

// Invoke the callback for configuration key/value pair.
func (c *binkyNetConnectionConfiguration) ForEach(cb func(key, value string)) {
	for k, v := range c.Data {
		cb(k, v)
	}
}

// Remove the value for the given key.
// Returns true if it was removed, false otherwise
func (c *binkyNetConnectionConfiguration) Remove(key string) bool {
	if _, found := c.Data[key]; !found {
		return false
	}
	delete(c.Data, key)
	c.OnModified()
	return true
}

// Set a given key/value pair
func (c *binkyNetConnectionConfiguration) Set(key, value string) {
	current, found := c.Get(key)
	if !found || current != value {
		if c.Data == nil {
			c.Data = make(map[string]string)
		}
		c.Data[key] = value
		c.OnModified()
	}
}

// OnModified triggers the modified function of the parent (if any)
func (c *binkyNetConnectionConfiguration) OnModified() {
	if c.container != nil {
		c.container.OnModified()
	}
}
