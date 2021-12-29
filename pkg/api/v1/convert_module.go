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

// FromModel converts a model module to an API module
func (dst *Module) FromModel(ctx context.Context, src model.Module) error {
	dst.Id = src.GetID()
	dst.Description = src.GetDescription()
	dst.Width = int32(src.GetWidth())
	dst.Height = int32(src.GetHeight())

	src.GetBlocks().ForEach(func(x model.Block) {
		dst.Blocks = append(dst.Blocks, &BlockRef{
			Id: JoinModuleEntityID(src.GetID(), x.GetID()),
		})
	})
	return nil
}

// ToModel converts an API module to a model module
func (src *Module) ToModel(ctx context.Context, dst model.Module) error {
	if src.GetId() != dst.GetID() {
		return InvalidArgument("Unexpected module ID: '%s'", src.GetId())
	}
	dst.SetDescription(src.GetDescription())
	return nil
}
