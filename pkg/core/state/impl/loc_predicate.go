// Copyright 2022 Ewout Prangsma
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
	"context"
	"fmt"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
	"go.uber.org/multierr"
)

type locPredicate func(ctx context.Context, loc state.Loc) bool
type locPredicates []locPredicate

// Evaluate this predicate for the given loc.
func (lp locPredicate) Evaluate(ctx context.Context, loc state.Loc) bool {
	return lp(ctx, loc)
}

// newLocPredicate builds a predicate
func newLocPredicate(railway state.Railway, p model.LocPredicate) (locPredicate, error) {
	if p == nil {
		return func(ctx context.Context, loc state.Loc) bool {
			return true
		}, nil
	}
	switch p := p.(type) {
	case model.LocAndPredicate:
		list, err := newLocPredicates(railway, p.GetPredicates())
		return func(ctx context.Context, loc state.Loc) bool {
			return list.All(ctx, loc)
		}, err
	case model.LocCanChangeDirectionPredicate:
		return func(ctx context.Context, loc state.Loc) bool {
			return loc.GetChangeDirection(ctx) != model.ChangeDirectionAvoid
		}, nil
	case model.LocEqualsPredicate:
		expected, _ := p.GetLoc()
		return func(ctx context.Context, loc state.Loc) bool {
			return loc.GetID() == expected.GetID()
		}, nil
	case model.LocGroupEqualsPredicate:
		group := p.GetGroup()
		if group == nil {
			return func(ctx context.Context, loc state.Loc) bool {
				return false
			}, nil
		}
		expected := group.GetLocs()
		return func(ctx context.Context, loc state.Loc) bool {
			return expected.ContainsID(loc.GetID())
		}, nil
	case model.LocOrPredicate:
		list, err := newLocPredicates(railway, p.GetPredicates())
		return func(ctx context.Context, loc state.Loc) bool {
			return list.Any(ctx, loc)
		}, err
	case model.LocStandardPredicate:
		included, err := newLocPredicates(railway, p.GetIncludes().GetPredicates())
		if err != nil {
			return nil, err
		}
		excluded, err := newLocPredicates(railway, p.GetExcludes().GetPredicates())
		if err != nil {
			return nil, err
		}
		return func(ctx context.Context, loc state.Loc) bool {
			// The predicate evaluates to true if:
			// - Includes is empty and the excludes predicate for the loc evaluates to false.
			// - The Includes predicate evaluates to true and the excludes predicate for the loc evaluates to false
			if len(included) == 0 {
				return !excluded.Any(ctx, loc)
			}
			if !included.Any(ctx, loc) {
				return false
			}
			return !excluded.Any(ctx, loc)
		}, nil
	default:
		return nil, fmt.Errorf("Unknown predicate type %T", p)
	}
}

// newLocPredicates builds a predicate set
func newLocPredicates(railway state.Railway, ps model.LocPredicateSet) (locPredicates, error) {
	if ps == nil {
		return nil, nil
	}
	result := make([]locPredicate, 0, ps.GetCount())
	var err error
	ps.ForEach(func(lp model.LocPredicate) {
		rp, lerr := newLocPredicate(railway, lp)
		if !multierr.AppendInto(&err, lerr) {
			result = append(result, rp)
		}
	})
	return result, err
}

// Returns true if at least one of the given predicates returns true
func (lps locPredicates) Any(ctx context.Context, loc state.Loc) bool {
	for _, x := range lps {
		if x(ctx, loc) {
			return true
		}
	}
	return false
}

// Returns true if all of the given predicates returns true
func (lps locPredicates) All(ctx context.Context, loc state.Loc) bool {
	for _, x := range lps {
		if !x(ctx, loc) {
			return false
		}
	}
	return true
}
