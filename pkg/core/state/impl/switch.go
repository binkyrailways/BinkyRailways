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
	"context"
	"fmt"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// Switch adds implementation functions to state.Switch.
type Switch interface {
	Entity
	Junction
	state.Switch
}

type stdSwitch struct {
	junction
	commandStation CommandStation
	direction      switchDirectionProperty
}

var _ Switch = &stdSwitch{}

// Create a new entity
func newSwitch(en model.Switch, railway Railway) Switch {
	bo := &stdSwitch{}
	bo.junction = newJunction(en, bo, railway)
	bo.direction.Configure("direction", bo, nil, railway, railway)
	bo.direction.SetActual(context.Background(), model.SwitchDirectionStraight)
	bo.direction.SetRequested(context.Background(), model.SwitchDirectionStraight)
	bo.direction.SubscribeRequestChanges(func(ctx context.Context, value model.SwitchDirection) {
		if bo.commandStation != nil {
			bo.commandStation.SendSwitchDirection(ctx, bo)
		}
	})
	bo.direction.SubscribeActualChanges(func(ctx context.Context, value model.SwitchDirection) {
		if bo.commandStation != nil && value != bo.direction.GetRequested(ctx) {
			// We got a different actual than what we requested.
			// Send again
			fmt.Println("Get unexpected switch actual")
			bo.commandStation.SendSwitchDirection(ctx, bo)
		}
	})
	return bo
}

// GetModel returns the entity as Switch.
func (bo *stdSwitch) GetSwitchModel() model.Switch {
	return bo.getSwitch()
}

// getSwitch returns the entity as Switch.
func (bo *stdSwitch) getSwitch() model.Switch {
	return bo.GetEntity().(model.Switch)
}

// Try to prepare the entity for use.
// Returns nil when the entity is successfully prepared,
// returns an error otherwise.
func (bo *stdSwitch) TryPrepareForUse(ctx context.Context, _ state.UserInterface, _ state.Persistence) error {
	// Resolve command station
	var err error
	bo.commandStation, err = bo.railway.SelectCommandStation(ctx, bo.getSwitch())
	if err != nil {
		return err
	}
	if bo.commandStation == nil {
		return fmt.Errorf("Switch does not have a commandstation attached.")
	}
	bo.commandStation.RegisterJunction(bo)

	return nil
}

// Wrap up the preparation fase.
func (bo *stdSwitch) FinalizePrepare(ctx context.Context) {
	// TODO
}

// Address of the entity
func (bo *stdSwitch) GetAddress() model.Address {
	return bo.getSwitch().GetAddress()
}

// Address of the feedback line of the entity
func (bo *stdSwitch) GetFeedbackAddress() model.Address {
	return bo.getSwitch().GetFeedbackAddress()
}

// Does this switch send a feedback when switched?
func (bo *stdSwitch) GetHasFeedback() bool {
	return bo.getSwitch().GetHasFeedback()
}

// Time (in ms) it takes for the switch to move from one direction to the other?
// This property is only used when <see cref="HasFeedback"/> is false.
func (bo *stdSwitch) GetSwitchDuration() int {
	return bo.getSwitch().GetSwitchDuration()
}

// If set, the straight/off commands are inverted.
func (bo *stdSwitch) GetInvert() bool {
	return bo.getSwitch().GetInvert()
}

// If set, the straight/off feedback states are inverted.
func (bo *stdSwitch) GetInvertFeedback() bool {
	return bo.getSwitch().GetInvertFeedback()
}

// Direction of the switch.
func (bo *stdSwitch) GetDirection() state.SwitchDirectionProperty {
	return &bo.direction
}
