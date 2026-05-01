// Copyright 2024 Ewout Prangsma
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
	"testing"

	"github.com/stretchr/testify/assert"
)

func TestRouteRailPoints(t *testing.T) {
	m := NewModule(nil)
	rp1 := m.GetRailPoints().AddNew()
	rp2 := m.GetRailPoints().AddNew()
	r := m.GetRoutes().AddNew()

	// Initial state
	assert.Equal(t, 0, r.GetRailPoints().GetCount())

	// Add points
	err := r.GetRailPoints().Add(rp1)
	assert.NoError(t, err)
	assert.Equal(t, 1, r.GetRailPoints().GetCount())
	assert.True(t, r.GetRailPoints().Contains(rp1))

	err = r.GetRailPoints().Add(rp2)
	assert.NoError(t, err)
	assert.Equal(t, 2, r.GetRailPoints().GetCount())
	
	// Check order
	p1, ok := r.GetRailPoints().Get(0)
	assert.True(t, ok)
	assert.Equal(t, rp1.GetID(), p1.GetID())

	p2, ok := r.GetRailPoints().Get(1)
	assert.True(t, ok)
	assert.Equal(t, rp2.GetID(), p2.GetID())

	// Remove point from module
	m.GetRailPoints().Remove(rp1)
	assert.Equal(t, 1, r.GetRailPoints().GetCount(), "Point should be removed from route when removed from module")
	p, ok := r.GetRailPoints().Get(0)
	assert.True(t, ok)
	assert.Equal(t, rp2.GetID(), p.GetID())
}
