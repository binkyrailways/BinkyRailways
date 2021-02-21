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
	"github.com/binkyrailways/BinkyRailways/pkg/app/canvas"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// builder implements an entity visitor to create canvas widgets.
type builder struct {
	model.DefaultEntityVisitor
}

// NewBuilder creates an entity visitor to create canvas widgets.
func NewBuilder() canvas.WidgetBuilder {
	return &builder{}
}

// Create a widget for the given entity.
// A return value of nil means no widget.
func (v *builder) CreateWidget(x canvas.Entity) canvas.Widget {
	entity := x.(model.Entity)
	raw := entity.Accept(v)
	if w, ok := raw.(canvas.Widget); ok {
		return w
	}
	return nil
}

func (v *builder) VisitBlock(x model.Block) interface{} {
	return &block{
		entity: x,
	}
}

func (v *builder) VisitBinarySensor(x model.BinarySensor) interface{} {
	return &sensor{
		entity: x,
	}
}

func (v *builder) VisitSensor(x model.Sensor) interface{} {
	return &sensor{
		entity: x,
	}
}

func (v *builder) VisitSwitch(x model.Switch) interface{} {
	return &stdSwitch{
		entity: x,
	}
}
