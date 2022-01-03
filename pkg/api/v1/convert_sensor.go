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

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"go.uber.org/multierr"
)

// FromModel converts a model Sensor to an API Sensor
func (dst *Sensor) FromModel(ctx context.Context, src model.Sensor) error {
	dst.Id = JoinParentChildID(src.GetModule().GetID(), src.GetID())
	dst.Description = src.GetDescription()
	dst.ModuleId = src.GetModule().GetID()
	dst.Position = &Position{}
	if err := dst.Position.FromModel(ctx, src); err != nil {
		return err
	}
	dst.Address = src.GetAddress().String()
	if b := src.GetBlock(); b != nil {
		dst.Block = &BlockRef{
			Id: b.GetID(),
		}
	}
	dst.Shape.FromModel(ctx, src.GetShape())
	if _, ok := src.(model.BinarySensor); ok {
		dst.BinarySensor = &BinarySensor{}
	}
	return nil
}

// ToModel converts an API Sensor to a model Sensor
func (src *Sensor) ToModel(ctx context.Context, dst model.Sensor) error {
	expectedID := JoinParentChildID(dst.GetModule().GetID(), dst.GetID())
	if src.GetId() != expectedID {
		return InvalidArgument("Unexpected sensor ID: '%s'", src.GetId())
	}
	addr, err := model.NewAddressFromString(src.GetAddress())
	if err != nil {
		return err
	}
	shape, err := src.GetShape().ToModel(ctx)
	if err != nil {
		return err
	}
	var block model.Block
	if id := src.GetBlock().GetId(); id != "" {
		var ok bool
		block, ok = dst.GetModule().GetBlocks().Get(id)
		if !ok {
			return InvalidArgument("Unknown block '%s'", id)
		}
	}
	multierr.AppendInto(&err, dst.SetDescription(src.GetDescription()))
	multierr.AppendInto(&err, src.GetPosition().ToModel(ctx, dst))
	multierr.AppendInto(&err, dst.SetAddress(addr))
	multierr.AppendInto(&err, dst.SetShape(shape))
	multierr.AppendInto(&err, dst.SetBlock(block))
	return err
}
