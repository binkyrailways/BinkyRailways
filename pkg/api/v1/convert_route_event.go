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

// FromModel converts a model RouteEvent to an API RouteEvent
func (dst *RouteEvent) FromModel(ctx context.Context, src model.RouteEvent) error {
	id := JoinParentChildID(src.GetSensor().GetModule().GetID(), src.GetSensor().GetID())
	dst.Sensor = &SensorRef{
		Id: id,
	}
	src.GetBehaviors().ForEach(func(reb model.RouteEventBehavior) {
		dstReb := &RouteEventBehavior{}
		dstReb.FromModel(ctx, reb)
		dst.Behaviors = append(dst.Behaviors, dstReb)
	})
	return nil
}

// ToModel converts an API RouteEvent to a model RouteEvent
func (src *RouteEvent) ToModel(ctx context.Context, dst model.RouteEvent) error {
	expectedID := JoinParentChildID(dst.GetSensor().GetModule().GetID(), dst.GetSensor().GetID())
	if src.GetSensor().GetId() != expectedID {
		return InvalidArgument("Unexpected sensor ID: '%s'", src.GetSensor().GetId())
	}
	if len(src.GetBehaviors()) != dst.GetBehaviors().GetCount() {
		return InvalidArgument("Unexpected number of behaviors (got %d, expected %d)", len(src.GetBehaviors()), dst.GetBehaviors().GetCount())
	}
	var err error
	for index, bhv := range src.GetBehaviors() {
		dstBhv, ok := dst.GetBehaviors().GetAt(index)
		if !ok {
			return InvalidArgument("Route event behavior not found at index %d", index)
		}
		multierr.AppendInto(&err, bhv.ToModel(ctx, dstBhv))
	}
	return err
}
