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

// OutputContainer is uses to correctly marshal/unmarshal different
// types of Outputs.
type OutputContainer struct {
	Output
}

// UnmarshalXML unmarshals any persistent entity.
func (jc *OutputContainer) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	// Find xsi:Type attribute
	a, found := FindAttr(start.Attr, "type", nsSchemaInstance)
	if !found {
		return fmt.Errorf("Type attribute not found in %#v", start.Attr)
	}

	// Create correct entity based on type
	var output Output
	switch a.Value {
	case TypeBinaryOutput:
		output = newBinaryOutput()
	case TypeClock4StageOutput:
		output = newClock4StageOutput()
	default:
		return fmt.Errorf("Unknown type: '%s'", a.Value)
	}

	// Decode entity
	if err := d.DecodeElement(output, &start); err != nil {
		return err
	}

	// Store junction
	jc.Output = output

	return nil
}

// MarshalXML marshals any persistent entity.
func (jc OutputContainer) MarshalXML(e *xml.Encoder, start xml.StartElement) error {
	// Add Type attribute
	tEntity, ok := jc.Output.(TypedEntity)
	if !ok {
		return fmt.Errorf("Entity does not implement TypedEntity")
	}
	start.Attr = UpdateOrAddAttr(start.Attr, "type", nsSchemaInstance, tEntity.GetEntityType())

	// Normal encoding
	if err := e.EncodeElement(jc.Output, start); err != nil {
		return err
	}

	return nil
}
