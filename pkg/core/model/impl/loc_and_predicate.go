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

// LocAndPredicate extends implementation methods to model.LocAndPredicate
type LocAndPredicate interface {
	LocPredicate
	model.LocAndPredicate
}

type locAndPredicate struct {
	locPredicatesPredicate
}

var _ model.LocAndPredicate = &locAndPredicate{}

// newLocAndPredicate creates a new And predicate
func newLocAndPredicate() *locAndPredicate {
	p := &locAndPredicate{}
	p.Predicates.SetContainer(p)
	return p
}

// GetEntityType returns the type of this entity
func (p *locAndPredicate) GetEntityType() string {
	return TypeLocAndPredicate
}

// Accept a visit by the given visitor
func (p *locAndPredicate) Accept(v model.EntityVisitor) interface{} {
	return v.VisitLocAndPredicate(p)
}

// Create a deep clone.
func (p *locAndPredicate) Clone() model.LocPredicate {
	c := newLocAndPredicate()
	p.Predicates.ForEach(func(cp model.LocPredicate) {
		ccp := cp.Clone()
		c.Predicates.Add(ccp)
	})
	return c
}

// Evaluate this predicate for the given loc.
func (p *locAndPredicate) Evaluate(loc model.Loc) bool {
	result := true
	p.Predicates.ForEach(func(cp model.LocPredicate) {
		if !cp.Evaluate(loc) {
			result = false
		}
	})
	return result
}
