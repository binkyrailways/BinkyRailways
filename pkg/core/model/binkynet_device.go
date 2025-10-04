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

package model

import (
	"context"

	api "github.com/binkynet/BinkyNet/apis/v1"
)

// BinkyNetDevice represents a hardware device such as a I2C chip.
type BinkyNetDevice interface {
	Entity

	// Gets the local worker this device belongs to
	GetLocalWorker() BinkyNetLocalWorker

	// Gets the router that routes command & state to/from this device
	GetRouter() BinkyNetRouter
	SetRouter(ctx context.Context, value BinkyNetRouter) error

	// ID of the device
	GetDeviceID() api.DeviceID
	SetDeviceID(ctx context.Context, value api.DeviceID) error

	// Type of the device
	GetDeviceType() api.DeviceType
	SetDeviceType(ctx context.Context, value api.DeviceType) error

	// Address of the device
	GetAddress() string
	SetAddress(ctx context.Context, value string) error

	// Is this device disabled?
	GetIsDisabled() bool
	SetIsDisabled(context.Context, bool) error
}
