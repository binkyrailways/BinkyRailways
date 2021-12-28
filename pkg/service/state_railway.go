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
	"github.com/binkyrailways/BinkyRailways/pkg/core/state/impl"
)

// Gets the current railway state
func (s *service) GetRailwayState(ctx context.Context, req *api.Empty) (*api.RailwayState, error) {
	rwState := s.railwayState
	rw := &api.Railway{}
	if err := rw.FromModel(ctx, s.railway); err != nil {
		return nil, err
	}
	if rwState == nil {
		return &api.RailwayState{
			Model:                rw,
			IsRunModeEnabled:     false,
			IsVirtualModeEnabled: false,
		}, nil
	}
	return &api.RailwayState{
		Model:                rw,
		IsRunModeEnabled:     true,
		IsVirtualModeEnabled: rwState.GetVirtualMode().GetEnabled(),
	}, nil
}

// Enable the run mode of the process.
func (s *service) EnableRunMode(ctx context.Context, req *api.EnableRunModeRequest) (*api.RailwayState, error) {
	s.mutex.Lock()
	defer s.mutex.Unlock()

	if s.railwayState == nil {
		// Enable run state
		var err error
		s.railwayState, err = impl.New(ctx, s.railway, s.Logger, s, s, req.GetVirtual())
		if err != nil {
			return nil, err
		}
		s.cancelEventSubscription = s.railwayState.Subscribe(context.Background(), func(e state.Event) {
			switch evt := e.(type) {
			case state.ActualStateChangedEvent:
				if change := s.stateChangeBuilder(evt.Subject); change != nil {
					s.stateChanges.Pub(change)
				}
			case state.RequestedStateChangedEvent:
				if change := s.stateChangeBuilder(evt.Subject); change != nil {
					s.stateChanges.Pub(change)
				}
			}
		})
	}
	return s.GetRailwayState(ctx, &api.Empty{})
}

// Disable the run mode of the process, switching back to edit mode.
func (s *service) DisableRunMode(ctx context.Context, req *api.Empty) (*api.RailwayState, error) {
	s.mutex.Lock()
	defer s.mutex.Unlock()

	if rwState := s.railwayState; rwState != nil {
		// Disable run state
		s.railwayState = nil
		rwState.Close(ctx)
	}
	if cf := s.cancelEventSubscription; cf != nil {
		s.cancelEventSubscription = nil
		cf()
	}
	return s.GetRailwayState(ctx, &api.Empty{})
}
