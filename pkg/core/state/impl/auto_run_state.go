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
	"sync"
	"time"

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// autoRunState implements auto-run behavior in virtual mode.
type autoRunState struct {
	mutex   sync.Mutex
	railway Railway
	enabled bool
	cancel  context.CancelFunc
}

// newAutoRunState constructs an autoRunState
func newAutoRunState(railway Railway) *autoRunState {
	return &autoRunState{
		railway: railway,
	}
}

// GetEnabled returns the enabled flag
func (s *autoRunState) GetEnabled() bool {
	s.mutex.Lock()
	defer s.mutex.Unlock()

	return s.enabled
}

// SetEnabled sets the enabled flag
func (s *autoRunState) SetEnabled(value bool) error {
	s.mutex.Lock()
	defer s.mutex.Unlock()

	if s.enabled == value {
		return nil
	}
	s.enabled = value
	if !value {
		if s.cancel != nil {
			s.cancel()
			s.cancel = nil
		}
	} else {
		// Start auto run
		ctx, cancel := context.WithCancel(context.Background())
		s.cancel = cancel
		go s.run(ctx)
	}
	return nil
}

// run until the given context is canceled
func (s *autoRunState) run(ctx context.Context) {
	// Build loc states
	var locStates []*autoRunLocState
	s.railway.ForEachLoc(func(loc state.Loc) {
		locStates = append(locStates, newAutoRunLocState(loc))
	})

	// Build ticker
	t := time.NewTicker(time.Second * 2)
	defer t.Stop()
	for {
		select {
		case <-ctx.Done():
			// Context canceled
			return
		case <-t.C:
			// Tick
			for _, ls := range locStates {
				ls.Tick()
			}
		}
	}
}
