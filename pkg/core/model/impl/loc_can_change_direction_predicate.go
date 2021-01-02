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

// LocCanChangeDirectionPredicate extends implementation methods to model.LocCanChangeDirectionPredicate
type LocCanChangeDirectionPredicate interface {
	ModuleEntity
	model.LocCanChangeDirectionPredicate
}

type locCanChangeDirectionPredicate struct {
	locPredicate
}

var _ model.LocCanChangeDirectionPredicate = &locCanChangeDirectionPredicate{}

// newLocCanChangeDirectionPredicate creates a new can-change-direction predicate
func newLocCanChangeDirectionPredicate() *locCanChangeDirectionPredicate {
	p := &locCanChangeDirectionPredicate{}
	return p
}

// Accept a visit by the given visitor
func (p *locCanChangeDirectionPredicate) Accept(v model.EntityVisitor) interface{} {
	return v.VisitLocCanChangeDirectionPredicate(p)
}

// Create a deep clone.
func (p *locCanChangeDirectionPredicate) Clone() model.LocPredicate {
	return newLocCanChangeDirectionPredicate()
}

// Evaluate this predicate for the given loc.
func (p *locCanChangeDirectionPredicate) Evaluate(loc model.Loc) bool {
	return loc.GetChangeDirection() == model.ChangeDirectionAllow
}
