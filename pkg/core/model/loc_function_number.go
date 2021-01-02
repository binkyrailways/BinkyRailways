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
	"encoding/xml"
	"fmt"
)

// LocFunctionNumber is a strongly type function number
type LocFunctionNumber int

const (
	Light LocFunctionNumber = 0
	F1    LocFunctionNumber = 1
	F2    LocFunctionNumber = 2
	F3    LocFunctionNumber = 3
	F4    LocFunctionNumber = 4
	F5    LocFunctionNumber = 5
	F6    LocFunctionNumber = 6
	F7    LocFunctionNumber = 7
	F8    LocFunctionNumber = 8
	F9    LocFunctionNumber = 9
	F10   LocFunctionNumber = 10
	F11   LocFunctionNumber = 11
	F12   LocFunctionNumber = 12
	F13   LocFunctionNumber = 13
	F14   LocFunctionNumber = 14
	F15   LocFunctionNumber = 15
	F16   LocFunctionNumber = 16
)

var (
	locFunctionNumberNames = map[LocFunctionNumber]string{
		Light: "Light",
		F1:    "F1",
		F2:    "F2",
		F3:    "F3",
		F4:    "F4",
		F5:    "F5",
		F6:    "F6",
		F7:    "F7",
		F8:    "F8",
		F9:    "F9",
		F10:   "F10",
		F11:   "F11",
		F12:   "F12",
		F13:   "F13",
		F14:   "F14",
		F15:   "F15",
		F16:   "F16",
	}
)

// NewLocFunctionNumberFromString creates a loc function number from given string
func NewLocFunctionNumberFromString(value string) (LocFunctionNumber, error) {
	for nr, s := range locFunctionNumberNames {
		if s == value {
			return nr, nil
		}
	}
	return 0, fmt.Errorf("Unknown loc function number '%s'", value)
}

// String converts to string
func (lfn LocFunctionNumber) String() string {
	return locFunctionNumberNames[lfn]
}

func (lfn *LocFunctionNumber) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	var s string
	if err := d.DecodeElement(&s, &start); err != nil {
		return err
	}
	nr, err := NewLocFunctionNumberFromString(s)
	if err != nil {
		return err
	}
	*lfn = nr
	return nil
}

func (lfn LocFunctionNumber) MarshalXML(e *xml.Encoder, start xml.StartElement) error {
	return e.EncodeElement(lfn.String(), start)
}
