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

package impl

import (
	"go.uber.org/multierr"

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// powerProperty implements the power for all command stations of a railway.
type powerProperty struct {
	Railway Railway

	actual    bool
	requested bool
}

var _ state.BoolProperty = &powerProperty{}

// Gets / sets the actual value
func (sp *powerProperty) GetActual() bool {
	return sp.actual
}
func (sp *powerProperty) SetActual(value bool) error {
	if sp.actual != value {
		var err error
		sp.Railway.ForEachCommandStation(func(cs state.CommandStation) {
			multierr.AppendInto(&err, cs.GetPower().SetActual(value))
		})
		if err != nil {
			return err
		}
		sp.actual = value
		sp.Railway.Send(state.ActualStateChangedEvent{
			Subject:  sp.Railway,
			Property: sp,
		})
	}
	return nil
}

// Gets / sets the requested value
func (sp *powerProperty) GetRequested() bool {
	return sp.requested
}
func (sp *powerProperty) SetRequested(value bool) error {
	if sp.requested != value {
		var err error
		sp.Railway.ForEachCommandStation(func(cs state.CommandStation) {
			multierr.AppendInto(&err, cs.GetPower().SetRequested(value))
		})
		if err != nil {
			return err
		}
		sp.requested = value
		sp.Railway.Send(state.RequestedStateChangedEvent{
			Subject:  sp.Railway,
			Property: sp,
		})
	}
	return nil
}

func (sp *powerProperty) IsConsistent() bool {
	return sp.GetActual() == sp.GetRequested()
}
