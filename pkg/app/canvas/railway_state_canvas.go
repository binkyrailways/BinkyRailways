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
	"gioui.org/f32"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// RailwayStateCanvas creates an entity canvas for the given railway in running mode.
func RailwayStateCanvas(railway state.Railway, builder WidgetBuilder) *EntityCanvas {
	//rm := railway.GetModel()
	ec := &EntityCanvas{
		GetMaxSize: func() f32.Point {
			return f32.Point{
				X: float32(200 /* TODO rm.GetWidth()*/),
				Y: float32(200 /* TODO rm.GetHeight() */),
			}
		},
		Entities: func(cb func(Entity)) {
			railway.ForEachBlock(func(x state.Block) {
				cb(x)
			})
		},
		Builder: builder,
		scale:   1,
	}
	/*	if bgImage := module.GetBackgroundImage(); bgImage != nil {
		if img, format, err := image.Decode(bytes.NewReader(bgImage)); err != nil {
			fmt.Println(err)
		} else if img != nil {
			fmt.Printf("Found format '%s'\n", format)
			ec.SetBackground(img)
		}
	}*/
	return ec
}
