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

package edit

import (
	"gioui.org/f32"
	"gioui.org/layout"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type (
	C = layout.Context
	D = layout.Dimensions
)

// getPositionedEntityBounds returns the bounds of the given entity.
func getPositionedEntityBounds(entity model.PositionedEntity) f32.Rectangle {
	return f32.Rectangle{
		Min: f32.Point{
			X: float32(entity.GetX()),
			Y: float32(entity.GetY()),
		},
		Max: f32.Point{
			X: float32(entity.GetX() + entity.GetWidth()),
			Y: float32(entity.GetY() + entity.GetHeight()),
		},
	}
}