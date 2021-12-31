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

// FromModel converts a model block to an API block
func (dst *Block) FromModel(ctx context.Context, src model.Block) error {
	dst.Id = JoinModuleEntityID(src.GetModule().GetID(), src.GetID())
	dst.Description = src.GetDescription()
	dst.ModuleId = src.GetModule().GetID()
	dst.Position = &Position{}
	if err := dst.Position.FromModel(ctx, src); err != nil {
		return err
	}
	return nil
}

// ToModel converts an API block to a model block
func (src *Block) ToModel(ctx context.Context, dst model.Block) error {
	expectedID := JoinModuleEntityID(dst.GetModule().GetID(), dst.GetID())
	if src.GetId() != expectedID {
		return InvalidArgument("Unexpected block ID: '%s'", src.GetId())
	}
	dst.SetDescription(src.GetDescription())
	if err := src.GetPosition().ToModel(ctx, dst); err != nil {
		return err
	}
	return nil
}
