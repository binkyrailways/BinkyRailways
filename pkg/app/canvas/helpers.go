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

package canvas

import (
	"math"

	"gioui.org/f32"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// getPositionedEntityBounds returns the bounds of the given entity.
func getPositionedEntityBounds(entity model.PositionedEntity) f32.Rectangle {
	return f32.Rectangle{
		Min: f32.Pt(float32(entity.GetX()), float32(entity.GetY())),
		Max: f32.Pt(float32(entity.GetX()+entity.GetWidth()), float32(entity.GetY()+entity.GetHeight())),
	}
}

// GetPositionedEntityAffineAndSize returns a matrix for positioning the entity
// and the size of the clipping area.
// Note that scaling is not applied.
// Returns: Matrix, Size, Rotation (in radials, already applied in Matrix)
func GetPositionedEntityAffineAndSize(entity model.PositionedEntity) (f32.Affine2D, f32.Point, float32) {
	// Prepare transformation
	bounds := getPositionedEntityBounds(entity)
	// Translate, scale & rotate
	rot := entity.GetRotation()
	rad := float32(rot%360) * (math.Pi / 180)
	tr := f32.Affine2D{}.
		Offset(bounds.Min).
		Rotate(bounds.Min, rad)
	// Set clip rectangle
	size := bounds.Size()
	return tr, size, rad
}
