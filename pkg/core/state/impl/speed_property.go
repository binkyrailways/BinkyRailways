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
	"math"

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// speedProperty implements the speed using given speed in steps
type speedProperty struct {
	loc Loc
}

var _ state.IntProperty = &speedProperty{}

// Gets / sets the actual value
func (sp *speedProperty) GetActual() int {
	return sp.convertFromSpeedSteps(sp.loc.GetSpeedInSteps().GetActual())
}
func (sp *speedProperty) SetActual(value int) error {
	return sp.loc.GetSpeedInSteps().SetActual(sp.convertToSpeedSteps(value))
}

// Gets / sets the requested value
func (sp *speedProperty) GetRequested() int {
	return sp.convertFromSpeedSteps(sp.loc.GetSpeedInSteps().GetRequested())
}
func (sp *speedProperty) SetRequested(value int) error {
	return sp.loc.GetSpeedInSteps().SetRequested(sp.convertToSpeedSteps(value))
}

func (sp *speedProperty) IsConsistent() bool {
	return sp.loc.GetSpeedInSteps().IsConsistent()
}

// Convert a speed percentage to speed steps
func (sp *speedProperty) convertToSpeedSteps(percentage int) int {
	speedSteps := sp.loc.GetSpeedSteps()
	p := math.Max(0, math.Min(100, float64(percentage))) / 100.0
	return int(math.Round(p * float64(speedSteps-1)))
}

// Convert a speed steps value to a percentage value
func (sp *speedProperty) convertFromSpeedSteps(steps int) int {
	speedSteps := sp.loc.GetSpeedSteps()
	maxSteps := float64(speedSteps - 1)
	p := math.Max(0, math.Min(maxSteps, float64(steps))) / maxSteps
	return int(math.Round(p * 100))
}
