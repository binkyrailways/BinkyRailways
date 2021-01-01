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

type locPredicateBuilder struct {
}

var _ model.LocPredicateBuilder = &locPredicateBuilder{}

// Create an 'and' predicate
func (b *locPredicateBuilder) CreateAnd() model.LocAndPredicate {
	return newLocAndPredicate()
}

// Create an 'or' predicate
func (b *locPredicateBuilder) CreateOr() model.LocOrPredicate {
	return newLocOrPredicate()
}

// Create a 'loc equals' predicate
func (b *locPredicateBuilder) CreateEquals(loc model.Loc) model.LocEqualsPredicate {
	p := newLocEqualsPredicate()
	p.SetLoc(loc)
	return p
}

// Create a 'loc group equals' predicate
func (b *locPredicateBuilder) CreateGroupEquals(group model.LocGroup) model.LocGroupEqualsPredicate {
	p := newLocGroupEqualsPredicate()
	p.SetGroup(group)
	return p
}

// Create a 'loc is allowed to change direction' predicate
func (b *locPredicateBuilder) CreateCanChangeDirection() model.LocCanChangeDirectionPredicate {
	return newLocCanChangeDirectionPredicate()
}

// Create a 'allowed between start and end time' predicate
//      ILocTimePredicate CreateTime(Time periodStart, Time periodEnd);
