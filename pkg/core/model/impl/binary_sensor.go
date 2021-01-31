// Copyright 2020 Ewout Prangsma
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

type binarySensor struct {
	sensor
}

var _ model.BinarySensor = &binarySensor{}

func newBinarySensor() *binarySensor {
	sw := &binarySensor{}
	sw.sensor.Initialize(12, 12)
	return sw
}

// GetEntityType returns the type of this entity
func (bs *binarySensor) GetEntityType() string {
	return TypeBinarySensor
}

// Accept a visit by the given visitor
func (bs *binarySensor) Accept(v model.EntityVisitor) interface{} {
	return v.VisitBinarySensor(bs)
}
