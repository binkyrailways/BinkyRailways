// Copyright 2024 Ewout Prangsma
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
	"encoding/xml"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type railPoint struct {
	positionedModuleEntity
}

var _ model.RailPoint = &railPoint{}

// newRailPoint initialize a new rail point
func newRailPoint() *railPoint {
	rp := &railPoint{}
	rp.positionedModuleEntity.Initialize(20, 20)
	return rp
}

// UnmarshalXML unmarshals a rail point.
func (rp *railPoint) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&rp.positionedModuleEntity, &start); err != nil {
		return err
	}
	return nil
}

// Accept a visit by the given visitor
func (rp *railPoint) Accept(v model.EntityVisitor) interface{} {
	return v.VisitRailPoint(rp)
}
