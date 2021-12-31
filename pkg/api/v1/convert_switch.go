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

// FromModel converts a model Switch to an API Switch
func (dst *Switch) FromModel(ctx context.Context, src model.Switch) error {
	dst.Address = src.GetAddress().String()
	dst.HasFeedback = src.GetHasFeedback()
	dst.FeedbackAddress = src.GetFeedbackAddress().String()
	dst.SwitchDuration = int32(src.GetSwitchDuration())
	dst.Invert = src.GetInvert()
	dst.InvertFeedback = src.GetInvertFeedback()
	if err := dst.InitialDirection.FromModel(ctx, src.GetInitialDirection()); err != nil {
		return err
	}
	return nil
}

// ToModel converts an API Switch to a model Switch
func (src *Switch) ToModel(ctx context.Context, dst model.Switch) error {
	addr, err := model.NewAddressFromString(src.GetAddress())
	if err != nil {
		return err
	}
	if err := dst.SetAddress(addr); err != nil {
		return err
	}
	if err := dst.SetHasFeedback(src.GetHasFeedback()); err != nil {
		return err
	}
	addr, err = model.NewAddressFromString(src.GetFeedbackAddress())
	if err != nil {
		return err
	}
	if err := dst.SetFeedbackAddress(addr); err != nil {
		return err
	}
	if err := dst.SetSwitchDuration(int(src.GetSwitchDuration())); err != nil {
		return err
	}
	if err := dst.SetInvert(src.GetInvert()); err != nil {
		return err
	}
	if err := dst.SetInvertFeedback(src.GetInvertFeedback()); err != nil {
		return err
	}
	dir, err := src.GetInitialDirection().ToModel(ctx)
	if err != nil {
		return err
	}
	if err := dst.SetInitialDirection(dir); err != nil {
		return err
	}
	return nil
}
