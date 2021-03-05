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

package model

import (
	api "github.com/binkynet/BinkyNet/apis/v1"
)

// BinkyNetObject represents a logical object on the railway such as a switch.
type BinkyNetObject interface {
	Entity

	// ID of the object (equal to entity ID)
	GetObjectID() api.ObjectID

	// Type of the object
	GetObjectType() api.ObjectType
	SetObjectType(value api.ObjectType) error

	// Connections to devices used by this object
	// The keys used in this map are specific to the type of object.
	GetConnections() BinkyNetConnectionSet
}
