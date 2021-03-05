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

type binkyNetObject struct {
	onModified func()
	entity

	Type        api.ObjectType        `xml:"Type,omitempty"`
	Connections binkyNetConnectionSet `xml:"Connections"`
}

var _ model.BinkyNetObject = &binkyNetObject{}

// ID of the object (equal to entity ID)
func (o *binkyNetObject) GetObjectID() api.ObjectID {
	return api.ObjectID(o.GetID())
}

// Type of the object
func (o *binkyNetObject) GetObjectType() api.ObjectType {
	return o.Type
}
func (o *binkyNetObject) SetObjectType(value api.ObjectType) error {
	if o.Type != value {
		o.Type = value
		o.OnModified()
	}
	return nil
}

// Connections to devices used by this object
// The keys used in this map are specific to the type of object.
func (o *binkyNetObject) GetConnections() model.BinkyNetConnectionSet {
	o.Connections.onModified = o.OnModified
	return &o.Connections
}

// OnModified triggers the modified function of the parent (if any)
func (o *binkyNetObject) OnModified() {
	if o.onModified != nil {
		o.onModified()
	}
	o.entity.OnModified()
}
