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
	"fmt"

	v1 "github.com/binkynet/BinkyNet/apis/v1"
	api "github.com/binkyrailways/BinkyRailways/pkg/api/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// Gets a binkynet local worker by ID.
func (s *service) getBinkyNetLocalWorker(ctx context.Context, fullLocalWorkerID string) (model.BinkyNetLocalWorker, error) {
	csID, lwID, err := api.SplitParentChildID(fullLocalWorkerID)
	if err != nil {
		return nil, err
	}
	cs, _, err := s.getCommandStation(ctx, csID)
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
	cs, _, err := s.getCommandStation(ctx, req.GetId())
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

// Delete a BinkyNetLocalWorker by ID.
func (s *service) DeleteBinkyNetLocalWorker(ctx context.Context, req *api.IDRequest) (*api.CommandStation, error) {
	csID, lwID, err := api.SplitParentChildID(req.GetId())
	if err != nil {
		return nil, err
	}
	cs, csRef, err := s.getCommandStation(ctx, csID)
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
	bncs.GetLocalWorkers().Remove(lw)
	var result api.CommandStation
	if err := result.FromModel(ctx, cs, csRef); err != nil {
		return nil, err
	}
	return &result, nil
}

// Adds a new BinkyNetRouter to the local worker identified by given by ID.
func (s *service) AddBinkyNetRouter(ctx context.Context, req *api.IDRequest) (*api.BinkyNetRouter, error) {
	lw, err := s.getBinkyNetLocalWorker(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	bnRouter, err := lw.GetRouters().AddNew()
	if err != nil {
		return nil, err
	}
	var result api.BinkyNetRouter
	if err := result.FromModel(ctx, bnRouter); err != nil {
		return nil, err
	}
	return &result, nil
}

// Delete a BinkyNetRouter by ID.
func (s *service) DeleteBinkyNetRouter(ctx context.Context, req *api.SubIDRequest) (*api.BinkyNetLocalWorker, error) {
	lw, err := s.getBinkyNetLocalWorker(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	var router model.BinkyNetRouter
	lw.GetRouters().ForEach(func(x model.BinkyNetRouter) {
		if x.GetID() == req.GetSubId() {
			router = x
		}
	})
	if router == nil {
		return nil, fmt.Errorf("router '%s' not found in local worker '%s'", req.GetSubId(), req.GetId())
	}
	lw.GetRouters().Remove(router)
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
	bnDev := lw.GetDevices().AddNew(ctx)
	var result api.BinkyNetDevice
	if err := result.FromModel(ctx, bnDev); err != nil {
		return nil, err
	}
	return &result, nil
}

// Delete a BinkyNetDevice by ID.
func (s *service) DeleteBinkyNetDevice(ctx context.Context, req *api.SubIDRequest) (*api.BinkyNetLocalWorker, error) {
	lw, err := s.getBinkyNetLocalWorker(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	var dev model.BinkyNetDevice
	lw.GetDevices().ForEach(func(x model.BinkyNetDevice) {
		if x.GetID() == req.GetSubId() {
			dev = x
		}
	})
	if dev == nil {
		return nil, fmt.Errorf("device '%s' not found in local worker '%s'", req.GetSubId(), req.GetId())
	}
	lw.GetDevices().Remove(dev)
	var result api.BinkyNetLocalWorker
	if err := result.FromModel(ctx, lw); err != nil {
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
	bnObj := lw.GetObjects().AddNew(ctx)
	var result api.BinkyNetObject
	if err := result.FromModel(ctx, bnObj); err != nil {
		return nil, err
	}
	return &result, nil
}

// Delete a BinkyNetObject by ID.
func (s *service) DeleteBinkyNetObject(ctx context.Context, req *api.SubIDRequest) (*api.BinkyNetLocalWorker, error) {
	lw, err := s.getBinkyNetLocalWorker(ctx, req.GetId())
	if err != nil {
		return nil, err
	}
	var obj model.BinkyNetObject
	lw.GetObjects().ForEach(func(x model.BinkyNetObject) {
		if x.GetID() == req.GetSubId() {
			obj = x
		}
	})
	if obj == nil {
		return nil, fmt.Errorf("object '%s' not found in local worker '%s'", req.GetSubId(), req.GetId())
	}
	lw.GetObjects().Remove(obj)
	var result api.BinkyNetLocalWorker
	if err := result.FromModel(ctx, lw); err != nil {
		return nil, err
	}
	return &result, nil
}

// Adds one or more new BinkyNetObject to the binkynet local worker identified by given
// by ID and attach them to the given device.
func (s *service) AddBinkyNetObjectsGroup(ctx context.Context, req *api.AddBinkyNetObjectsGroupRequest) (*api.BinkyNetLocalWorker, error) {
	lw, err := s.getBinkyNetLocalWorker(ctx, req.GetLocalWorkerId())
	if err != nil {
		return nil, err
	}
	bnDev, found := lw.GetDevices().Get(v1.DeviceID(req.GetDeviceId()))
	if !found {
		return nil, api.NotFound("Device %s", req.GetDeviceId())
	}
	var firstPin int

	addSensors := func(count int) error {
		getObjectWithPin := func(pinIndex v1.DeviceIndex) model.BinkyNetObject {
			var result model.BinkyNetObject
			lw.GetObjects().ForEach(func(bnObj model.BinkyNetObject) {
				if conn, found := bnObj.GetConnections().Get(v1.ConnectionNameSensor); found {
					if pin, found := conn.GetPins().Get(0); found {
						if pin.GetDeviceID() == bnDev.GetDeviceID() && pin.GetIndex() == pinIndex {
							result = bnObj
						}
					}
				}
			})
			return result
		}
		for i := 0; i < count; i++ {
			bnObj := getObjectWithPin(v1.DeviceIndex(firstPin + i))
			if bnObj == nil {
				// Object not found, create it
				bnObj = lw.GetObjects().AddNew(ctx)
			}
			bnObj.SetObjectID(ctx, v1.ObjectID(fmt.Sprintf("%s_pin%d", bnDev.GetDeviceID(), firstPin+i)))
			bnObj.SetObjectType(ctx, v1.ObjectTypeBinarySensor)
			conn, err := bnObj.GetConnections().GetOrAdd(v1.ConnectionNameSensor)
			if err != nil {
				return err
			}
			pin, err := conn.GetPins().GetOrAdd(0)
			if err != nil {
				return err
			}
			pin.SetDeviceID(ctx, bnDev.GetDeviceID())
			pin.SetIndex(ctx, v1.DeviceIndex(firstPin+i))
		}
		return nil
	}

	switch req.GetType() {
	case api.BinkyNetObjectsGroupType_SENSORS_8:
		// Device must be of GPIO type
		switch bnDev.GetDeviceType() {
		case v1.DeviceTypeMCP23008, v1.DeviceTypeMCP23017, v1.DeviceTypePCF8574, v1.DeviceTypeBinkyCarSensor:
			firstPin = 1
		default:
			return nil, api.InvalidArgument("Invalid device type (%s) for object group type %s", bnDev.GetDeviceType(), req.GetType().String())
		}
		if err := addSensors(8); err != nil {
			return nil, err
		}
	case api.BinkyNetObjectsGroupType_SENSORS_4:
		// Device must be of GPIO type
		switch bnDev.GetDeviceType() {
		case v1.DeviceTypeBinkyCarSensor:
			firstPin = 1
		default:
			return nil, api.InvalidArgument("Invalid device type (%s) for object group type %s", bnDev.GetDeviceType(), req.GetType().String())
		}
		if err := addSensors(4); err != nil {
			return nil, err
		}
	}
	var result api.BinkyNetLocalWorker
	if err := result.FromModel(ctx, lw); err != nil {
		return nil, err
	}
	return &result, nil
}
