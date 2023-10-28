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

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type groupLocRef struct {
	ID           string `xml:",chardata"`
	onTryResolve func(id string) (model.Loc, error)
}

var _ model.LocRef = groupLocRef{}

// newLocRef creates a new loc ref
func newGroupLocRef(id string, onTryResolve func(id string) (model.Loc, error)) groupLocRef {
	return groupLocRef{
		ID:           id,
		onTryResolve: onTryResolve,
	}
}

func (lr *groupLocRef) SetResolver(onTryResolve func(id string) (model.Loc, error)) {
	lr.onTryResolve = onTryResolve
}

// Get the Identification value.
func (lr groupLocRef) GetID() string {
	return lr.ID
}

// Try to resolve the loc reference.
// Returns non-nil Loc or nil if not found.
func (lr groupLocRef) TryResolve() (model.Loc, error) {
	if lr.ID == "" {
		return nil, nil
	}
	if lr.onTryResolve == nil {
		return nil, fmt.Errorf("onTryResolve is nil")
	}
	return lr.onTryResolve(lr.ID)
}
