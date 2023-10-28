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

// FromModel converts a shape to an API type
func (dst *Shape) FromModel(ctx context.Context, src model.Shape) error {
	switch src {
	case model.ShapeCircle:
		*dst = Shape_CIRCLE
	case model.ShapeTriangle:
		*dst = Shape_TRIANGLE
	case model.ShapeSquare:
		*dst = Shape_SQUARE
	case model.ShapeDiamond:
		*dst = Shape_DIAMOND
	}
	return nil
}

// ToModel converts a shape from an API type
func (src Shape) ToModel(ctx context.Context) (model.Shape, error) {
	switch src {
	case Shape_CIRCLE:
		return model.ShapeCircle, nil
	case Shape_TRIANGLE:
		return model.ShapeTriangle, nil
	case Shape_SQUARE:
		return model.ShapeSquare, nil
	case Shape_DIAMOND:
		return model.ShapeDiamond, nil
	}
	return model.ShapeCircle, InvalidArgument("Unknown Shape: %s", src)
}
