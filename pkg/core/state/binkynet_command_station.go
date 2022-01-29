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

package state

import (
	"context"

	api "github.com/binkynet/BinkyNet/apis/v1"
)

// BinkyNetCommandStation specifies the state of a binkynet command station.
type BinkyNetCommandStation interface {
	CommandStation

	// GetLocalWorkerInfo fetches the last known info for a local worker with given ID.
	GetLocalWorkerInfo(ctx context.Context, id string) (api.LocalWorkerInfo, bool)
	// GetAllLocalWorkers fetches the last known info for all local workers.
	GetAllLocalWorkers(ctx context.Context) []api.LocalWorkerInfo
}
