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

	api "github.com/binkynet/BinkyNet/apis/v1"
	v1 "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model/validation"
	"github.com/samber/lo"
)

// FromModel converts a model BinkyNetDevice to an API BinkyNetDevice
func (dst *BinkyNetDevice) FromModel(ctx context.Context, src model.BinkyNetDevice) error {
	dst.Id = src.GetID()
	dst.DeviceId = string(src.GetDeviceID())
	dst.DeviceType.FromModel(ctx, src.GetDeviceType())
	dst.Address = src.GetAddress()
	dst.Disabled = src.GetIsDisabled()
	dst.RouterId = ""
	if r := src.GetRouter(); r != nil {
		dst.RouterId = r.GetID()
	}
	switch src.GetDeviceType() {
	case v1.DeviceTypeGPIO, v1.DeviceTypeMCP23008, v1.DeviceTypeMCP23017, v1.DeviceTypePCF8574:
		dst.CanAddSensors_4Group = false
		dst.CanAddSensors_8Group = true
	case v1.DeviceTypeBinkyCarSensor:
		dst.CanAddSensors_4Group = true
		dst.CanAddSensors_8Group = true
	default:
		dst.CanAddSensors_4Group = false
		dst.CanAddSensors_8Group = false
	}
	dst.ValidationFindings = lo.Map(validation.Validate(src), func(item model.Finding, _ int) string {
		return item.GetDescription()
	})
	return nil
}

// ToModel converts an API BinkyNetDevice to a model BinkyNetDevice
func (src *BinkyNetDevice) ToModel(ctx context.Context, dst model.BinkyNetDevice) error {
	expectedID := dst.GetID()
	if src.GetId() != expectedID {
		return InvalidArgument("Unexpected binkynet device ID: '%s'", src.GetId())
	}
	var err error
	multierr.AppendInto(&err, dst.SetDeviceID(ctx, api.DeviceID(src.GetDeviceId())))
	if dt, err := src.GetDeviceType().ToModel(ctx); err != nil {
		return err
	} else {
		multierr.AppendInto(&err, dst.SetDeviceType(ctx, dt))
	}
	multierr.AppendInto(&err, dst.SetAddress(ctx, src.GetAddress()))
	multierr.AppendInto(&err, dst.SetIsDisabled(ctx, src.GetDisabled()))
	if id := src.GetRouterId(); id != "" {
		router, ok := dst.GetLocalWorker().GetRouters().Get(id)
		if !ok {
			return InvalidArgument("Unexpected router with id '%s'", id)
		}
		multierr.AppendInto(&err, dst.SetRouter(ctx, router))
	} else {
		multierr.AppendInto(&err, dst.SetRouter(ctx, nil))
	}
	return err
}
