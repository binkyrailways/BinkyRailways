// Copyright 2025 Ewout Prangsma
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

// FromModel converts a object type to an API type
func (dst *BinkyNetLocalWorkerType) FromModel(ctx context.Context, src model.BinkyNetLocalWorkerType) error {
	switch src {
	case model.BinkynetLocalWorkerTypeLinux:
		*dst = BinkyNetLocalWorkerType_LINUX
	case model.BinkynetLocalWorkerTypeEsphome:
		*dst = BinkyNetLocalWorkerType_ESPHOME
	}
	return nil
}

// ToModel converts a object type from an API type
func (src BinkyNetLocalWorkerType) ToModel(ctx context.Context) (model.BinkyNetLocalWorkerType, error) {
	switch src {
	case BinkyNetLocalWorkerType_LINUX:
		return model.BinkynetLocalWorkerTypeLinux, nil
	case BinkyNetLocalWorkerType_ESPHOME:
		return model.BinkynetLocalWorkerTypeEsphome, nil
	}
	return model.BinkynetLocalWorkerTypeLinux, InvalidArgument("Unknown BinkyNetLocalWorkerType: %s", src)
}
