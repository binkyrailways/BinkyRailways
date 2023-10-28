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

// LocStandardPredicate extends implementation methods to model.LocStandardPredicate
type LocStandardPredicate interface {
	LocPredicate
	model.LocStandardPredicate
}

type locStandardPredicate struct {
	locPredicate

	Includes locOrPredicate `xml:"Includes"`
	Excludes locOrPredicate `xml:"Excludes"`
}

var _ model.LocStandardPredicate = &locStandardPredicate{}

func newLocStandardPredicate() *locStandardPredicate {
	result := &locStandardPredicate{
		Includes: *newLocOrPredicate(),
		Excludes: *newLocOrPredicate(),
	}
	return result
}

// GetEntityType returns the type of this entity
func (p *locStandardPredicate) GetEntityType() string {
	return TypeLocStandardPredicate
}

// Accept a visit by the given visitor
func (p *locStandardPredicate) Accept(v model.EntityVisitor) interface{} {
	return v.VisitLocStandardPredicate(p)
}

// Gets the set of nested predicates.
func (p *locStandardPredicate) SetContainer(parent ModuleEntityContainer) {
	p.locPredicate.SetContainer(parent)
	p.Includes.SetContainer(p)
	p.Excludes.SetContainer(p)
}

// Gets the including predicates.
func (p *locStandardPredicate) GetIncludes() model.LocOrPredicate {
	return &p.Includes
}

// Gets the excluding predicates.
func (p *locStandardPredicate) GetExcludes() model.LocOrPredicate {
	return &p.Excludes
}

// Are both the <see cref="Includes"/> and <see cref="Excludes"/> set empty?
func (p *locStandardPredicate) IsEmpty() bool {
	return p.Includes.IsEmpty() && p.Excludes.IsEmpty()
}

// Create a deep clone.
func (p *locStandardPredicate) Clone() model.LocPredicate {
	c := newLocStandardPredicate()
	p.Includes.GetPredicates().ForEach(func(cp model.LocPredicate) {
		ccp := cp.Clone()
		c.Includes.GetPredicates().Add(ccp)
	})
	p.Excludes.GetPredicates().ForEach(func(cp model.LocPredicate) {
		ccp := cp.Clone()
		c.Excludes.GetPredicates().Add(ccp)
	})
	return c
}

// Evaluate this predicate for the given loc.
func (p *locStandardPredicate) Evaluate(loc model.Loc) bool {
	if p.IsEmpty() {
		return true
	}
	if p.Includes.IsEmpty() {
		return !p.Excludes.Evaluate(loc)
	}
	return p.Includes.Evaluate(loc) && !p.Excludes.Evaluate(loc)
}

// Copy from the given source
func (p *locStandardPredicate) CopyFrom(src model.LocStandardPredicate) {
	p.Includes.CopyFrom(src.GetIncludes())
	p.Excludes.CopyFrom(src.GetExcludes())
}

func (p *locStandardPredicate) ImplementsStandardPredicate() {}
