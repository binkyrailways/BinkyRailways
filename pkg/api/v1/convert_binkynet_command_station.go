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

	"go.uber.org/multierr"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// FromModel converts a model binkynet command station to an API binkynet command station
func (dst *BinkyNetCommandStation) FromModel(ctx context.Context, src model.BinkyNetCommandStation) error {
	dst.ServerHost = src.GetServerHost()
	dst.GrpcPort = int32(src.GetGRPCPort())
	dst.RequiredWorkerVersion = src.GetRequiredWorkerVersion()
	dst.ExcludeUnusedObjects = src.GetExcludeUnUsedObjects()
	src.GetLocalWorkers().ForEach(func(lw model.BinkyNetLocalWorker) {
		dst.LocalWorkers = append(dst.LocalWorkers, &BinkyNetLocalWorkerRef{
			Id: JoinParentChildID(src.GetID(), lw.GetID()),
		})

	})
	return nil
}

// ToModel converts an API binkynet command station to a model binkynet command station
func (src *BinkyNetCommandStation) ToModel(ctx context.Context, dst model.BinkyNetCommandStation) error {
	var err error
	multierr.AppendInto(&err, dst.SetServerHost(src.GetServerHost()))
	multierr.AppendInto(&err, dst.SetGRPCPort(int(src.GetGrpcPort())))
	multierr.AppendInto(&err, dst.SetRequiredWorkerVersion(src.GetRequiredWorkerVersion()))
	multierr.AppendInto(&err, dst.SetExcludeUnUsedObjects(src.GetExcludeUnusedObjects()))
	return err
}
