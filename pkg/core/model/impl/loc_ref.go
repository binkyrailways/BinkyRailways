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
	"encoding/xml"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type locRef struct {
	id           string
	onTryResolve func(id string) model.Loc
}

var _ model.LocRef = locRef{}

// newLocRef creates a new loc ref
func newLocRef(id string, onTryResolve func(id string) model.Loc) locRef {
	return locRef{
		id:           id,
		onTryResolve: onTryResolve,
	}
}

func (lr *locRef) SetResolver(onTryResolve func(id string) model.Loc) {
	lr.onTryResolve = onTryResolve
}

func (lr *locRef) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	var s string
	if err := d.DecodeElement(&s, &start); err != nil {
		return err
	}
	locRef := newLocRef(s, nil)
	*lr = locRef
	return nil
}

func (lr locRef) MarshalXML(e *xml.Encoder, start xml.StartElement) error {
	return e.EncodeElement(lr.id, start)
}

// Get the Identification value.
func (lr locRef) GetID() string {
	return lr.id
}

// Try to resolve the loc reference.
// Returns non-nil Loc or nil if not found.
func (lr locRef) TryResolve() model.Loc {
	if lr.onTryResolve == nil {
		return nil
	}
	return lr.onTryResolve(lr.id)
}
