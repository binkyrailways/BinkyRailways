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

// TurnTable or fiddle yard
type TurnTable interface {
	Junction
	AddressEntity

	// Address of first position bit.
	// This is an output signal.
	GetPositionAddress1() Address
	SetPositionAddress1(value Address) error

	// Address of second position bit.
	// This is an output signal.
	GetPositionAddress2() Address
	SetPositionAddress2(value Address) error

	// Address of third position bit.
	// This is an output signal.
	GetPositionAddress3() Address
	SetPositionAddress3(value Address) error

	// Address of fourth position bit.
	// This is an output signal.
	GetPositionAddress4() Address
	SetPositionAddress4(value Address) error

	// Address of fifth position bit.
	// This is an output signal.
	GetPositionAddress5() Address
	SetPositionAddress5(value Address) error

	// Address of sixth position bit.
	// This is an output signal.
	GetPositionAddress6() Address
	SetPositionAddress6(value Address) error

	// If set, the straight/off commands used for position addresses are inverted.
	GetInvertPositions() bool
	SetInvertPositions(value bool) error

	// Address of the line used to indicate a "write address".
	// This is an output signal.
	GetWriteAddress() Address
	SetWriteAddress(value Address) error

	// If set, the straight/off command used for "write address" line is inverted.
	GetInvertWrite() bool
	SetInvertWrite(value bool) error

	// Address of the line used to indicate a "change of position in progress".
	// This is an input signal.
	GetBusyAddress() Address
	SetBusyAddress(value Address) error

	// If set, the input level used for "busy" line is inverted.
	GetInvertBusy() bool
	SetInvertBusy(value bool) error

	// First position number. Typically 1.
	GetFirstPosition() int
	SetFirstPosition(value int) error

	// Last position number. Typically 63.
	GetLastPosition() int
	SetLastPosition(value int) error

	// Position number used to initialize the turntable with?
	GetInitialPosition() int
	SetInitialPosition(value int) error
}
