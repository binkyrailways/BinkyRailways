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

package model

// LocPredicateBuilder is used to create predicate instances.
type LocPredicateBuilder interface {
	// Create an 'and' predicate
	CreateAnd() LocAndPredicate

	// Create an 'or' predicate
	CreateOr() LocOrPredicate

	// Create a 'loc equals' predicate
	CreateEquals(loc Loc) LocEqualsPredicate

	// Create a 'loc group equals' predicate
	CreateGroupEquals(group LocGroup) LocGroupEqualsPredicate

	// Create a 'loc is allowed to change direction' predicate
	CreateCanChangeDirection() LocCanChangeDirectionPredicate

	// Create a 'allowed between start and end time' predicate
	//      ILocTimePredicate CreateTime(Time periodStart, Time periodEnd);
}
