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

// Clock4StageOutput is a device that sends clock signal in 2 bits to the track.
type Clock4StageOutput interface {
	Output
	AddressEntity

	// Address of first clock bit.
	// This is an output signal.
	GetAddress1() Address
	SetAddress1(ctx context.Context, value Address) error

	// Address of second clock bit.
	// This is an output signal.
	GetAddress2() Address
	SetAddress2(ctx context.Context, value Address) error

	// Bit pattern used for "morning".
	GetMorningPattern() int
	SetMorningPattern(value int) error

	// Bit pattern used for "afternoon".
	GetAfternoonPattern() int
	SetAfternoonPattern(value int) error

	// Bit pattern used for "evening".
	GetEveningPattern() int
	SetEveningPattern(value int) error

	// Bit pattern used for "night".
	GetNightPattern() int
	SetNightPattern(value int) error
}
