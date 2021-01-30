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

package model

// Module is an unbreakable part of an entire railway.
type Module interface {
	PersistentEntity
	ImportableEntity

	// Gets all blocks contained in this module.
	GetBlocks() BlockSet

	/// Gets all block groups contained in this module.
	GetBlockGroups() BlockGroupSet

	// Gets all edges of this module.
	GetEdges() EdgeSet

	// Gets all junctions contained in this module.
	GetJunctions() JunctionSet

	// Gets all sensors contained in this module.
	GetSensors() SensorSet

	// Gets all signals contained in this module.
	GetSignals() SignalSet

	// Gets all outputs contained in this module.
	GetOutputs() OutputSet

	// Gets all routes contained in this module.
	GetRoutes() RouteSet

	// Gets/sets the background image of the this module.
	// Null if there is no image.</value>
	// Image must be png, bmp, gif, jpg, wmf or emf.
	GetBackgroundImage() []byte
	SetBackgroundImage(value []byte) error

	// Gets the horizontal size (in pixels) of this entity.
	GetWidth() int

	// Gets the vertical size (in pixels) of this entity.
	GetHeight() int

	// Call the callback for each positioned item in the module
	ForEachPositionedEntity(cb func(PositionedEntity))
}
