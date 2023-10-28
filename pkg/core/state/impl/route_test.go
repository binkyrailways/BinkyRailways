// Copyright 2022 Ewout Prangsma
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
	"context"
	"testing"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/storage"
	"github.com/rs/zerolog"
	"github.com/stretchr/testify/assert"
	"github.com/stretchr/testify/require"
)

// TestRouteLockink checks locking functions of a route
func TestRouteLockink(t *testing.T) {
	// Create empty railway
	ctx := context.Background()
	log := zerolog.Logger{}
	p := storage.NewPackage("test.brw")
	require.NotNil(t, p)
	r := p.GetRailway()
	require.NotNil(t, r)

	// Add a loc
	l1, err := p.AddNewLoc()
	assert.NoError(t, err)
	require.NotNil(t, l1)
	assert.NotEmpty(t, l1.GetID())
	l1Ref := r.GetLocs().Add(l1)
	assert.NotNil(t, l1Ref)
	assert.Equal(t, l1.GetID(), l1Ref.GetID())
	assert.NoError(t, l1.SetAddress(ctx, model.NewAddress(model.NewNetwork(model.AddressTypeDcc, ""), "136")))

	// Add another loc
	l2, err := p.AddNewLoc()
	assert.NoError(t, err)
	require.NotNil(t, l2)
	assert.NotEmpty(t, l2.GetID())
	l2Ref := r.GetLocs().Add(l2)
	assert.NotNil(t, l2Ref)
	assert.Equal(t, l2.GetID(), l2Ref.GetID())
	assert.NoError(t, l2.SetAddress(ctx, model.NewAddress(model.NewNetwork(model.AddressTypeDcc, ""), "137")))

	// Add a module
	m, err := p.AddNewModule()
	require.NoError(t, err)
	r.GetModules().Add(m)

	// Add switchs
	sw1 := m.GetJunctions().AddSwitch()
	sw1.SetAddress(ctx, model.NewAddress(model.NewNetwork(model.AddressTypeBinkyNet, ""), "M1/sw1"))
	sw2 := m.GetJunctions().AddSwitch()
	sw2.SetAddress(ctx, model.NewAddress(model.NewNetwork(model.AddressTypeBinkyNet, ""), "M1/sw2"))

	// Add blocks
	b1 := m.GetBlocks().AddNew()
	b2 := m.GetBlocks().AddNew()

	// Add route
	rt := m.GetRoutes().AddNew()
	assert.NoError(t, rt.GetCrossingJunctions().AddSwitch(sw1, model.SwitchDirectionOff))
	assert.NoError(t, rt.SetFrom(b1))
	assert.NoError(t, rt.SetTo(b2))

	// Create state
	rs, err := New(ctx, r, log, nil, nil, nil, true)
	assert.NoError(t, err)
	require.NotNil(t, rs)

	// Check virtual mode
	assert.True(t, rs.GetVirtualMode().GetEnabled())
	assert.False(t, rs.GetVirtualMode().GetAutoRun())

	// Get state of route & switches & locs
	sw1State, err := rs.GetJunction(sw1.GetID())
	assert.NoError(t, err)
	assert.NotNil(t, sw1State)
	sw2State, err := rs.GetJunction(sw2.GetID())
	assert.NoError(t, err)
	assert.NotNil(t, sw2State)
	rtState, err := rs.GetRoute(rt.GetID())
	assert.NoError(t, err)
	assert.NotNil(t, rtState)
	l1State, err := rs.GetLoc(l1.GetID())
	assert.NoError(t, err)
	assert.NotNil(t, l1State)
	l2State, err := rs.GetLoc(l2.GetID())
	assert.NoError(t, err)
	assert.NotNil(t, l2State)
	b1State, err := rs.GetBlock(b1.GetID())
	assert.NoError(t, err)
	assert.NotNil(t, b1State)
	b2State, err := rs.GetBlock(b2.GetID())
	assert.NoError(t, err)
	assert.NotNil(t, b2State)

	// Nothing must be locked
	assert.Nil(t, sw1State.GetLockedBy(ctx))
	assert.Nil(t, sw2State.GetLockedBy(ctx))
	assert.Nil(t, rtState.GetLockedBy(ctx))
	assert.Nil(t, b1State.GetLockedBy(ctx))
	assert.Nil(t, b2State.GetLockedBy(ctx))

	// Lock the route
	assert.NoError(t, rtState.Lock(ctx, l1State))

	// Route, blocks & sw1 must be locked
	assert.Equal(t, l1State, sw1State.GetLockedBy(ctx))
	assert.Nil(t, sw2State.GetLockedBy(ctx))
	assert.Equal(t, l1State, rtState.GetLockedBy(ctx))
	assert.Equal(t, l1State, b1State.GetLockedBy(ctx))
	assert.Equal(t, l1State, b2State.GetLockedBy(ctx))

	// Lock the route again for same loc (must succeed)
	assert.NoError(t, rtState.Lock(ctx, l1State))

	// Route, blocks & sw1 must still be locked
	assert.Equal(t, l1State, sw1State.GetLockedBy(ctx))
	assert.Nil(t, sw2State.GetLockedBy(ctx))
	assert.Equal(t, l1State, rtState.GetLockedBy(ctx))
	assert.Equal(t, l1State, b1State.GetLockedBy(ctx))
	assert.Equal(t, l1State, b2State.GetLockedBy(ctx))

	// Lock the route again for another loc (must fail)
	assert.Error(t, rtState.Lock(ctx, l2State))

	// Route, blocks & sw1 must still be locked
	assert.Equal(t, l1State, sw1State.GetLockedBy(ctx))
	assert.Nil(t, sw2State.GetLockedBy(ctx))
	assert.Equal(t, l1State, rtState.GetLockedBy(ctx))
	assert.Equal(t, l1State, b1State.GetLockedBy(ctx))
	assert.Equal(t, l1State, b2State.GetLockedBy(ctx))

	// Unlock the route
	rtState.Unlock(ctx, nil)

	// Route, blocks & sw1 must not be locked
	assert.Nil(t, sw1State.GetLockedBy(ctx))
	assert.Nil(t, sw2State.GetLockedBy(ctx))
	assert.Nil(t, rtState.GetLockedBy(ctx))
	assert.Nil(t, b1State.GetLockedBy(ctx))
	assert.Nil(t, b2State.GetLockedBy(ctx))

	// Lock the route again for another loc (must succeed)
	assert.NoError(t, rtState.Lock(ctx, l2State))

	// Route, blocks & sw1 must still be locked
	assert.Equal(t, l2State, sw1State.GetLockedBy(ctx))
	assert.Nil(t, sw2State.GetLockedBy(ctx))
	assert.Equal(t, l2State, rtState.GetLockedBy(ctx))
	assert.Equal(t, l2State, b1State.GetLockedBy(ctx))
	assert.Equal(t, l2State, b2State.GetLockedBy(ctx))

	// Unlock the route except b2
	rtState.Unlock(ctx, b2State)

	// Route, blocks & sw1 must not be locked
	assert.Nil(t, sw1State.GetLockedBy(ctx))
	assert.Nil(t, sw2State.GetLockedBy(ctx))
	assert.Nil(t, rtState.GetLockedBy(ctx))
	assert.Nil(t, b1State.GetLockedBy(ctx))
	assert.Equal(t, l2State, b2State.GetLockedBy(ctx))
}
