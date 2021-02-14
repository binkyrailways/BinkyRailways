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

package util

import (
	"context"
	"testing"

	"github.com/stretchr/testify/assert"
	"github.com/stretchr/testify/require"
	"golang.org/x/sync/errgroup"
)

func TestExclusive(t *testing.T) {
	e := NewExclusive()
	require.NotNil(t, e)

	// No nesting
	var result int
	err := e.Exclusive(context.Background(), func(ctx context.Context) error {
		result = 5
		return nil
	})
	assert.NoError(t, err)
	assert.Equal(t, 5, result)

	// Nesting once
	result = 0
	err = e.Exclusive(context.Background(), func(ctx context.Context) error {
		return e.Exclusive(ctx, func(ctx context.Context) error {
			result = 55
			return nil
		})
	})
	assert.NoError(t, err)
	assert.Equal(t, 55, result)
}

func TestExclusiveConcurrent(t *testing.T) {
	e := NewExclusive()
	require.NotNil(t, e)

	// No nesting
	var result int

	adding := func(ctx context.Context) error {
		for i := 0; i < 50000; i++ {
			if err := e.Exclusive(ctx, func(ctx context.Context) error {
				result++
				return nil
			}); err != nil {
				return err
			}
		}
		return nil
	}

	removing := func(ctx context.Context) error {
		for i := 0; i < 50000; i++ {
			if err := e.Exclusive(ctx, func(ctx context.Context) error {
				result--
				return nil
			}); err != nil {
				return err
			}
		}
		return nil
	}

	g, ctx := errgroup.WithContext(context.Background())
	g.Go(func() error { return adding(ctx) })
	g.Go(func() error { return removing(ctx) })

	err := g.Wait()
	assert.NoError(t, err)
	assert.Equal(t, 0, result)
}
