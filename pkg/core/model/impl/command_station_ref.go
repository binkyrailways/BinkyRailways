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

package impl

import (
	"fmt"
	"strings"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model/refs"
)

// CommandStationRef adds implementation methods to model.CommandStationRef
type CommandStationRef interface {
	model.CommandStationRef
}

type commandStationRef struct {
	ID            string    `xml:"Id"`
	AddressSpaces *[]string `xml:"AddressSpaces,omitempty"`
	onTryResolve  func(id string) (model.CommandStation, error)
}

var _ CommandStationRef = &commandStationRef{}

// newCommandStationRef creates a new cs ref
func newCommandStationRef(id string, onTryResolve func(id string) (model.CommandStation, error)) commandStationRef {
	return commandStationRef{
		ID:           id,
		onTryResolve: onTryResolve,
	}
}

func (lr *commandStationRef) SetResolver(onTryResolve func(id string) (model.CommandStation, error)) {
	lr.onTryResolve = onTryResolve
}

// Get the Identification value.
func (lr *commandStationRef) GetID() string {
	return lr.ID
}

// The names of address spaces served by this command station
func (lr *commandStationRef) GetAddressSpaces() []string {
	return refs.StringSliceValue(lr.AddressSpaces, nil)
}
func (lr *commandStationRef) SetAddressSpaces(value []string) error {
	if strings.Join(lr.GetAddressSpaces(), ",") != strings.Join(value, ",") {
		lr.AddressSpaces = refs.NewStringSlice(value)
	}
	return nil
}

// Get the Identification value.
func (lr *commandStationRef) Set(value model.CommandStation, onModified func()) error {
	id := ""
	if value == nil {
		id = value.GetID()
	}
	if lr.ID != id {
		lr.ID = id
		onModified()
	}
	return nil
}

// Try to resolve the loc reference.
// Returns non-nil CommandStation or nil if not found.
func (lr *commandStationRef) TryResolve() (model.CommandStation, error) {
	if lr.ID == "" {
		return nil, nil
	}
	if lr.onTryResolve == nil {
		return nil, fmt.Errorf("onTryResolve is nil")
	}
	return lr.onTryResolve(lr.ID)
}
