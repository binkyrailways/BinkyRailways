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
	"math"

	"gioui.org/f32"
	"github.com/binkyrailways/BinkyRailways/pkg/app/widgets"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

type moduleTransform struct {
	bounds f32.Rectangle
	rad    float32
}

// RailwayStateCanvas creates an entity canvas for the given railway in running mode.
func RailwayStateCanvas(railway state.Railway, builder WidgetBuilder) *EntityCanvas {
	// Calculate dimensions & prepare background images
	bounds := f32.Rectangle{}
	rm := railway.GetModel()
	var bgWidgets []Widget
	moduleTxs := make(map[string]moduleTransform)
	rm.GetModules().ForEach(func(modRef model.ModuleRef) {
		if module := modRef.TryResolve(); module != nil {
			// Calculate module bounds
			msz := f32.Pt(float32(module.GetWidth()), float32(module.GetHeight()))
			mofs := f32.Pt(float32(modRef.GetX()), float32(modRef.GetY()))
			mbounds := f32.Rectangle{Min: mofs, Max: mofs.Add(msz)}
			bounds = bounds.Union(mbounds)

			// Prepare module transform
			moduleTxs[module.GetID()] = moduleTransform{
				bounds: mbounds,
				rad:    float32(modRef.GetRotation()%360) * (math.Pi / 180),
			}

			// Prepare module border
			borderWidget := NewBorderWidget(mbounds, widgets.ARGB(0x88333333))
			bgWidgets = append(bgWidgets, borderWidget)

			// Prepare module background (if any)
			if bgImage := module.GetBackgroundImage(); bgImage != nil {
				if img, _, err := image.Decode(bytes.NewReader(bgImage)); err != nil {
					fmt.Println(err)
				} else if img != nil {
					bgWidget := NewImageWidget(mbounds, img)
					bgWidgets = append(bgWidgets, bgWidget)
				}
			}
		}
	})

	ec := &EntityCanvas{
		Exclusive: railway,
		GetMaxSize: func() f32.Point {
			return bounds.Size()
		},
		Transformer: func(entity Entity, tr f32.Affine2D) f32.Affine2D {
			if modEntity, ok := entity.(state.ModuleEntity); ok {
				if modTx, found := moduleTxs[modEntity.GetModuleID()]; found {
					mtr := f32.Affine2D{}.Offset(modTx.bounds.Min).Rotate(modTx.bounds.Min, modTx.rad)
					tr = mtr.Mul(tr)
				}
			}
			return tr
		},
		Entities: func(cb func(Entity)) {
			railway.ForEachBlock(func(x state.Block) {
				cb(x)
			})
			railway.ForEachJunction(func(x state.Junction) {
				cb(x)
			})
			railway.ForEachOutput(func(x state.Output) {
				cb(x)
			})
			railway.ForEachSensor(func(x state.Sensor) {
				cb(x)
			})
			railway.ForEachSignal(func(x state.Signal) {
				cb(x)
			})
		},
		Builder: builder,
		scale:   1,
	}
	ec.SetBackground(bgWidgets...)
	return ec
}
