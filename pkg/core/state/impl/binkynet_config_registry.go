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

	api "github.com/binkynet/BinkyNet/apis/v1"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// binkyNetCommandStation implements the BinkyNetCommandStation.
type binkyNetConfigRegistry struct {
	lwSet                model.BinkyNetLocalWorkerSet
	onUnknownLocalWorker func(hardwareID string)
	isObjectUsed         func(model.BinkyNetObject) bool

	lwConfig map[string]api.LocalWorkerConfig
}

// Create a new registry
func newBinkyNetConfigRegistry(lwSet model.BinkyNetLocalWorkerSet,
	onUnknownLocalWorker func(hardwareID string),
	isObjectUsed func(model.BinkyNetObject) bool) *binkyNetConfigRegistry {
	cs := &binkyNetConfigRegistry{
		lwSet:                lwSet,
		onUnknownLocalWorker: onUnknownLocalWorker,
		isObjectUsed:         isObjectUsed,
	}
	cs.Reconfigure()
	return cs
}

// Rebuild config from entity
func (r *binkyNetConfigRegistry) Reconfigure() {
	lwConfig := make(map[string]api.LocalWorkerConfig)
	r.lwSet.ForEach(func(lwModel model.BinkyNetLocalWorker) {
		// Create config
		lw := api.LocalWorkerConfig{
			Alias: lwModel.GetAlias(),
		}
		// Add devices
		lwModel.GetDevices().ForEach(func(devModel model.BinkyNetDevice) {
			if !devModel.GetIsDisabled() {
				d := &api.Device{
					Id:      devModel.GetDeviceID(),
					Type:    devModel.GetDeviceType(),
					Address: devModel.GetAddress(),
				}
				lw.Devices = append(lw.Devices, d)
			}
		})
		// Add objects
		lwModel.GetObjects().ForEach(func(objModel model.BinkyNetObject) {
			if !r.isObjectUsed(objModel) {
				return
			}
			disabled := false
			o := &api.Object{
				Id:            objModel.GetObjectID(),
				Type:          objModel.GetObjectType(),
				Configuration: make(map[api.ObjectConfigKey]string),
			}
			objModel.GetConfiguration().ForEach(func(k, v string) {
				o.Configuration[api.ObjectConfigKey(k)] = v
			})
			objModel.GetConnections().ForEach(func(cm model.BinkyNetConnection) {
				if anyPinsHaveDisabledDevice(cm, lwModel) {
					// Using a disabled device
					disabled = true
				} else if allPinsHaveNoDevice(cm) {
					// No device configured for this connection, ignore it
				} else {
					conn := &api.Connection{
						Key: cm.GetKey(),
					}
					cm.GetPins().ForEach(func(pm model.BinkyNetDevicePin) {
						conn.Pins = append(conn.Pins, &api.DevicePin{
							DeviceId: pm.GetDeviceID(),
							Index:    pm.GetIndex(),
						})
					})
					conn.Configuration = make(map[api.ConfigKey]string)
					if lwModel.GetLocalWorkerType() == model.BinkynetLocalWorkerTypeEsphome {
						conn.Configuration[api.ConfigKeyMQTTStateTopic] = objModel.GetMQTTStateTopic(cm.GetKey())
						conn.Configuration[api.ConfigKeyMQTTCommandTopic] = objModel.GetMQTTCommandTopic(cm.GetKey())
					}
					cm.GetConfiguration().ForEach(func(k, v string) {
						conn.Configuration[api.ConfigKey(k)] = v
					})
					o.Connections = append(o.Connections, conn)
				}
			})
			if !disabled {
				lw.Objects = append(lw.Objects, o)
			}
		})
		// Store config
		lwConfig[lwModel.GetHardwareID()] = lw
	})
	r.lwConfig = lwConfig
}

// allPinsHaveNoDevice returns true if all of the pins in the
// given connection have an empty device ID.
func allPinsHaveNoDevice(cm model.BinkyNetConnection) bool {
	anyDevice := false
	cm.GetPins().ForEach(func(pin model.BinkyNetDevicePin) {
		if pin.GetDeviceID() != "" {
			anyDevice = true
		}
	})
	return !anyDevice
}

// anyPinsHaveDisabledDevice returns true if any of the pins in the
// given connection refer to a disabled device.
func anyPinsHaveDisabledDevice(cm model.BinkyNetConnection, lw model.BinkyNetLocalWorker) bool {
	foundDisabledDevice := false
	cm.GetPins().ForEach(func(pin model.BinkyNetDevicePin) {
		if id := pin.GetDeviceID(); id != "" {
			if dev, found := lw.GetDevices().Get(id); found {
				if dev.GetIsDisabled() {
					foundDisabledDevice = true
				}
			}
		}
	})
	return foundDisabledDevice
}

// Get returns the configuration for a worker with given hardware ID.
func (r *binkyNetConfigRegistry) Get(hardwareID string) (api.LocalWorkerConfig, error) {
	result, found := r.lwConfig[hardwareID]
	if !found {
		if r.onUnknownLocalWorker != nil {
			r.onUnknownLocalWorker(hardwareID)
		}
		return api.LocalWorkerConfig{}, api.NotFound(hardwareID)
	}
	return result, nil
}

// Get returns the configuration for a worker with given hardware ID.
func (r *binkyNetConfigRegistry) ForEach(ctx context.Context, cb func(context.Context, api.LocalWorker) error) error {
	for id, cfg := range r.lwConfig {
		if err := cb(ctx, api.LocalWorker{
			Id:      id,
			Request: &cfg,
		}); err != nil {
			return err
		}
	}
	return nil
}
