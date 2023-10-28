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
	"math/rand"
	"sync"
	"time"

	"github.com/rs/zerolog"
)

// Exclusive implements exclusive access to a resource.
type Exclusive interface {
	// Exclusive runs the given function while holding an exclusive lock.
	// This function allows nesting.
	Exclusive(ctx context.Context, timeout time.Duration,
		description string, run func(context.Context) error) error
}

// NewExclusive creates a new instance of Exclusive.
func NewExclusive(log zerolog.Logger) Exclusive {
	return &exclusive{log: log}
}

type exclusive struct {
	sync.Mutex
	unique int64
	log    zerolog.Logger
}

func init() {
	// Seed RNG
	rand.Seed(time.Now().UnixNano())
}

// Exclusive runs the given function while holding an exclusive lock.
// This function allows nesting.
func (m *exclusive) Exclusive(ctx context.Context,
	timeout time.Duration,
	description string,
	cb func(context.Context) error) error {
	// Log timeout errors
	start := time.Now()
	defer func() {
		elapsed := time.Since(start)
		if elapsed > timeout {
			m.log.Warn().
				Str("description", description).
				Dur("elapsed", elapsed).
				Dur("timeout", timeout).
				Msg("Exclusive operation timed out")
		}
	}()

	// Does the caller already have exclusive access to me?
	value, ok := ctx.Value(m).(int64)
	if ok && value == m.unique {
		// Yes, already has exclusive access
		return cb(ctx)
	}

	// Caller does not yet have exclusive access
	newUnique := rand.Int63()
	m.Lock()
	defer m.Unlock()

	m.unique = newUnique
	nctx := context.WithValue(ctx, m, newUnique)
	return cb(nctx)
}
