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
	"context"
	"testing"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
	"github.com/binkyrailways/BinkyRailways/pkg/core/storage"
	"github.com/rs/zerolog"
	"github.com/stretchr/testify/assert"
	"github.com/stretchr/testify/require"
)

// TestRailwayFromFile tests the construction on of a new Railway state.
func TestRailwayFromFile(t *testing.T) {
	// Open package
	ctx := context.Background()
	log := zerolog.Logger{}
	p, err := storage.NewPackageFromFile("../../../../Fixtures/tzl.brw")
	require.NoError(t, err)
	require.NotNil(t, p)

	// Create state in non-virtual mode
	t.Run("non-virtual mode", func(t *testing.T) {
		st, err := New(ctx, p.GetRailway(), log, nil, nil, false)
		require.NoError(t, err)
		require.NotNil(t, st)
	})

	// Create state in virtual mode
	t.Run("virtual mode", func(t *testing.T) {
		st, err := New(ctx, p.GetRailway(), log, nil, nil, true)
		require.NoError(t, err)
		require.NotNil(t, st)
	})
}

// TestRailwayVirtualMode checks a railway state in virtual mode
func TestRailwayVirtualMode(t *testing.T) {
	// Create empty railway
	ctx := context.Background()
	log := zerolog.Logger{}
	p := storage.NewPackage("test.brw")
	require.NotNil(t, p)
	r := p.GetRailway()
	require.NotNil(t, r)

	// Add a loc
	l, err := p.AddNewLoc()
	assert.NoError(t, err)
	require.NotNil(t, l)
	assert.NotEmpty(t, l.GetID())
	lref := r.GetLocs().Add(l)
	assert.NotNil(t, lref)
	assert.Equal(t, l.GetID(), lref.GetID())
	assert.NoError(t, l.SetAddress(model.NewAddress(model.NewNetwork(model.AddressTypeDcc, ""), "136")))

	// Create state
	rs, err := New(ctx, r, log, nil, nil, true)
	assert.NoError(t, err)
	require.NotNil(t, rs)

	// Check virtual mode
	assert.True(t, rs.GetVirtualMode().GetEnabled())
	assert.False(t, rs.GetVirtualMode().GetAutoRun())

	// Check command station
	csCount := 0
	rs.ForEachCommandStation(func(cs state.CommandStation) {
		csCount++
		assert.IsType(t, &virtualCommandStation{}, cs)
		assert.NotEmpty(t, cs.GetID())
		assert.NotEmpty(t, cs.GetDescription())
	})
	assert.Equal(t, 1, csCount)

	// Check loc
	rs.ForEachLoc(func(ls state.Loc) {
		assert.Equal(t, l.GetID(), ls.GetID())
		assert.Equal(t, "DCC 136", ls.GetAddress(ctx).String())
		assert.Equal(t, rs, ls.GetRailway())
	})
}

// TestRailwayPower checks a railway power property
func TestRailwayPower(t *testing.T) {
	// Create empty railway
	ctx := context.Background()
	log := zerolog.Logger{}
	p := storage.NewPackage("test.brw")
	require.NotNil(t, p)
	r := p.GetRailway()
	require.NotNil(t, r)

	// Create state (virtual mode)
	rs, err := New(ctx, r, log, nil, nil, true)
	assert.NoError(t, err)
	require.NotNil(t, rs)

	// Check initial state
	t.Run("Check initial state", func(t *testing.T) {
		assert.False(t, rs.GetPower().GetActual(ctx), "actual")
		assert.False(t, rs.GetPower().GetRequested(ctx), "requested")
		assert.True(t, rs.GetPower().IsConsistent(ctx), "consistent")
	})

	// Check turning power on
	t.Run("Check turn on", func(t *testing.T) {
		assert.NoError(t, rs.GetPower().SetRequested(ctx, true))
		assert.True(t, rs.GetPower().GetActual(ctx), "actual") // In virtual mode, change must be immediate
		assert.True(t, rs.GetPower().GetRequested(ctx), "requested")
		assert.True(t, rs.GetPower().IsConsistent(ctx), "consistent")
	})
}
