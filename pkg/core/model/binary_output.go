// Copyright 2020 Ewout Prangsma
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

import "context"

// BinaryOutput is a device that triggers an output on the railway with a state of "on" or "off".
type BinaryOutput interface {
	Output
	AddressEntity

	// Type of binary output
	GetBinaryOutputType() BinaryOutputType
	SetBinaryOutputType(ctx context.Context, value BinaryOutputType) error
}
