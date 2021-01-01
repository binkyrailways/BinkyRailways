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
	"fmt"
)

// JunctionWithStateContainer is uses to correctly marshal/unmarshal different
// types of Junctions with state.
type JunctionWithStateContainer struct {
	JunctionWithState
}

// UnmarshalXML unmarshals any persistent entity.
func (jc *JunctionWithStateContainer) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	// Find xsi:Type attribute
	a, found := FindAttr(start.Attr, "type", nsSchemaInstance)
	if !found {
		return fmt.Errorf("Type attribute not found in %#v", start.Attr)
	}

	// Create correct entity based on type
	var jws JunctionWithState
	switch a.Value {
	case "SwitchWithState":
		jws = newSwitchWithState()
	case "PassiveJunctionWithState":
		jws = newPassiveJunctionWithState()
	default:
		return fmt.Errorf("Unknown type: '%s'", a.Value)
	}

	// Decode entity
	if err := d.DecodeElement(jws, &start); err != nil {
		return err
	}

	// Store junction
	jc.JunctionWithState = jws

	return nil
}

// MarshalXML marshals any persistent entity.
func (jc JunctionWithStateContainer) MarshalXML(e *xml.Encoder, start xml.StartElement) error {
	// TODO set Type attribute
	return e.EncodeElement(jc.JunctionWithState, start)
}
