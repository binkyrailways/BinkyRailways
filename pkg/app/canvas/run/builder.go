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

package run

import (
	"github.com/binkyrailways/BinkyRailways/pkg/app/canvas"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// builder implements an entity visitor to create canvas widgets.
type builder struct {
}

// NewBuilder creates an entity visitor to create canvas widgets.
func NewBuilder() canvas.WidgetBuilder {
	return &builder{}
}

// Create a widget for the given entity.
// A return value of nil means no widget.
func (v *builder) CreateWidget(x canvas.Entity) canvas.Widget {
	switch entity := x.(type) {
	case state.Block:
		return &block{entity: entity}
	default:
		return nil
	}
}
