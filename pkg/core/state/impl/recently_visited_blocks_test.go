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

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
	"github.com/binkyrailways/BinkyRailways/pkg/core/util"
	"github.com/stretchr/testify/assert"
)

func TestRecentlyVisitedBlocks(t *testing.T) {
	ctx := context.Background()

	t.Run("Empty test", func(t *testing.T) {
		rvb := newRecentlyVisitedBlocks(util.NewExclusive())
		count := 0
		rvb.ForEach(ctx, func(c context.Context, b state.Block) error {
			count++
			return nil
		})
		assert.Equal(t, 0, count)
	})

	t.Run("Insert test", func(t *testing.T) {
		rvb := newRecentlyVisitedBlocks(util.NewExclusive())

		b0 := &block{}
		rvb.Insert(ctx, b0)
		count := 0
		rvb.ForEach(ctx, func(c context.Context, b state.Block) error {
			assert.Equal(t, b0, b)
			count++
			return nil
		})
		assert.Equal(t, 1, count)

		b1 := &block{}
		rvb.Insert(ctx, b1)
		count = 0
		rvb.ForEach(ctx, func(c context.Context, b state.Block) error {
			if count == 0 {
				assert.Equal(t, b1, b)
			} else {
				assert.Equal(t, b0, b)
			}
			count++
			return nil
		})
		assert.Equal(t, 2, count)

		rvb.Insert(ctx, b0)
		count = 0
		rvb.ForEach(ctx, func(c context.Context, b state.Block) error {
			if count == 0 {
				assert.Equal(t, b0, b)
			} else {
				assert.Equal(t, b1, b)
			}
			count++
			return nil
		})
		assert.Equal(t, 2, count)
	})

	t.Run("CloneFrom test", func(t *testing.T) {
		rvb := newRecentlyVisitedBlocks(util.NewExclusive())
		source := newRecentlyVisitedBlocks(util.NewExclusive())

		b0 := &block{}
		b1 := &block{}
		source.Insert(ctx, b0)
		source.Insert(ctx, b1)
		count := 0
		source.ForEach(ctx, func(c context.Context, b state.Block) error {
			if count == 0 {
				assert.Equal(t, b1, b)
			} else {
				assert.Equal(t, b0, b)
			}
			count++
			return nil
		})
		assert.Equal(t, 2, count)

		count = 0
		rvb.ForEach(ctx, func(c context.Context, b state.Block) error {
			count++
			return nil
		})
		assert.Equal(t, 0, count)

		rvb.CloneFrom(ctx, source)
		count = 0
		rvb.ForEach(ctx, func(c context.Context, b state.Block) error {
			if count == 0 {
				assert.Equal(t, b1, b)
			} else {
				assert.Equal(t, b0, b)
			}
			count++
			return nil
		})
		assert.Equal(t, 2, count)
	})
}
