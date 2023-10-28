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

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"go.uber.org/multierr"
)

// FromModel converts a model BinkyNetConnection to an API BinkyNetConnection
func (dst *BinkyNetConnection) FromModel(ctx context.Context, src model.BinkyNetConnection) error {
	dst.Key = string(src.GetKey())
	src.GetPins().ForEach(func(bndp model.BinkyNetDevicePin) {
		pin := &BinkyNetDevicePin{}
		pin.FromModel(ctx, bndp)
		dst.Pins = append(dst.Pins, pin)
	})
	dst.Configuration = make(map[string]string)
	src.GetConfiguration().ForEach(func(key, value string) {
		dst.Configuration[key] = value
	})
	return nil
}

// ToModel converts an API BinkyNetConnection to a model BinkyNetConnection
func (src *BinkyNetConnection) ToModel(ctx context.Context, dst model.BinkyNetConnection) error {
	expectedKey := string(dst.GetKey())
	if src.GetKey() != expectedKey {
		return InvalidArgument("Unexpected binkynet connection key: '%s'", src.GetKey())
	}
	if len(src.GetPins()) != dst.GetPins().GetCount() {
		return InvalidArgument("Invalid number of device pins (got %d, expected %d)", len(src.GetPins()), dst.GetPins().GetCount())
	}
	var err error
	for i, p := range src.GetPins() {
		dstPin, ok := dst.GetPins().Get(i)
		if !ok {
			return InvalidArgument("Failed to get device pin at index %d", i)
		}
		multierr.AppendInto(&err, p.ToModel(ctx, dstPin))
	}
	for k, v := range src.GetConfiguration() {
		dst.GetConfiguration().Set(k, v)
	}
	// Remove other keys
	dst.GetConfiguration().ForEach(func(key, value string) {
		if _, ok := src.GetConfiguration()[key]; !ok {
			dst.GetConfiguration().Remove(key)
		}
	})
	return err
}
