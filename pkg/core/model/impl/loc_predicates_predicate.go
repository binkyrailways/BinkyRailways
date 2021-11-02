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

// LocPredicatesPredicate extends implementation methods to model.LocPredicatesPredicate
type LocPredicatesPredicate interface {
	LocPredicate
	model.LocPredicatesPredicate
}

type locPredicatesPredicate struct {
	locPredicate

	Predicates locPredicateSet `xml:"Predicates"`
}

// Gets the set of nested predicates.
func (p *locPredicatesPredicate) SetContainer(parent ModuleEntityContainer) {
	p.locPredicate.SetContainer(parent)
	p.Predicates.SetContainer(p)
}

// Gets the set of nested predicates.
func (p *locPredicatesPredicate) GetPredicates() model.LocPredicateSet {
	return &p.Predicates
}

// Is the <see cref="Predicates"/> set emtpy?
func (p *locPredicatesPredicate) IsEmpty() bool {
	return p.Predicates.GetCount() == 0
}
