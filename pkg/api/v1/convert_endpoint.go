// Copyright 2022 Ewout Prangsma
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

// FromModel converts a model endpoint to an API endpoint
func (dst *Endpoint) FromModel(ctx context.Context, src model.EndPoint, srcSide model.BlockSide) error {
	if b, ok := src.(model.Block); ok {
		dst.Block = &BlockRef{
			Id: JoinParentChildID(b.GetModule().GetID(), b.GetID()),
		}
		dst.Edge = nil
		dst.BlockSide.FromModel(ctx, srcSide)
	} else if e, ok := src.(model.Edge); ok {
		dst.Block = nil
		dst.Edge = &EdgeRef{
			Id: JoinParentChildID(e.GetModule().GetID(), e.GetID()),
		}
		dst.BlockSide = BlockSide_FRONT
	}
	return nil
}

// ToModel converts an API endpoint to a model endpoint
func (src *Endpoint) ToModel(ctx context.Context, module model.Module) (model.EndPoint, model.BlockSide, error) {
	if id := src.GetBlock().GetId(); id != "" {
		moduleID, blockID, err := SplitParentChildID(id)
		if err != nil {
			return nil, model.BlockSideFront, err
		}
		if moduleID != module.GetID() {
			return nil, model.BlockSideFront, InvalidArgument("Unexpected module ID: '%s'", moduleID)
		}
		b, ok := module.GetBlocks().Get(blockID)
		if !ok {
			return nil, model.BlockSideFront, InvalidArgument("Unknowm block ID: '%s'", blockID)
		}
		dstSide, err := src.GetBlockSide().ToModel(ctx)
		if err != nil {
			return nil, model.BlockSideFront, err
		}
		return b, dstSide, nil
	}
	if id := src.GetEdge().GetId(); id != "" {
		moduleID, edgeID, err := SplitParentChildID(id)
		if err != nil {
			return nil, model.BlockSideFront, err
		}
		if moduleID != module.GetID() {
			return nil, model.BlockSideFront, InvalidArgument("Unexpected module ID: '%s'", moduleID)
		}
		e, ok := module.GetEdges().Get(edgeID)
		if !ok {
			return nil, model.BlockSideFront, InvalidArgument("Unknowm edge ID: '%s'", edgeID)
		}
		return e, model.BlockSideFront, nil
	}
	return nil, model.BlockSideFront, nil
}
