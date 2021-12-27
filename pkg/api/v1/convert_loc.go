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

// FromModel converts a loc module to an API loc
func (dst *Loc) FromModel(ctx context.Context, src model.Loc) error {
	dst.Id = src.GetID()
	dst.Description = src.GetDescription()
	dst.Owner = src.GetOwner()
	dst.Remarks = src.GetRemarks()
	dst.Address = &Address{}
	dst.Address.FromModel(ctx, src.GetAddress())
	dst.SlowSpeed = int32(src.GetSlowSpeed())
	dst.MediumSpeed = int32(src.GetMediumSpeed())
	dst.MaximumSpeed = int32(src.GetMaximumSpeed())
	dst.SpeedSteps = int32(src.GetSpeedSteps())
	dst.ChangeDirection.FromModel(ctx, src.GetChangeDirection())
	return nil
}
