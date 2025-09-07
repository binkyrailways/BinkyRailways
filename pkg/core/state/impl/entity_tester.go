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
	"context"
	"math/rand"
	"sync/atomic"
	"time"

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
	"github.com/rs/zerolog"
)

// Entity adds implementation functions to state.Entity.
type EntityTester interface {
	state.EntityTester
}

type entityTester struct {
	log     zerolog.Logger
	railway Railway
	enabled boolProperty
	started int32
	closing int32

	outputs   map[string]state.BinaryOutput
	outputIDs []string
	switches  map[string]state.Switch
	switchIDs []string
}

// Create a new entity tester
func newEntityTester(log zerolog.Logger, railway Railway) *entityTester {
	et := &entityTester{
		log:      log,
		railway:  railway,
		outputs:  make(map[string]state.BinaryOutput),
		switches: make(map[string]state.Switch),
	}
	et.enabled.Configure("enabled", et, nil, railway, railway)
	return et
}

// Gets my unique ID
func (et *entityTester) GetID() string {
	return "entity-tester"
}

// Gets my description
func (et *entityTester) GetDescription() string {
	return "entity tester"
}

// Is the tester enabled?
func (et *entityTester) GetEnabled() state.BoolProperty {
	return &et.enabled
}

// Include the given entity in the test.
func (et *entityTester) Include(ctx context.Context, e state.Entity) {
	et.railway.Exclusive(ctx, time.Second, "include entity", func(ctx context.Context) error {
		if x, ok := e.(state.Switch); ok {
			et.switches[e.GetID()] = x
			et.switchIDs = getKeySlice(et.switches)
		} else if x, ok := e.(state.BinaryOutput); ok {
			et.outputs[e.GetID()] = x
			et.outputIDs = getKeySlice(et.outputs)
		}
		return nil
	})
}

// Exclude the given entity from the test.
func (et *entityTester) Exclude(ctx context.Context, e state.Entity) {
	et.railway.Exclusive(ctx, time.Second, "exclude entity", func(ctx context.Context) error {
		if _, ok := e.(state.Switch); ok {
			delete(et.switches, e.GetID())
			et.switchIDs = getKeySlice(et.switches)
		} else if _, ok := e.(state.BinaryOutput); ok {
			delete(et.outputs, e.GetID())
			et.outputIDs = getKeySlice(et.outputs)
		}
		return nil
	})
}

// Is the given entity included in the test?
func (et *entityTester) IsIncluded(ctx context.Context, e state.Entity) bool {
	included := false
	et.railway.Exclusive(ctx, time.Second, "isincluded", func(ctx context.Context) error {
		if _, ok := e.(state.Switch); ok {
			included = et.switches[e.GetID()] != nil
		} else if _, ok := e.(state.BinaryOutput); ok {
			included = et.outputs[e.GetID()] != nil
		}
		return nil
	})
	return included
}

// Stop any tests
func (et *entityTester) Close() {
	atomic.StoreInt32(&et.closing, 1)
}

// Start running the tester
func (et *entityTester) Start() {
	if atomic.CompareAndSwapInt32(&et.started, 0, 1) {
		go et.run()
	}
}

// Run the tester when enabled
func (et *entityTester) run() {
	ctx := context.Background()
	for {
		if atomic.LoadInt32(&et.closing) == 1 {
			return
		}
		time.Sleep(time.Second)
		et.runOnce(ctx)
	}
}

// Run one iteration of the tester
func (et *entityTester) runOnce(ctx context.Context) {
	et.railway.Exclusive(ctx, time.Second*2, "runOnce", func(ctx context.Context) error {
		if et.GetEnabled().GetRequested(ctx) {
			// Update actual
			et.GetEnabled().SetActual(ctx, true)

			// Change next switch
			et.testNextSwitch(ctx)
			// Change next output
			et.testNextOutput(ctx)
		} else {
			// Update actual
			et.GetEnabled().SetActual(ctx, false)
			clear(et.switches)
			clear(et.outputs)
			et.switchIDs = nil
			et.outputIDs = nil
		}
		return nil
	})
}

// Select a switch and change direction
func (et *entityTester) testNextSwitch(ctx context.Context) {
	if l := len(et.switchIDs); l > 0 {
		id := et.switchIDs[rand.Intn(l)]
		sw := et.switches[id]
		dir := sw.GetDirection().GetRequested(ctx)
		sw.GetDirection().SetRequested(ctx, dir.Invert())
	}
}

// Select an output and change state
func (et *entityTester) testNextOutput(ctx context.Context) {
	if l := len(et.outputIDs); l > 0 {
		id := et.outputIDs[rand.Intn(l)]
		op := et.outputs[id]
		active := op.GetActive().GetRequested(ctx)
		op.GetActive().SetRequested(ctx, !active)
	}
}

func getKeySlice[T any](m map[string]T) []string {
	if len(m) == 0 {
		return nil
	}
	result := make([]string, 0, len(m))
	for k := range m {
		result = append(result, k)
	}
	return result
}
