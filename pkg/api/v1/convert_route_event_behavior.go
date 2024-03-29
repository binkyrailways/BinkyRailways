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
	"github.com/binkyrailways/BinkyRailways/pkg/core/model/predicates"
	"go.uber.org/multierr"
)

// FromModel converts a model RouteEventBehavior to an API RouteEventBehavior
func (dst *RouteEventBehavior) FromModel(ctx context.Context, src model.RouteEventBehavior) error {
	var err error
	dst.AppliesTo = predicates.GeneratePredicate(src.GetAppliesTo())
	multierr.AppendInto(&err, dst.StateBehavior.FromModel(ctx, src.GetStateBehavior()))
	multierr.AppendInto(&err, dst.SpeedBehavior.FromModel(ctx, src.GetSpeedBehavior()))
	return err
}

// ToModel converts an API RouteEventBehavior to a model RouteEventBehavior
func (src *RouteEventBehavior) ToModel(ctx context.Context, dst model.RouteEventBehavior) error {
	railway := dst.GetRailway()
	if railway == nil {
		return InvalidArgument("RouteEventBehavior has no railway reference")
	}
	stB, err := src.GetStateBehavior().ToModel(ctx)
	if err != nil {
		return err
	}
	spB, err := src.GetSpeedBehavior().ToModel(ctx)
	if err != nil {
		return err
	}
	if src.GetAppliesTo() != predicates.GeneratePredicate(dst.GetAppliesTo()) {
		// AppliesTo changes
		perm, err := predicates.ParseStandardPredicate(src.GetAppliesTo(), railway)
		if err != nil {
			return err
		}
		if err := dst.SetAppliesTo(perm); err != nil {
			return err
		}
	}
	multierr.AppendInto(&err, dst.SetStateBehavior(stB))
	multierr.AppendInto(&err, dst.SetSpeedBehavior(spB))
	return nil
}
