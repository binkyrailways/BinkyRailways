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

// LocGroup extends implementation methods to model.LocGroup
type LocGroup interface {
	RailwayEntity
	model.LocGroup
}

type locGroup struct {
	railwayEntity

	Locs groupLocRefSet `xml:"Locs"`
}

var (
	_ LocGroup = &locGroup{}
)

// newLocGroup initialize a new loc group
func newLocGroup() *locGroup {
	m := &locGroup{}
	m.EnsureID()
	m.Locs.SetContainer(m)
	return m
}

// Accept a visit by the given visitor
func (p *locGroup) Accept(v model.EntityVisitor) interface{} {
	return v.VisitLocGroup(p)
}

// Set of locs which make up this group.
func (lg *locGroup) GetLocs() model.LocRefSet {
	return &lg.Locs
}
