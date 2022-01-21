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

// LocStandardPredicate is a predicate that evaluates to true based on includes and excludes predicates.
// The predicate evaluates to true if:
// - Includes is empty and the excludes predicate for the loc evaluates to false.
// - The Includes predicate evaluates to true and the excludes predicate for the loc evaluates to false
type LocStandardPredicate interface {
	LocPredicate
	ImplementsStandardPredicate()

	// Including predicates.
	GetIncludes() LocOrPredicate

	// Excluding predicates.
	GetExcludes() LocOrPredicate

	// Are both the <see cref="Includes"/> and <see cref="Excludes"/> set empty?
	IsEmpty() bool
}
