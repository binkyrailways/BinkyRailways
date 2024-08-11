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
)

// FromModel converts a model Signal to an API Signal
func (dst *Signal) FromModel(ctx context.Context, src model.Signal) error {
	dst.Id = JoinParentChildID(src.GetModule().GetID(), src.GetID())
	dst.Description = src.GetDescription()
	dst.ModuleId = src.GetModule().GetID()
	dst.Position = &Position{}
	if err := dst.Position.FromModel(ctx, src); err != nil {
		return err
	}
	if bs, ok := src.(model.BlockSignal); ok {
		dst.BlockSignal = &BlockSignal{}
		if err := dst.BlockSignal.FromModel(ctx, bs); err != nil {
			return err
		}
	}
	return nil
}

// ToModel converts an API Signal to a model Signal
func (src *Signal) ToModel(ctx context.Context, dst model.Signal) error {
	expectedID := JoinParentChildID(dst.GetModule().GetID(), dst.GetID())
	if src.GetId() != expectedID {
		return InvalidArgument("Unexpected signal ID: '%s'", src.GetId())
	}
	dst.SetDescription(src.GetDescription())
	if err := src.GetPosition().ToModel(ctx, dst); err != nil {
		return err
	}
	if bs, ok := dst.(model.BlockSignal); ok {
		bsSrc := src.GetBlockSignal()
		if bsSrc == nil {
			return InvalidArgument("Expected block signal")
		}
		if err := bsSrc.ToModel(ctx, bs); err != nil {
			return err
		}
	}
	return nil
}
