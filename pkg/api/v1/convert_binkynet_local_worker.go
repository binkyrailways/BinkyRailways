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

package v1

import (
	context "context"

	"go.uber.org/multierr"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// FromModel converts a model BinkyNetLocalWorker to an API BinkyNetLocalWorker
func (dst *BinkyNetLocalWorker) FromModel(ctx context.Context, src model.BinkyNetLocalWorker) error {
	dst.Id = JoinParentChildID(src.GetCommandStation().GetID(), src.GetID())
	dst.Description = src.GetDescription()
	dst.CommandStationId = src.GetCommandStation().GetID()
	dst.HardwareId = src.GetHardwareID()
	dst.Alias = src.GetAlias()
	src.GetDevices().ForEach(func(src model.BinkyNetDevice) {
		bd := &BinkyNetDevice{}
		bd.FromModel(ctx, src)
		dst.Devices = append(dst.Devices, bd)
	})
	src.GetObjects().ForEach(func(src model.BinkyNetObject) {
		bo := &BinkyNetObject{}
		bo.FromModel(ctx, src)
		dst.Objects = append(dst.Objects, bo)
	})
	return nil
}

// ToModel converts an API BinkyNetLocalWorker to a model BinkyNetLocalWorker
func (src *BinkyNetLocalWorker) ToModel(ctx context.Context, dst model.BinkyNetLocalWorker) error {
	expectedID := JoinParentChildID(dst.GetCommandStation().GetID(), dst.GetID())
	if src.GetId() != expectedID {
		return InvalidArgument("Unexpected binkynet local worker ID: '%s'", src.GetId())
	}
	if len(src.GetDevices()) != dst.GetDevices().GetCount() {
		return InvalidArgument("Unexpected number of devices in local worker (got %d, expected %d)", len(src.GetDevices()), dst.GetDevices().GetCount())
	}
	if len(src.GetObjects()) != dst.GetObjects().GetCount() {
		return InvalidArgument("Unexpected number of objects in local worker (got %d, expected %d)", len(src.GetObjects()), dst.GetObjects().GetCount())
	}
	var err error
	multierr.AppendInto(&err, dst.SetDescription(src.GetDescription()))
	multierr.AppendInto(&err, dst.SetHardwareID(src.GetHardwareId()))
	multierr.AppendInto(&err, dst.SetAlias(src.GetAlias()))
	for i, src := range src.GetDevices() {
		dst, ok := dst.GetDevices().GetAt(i)
		if !ok {
			return InvalidArgument("Unexpected device at index %d", i)
		}
		multierr.AppendInto(&err, src.ToModel(ctx, dst))
	}
	for i, src := range src.GetObjects() {
		dst, ok := dst.GetObjects().GetAt(i)
		if !ok {
			return InvalidArgument("Unexpected object at index %d", i)
		}
		multierr.AppendInto(&err, src.ToModel(ctx, dst))
	}
	return err
}
