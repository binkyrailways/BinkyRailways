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

import "github.com/binkyrailways/BinkyRailways/pkg/core/model"

// LocGroupEqualsPredicate extends implementation methods to model.LocGroupEqualsPredicate
type LocGroupEqualsPredicate interface {
	ModuleEntity
	model.LocGroupEqualsPredicate
}

type locGroupEqualsPredicate struct {
	locPredicate

	GroupID locGroupRef `xml:"GroupId"`
}

var _ model.LocGroupEqualsPredicate = &locGroupEqualsPredicate{}

// newLocGroupEqualsPredicate creates a new part-of-group predicate
func newLocGroupEqualsPredicate() *locGroupEqualsPredicate {
	p := &locGroupEqualsPredicate{}
	return p
}

// Accept a visit by the given visitor
func (p *locGroupEqualsPredicate) Accept(v model.EntityVisitor) interface{} {
	return v.VisitLocGroupEqualsPredicate(p)
}

// Gets the set of nested predicates.
func (p *locGroupEqualsPredicate) SetContainer(parent ModuleEntityContainer) {
	p.locPredicate.SetContainer(parent)
	p.GroupID.SetResolver(func(id string) model.LocGroup {
		m, ok := p.GetModule().(Module)
		if !ok || m == nil {
			return nil
		}
		pkg := m.GetPackage()
		if pkg == nil {
			return nil
		}
		if gr, ok := pkg.GetRailway().GetLocGroups().Get(id); ok {
			return gr
		}
		return nil
	})
}

// Gets/Sets the loc to compare to.
func (p *locGroupEqualsPredicate) GetGroup() model.LocGroup {
	return p.GroupID.TryResolve()
}
func (p *locGroupEqualsPredicate) SetGroup(value model.LocGroup) error {
	if value == nil {
		p.GroupID.id = ""
	} else {
		p.GroupID.id = value.GetID()
	}
	return nil
}

// Create a deep clone.
func (p *locGroupEqualsPredicate) Clone() model.LocPredicate {
	c := newLocGroupEqualsPredicate()
	c.GroupID.id = p.GroupID.id
	return c
}

// Evaluate this predicate for the given loc.
func (p *locGroupEqualsPredicate) Evaluate(loc model.Loc) bool {
	group := p.GetGroup()
	if group == nil || loc == nil {
		return false
	}
	return group.GetLocs().ContainsID(loc.GetID())
}
