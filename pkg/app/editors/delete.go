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

package editors

import (
	"context"
	"fmt"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// createOnDelete an onDelete helper for the given entity
func createOnDelete(etx EditorContext, entity interface{}) (string, func(context.Context) error) {
	if entity == nil {
		return "", nil
	}
	switch entity := entity.(type) {
	case model.Module:
		return "Module: " + entity.GetDescription(), func(c context.Context) error {
			pkg := entity.GetPackage()
			railway := pkg.GetRailway()
			if modRef, found := railway.GetModules().Get(entity.GetID()); found {
				if !railway.GetModules().Remove(modRef) {
					return fmt.Errorf("Failed to remove module")
				}
			}
			if err := pkg.Remove(entity); err != nil {
				return fmt.Errorf("Failed to remove module from package: %s", err)
			}
			return nil
		}
	case model.Block:
		if module := entity.GetModule(); module != nil {
			return "Block: " + entity.GetDescription(), func(c context.Context) error {
				if !module.GetBlocks().Remove(entity) {
					return fmt.Errorf("Failed to remove block from module")
				}
				return nil
			}
		}
	case model.BlockGroup:
		if module := entity.GetModule(); module != nil {
			return "Block group: " + entity.GetDescription(), func(c context.Context) error {
				if !module.GetBlockGroups().Remove(entity) {
					return fmt.Errorf("Failed to remove block group from module")
				}
				return nil
			}
		}
	case model.Edge:
		if module := entity.GetModule(); module != nil {
			return "Edge: " + entity.GetDescription(), func(c context.Context) error {
				if !module.GetEdges().Remove(entity) {
					return fmt.Errorf("Failed to remove edge from module")
				}
				return nil
			}
		}
	case model.Junction:
		if module := entity.GetModule(); module != nil {
			return "Junction: " + entity.GetDescription(), func(c context.Context) error {
				if !module.GetJunctions().Remove(entity) {
					return fmt.Errorf("Failed to remove junction from module")
				}
				return nil
			}
		}
	case model.Output:
		if module := entity.GetModule(); module != nil {
			return "Output: " + entity.GetDescription(), func(c context.Context) error {
				if !module.GetOutputs().Remove(entity) {
					return fmt.Errorf("Failed to remove output from module")
				}
				return nil
			}
		}
	case model.Route:
		if module := entity.GetModule(); module != nil {
			return "Route: " + entity.GetDescription(), func(c context.Context) error {
				if !module.GetRoutes().Remove(entity) {
					return fmt.Errorf("Failed to remove route from module")
				}
				return nil
			}
		}
	case model.Sensor:
		if module := entity.GetModule(); module != nil {
			return "Sensor: " + entity.GetDescription(), func(c context.Context) error {
				if !module.GetSensors().Remove(entity) {
					return fmt.Errorf("Failed to remove sensor from module")
				}
				return nil
			}
		}
	case model.Signal:
		if module := entity.GetModule(); module != nil {
			return "Signal: " + entity.GetDescription(), func(c context.Context) error {
				if !module.GetSignals().Remove(entity) {
					return fmt.Errorf("Failed to remove signal from module")
				}
				return nil
			}
		}
	case model.BinkyNetLocalWorker:
		return "BinkyNet local worker: " + entity.GetDescription(), func(c context.Context) error {
			cs := entity.GetCommandStation()
			if !cs.GetLocalWorkers().Remove(entity) {
				return fmt.Errorf("Failed to remove local worker")
			}
			return nil
		}
	case model.BinkyNetDevice:
		if lw := entity.GetLocalWorker(); lw != nil {
			return "BinkyNet device: " + entity.GetDescription(), func(c context.Context) error {
				if !lw.GetDevices().Remove(entity) {
					return fmt.Errorf("Failed to remove device")
				}
				return nil
			}
		}
	case model.BinkyNetObject:
		if lw := entity.GetLocalWorker(); lw != nil {
			return "BinkyNet object: " + entity.GetDescription(), func(c context.Context) error {
				if !lw.GetObjects().Remove(entity) {
					return fmt.Errorf("Failed to remove object")
				}
				return nil
			}
		}
	}
	return "", nil
}
