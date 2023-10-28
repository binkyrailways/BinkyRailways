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

// LocOrPredicate extends implementation methods to model.LocOrPredicate
type LocOrPredicate interface {
	LocPredicate
	model.LocOrPredicate
}

type locOrPredicate struct {
	locPredicatesPredicate
}

var _ model.LocOrPredicate = &locOrPredicate{}

// newLocOrPredicate creates a new Or predicate
func newLocOrPredicate() *locOrPredicate {
	p := &locOrPredicate{}
	p.Predicates.SetContainer(p)
	return p
}

// GetEntityType returns the type of this entity
func (p *locOrPredicate) GetEntityType() string {
	return TypeLocOrPredicate
}

// Accept a visit by the given visitor
func (p *locOrPredicate) Accept(v model.EntityVisitor) interface{} {
	return v.VisitLocOrPredicate(p)
}

// Create a deep clone.
func (p *locOrPredicate) Clone() model.LocPredicate {
	c := newLocOrPredicate()
	p.Predicates.ForEach(func(cp model.LocPredicate) {
		ccp := cp.Clone()
		c.Predicates.Add(ccp)
	})
	return c
}

// Evaluate this predicate for the given loc.
func (p *locOrPredicate) Evaluate(loc model.Loc) bool {
	result := false
	p.Predicates.ForEach(func(cp model.LocPredicate) {
		if cp.Evaluate(loc) {
			result = true
		}
	})
	return result
}

func (p *locOrPredicate) ImplementsOrPredicate() {}
