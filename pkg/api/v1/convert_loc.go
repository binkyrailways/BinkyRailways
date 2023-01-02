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
	fmt "fmt"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// FromModel converts a model loc to an API loc
func (dst *Loc) FromModel(ctx context.Context, src model.Loc, httpHost string) error {
	dst.Id = src.GetID()
	dst.Description = src.GetDescription()
	dst.Owner = src.GetOwner()
	dst.Remarks = src.GetRemarks()
	dst.Address = src.GetAddress().String()
	if len(src.GetImage()) > 0 {
		dst.ImageUrl = fmt.Sprintf("http://%s/loc/%s/image", httpHost, src.GetID())
	}
	dst.SlowSpeed = int32(src.GetSlowSpeed())
	dst.MediumSpeed = int32(src.GetMediumSpeed())
	dst.MaximumSpeed = int32(src.GetMaximumSpeed())
	dst.SpeedSteps = int32(src.GetSpeedSteps())
	if err := dst.ChangeDirection.FromModel(ctx, src.GetChangeDirection()); err != nil {
		return err
	}
	return nil
}

// ToModel converts an API loc to a model loc
func (src *Loc) ToModel(ctx context.Context, dst model.Loc) error {
	if src.GetId() != dst.GetID() {
		return InvalidArgument("Unexpected loc ID: '%s'", src.GetId())
	}
	dst.SetDescription(src.GetDescription())
	dst.SetOwner(src.GetOwner())
	dst.SetRemarks(src.GetRemarks())
	addr, err := model.NewAddressFromString(src.GetAddress())
	if err != nil {
		return err
	}
	dst.SetAddress(ctx, addr)
	dst.SetSlowSpeed(int(src.GetSlowSpeed()))
	dst.SetMediumSpeed(int(src.GetMediumSpeed()))
	dst.SetMaximumSpeed(int(src.GetMaximumSpeed()))
	dst.SetSpeedSteps(int(src.GetSpeedSteps()))
	cd, err := src.GetChangeDirection().ToModel(ctx)
	if err != nil {
		return err
	}
	dst.SetChangeDirection(cd)
	return nil
}
