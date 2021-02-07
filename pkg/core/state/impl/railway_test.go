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
	"testing"

	"github.com/binkyrailways/BinkyRailways/pkg/core/storage"
	"github.com/stretchr/testify/require"
)

// TestRailway tests the construction on of a new Railway state.
func TestRailway(t *testing.T) {
	// Open package
	p, err := storage.NewPackageFromFile("../../../../Fixtures/tzl.brw")
	require.NoError(t, err)
	require.NotNil(t, p)

	// Create state
	st, err := New(p.GetRailway(), nil, nil)
	require.NoError(t, err)
	require.NotNil(t, st)
}
