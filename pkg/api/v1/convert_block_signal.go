// Copyright 2024 Ewout Prangsma
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

// FromModel converts a model BlockSignal to an API BlockSignal
func (dst *BlockSignal) FromModel(ctx context.Context, src model.BlockSignal) error {
	dst.Address1 = src.GetAddress1().String()
	dst.Address2 = src.GetAddress2().String()
	dst.Address3 = src.GetAddress3().String()
	dst.Address4 = src.GetAddress4().String()
	dst.IsRedAvailable = src.GetIsRedAvailable()
	dst.RedPattern = int32(src.GetRedPattern())
	dst.IsGreenAvailable = src.GetIsGreenAvailable()
	dst.GreenPattern = int32(src.GetGreenPattern())
	dst.IsYellowAvailable = src.GetIsYellowAvailable()
	dst.YellowPattern = int32(src.GetYellowPattern())
	dst.IsWhiteAvailable = src.GetIsWhiteAvailable()
	dst.WhitePattern = int32(src.GetWhitePattern())
	if x := src.GetBlock(); x != nil {
		dst.Block = &BlockRef{
			Id: x.GetID(),
		}
	}
	dst.BlockSide.FromModel(ctx, src.GetPosition())
	dst.Type.FromModel(ctx, src.GetType())

	return nil
}

// ToModel converts an API BlockSignal to a model BlockSignal
func (src *BlockSignal) ToModel(ctx context.Context, dst model.BlockSignal) error {
	addr, err := model.NewAddressFromString(src.GetAddress1())
	if err != nil {
		return err
	}
	if err := dst.SetAddress1(ctx, addr); err != nil {
		return err
	}
	addr, err = model.NewAddressFromString(src.GetAddress2())
	if err != nil {
		return err
	}
	if err := dst.SetAddress2(ctx, addr); err != nil {
		return err
	}
	addr, err = model.NewAddressFromString(src.GetAddress3())
	if err != nil {
		return err
	}
	if err := dst.SetAddress3(ctx, addr); err != nil {
		return err
	}
	addr, err = model.NewAddressFromString(src.GetAddress4())
	if err != nil {
		return err
	}
	if err := dst.SetAddress4(ctx, addr); err != nil {
		return err
	}
	if err := dst.SetRedPattern(int(src.GetRedPattern())); err != nil {
		return err
	}
	if err := dst.SetGreenPattern(int(src.GetGreenPattern())); err != nil {
		return err
	}
	if err := dst.SetYellowPattern(int(src.GetYellowPattern())); err != nil {
		return err
	}
	if err := dst.SetWhitePattern(int(src.GetWhitePattern())); err != nil {
		return err
	}
	var block model.Block
	if id := src.GetBlock().GetId(); id != "" {
		var ok bool
		block, ok = dst.GetModule().GetBlocks().Get(id)
		if block == nil || !ok {
			return InvalidArgument("Invalid block '%s'", id)
		}
	}
	if err := dst.SetBlock(block); err != nil {
		return err
	}
	dstSide, err := src.GetBlockSide().ToModel(ctx)
	if err != nil {
		return err
	}
	if err := dst.SetPosition(dstSide); err != nil {
		return nil
	}
	blockType, err := src.GetType().ToModel(ctx)
	if err != nil {
		return err
	}
	if err := dst.SetType(blockType); err != nil {
		return err
	}
	return nil
}
