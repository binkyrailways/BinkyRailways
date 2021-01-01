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

// LocPredicateContainer is uses to correctly marshal/unmarshal different
// types of loc predicates.
type LocPredicateContainer struct {
	LocPredicate
}

// UnmarshalXML unmarshals any persistent entity.
func (jc *LocPredicateContainer) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	// Find xsi:Type attribute
	a, found := FindAttr(start.Attr, "type", nsSchemaInstance)
	if !found {
		return fmt.Errorf("Type attribute not found in %#v", start.Attr)
	}

	// Create correct entity based on type
	var predicate LocPredicate
	switch a.Value {
	case "LocAndPredicate":
		predicate = newLocAndPredicate()
	case "LocOrPredicate":
		predicate = newLocOrPredicate()
	case "LocCanChangeDirectionPredicate":
		predicate = newLocCanChangeDirectionPredicate()
	case "LocEqualsPredicate":
		predicate = newLocEqualsPredicate()
	case "LocGroupEqualsPredicate":
		predicate = newLocGroupEqualsPredicate()
	case "LocStandardPredicate":
		predicate = newLocStandardPredicate()
	default:
		return fmt.Errorf("Unknown type: '%s'", a.Value)
	}

	// Decode entity
	if err := d.DecodeElement(predicate, &start); err != nil {
		return err
	}

	// Store predicate
	jc.LocPredicate = predicate

	return nil
}

// MarshalXML marshals any persistent entity.
func (jc LocPredicateContainer) MarshalXML(e *xml.Encoder, start xml.StartElement) error {
	// TODO set Type attribute
	return e.EncodeElement(jc.LocPredicate, start)
}
