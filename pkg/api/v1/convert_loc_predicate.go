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

package v1

import (
	context "context"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"go.uber.org/multierr"
)

// FromModel converts a model loc predicate to an API loc predicate
func (dst *LocPredicate) FromModel(ctx context.Context, src model.LocPredicate) (*LocPredicate, error) {
	if src == nil {
		return nil, nil
	}
	if dst == nil {
		dst = &LocPredicate{}
	}
	var err error
	switch p := src.(type) {
	case model.LocAndPredicate:
		dst.And = &LocAndPredicate{}
		p.GetPredicates().ForEach(func(lp model.LocPredicate) {
			x, lerr := (&LocPredicate{}).FromModel(ctx, lp)
			if !multierr.AppendInto(&err, lerr) {
				dst.And.Predicates = append(dst.And.Predicates, x)
			}
		})
	case model.LocEqualsPredicate:
		if loc, e := p.GetLoc(); e != nil {
			multierr.AppendInto(&err, e)
		} else {
			dst.Equals = &LocEqualsPredicate{
				Loc: &LocRef{
					Id: loc.GetID(),
				},
			}
		}
	case model.LocGroupEqualsPredicate:
		group := p.GetGroup()
		dst.Group = &LocGroupEqualsPredicate{
			Group: &LocGroupRef{
				Id: group.GetID(),
			},
		}
	case model.LocOrPredicate:
		dst.Or = &LocOrPredicate{}
		p.GetPredicates().ForEach(func(lp model.LocPredicate) {
			x, lerr := (&LocPredicate{}).FromModel(ctx, lp)
			if !multierr.AppendInto(&err, lerr) {
				dst.Or.Predicates = append(dst.Or.Predicates, x)
			}
		})
	case model.LocStandardPredicate:
		dst.Standard = &LocStandardPredicate{}
		p.GetIncludes().GetPredicates().ForEach(func(lp model.LocPredicate) {
			x, lerr := (&LocPredicate{}).FromModel(ctx, lp)
			if !multierr.AppendInto(&err, lerr) {
				dst.Standard.Includes = append(dst.Standard.Includes, x)
			}
		})
		p.GetExcludes().GetPredicates().ForEach(func(lp model.LocPredicate) {
			x, lerr := (&LocPredicate{}).FromModel(ctx, lp)
			if !multierr.AppendInto(&err, lerr) {
				dst.Standard.Excludes = append(dst.Standard.Excludes, x)
			}
		})
	case model.LocCanChangeDirectionPredicate:
		dst.CanChangeDirection = &LocCanChangeDirectionPredicate{}
	default:
		return nil, InvalidArgument("Unknown loc predicate type %T", src)
	}
	return dst, err
}

// ToModel converts an API loc predicate to a model loc predicate
func (src *LocPredicate) ToModel(ctx context.Context, railway model.Railway) (model.LocPredicate, error) {
	if src == nil {
		return nil, nil
	}
	builder := railway.GetPredicateBuilder()
	if src := src.GetAnd(); src != nil {
		dst := builder.CreateAnd()
		var err error
		for _, x := range src.GetPredicates() {
			xdst, lerr := x.ToModel(ctx, railway)
			if !multierr.AppendInto(&err, lerr) {
				dst.GetPredicates().Add(xdst)
			}
		}
		return dst, err
	}
	if src := src.GetCanChangeDirection(); src != nil {
		dst := builder.CreateCanChangeDirection()
		return dst, nil
	}
	if src := src.GetEquals(); src != nil {
		if locRef, ok := railway.GetLocs().Get(src.GetLoc().GetId()); ok {
			if loc, err := locRef.TryResolve(); err != nil {
				return nil, err
			} else {
				dst := builder.CreateEquals(loc)
				return dst, nil
			}
		}
		return nil, InvalidArgument("Unknown loc in LocPredicate.Equals '%s'", src.GetLoc().GetId())
	}
	if src := src.GetGroup(); src != nil {
		if locGroup, ok := railway.GetLocGroups().Get(src.GetGroup().GetId()); ok {
			dst := builder.CreateGroupEquals(locGroup)
			return dst, nil
		}
		return nil, InvalidArgument("Unknown loc group in LocPredicate.Group '%s'", src.GetGroup().GetId())
	}
	if src := src.GetOr(); src != nil {
		dst := builder.CreateOr()
		var err error
		for _, x := range src.GetPredicates() {
			xdst, lerr := x.ToModel(ctx, railway)
			if !multierr.AppendInto(&err, lerr) {
				dst.GetPredicates().Add(xdst)
			}
		}
		return dst, err
	}
	if src := src.GetStandard(); src != nil {
		dst := builder.CreateStandard()
		var err error
		for _, x := range src.GetIncludes() {
			xdst, lerr := x.ToModel(ctx, railway)
			if !multierr.AppendInto(&err, lerr) {
				dst.GetIncludes().GetPredicates().Add(xdst)
			}
		}
		for _, x := range src.GetExcludes() {
			xdst, lerr := x.ToModel(ctx, railway)
			if !multierr.AppendInto(&err, lerr) {
				dst.GetExcludes().GetPredicates().Add(xdst)
			}
		}
		return dst, err
	}
	return nil, InvalidArgument("Unknown LocPredicate")
}
