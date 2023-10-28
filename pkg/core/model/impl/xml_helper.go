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
)

const (
	nsSchemaInstance = "http://www.w3.org/2001/XMLSchema-instance"
)

// FindAttr returns the first attribute in the given list with give
// name and namespace.
func FindAttr(list []xml.Attr, name, namespace string) (xml.Attr, bool) {
	for _, x := range list {
		if x.Name.Local == name && x.Name.Space == namespace {
			return x, true
		}
	}
	return xml.Attr{}, false
}

// UpdateOrAddAttr returns the first a modified attribute list.
// The new list contains an attribute with given name and value.
func UpdateOrAddAttr(list []xml.Attr, name, namespace, value string) []xml.Attr {
	for idx, x := range list {
		if x.Name.Local == name && x.Name.Space == namespace {
			list[idx].Value = value
			return list
		}
	}
	return append(list, xml.Attr{
		Name: xml.Name{
			Space: namespace,
			Local: name,
		},
		Value: value,
	})
}
