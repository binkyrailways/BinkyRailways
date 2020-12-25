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

package impl

import (
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type blockGroup struct {
	moduleEntity

	MinimumLocsInGroup                           int `xml:"MinimumLocsInGroup"`
	MinimumLocsOnTrackForMinimumLocsInGroupStart int `xml:"MinimumLocsOnTrackForMinimumLocsInGroupStart"`
}

var _ model.BlockGroup = &blockGroup{}

// newBlockGroup initialize a new block group
func newBlockGroup() *blockGroup {
	b := &blockGroup{
		MinimumLocsInGroup:                           model.DefaultBlockGroupMinimumLocsInGroup,
		MinimumLocsOnTrackForMinimumLocsInGroupStart: model.DefaultBlockGroupMinimumLocsOnTrackForMinimumLocsInGroupStart,
	}
	return b
}

// The minimum number of locs that must be present in this group.
// Locs cannot leave if that results in a lower number of locs in this group.
func (bg *blockGroup) GetMinimumLocsInGroup() int {
	return bg.MinimumLocsInGroup
}
func (bg *blockGroup) SetMinimumLocsInGroup(value int) error {
	if bg.MinimumLocsInGroup != value {
		bg.MinimumLocsInGroup = value
		bg.OnModified()
	}
	return nil

}

// The minimum number of locs that must be on the track before the <see cref="MinimumLocsInGroup"/> becomes active.
func (bg *blockGroup) GetMinimumLocsOnTrackForMinimumLocsInGroupStart() int {
	return bg.MinimumLocsOnTrackForMinimumLocsInGroupStart
}
func (bg *blockGroup) SetMinimumLocsOnTrackForMinimumLocsInGroupStart(value int) error {
	if bg.MinimumLocsOnTrackForMinimumLocsInGroupStart != value {
		bg.MinimumLocsOnTrackForMinimumLocsInGroupStart = value
		bg.OnModified()
	}
	return nil

}
