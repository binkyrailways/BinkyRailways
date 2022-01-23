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

// BlockSignal is a device that outputs some signal on the railway.
type BlockSignal interface {
	Signal

	// First address
	// This is an output signal.
	GetAddress1() Address
	SetAddress1(ctx context.Context, value Address) error

	// Second address
	// This is an output signal.
	GetAddress2() Address
	SetAddress2(ctx context.Context, value Address) error

	// Third address
	// This is an output signal.
	GetAddress3() Address
	SetAddress3(ctx context.Context, value Address) error

	// Fourth address
	// This is an output signal.
	GetAddress4() Address
	SetAddress4(ctx context.Context, value Address) error

	// Is the Red color available?
	GetIsRedAvailable() bool

	// Bit pattern used for color Red.
	GetRedPattern() int
	SetRedPattern(value int) error

	// Is the Green color available?
	GetIsGreenAvailable() bool

	// Bit pattern used for color Green.
	GetGreenPattern() int
	SetGreenPattern(value int) error

	// Is the Yellow color available?
	GetIsYellowAvailable() bool

	// Bit pattern used for color Yellow.
	GetYellowPattern() int
	SetYellowPattern(value int) error

	// Is the White color available?
	GetIsWhiteAvailable() bool

	// Bit pattern used for color White.
	GetWhitePattern() int
	SetWhitePattern(value int) error

	// The block this signal protects.
	GetBlock() Block
	SetBlock(value Block) error

	// Side of the block where the signal is located.
	GetPosition() BlockSide
	SetPosition(value BlockSide) error

	// Type of signal
	GetType() BlockSignalType
	SetType(value BlockSignalType) error
}
