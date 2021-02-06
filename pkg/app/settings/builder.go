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

package settings

import (
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// builder implements an entity visitor to create settings components.
type builder struct {
	model.DefaultEntityVisitor
}

// NewBuilder creates an entity visitor to create settings components.
func NewBuilder() model.EntityVisitor {
	return &builder{}
}

func (v *builder) VisitBlock(x model.Block) interface{} {
	return NewBlockSettings(x)
}

func (v *builder) VisitLoc(x model.Loc) interface{} {
	return NewLocSettings(x)
}

func (v *builder) VisitModule(x model.Module) interface{} {
	return NewModuleSettings(x)
}

func (v *builder) VisitRailway(x model.Railway) interface{} {
	return NewRailwaySettings(x)
}

func (v *builder) VisitRoute(x model.Route) interface{} {
	return NewRouteSettings(x)
}
