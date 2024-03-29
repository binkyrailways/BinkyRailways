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

// FromModel converts a model BinaryOutput to an API BinaryOutput
func (dst *BinaryOutput) FromModel(ctx context.Context, src model.BinaryOutput) error {
	dst.Address = src.GetAddress().String()
	dst.OutputType.FromModel(ctx, src.GetBinaryOutputType())
	dst.ActiveText = src.GetActiveText()
	dst.InactiveText = src.GetInactiveText()
	return nil
}

// ToModel converts an API BinaryOutput to a model BinaryOutput
func (src *BinaryOutput) ToModel(ctx context.Context, dst model.BinaryOutput) error {
	addr, err := model.NewAddressFromString(src.GetAddress())
	if err != nil {
		return err
	}
	bot, err := src.GetOutputType().ToModel(ctx)
	if err != nil {
		return err
	}
	multierr.AppendInto(&err, dst.SetAddress(ctx, addr))
	multierr.AppendInto(&err, dst.SetBinaryOutputType(ctx, bot))
	multierr.AppendInto(&err, dst.SetActiveText(ctx, src.GetActiveText()))
	multierr.AppendInto(&err, dst.SetInactiveText(ctx, src.GetInactiveText()))
	return nil
}
