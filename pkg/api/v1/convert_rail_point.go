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

// FromModel converts a model rail point to an API rail point
func (dst *RailPoint) FromModel(ctx context.Context, src model.RailPoint) error {
	dst.Id = JoinParentChildID(src.GetModule().GetID(), src.GetID())
	dst.ModuleId = src.GetModule().GetID()
	dst.Position = &Position{
		X: int32(src.GetX()),
		Y: int32(src.GetY()),
	}
	return nil
}

// ToModel converts an API rail point to a model rail point
func (src *RailPoint) ToModel(ctx context.Context, dst model.RailPoint) error {
	expectedID := JoinParentChildID(dst.GetModule().GetID(), dst.GetID())
	if src.GetId() != expectedID {
		return InvalidArgument("Unexpected rail point ID: '%s'", src.GetId())
	}
	pos := src.GetPosition()
	dst.SetX(int(pos.GetX()))
	dst.SetY(int(pos.GetY()))
	return nil
}
