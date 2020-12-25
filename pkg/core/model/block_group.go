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

// BlockGroup is a group of blocks that share a similar function.
type BlockGroup interface {
	ModuleEntity

	// The minimum number of locs that must be present in this group.
	// Locs cannot leave if that results in a lower number of locs in this group.
	GetMinimumLocsInGroup() int
	SetMinimumLocsInGroup(value int) error

	// The minimum number of locs that must be on the track before the <see cref="MinimumLocsInGroup"/> becomes active.
	GetMinimumLocsOnTrackForMinimumLocsInGroupStart() int
	SetMinimumLocsOnTrackForMinimumLocsInGroupStart(value int) error
}
