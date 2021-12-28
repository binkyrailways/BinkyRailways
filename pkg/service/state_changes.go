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

package service

import (
	"context"

	api "github.com/binkyrailways/BinkyRailways/pkg/api/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// Fetch state changes when they happen
func (s *service) GetStateChanges(req *api.GetStateChangesRequest, server api.StateService_GetStateChangesServer) error {
	ctx := server.Context()
	changes := make(chan *api.StateChange)
	defer close(changes)

	cb := func(stateChange *api.StateChange) {
		changes <- stateChange
	}
	s.stateChanges.Sub(cb)
	defer s.stateChanges.Leave(cb)

	for {
		var msg *api.StateChange
		var ok bool
		select {
		case msg, ok = <-changes:
			if !ok {
				return nil
			}
		case <-ctx.Done():
			// Context canceled
			return nil
		}
		if err := server.Send(msg); err != nil {
			return err
		}
	}
}

// stateChangeBuilder constructs a StateChange for the given event.
// Can result nil.
func (s *service) stateChangeBuilder(entity state.Entity) *api.StateChange {
	switch entity.(type) {
	case state.Railway:
		rs, _ := s.GetRailwayState(context.Background(), nil)
		return &api.StateChange{Railway: rs}
	default:
		return nil
	}
}
