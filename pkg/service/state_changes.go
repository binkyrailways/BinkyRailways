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
	"crypto/sha1"
	"fmt"
	"time"

	api "github.com/binkyrailways/BinkyRailways/pkg/api/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// Fetch state changes when they happen
func (s *service) GetStateChanges(req *api.GetStateChangesRequest, server api.StateService_GetStateChangesServer) error {
	rwState := s.railwayState
	if rwState == nil {
		// Nothing to notify about
		return nil
	}
	ctx := server.Context()
	changes := make(chan *api.StateChange, 64)

	sendEntireState := func() int {
		sentChanges := 0
		send := func(sc *api.StateChange, id string) bool {
			// Is change empty?
			if sc == nil {
				return true
			}
			// Prepare hash
			h := hash(sc.String())
			if req.Hashes[id] == h {
				// Caller already has the same state
				return true
			}

			// Send it
			sentChanges++
			sc.Id = id
			sc.Hash = h
			select {
			case changes <- sc:
				// Change put in channel
				return true
			case <-ctx.Done():
				// Context canceled
				return false
			}
		}
		send(s.stateChangeBuilder(ctx, rwState), "rw:"+rwState.GetID())
		rwState.ForEachCommandStation(func(b state.CommandStation) {
			send(s.stateChangeBuilder(ctx, b), "cs:"+b.GetID())
		})
		rwState.ForEachLoc(func(b state.Loc) {
			send(s.stateChangeBuilder(ctx, b), "l:"+b.GetID())
		})
		rwState.ForEachBlock(func(b state.Block) {
			send(s.stateChangeBuilder(ctx, b), "b:"+b.GetID())
		})
		/*rwState.ForEachBlockGroup(func(b state.BlockGroup) {
			send(s.stateChangeBuilder(ctx, b), "bg:" + b.GetID())
		})*/
		rwState.ForEachJunction(func(b state.Junction) {
			send(s.stateChangeBuilder(ctx, b), "j:"+b.GetID())
		})
		rwState.ForEachOutput(func(b state.Output) {
			send(s.stateChangeBuilder(ctx, b), "o:"+b.GetID())
		})
		rwState.ForEachRoute(func(b state.Route) {
			send(s.stateChangeBuilder(ctx, b), "r:"+b.GetID())
		})
		rwState.ForEachSensor(func(b state.Sensor) {
			send(s.stateChangeBuilder(ctx, b), "sn:"+b.GetID())
		})
		rwState.ForEachSignal(func(b state.Signal) {
			send(s.stateChangeBuilder(ctx, b), "sig:"+b.GetID())
		})
		return sentChanges
	}

	// If requested, send all state objects
	go func() {
		for {
			cnt := sendEntireState()
			time.Sleep(time.Millisecond * 25)
			if cnt > 0 {
				close(changes)
				return
			}
		}
	}()

	// Send messages
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
func (s *service) stateChangeBuilder(ctx context.Context, entity state.Subject) *api.StateChange {
	switch x := entity.(type) {
	case state.Railway:
		rs, _ := s.GetRailwayState(ctx, nil)
		return &api.StateChange{Railway: rs}
	case state.CommandStation:
		var rs api.CommandStationState
		if err := rs.FromState(ctx, x); err != nil {
			return nil
		}
		return &api.StateChange{CommandStation: &rs}
	case state.Loc:
		httpHost, err := s.getHttpHost(ctx)
		if err != nil {
			return nil
		}
		var rs api.LocState
		if err := rs.FromState(ctx, x, httpHost, s.HTTPSecure); err != nil {
			return nil
		}
		return &api.StateChange{Loc: &rs}
	case state.Block:
		var rs api.BlockState
		if err := rs.FromState(ctx, x); err != nil {
			return nil
		}
		return &api.StateChange{Block: &rs}
	case state.BlockGroup:
		var rs api.BlockGroupState
		if err := rs.FromState(ctx, x); err != nil {
			return nil
		}
		return &api.StateChange{BlockGroup: &rs}
	case state.Junction:
		var rs api.JunctionState
		if err := rs.FromState(ctx, x); err != nil {
			return nil
		}
		return &api.StateChange{Junction: &rs}
	case state.Output:
		var rs api.OutputState
		if err := rs.FromState(ctx, x); err != nil {
			return nil
		}
		return &api.StateChange{Output: &rs}
	case state.Route:
		var rs api.RouteState
		if err := rs.FromState(ctx, x); err != nil {
			return nil
		}
		return &api.StateChange{Route: &rs}
	case state.Sensor:
		var rs api.SensorState
		if err := rs.FromState(ctx, x); err != nil {
			return nil
		}
		return &api.StateChange{Sensor: &rs}
	case state.Signal:
		var rs api.SignalState
		if err := rs.FromState(ctx, x); err != nil {
			return nil
		}
		return &api.StateChange{Signal: &rs}
	default:
		return nil
	}
}

// Hash returns a sha256 hash of the given string
func hash(input string) string {
	h := sha1.Sum([]byte(input))
	return fmt.Sprintf("%x", string(h[:]))
}
