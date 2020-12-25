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

// StationMode indicates if a block is part of a station.
type StationMode string

const (
	// StationModeAuto specifies to automatically decide if a block is a station
	StationModeAuto StationMode = "Auto"

	// StationModeAlways specifies that a block is always a station
	StationModeAlways StationMode = "Always"

	// StationModeNever specifies that a block is never a station
	StationModeNever StationMode = "Never"
)
