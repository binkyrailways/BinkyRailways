// Copyright 2026 Ewout Prangsma
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

package service

import (
	"context"

	api "github.com/binkyrailways/BinkyRailways/pkg/api/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model/predicates"
)

// Evaluates a loc predicate for a given loc
func (s *service) EvaluateLocPredicate(ctx context.Context, req *api.EvaluateLocPredicateRequest) (*api.EvaluateLocPredicateResult, error) {
	rw, err := s.getRailway()
	if err != nil {
		return nil, err
	}
	// Find the model loc
	mLoc, ok := rw.GetLocs().Get(req.GetLoc().GetId())
	if !ok {
		return nil, api.NotFound("Loc %s not found", req.GetLoc().GetId())
	}
	// Resolve the loc
	loc, err := mLoc.TryResolve()
	if err != nil {
		return nil, err
	}
	// Parse the predicate
	perm, err := predicates.ParsePredicate(req.GetPredicate(), rw)
	if err != nil {
		return nil, err
	}
	// Evaluate the predicate
	result := false
	if loc != nil {
		result = perm.Evaluate(loc)
	}
	return &api.EvaluateLocPredicateResult{
		Result: result,
	}, nil
}
