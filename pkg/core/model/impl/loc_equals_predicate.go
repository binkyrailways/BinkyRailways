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
	"fmt"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// LocEqualsPredicate extends implementation methods to model.LocEqualsPredicate
type LocEqualsPredicate interface {
	LocPredicate
	model.LocEqualsPredicate
}

type locEqualsPredicate struct {
	locPredicate

	LocID string `xml:"LocId"`
}

var _ model.LocEqualsPredicate = &locEqualsPredicate{}

// newLocEqualsPredicate creates a new equals predicate
func newLocEqualsPredicate() *locEqualsPredicate {
	p := &locEqualsPredicate{}
	return p
}

// GetEntityType returns the type of this entity
func (p *locEqualsPredicate) GetEntityType() string {
	return TypeLocEqualsPredicate
}

// Accept a visit by the given visitor
func (p *locEqualsPredicate) Accept(v model.EntityVisitor) interface{} {
	return v.VisitLocEqualsPredicate(p)
}

// Gets the set of nested predicates.
func (p *locEqualsPredicate) SetContainer(parent ModuleEntityContainer) {
	p.locPredicate.SetContainer(parent)
}

// Gets/Sets the loc to compare to.
func (p *locEqualsPredicate) GetLoc() (model.Loc, error) {
	if p.LocID == "" {
		return nil, nil
	}
	m, ok := p.GetModule().(Module)
	if !ok {
		return nil, fmt.Errorf("module is not of type Module")
	}
	if m == nil {
		return nil, fmt.Errorf("module is nil")
	}
	pkg := m.GetPackage()
	if pkg == nil {
		return nil, fmt.Errorf("package is nil")
	}
	return pkg.GetLoc(p.LocID)
}
func (p *locEqualsPredicate) SetLoc(value model.Loc) error {
	if value == nil {
		p.LocID = ""
	} else {
		p.LocID = value.GetID()
	}
	return nil
}

// Create a deep clone.
func (p *locEqualsPredicate) Clone() model.LocPredicate {
	c := newLocEqualsPredicate()
	c.LocID = p.LocID
	return c
}

// Evaluate this predicate for the given loc.
func (p *locEqualsPredicate) Evaluate(loc model.Loc) bool {
	return loc.GetID() == p.LocID
}
