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

package predicates

import (
	"fmt"
	"strings"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// getLocByNamePredicate searches for the loc with given name and
// wraps that in a LocEqualsPredicate.
func getLocByNamePredicate(name string, rw model.Railway) (interface{}, error) {
	var result, altResult model.Loc
	addr, err := model.NewAddressFromString(name)
	hasAddr := err == nil
	upperName := strings.ToUpper(name)

	rw.GetLocs().ForEach(func(lr model.LocRef) {
		if result == nil {
			if loc, err := lr.TryResolve(); err == nil {
				if hasAddr {
					if loc.GetAddress().Equals(addr) {
						result = loc
					}
				} else {
					if loc.GetDescription() == name || loc.GetID() == name {
						result = loc
					} else if strings.ToUpper(loc.GetDescription()) == upperName {
						altResult = loc
					}
				}
			}
		}
	})
	if result == nil {
		result = altResult
	}
	if result == nil {
		return nil, fmt.Errorf("loc '%s' not found", name)
	}
	p := rw.GetPredicateBuilder().CreateEquals(result)
	return p, nil
}

// getLocGroupByNamePredicate searches for the loc group with given name and
// wraps that in a LocGroupEqualsPredicate.
func getLocGroupByNamePredicate(name string, rw model.Railway) (interface{}, error) {
	var result, altResult model.LocGroup
	upperName := strings.ToUpper(name)

	rw.GetLocGroups().ForEach(func(lg model.LocGroup) {
		if result == nil {
			if lg.GetDescription() == name || lg.GetID() == name {
				result = lg
			} else if strings.ToUpper(lg.GetDescription()) == upperName {
				altResult = lg
			}
		}
	})
	if result == nil {
		result = altResult
	}
	if result == nil {
		return nil, fmt.Errorf("loc group '%s' not found", name)
	}
	p := rw.GetPredicateBuilder().CreateGroupEquals(result)
	return p, nil
}

func getLocCanChangeDirectionPredicate(rw model.Railway) (interface{}, error) {
	return rw.GetPredicateBuilder().CreateCanChangeDirection(), nil
}

func getLocOrPredicate(first, rest interface{}, rw model.Railway) (interface{}, error) {
	op := rw.GetPredicateBuilder().CreateOr()
	if err := addPredicates(first, rest, op.GetPredicates()); err != nil {
		return nil, err
	}
	return op, nil
}

func getLocAndPredicate(first, rest interface{}, rw model.Railway) (interface{}, error) {
	op := rw.GetPredicateBuilder().CreateAnd()
	if err := addPredicates(first, rest, op.GetPredicates()); err != nil {
		return nil, err
	}
	return op, nil
}

func addPredicates(first, rest interface{}, op model.LocPredicateSet) error {
	if p, ok := first.(model.LocPredicate); !ok {
		return fmt.Errorf("expected (1) LocPredicate, got %T", first)
	} else {
		op.Add(p)
	}
	if rest == nil {
		return nil
	}
	restList, ok := rest.([]interface{})
	if !ok {
		return fmt.Errorf("expected []interface{}, got %T", rest)
	}
	for _, x := range restList {
		opElem, ok := x.([]interface{})
		if !ok || len(opElem) != 2 {
			return fmt.Errorf("expected [2]interface{}, got %T", x)
		}
		if p, ok := opElem[1].(model.LocPredicate); !ok {
			return fmt.Errorf("expected LocPredicate, got %T", opElem[1])
		} else {
			op.Add(p)
		}
	}
	return nil
}

func getLocStandardPredicate(ps interface{}, rw model.Railway) (interface{}, error) {
	var _only only
	var _except except
	if elems, ok := ps.([]interface{}); ok && len(elems) == 2 {
		// We have both only & except
		if _only, ok = elems[0].(only); !ok {
			return nil, fmt.Errorf("excepted first element to be only, got %T", elems[0])
		}
		if _except, ok = elems[1].(except); !ok {
			return nil, fmt.Errorf("excepted second element to be except, got %T", elems[1])
		}
	} else if _only, ok = ps.(only); ok {
		// We got only the only part
	} else if _except, ok = ps.(except); ok {
		// We got only the except part
	} else {
		return nil, fmt.Errorf("excepted only, except or both, got %T", ps)
	}
	op := rw.GetPredicateBuilder().CreateStandard()
	if _only.predicates != nil {
		if ops, ok := _only.predicates.(model.LocOrPredicate); ok {
			ops.GetPredicates().ForEach(func(lp model.LocPredicate) {
				op.GetIncludes().GetPredicates().Add(lp)
			})
		} else {
			return nil, fmt.Errorf("excepted only to be LocOrPredicate, got %T", _only.predicates)
		}
	}
	if _except.predicates != nil {
		if ops, ok := _except.predicates.(model.LocOrPredicate); ok {
			ops.GetPredicates().ForEach(func(lp model.LocPredicate) {
				op.GetExcludes().GetPredicates().Add(lp)
			})
		} else {
			return nil, fmt.Errorf("excepted except to be LocOrPredicate, got %T", _except.predicates)
		}
	}
	return op, nil
}
