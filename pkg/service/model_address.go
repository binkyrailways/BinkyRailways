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

package service

import (
	"context"
	"strings"

	bnapi "github.com/binkynet/BinkyNet/apis/v1"
	api "github.com/binkyrailways/BinkyRailways/pkg/api/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// Try to parse an address
func (s *service) ParseAddress(ctx context.Context, req *api.ParseAddressRequest) (*api.ParseAddressResult, error) {
	addr, err := model.NewAddressFromString(req.GetValue())
	if err != nil {
		return &api.ParseAddressResult{
			Valid:   false,
			Message: err.Error(),
		}, nil
	}
	if addr.Network.Type == model.AddressTypeBinkyNet {
		rw, err := s.getRailway()
		if err != nil {
			return nil, err
		}
		// Check that address exists
		parts := strings.SplitN(addr.Value, "/", 2)
		if len(parts) != 2 {
			return &api.ParseAddressResult{
				Valid:          false,
				Message:        "BinkyNet address value must contain alias/object_id",
				FormattedValue: addr.String(),
			}, nil
		}
		// Search for localworker + object
		isGlobal := parts[0] == bnapi.GlobalModuleID
		foundLW := false
		foundObject := false
		rw.GetCommandStations().ForEach(func(csr model.CommandStationRef) {
			if cs, err := csr.TryResolve(); err == nil {
				if bnCs, ok := cs.(model.BinkyNetCommandStation); ok {
					if isGlobal {
						bnCs.GetLocalWorkers().ForEach(func(lw model.BinkyNetLocalWorker) {
							foundLW = true
							if _, ok := lw.GetObjects().Get(bnapi.ObjectID(parts[1])); ok {
								foundObject = true
							}
						})
					} else {
						if lw, ok := bnCs.GetLocalWorkers().Get(parts[0]); ok {
							foundLW = true
							if _, ok := lw.GetObjects().Get(bnapi.ObjectID(parts[1])); ok {
								foundObject = true
							}
						}
					}
				}
			}
		})
		if !foundLW {
			return &api.ParseAddressResult{
				Valid:          false,
				Message:        "BinkyNet address does not refer to an existing local worker",
				FormattedValue: addr.String(),
			}, nil
		}
		if !foundObject {
			return &api.ParseAddressResult{
				Valid:          false,
				Message:        "BinkyNet address does not refer to an existing object",
				FormattedValue: addr.String(),
			}, nil
		}
	}
	return &api.ParseAddressResult{
		Valid:          true,
		FormattedValue: addr.String(),
	}, nil
}
