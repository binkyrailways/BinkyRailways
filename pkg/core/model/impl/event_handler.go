// Copyright 2020 Ewout Prangsma
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

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// EventHandler adds implementation details
type EventHandler interface {
	model.EventHandler
	Invoke(interface{})
}

type (
	eventHandler      []eventHandlerEntry
	eventHandlerEntry struct {
		cb    func(interface{})
		token uint32
	}
)

var _ model.EventHandler = &eventHandler{}

// NewEventHandler creates a new event handler
func NewEventHandler() EventHandler {
	return &eventHandler{}
}

// Add a callback
func (eh *eventHandler) Add(cb func(interface{})) context.CancelFunc {
	token := uint32(1)
	for _, x := range *eh {
		if x.token >= token {
			token = x.token + 1
		}
	}
	*eh = append(*eh, eventHandlerEntry{cb: cb, token: token})
	return func() {
		eh.remove(token)
	}
}

// Remove a callback
func (eh *eventHandler) remove(token uint32) {
	var remaining []eventHandlerEntry
	for _, x := range *eh {
		if x.token != token {
			remaining = append(remaining, x)
		}
	}
	*eh = remaining
}

// Invoke all callbacks
func (eh *eventHandler) Invoke(value interface{}) {
	for _, x := range *eh {
		x.cb(value)
	}
}
