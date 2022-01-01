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

	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"go.uber.org/multierr"
)

// FromModel converts a model BinkyNetDevicePin to an API BinkyNetDevicePin
func (dst *BinkyNetDevicePin) FromModel(ctx context.Context, src model.BinkyNetDevicePin) error {
	dst.DeviceId = string(src.GetDeviceID())
	dst.Index = uint32(src.GetIndex())
	return nil
}

// ToModel converts an API BinkyNetDevicePin to a model BinkyNetDevicePin
func (src *BinkyNetDevicePin) ToModel(ctx context.Context, dst model.BinkyNetDevicePin) error {
	var err error
	multierr.AppendInto(&err, dst.SetDeviceID(api.DeviceID(src.GetDeviceId())))
	multierr.AppendInto(&err, dst.SetIndex(api.DeviceIndex(src.GetIndex())))
	return err
}
