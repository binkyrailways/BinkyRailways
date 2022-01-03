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
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// Gets a binkynet local worker by ID.
func (s *service) getBinkyNetLocalWorker(ctx context.Context, fullLocalWorkerID string) (model.BinkyNetLocalWorker, error) {
	csID, lwID, err := api.SplitParentChildID(fullLocalWorkerID)
	if err != nil {
		return nil, err
	}
	cs, err := s.getCommandStation(ctx, csID)
	if err != nil {
		return nil, err
	}
	bncs, ok := cs.(model.BinkyNetCommandStation)
	if !ok {
		return nil, api.NotFound(lwID)
	}
	lw, ok := bncs.GetLocalWorkers().Get(lwID)
	if !ok {
		return nil, api.NotFound(lwID)
	}
	return lw, nil
}

// Gets a Output by ID.
func (s *service) GetBinkyNetLocalWorker(ctx context.Context, req *api.IDRequest) (*api.BinkyNetLocalWorker, error) {
	lw, err := s.getBinkyNetLocalWorker(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	var result api.BinkyNetLocalWorker
	if err := result.FromModel(ctx, lw); err != nil {
		return nil, err
	}
	return &result, nil
}

// Update a BinkyNetLocalWorker by ID.
func (s *service) UpdateBinkyNetLocalWorker(ctx context.Context, req *api.BinkyNetLocalWorker) (*api.BinkyNetLocalWorker, error) {
	lw, err := s.getBinkyNetLocalWorker(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	if err := req.ToModel(ctx, lw); err != nil {
		return nil, err
	}
	var result api.BinkyNetLocalWorker
	if err := result.FromModel(ctx, lw); err != nil {
		return nil, err
	}
	return &result, nil
}

// Adds a new BinkyNetLocalWorker to the command station identified by given by ID.
func (s *service) AddBinkyNetLocalWorker(ctx context.Context, req *api.IDRequest) (*api.BinkyNetLocalWorker, error) {
	cs, err := s.getCommandStation(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	bncs, ok := cs.(model.BinkyNetCommandStation)
	if !ok {
		return nil, api.InvalidArgument("Command station '%s' is not of type BinkyNet", req.GetId())
	}
	lw, err := bncs.GetLocalWorkers().AddNew("<newid>")
	if err != nil {
		return nil, err
	}
	var result api.BinkyNetLocalWorker
	if err := result.FromModel(ctx, lw); err != nil {
		return nil, err
	}
	return &result, nil
}

// Adds a new BinkyNetDevice to the local worker identified by given by ID.
func (s *service) AddBinkyNetDevice(ctx context.Context, req *api.IDRequest) (*api.BinkyNetDevice, error) {
	lw, err := s.getBinkyNetLocalWorker(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	bnDev := lw.GetDevices().AddNew()
	var result api.BinkyNetDevice
	if err := result.FromModel(ctx, bnDev); err != nil {
		return nil, err
	}
	return &result, nil
}

// Adds a new BinkyNetObject to the local worker identified by given by ID.
func (s *service) AddBinkyNetObject(ctx context.Context, req *api.IDRequest) (*api.BinkyNetObject, error) {
	lw, err := s.getBinkyNetLocalWorker(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	bnObj := lw.GetObjects().AddNew()
	var result api.BinkyNetObject
	if err := result.FromModel(ctx, bnObj); err != nil {
		return nil, err
	}
	return &result, nil
}
