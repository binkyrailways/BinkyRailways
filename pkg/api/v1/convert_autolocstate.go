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
	"fmt"

	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
)

// FromState converts an auto loc state to an API type
func (dst *AutoLocState) FromState(ctx context.Context, src state.AutoLocState) error {
	switch src {
	case state.AssignRoute:
		*dst = AutoLocState_ASSIGNROUTE
	case state.ReversingWaitingForDirectionChange:
		*dst = AutoLocState_REVERSINGWAITINGFORDIRECTIONCHANGE
	case state.WaitingForAssignedRouteReady:
		*dst = AutoLocState_WAITINGFORASSIGNEDROUTEREADY
	case state.Running:
		*dst = AutoLocState_RUNNING
	case state.EnterSensorActivated:
		*dst = AutoLocState_ENTERSENSORACTIVATED
	case state.EnteringDestination:
		*dst = AutoLocState_ENTERINGDESTINATION
	case state.ReachedSensorActivated:
		*dst = AutoLocState_REACHEDSENSORACTIVATED
	case state.ReachedDestination:
		*dst = AutoLocState_REACHEDDESTINATION
	case state.WaitingForDestinationTimeout:
		*dst = AutoLocState_WAITINGFORDESTINATIONTIMEOUT
	case state.WaitingForDestinationGroupMinimum:
		*dst = AutoLocState_WAITINGFORDESTINATIONGROUPMINIMUM
	default:
		return fmt.Errorf("invalid auto loc state: %d", src)
	}
	return nil
}
