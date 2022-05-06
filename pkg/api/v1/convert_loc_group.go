// Copyright 2021 Ewout Prangsma
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
)

// FromModel converts a model loc to an API loc
func (dst *LocGroup) FromModel(ctx context.Context, src model.LocGroup) error {
	dst.Id = src.GetID()
	dst.Description = src.GetDescription()
	src.GetLocs().ForEach(func(lr model.LocRef) {
		dst.Locs = append(dst.Locs, &LocRef{
			Id: lr.GetID(),
		})
	})
	return nil
}

// ToModel converts an API loc to a model loc
func (src *LocGroup) ToModel(ctx context.Context, dst model.LocGroup) error {
	if src.GetId() != dst.GetID() {
		return InvalidArgument("Unexpected loc group ID: '%s'", src.GetId())
	}
	dst.SetDescription(src.GetDescription())
	// Add locs from src to dst
	validIDs := make(map[string]struct{})
	for _, lr := range src.GetLocs() {
		validIDs[lr.GetId()] = struct{}{}
		if !dst.GetLocs().ContainsID(lr.GetId()) {
			if dstLR, found := dst.GetRailway().GetLocs().Get(lr.GetId()); !found {
				return InvalidArgument("Unknown loc with ID: '%s'", lr.GetId())
			} else {
				if dstLoc, err := dstLR.TryResolve(); err != nil {
					return InvalidArgument("Failed to resolve loc with ID '%s': %w", lr.GetId(), err)
				} else {
					dst.GetLocs().Add(dstLoc)
				}
			}
		}
	}
	// Remove locs from dst that are not found in src
	dst.GetLocs().ForEach(func(lr model.LocRef) {
		if _, isValid := validIDs[lr.GetID()]; !isValid {
			// Remove
			dst.GetLocs().Remove(lr)
		}
	})
	return nil
}
