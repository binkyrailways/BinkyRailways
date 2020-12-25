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

// Loc is a single locomotive
type Loc interface {
	AddressEntity
	PersistentEntity
	// ImportableEntity

	// Get percentage of speed steps for the slowest speed of this loc.
	// Value between 1 and 100.
	GetSlowSpeed() int
	SetSlowSpeed(value int) error

	// Get percentage of speed steps for the medium speed of this loc.
	// Value between 1 and 100.
	GetMediumSpeed() int
	// Set percentage of speed steps for the medium speed of this loc.
	// Value between 1 and 100.
	SetMediumSpeed(value int) error

	// Get percentage of speed steps for the maximum speed of this loc.
	// Value between 1 and 100.
	GetMaximumSpeed() int
	// Set percentage of speed steps for the maximum speed of this loc.
	// Value between 1 and 100.
	SetMaximumSpeed(value int) error

	// Gets the number of speed steps supported by this loc.
	GetSpeedSteps() int
	// Sets the number of speed steps supported by this loc.
	SetSpeedSteps(value int) error

	/// <summary>
	/// Gets/sets the image of the given loc.
	/// </summary>
	/// <value>Null if there is no image.</value>
	/// <remarks>Image must be png, bmp, gif, jpg, wmf or emf</remarks>
	//Stream Image { get; set; }

	// Get if it is allowed for this loc to change direction.
	GetChangeDirection() ChangeDirection
	// Set if it is allowed for this loc to change direction.
	SetChangeDirection(value ChangeDirection) error

	/// Gets the names of all functions supported by this loc.
	//        ILocFunctions Functions { get; }

	// Gets the name of the person that owns this loc.
	GetOwner() string
	// Sets the name of the person that owns this loc.
	SetOwner(value string) error

	// Gets remarks (free text) about this loc.
	GetRemarks() string
	// Sets remarks (free text) about this loc.
	SetRemarks(value string) error
}
