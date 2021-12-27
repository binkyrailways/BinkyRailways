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

// FromModel converts a positioned entity to an API structure
func (dst *Position) FromModel(ctx context.Context, src model.PositionedEntity) error {
	dst.X = int32(src.GetX())
	dst.Y = int32(src.GetY())
	dst.Width = int32(src.GetWidth())
	dst.Height = int32(src.GetHeight())
	dst.Rotation = int32(src.GetRotation())
	dst.Layer = src.GetLayer()
	return nil
}
