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
	"bytes"
	"fmt"
	"image"

	"gioui.org/f32"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// ModuleCanvas creates an entity canvas for the given module.
func ModuleCanvas(module model.Module, builder WidgetBuilder) *EntityCanvas {
	bounds := f32.Rect(0, 0, float32(module.GetWidth()), float32(module.GetHeight()))
	ec := &EntityCanvas{
		GetMaxSize: func() f32.Point {
			return bounds.Size()
		},
		Entities: func(cb func(Entity)) {
			module.ForEachPositionedEntity(func(x model.PositionedEntity) {
				cb(x)
			})
		},
		Builder: builder,
		scale:   1,
	}
	if bgImage := module.GetBackgroundImage(); bgImage != nil {
		if img, _, err := image.Decode(bytes.NewReader(bgImage)); err != nil {
			fmt.Println(err)
		} else if img != nil {
			bgWidget := NewImageWidget(bounds, img)
			ec.SetBackground(bgWidget)
		}
	}
	return ec
}
