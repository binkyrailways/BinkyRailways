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

package impl

import (
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// builder is used to build state objects for entities.
type builder struct {
	Railway Railway

	model.DefaultEntityVisitor
}

func (b *builder) VisitBinarySensor(x model.BinarySensor) interface{} {
	return newBinarySensor(x, b.Railway)
}

func (b *builder) VisitBlock(x model.Block) interface{} {
	return newBlock(x, b.Railway)
}

func (b *builder) VisitLoc(x model.Loc) interface{} {
	return newLoc(x, b.Railway)
}

func (b *builder) VisitRoute(x model.Route) interface{} {
	return newRoute(x, b.Railway)
}

func (b *builder) VisitBinkyNetCommandStation(x model.BinkyNetCommandStation) interface{} {
	return newBinkyNetCommandStation(x, b.Railway)
}

func (b *builder) VisitLocoBufferCommandStation(x model.LocoBufferCommandStation) interface{} {
	return newLocoBufferCommandStation(x, b.Railway)
}
