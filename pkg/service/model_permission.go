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

package service

import (
	"context"

	api "github.com/binkyrailways/BinkyRailways/pkg/api/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model/predicates"
)

// Try to parse a permission
func (s *service) ParsePermission(ctx context.Context, req *api.ParsePermissionRequest) (*api.ParsePermissionResult, error) {
	rw, err := s.getRailway()
	if err != nil {
		return nil, err
	}
	perm, err := predicates.ParsePredicate(req.GetValue(), rw)
	if err != nil {
		return &api.ParsePermissionResult{
			Valid:   false,
			Message: err.Error(),
		}, nil
	}
	return &api.ParsePermissionResult{
		Valid:          true,
		FormattedValue: predicates.GeneratePredicate(perm),
	}, nil
}
