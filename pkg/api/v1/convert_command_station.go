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

// FromModel converts a model command station to an API command station
func (dst *CommandStation) FromModel(ctx context.Context, src model.CommandStation) error {
	dst.Id = src.GetID()
	dst.Description = src.GetDescription()
	if bncs, ok := src.(model.BinkyNetCommandStation); ok {
		dst.BinkynetCommandStation = &BinkyNetCommandStation{}
		if err := dst.BinkynetCommandStation.FromModel(ctx, bncs); err != nil {
			return err
		}
	}
	return nil
}

// ToModel converts an API command station to a model command station
func (src *CommandStation) ToModel(ctx context.Context, dst model.CommandStation) error {
	if src.GetId() != dst.GetID() {
		return InvalidArgument("Unexpected command station ID: '%s'", src.GetId())
	}
	var err error
	multierr.AppendInto(&err, dst.SetDescription(src.GetDescription()))
	if bncs, ok := dst.(model.BinkyNetCommandStation); ok {
		if src.GetBinkynetCommandStation() == nil {
			return InvalidArgument("Expected BinkynetCommandStation")
		}
		if err := src.BinkynetCommandStation.ToModel(ctx, bncs); err != nil {
			return err
		}
	}
	return err
}
