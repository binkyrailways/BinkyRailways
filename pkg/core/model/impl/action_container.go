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

package impl

import (
	"encoding/xml"
	"fmt"
)

// ActionContainer is uses to correctly marshal/unmarshal different
// types of Actions.
type ActionContainer struct {
	Action
}

// UnmarshalXML unmarshals any persistent entity.
func (jc *ActionContainer) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	// Find xsi:Type attribute
	a, found := FindAttr(start.Attr, "type", nsSchemaInstance)
	if !found {
		return fmt.Errorf("Type attribute not found in %#v", start.Attr)
	}

	// Create correct entity based on type
	var action Action
	switch a.Value {
	case TypeInitializeJunctionAction:
		action = newInitializeJunctionAction()
	case TypeLocFunctionAction:
		action = newLocFunctionAction()
	default:
		return fmt.Errorf("Unknown type: '%s'", a.Value)
	}

	// Decode entity
	if err := d.DecodeElement(action, &start); err != nil {
		return err
	}

	// Store junction
	jc.Action = action

	return nil
}

// MarshalXML marshals any persistent entity.
func (jc ActionContainer) MarshalXML(e *xml.Encoder, start xml.StartElement) error {

	// Add Type attribute
	tEntity, ok := jc.Action.(TypedEntity)
	if !ok {
		return fmt.Errorf("Entity does not implement TypedEntity")
	}
	start.Attr = UpdateOrAddAttr(start.Attr, "type", nsSchemaInstance, tEntity.GetEntityType())

	// Normal encoding
	if err := e.EncodeElement(jc.Action, start); err != nil {
		return err
	}

	return nil
}
