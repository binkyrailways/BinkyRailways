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

package predicates

import (
	"context"
	"testing"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/storage"
	"github.com/stretchr/testify/assert"
	"github.com/stretchr/testify/require"
)

func TestParsePredicate(t *testing.T) {
	t.Run("Empty", func(t *testing.T) {
		for _, x := range []string{"", "  ", "\n", "  \t \n ", "all", "All", "ALL"} {
			result, err := ParsePredicate(x, nil)
			assert.NoError(t, err, x)
			assert.Nil(t, result, x)

			gen := GeneratePredicate(result)
			assert.Equal(t, "all", gen, x)
		}
	})

	// Prepare package
	ctx := context.Background()
	pkg := storage.NewPackage("")
	rw := pkg.GetRailway()
	// Add loc 1
	l1, err := pkg.AddNewLoc()
	require.NoError(t, err)
	l1.SetAddress(ctx, model.NewAddress(model.NewNetwork(model.AddressTypeDcc, ""), "3"))
	l1.SetDescription("loc 1")
	rw.GetLocs().Add(l1)
	// Add loc 2
	l2, err := pkg.AddNewLoc()
	require.NoError(t, err)
	l2.SetAddress(ctx, model.NewAddress(model.NewNetwork(model.AddressTypeDcc, ""), "2"))
	l2.SetDescription("loc 2")
	rw.GetLocs().Add(l2)
	// Add loc 2 upper
	l2u, err := pkg.AddNewLoc()
	require.NoError(t, err)
	l2u.SetAddress(ctx, model.NewAddress(model.NewNetwork(model.AddressTypeDcc, ""), "4"))
	l2u.SetDescription("LOC 2")
	rw.GetLocs().Add(l2u)
	// Add group 1
	lg1 := rw.GetLocGroups().AddNew()
	lg1.SetDescription("group 1")
	lg1.GetLocs().Add(l1)
	// Add group 2
	lg2 := rw.GetLocGroups().AddNew()
	lg2.SetDescription("group 2")
	lg2.GetLocs().Add(l1)
	lg2.GetLocs().Add(l2)
	// Add group 2 upper
	lg2u := rw.GetLocGroups().AddNew()
	lg2u.SetDescription("GROUP 2")
	lg2u.GetLocs().Add(l1)
	lg2u.GetLocs().Add(l2u)

	// Test package
	t.Run("Test package", func(t *testing.T) {
		p, err := getLocByNamePredicate("loc 1", rw)
		require.NoError(t, err)
		lep, ok := p.(model.LocEqualsPredicate)
		require.True(t, ok)
		assert.Equal(t, l1.GetID(), lep.GetLocID())
	})

	t.Run("Single loc", func(t *testing.T) {
		for _, x := range []string{`"loc 1"`, `"dcc 3"`} {
			result, err := ParsePredicate(x, rw)
			require.NoError(t, err, x)
			lep, ok := result.(model.LocEqualsPredicate)
			require.True(t, ok, x)
			assert.Equal(t, l1.GetID(), lep.GetLocID(), x)

			gen := GeneratePredicate(result)
			assert.Equal(t, `"loc 1"`, gen)
		}
		for _, x := range []string{`"loc 7"`, `"dcc 7"`} {
			_, err := ParsePredicate(x, rw)
			assert.Error(t, err, x)
		}
	})

	t.Run("Single loc, case sensitive", func(t *testing.T) {
		for _, x := range []string{`"loc 2"`} {
			result, err := ParsePredicate(x, rw)
			require.NoError(t, err, x)
			lep, ok := result.(model.LocEqualsPredicate)
			require.True(t, ok, x)
			assert.Equal(t, l2.GetID(), lep.GetLocID(), x)

			gen := GeneratePredicate(result)
			assert.Equal(t, `"loc 2"`, gen)
		}
		for _, x := range []string{`"LOC 2"`} {
			result, err := ParsePredicate(x, rw)
			require.NoError(t, err, x)
			lep, ok := result.(model.LocEqualsPredicate)
			require.True(t, ok, x)
			assert.Equal(t, l2u.GetID(), lep.GetLocID(), x)

			gen := GeneratePredicate(result)
			assert.Equal(t, `"LOC 2"`, gen)
		}
	})

	t.Run("Single loc, ignore case", func(t *testing.T) {
		for _, x := range []string{`"loC 2"`} {
			result, err := ParsePredicate(x, rw)
			require.NoError(t, err, x)
			lep, ok := result.(model.LocEqualsPredicate)
			require.True(t, ok, x)
			assert.True(t, (l2.GetID() == lep.GetLocID()) || (l2u.GetID() == lep.GetLocID()))
		}
	})

	t.Run("Single loc group", func(t *testing.T) {
		for _, x := range []string{`memberOf("group 1")`, `memberof("group 1")`, `member of("group 1")`} {
			result, err := ParsePredicate(x, rw)
			require.NoError(t, err, x)
			lgep, ok := result.(model.LocGroupEqualsPredicate)
			require.True(t, ok, x)
			assert.Equal(t, lg1.GetID(), lgep.GetGroupID(), x)

			gen := GeneratePredicate(result)
			assert.Equal(t, `memberOf("group 1")`, gen)
		}
		for _, x := range []string{`memberOf("group 22")`, `member Of("group 22")`} {
			_, err := ParsePredicate(x, rw)
			assert.Error(t, err, x)
		}
	})

	t.Run("Single loc group, case sensitive", func(t *testing.T) {
		for _, x := range []string{`memberOf("group 2")`} {
			result, err := ParsePredicate(x, rw)
			require.NoError(t, err, x)
			lgep, ok := result.(model.LocGroupEqualsPredicate)
			require.True(t, ok, x)
			assert.Equal(t, lg2.GetID(), lgep.GetGroupID(), x)
		}
		for _, x := range []string{`memberOf("GROUP 2")`} {
			result, err := ParsePredicate(x, rw)
			require.NoError(t, err, x)
			lgep, ok := result.(model.LocGroupEqualsPredicate)
			require.True(t, ok, x)
			assert.Equal(t, lg2u.GetID(), lgep.GetGroupID(), x)
		}
	})

	t.Run("Single loc group, ignore case", func(t *testing.T) {
		for _, x := range []string{`memberOf("groUP 2")`} {
			result, err := ParsePredicate(x, rw)
			require.NoError(t, err, x)
			lgep, ok := result.(model.LocGroupEqualsPredicate)
			require.True(t, ok, x)
			assert.True(t, (lg2.GetID() == lgep.GetGroupID()) || (lg2u.GetID() == lgep.GetGroupID()))
		}
	})

	t.Run("can change direction", func(t *testing.T) {
		for _, x := range []string{`canChangeDirection`, `CANCHANGEDIRECTION`, `canchangedirection`, `can Change direction`} {
			result, err := ParsePredicate(x, rw)
			require.NoError(t, err, x)
			_, ok := result.(model.LocCanChangeDirectionPredicate)
			require.True(t, ok, x)

			gen := GeneratePredicate(result)
			assert.Equal(t, `canChangeDirection`, gen)
		}
	})

	t.Run("Or 1", func(t *testing.T) {
		result, err := ParsePredicate(`("loc 1")`, rw)
		require.NoError(t, err)
		lop, ok := result.(model.LocOrPredicate)
		require.True(t, ok)
		assert.Equal(t, 1, lop.GetPredicates().GetCount())

		gen := GeneratePredicate(result)
		assert.Equal(t, `"loc 1"`, gen)
	})

	t.Run("Or 2", func(t *testing.T) {
		for _, x := range []string{`("loc 1" or "dcc 2")`, `("loc 1" OR "dcc 2")`} {
			result, err := ParsePredicate(x, rw)
			require.NoError(t, err, x)
			lop, ok := result.(model.LocOrPredicate)
			require.True(t, ok)
			assert.Equal(t, 2, lop.GetPredicates().GetCount())

			gen := GeneratePredicate(result)
			assert.Equal(t, `("loc 1" or "loc 2")`, gen)
		}
	})

	t.Run("Or 3", func(t *testing.T) {
		result, err := ParsePredicate(`("loc 1" or "dcc 2" or memberOf("group 1"))`, rw)
		require.NoError(t, err)
		lop, ok := result.(model.LocOrPredicate)
		require.True(t, ok)
		assert.Equal(t, 3, lop.GetPredicates().GetCount())

		gen := GeneratePredicate(result)
		assert.Equal(t, `("loc 1" or "loc 2" or memberOf("group 1"))`, gen)
	})

	t.Run("And 2", func(t *testing.T) {
		for _, x := range []string{`("loc 1" and "dcc 2")`, `("loc 1" And "dcc 2")`} {
			result, err := ParsePredicate(x, rw)
			require.NoError(t, err)
			lop, ok := result.(model.LocAndPredicate)
			require.True(t, ok)
			assert.Equal(t, 2, lop.GetPredicates().GetCount())

			gen := GeneratePredicate(result)
			assert.Equal(t, `("loc 1" and "loc 2")`, gen)
		}
	})

	t.Run("And 3", func(t *testing.T) {
		result, err := ParsePredicate(`("loc 1" and "dcc 2" and memberOf("group 1"))`, rw)
		require.NoError(t, err)
		lop, ok := result.(model.LocAndPredicate)
		require.True(t, ok)
		assert.Equal(t, 3, lop.GetPredicates().GetCount())

		gen := GeneratePredicate(result)
		assert.Equal(t, `("loc 1" and "loc 2" and memberOf("group 1"))`, gen)
	})

	t.Run("Standard only 1", func(t *testing.T) {
		result, err := ParsePredicate(`only("loc 1")`, rw)
		require.NoError(t, err)
		lop, ok := result.(model.LocStandardPredicate)
		require.True(t, ok)
		assert.Equal(t, 1, lop.GetIncludes().GetPredicates().GetCount())
		assert.Equal(t, 0, lop.GetExcludes().GetPredicates().GetCount())

		gen := GeneratePredicate(result)
		assert.Equal(t, `only("loc 1")`, gen)
	})

	t.Run("Standard only 2", func(t *testing.T) {
		result, err := ParsePredicate(`only("loc 1" or memberOf("group 1"))`, rw)
		require.NoError(t, err)
		lop, ok := result.(model.LocStandardPredicate)
		require.True(t, ok)
		assert.Equal(t, 2, lop.GetIncludes().GetPredicates().GetCount())
		assert.Equal(t, 0, lop.GetExcludes().GetPredicates().GetCount())

		gen := GeneratePredicate(result)
		assert.Equal(t, `only("loc 1" or memberOf("group 1"))`, gen)
	})

	t.Run("Standard except 1", func(t *testing.T) {
		result, err := ParsePredicate(`except(memberOf("group 1"))`, rw)
		require.NoError(t, err)
		lop, ok := result.(model.LocStandardPredicate)
		require.True(t, ok)
		assert.Equal(t, 0, lop.GetIncludes().GetPredicates().GetCount())
		assert.Equal(t, 1, lop.GetExcludes().GetPredicates().GetCount())

		gen := GeneratePredicate(result)
		assert.Equal(t, `except(memberOf("group 1"))`, gen)
	})

	t.Run("Standard except 2", func(t *testing.T) {
		result, err := ParsePredicate(`except("loc 1" or memberOf("group 1"))`, rw)
		require.NoError(t, err)
		lop, ok := result.(model.LocStandardPredicate)
		require.True(t, ok)
		assert.Equal(t, 0, lop.GetIncludes().GetPredicates().GetCount())
		assert.Equal(t, 2, lop.GetExcludes().GetPredicates().GetCount())

		gen := GeneratePredicate(result)
		assert.Equal(t, `except("loc 1" or memberOf("group 1"))`, gen)
	})

	t.Run("Standard full 1", func(t *testing.T) {
		result, err := ParsePredicate(`only("loc 1" or canChangeDirection or ("loc 2" and canChangeDirection)) except("loc 2" or memberOf("group 1"))`, rw)
		require.NoError(t, err)
		lop, ok := result.(model.LocStandardPredicate)
		require.True(t, ok)
		assert.Equal(t, 3, lop.GetIncludes().GetPredicates().GetCount())
		assert.Equal(t, 2, lop.GetExcludes().GetPredicates().GetCount())

		gen := GeneratePredicate(result)
		assert.Equal(t, `only("loc 1" or canChangeDirection or ("loc 2" and canChangeDirection)) except("loc 2" or memberOf("group 1"))`, gen)
	})
}
