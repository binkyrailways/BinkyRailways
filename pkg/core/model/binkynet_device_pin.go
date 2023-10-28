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

// BinkyNetDevicePin identifies a hardware device and an index within that hardware address.
type BinkyNetDevicePin interface {
	// Gets the containing local worker
	GetLocalWorker() BinkyNetLocalWorker

	// Gets the connection that contains this pin
	GetConnection() BinkyNetConnection

	// ID of the device that this connection refers to.
	GetDeviceID() api.DeviceID
	SetDeviceID(ctx context.Context, value api.DeviceID) error

	// Index on the device (1...)
	GetIndex() api.DeviceIndex
	SetIndex(ctx context.Context, value api.DeviceIndex) error
}
