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

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// eventDispatcher implements the EventDispatcher
type eventDispatcher struct {
	mutex         sync.RWMutex
	subscriptions map[int]*eventSubscription
	lastID        int
}

// eventSubscription maintains state for a single subscription
type eventSubscription struct {
	id    int
	queue chan state.Event
	cb    func(state.Event)
}

// Run keeps sending events to the callback until the queue is closed
func (d *eventSubscription) Run() {
	for evt := range d.queue {
		d.cb(evt)
	}
}

// Send the given event to the callback
func (d *eventSubscription) Send(evt state.Event) {
	// Try sync push into channel
	select {
	case d.queue <- evt:
		// Ok
		return
	default:
		// Try in go routine
		go func() {
			d.queue <- evt
		}()
	}
}

// Close the subscription
func (d *eventSubscription) Close() {
	queue := d.queue
	d.queue = nil
	if queue != nil {
		close(queue)
	}
}

// Send the given event to all interested receivers.
func (d *eventDispatcher) Send(evt state.Event) {
	d.mutex.RLock()
	defer d.mutex.RUnlock()

	for _, sub := range d.subscriptions {
		sub.Send(evt)
	}
}

// Subscribe to events.
// To cancel the subscription, call the given cancel function.
func (d *eventDispatcher) Subscribe(ctx context.Context, cb func(state.Event)) context.CancelFunc {
	d.mutex.Lock()
	defer d.mutex.Unlock()

	if d.subscriptions == nil {
		d.subscriptions = make(map[int]*eventSubscription)
	}
	d.lastID++

	sub := &eventSubscription{
		id:    d.lastID,
		queue: make(chan state.Event, 64),
		cb:    cb,
	}
	d.subscriptions[sub.id] = sub
	go sub.Run()

	return func() {
		d.cancel(sub.id)
	}
}

// Cancel all subscriptions
func (d *eventDispatcher) CancelAll() {
	d.mutex.Lock()
	defer d.mutex.Unlock()

	for id, sub := range d.subscriptions {
		delete(d.subscriptions, id)
		sub.Close()
	}
}

// cancel the subscription with given id.
func (d *eventDispatcher) cancel(id int) {
	d.mutex.Lock()
	defer d.mutex.Unlock()

	sub, found := d.subscriptions[id]
	if !found {
		return
	}
	delete(d.subscriptions, id)
	sub.Close()
}
