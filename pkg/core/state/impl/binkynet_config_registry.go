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
	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkynet/NetManager/service/config"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// binkyNetCommandStation implements the BinkyNetCommandStation.
type binkyNetConfigRegistry struct {
	lwSet model.BinkyNetLocalWorkerSet

	lwConfig map[string]api.LocalWorkerConfig
}

// Create a new registry
func newBinkyNetConfigRegistry(lwSet model.BinkyNetLocalWorkerSet) config.Registry {
	cs := &binkyNetConfigRegistry{
		lwSet: lwSet,
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
			d := &api.Device{
				Id:      devModel.GetDeviceID(),
				Type:    devModel.GetDeviceType(),
				Address: devModel.GetAddress(),
			}
			lw.Devices = append(lw.Devices, d)
		})
		// Add objects
		lwModel.GetObjects().ForEach(func(objModel model.BinkyNetObject) {
			o := &api.Object{
				Id:   objModel.GetObjectID(),
				Type: objModel.GetObjectType(),
			}
			objModel.GetConnections().ForEach(func(cm model.BinkyNetConnection) {
				conn := &api.Connection{
					Key: cm.GetKey(),
				}
				cm.GetPins().ForEach(func(pm model.BinkyNetDevicePin) {
					conn.Pins = append(conn.Pins, &api.DevicePin{
						DeviceId: pm.GetDeviceID(),
						Index:    pm.GetIndex(),
					})
				})
				conn.Configuration = make(map[string]string)
				cm.GetConfiguration().ForEach(func(k, v string) {
					conn.Configuration[k] = v
				})
				o.Connections = append(o.Connections, conn)
			})
			lw.Objects = append(lw.Objects, o)
		})
		// Store config
		lwConfig[lwModel.GetID()] = lw
	})
	r.lwConfig = lwConfig
}

// Get returns the configuration for a worker with given ID.
func (r *binkyNetConfigRegistry) Get(id string) (api.LocalWorkerConfig, error) {
	result, found := r.lwConfig[id]
	if !found {
		return api.LocalWorkerConfig{}, api.NotFound(id)
	}
	return result, nil
}
