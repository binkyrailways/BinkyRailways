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

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type binkyNetRouter struct {
	container *binkyNetRouterSet
	binkyNetRouterFields
}

type binkyNetRouterFields struct {
	entity
}

var _ model.BinkyNetRouter = &binkyNetRouter{}

// newBinkyNetRouter creates and initializes a new binky router.
func newBinkyNetRouter() *binkyNetRouter {
	lw := &binkyNetRouter{}
	lw.EnsureID()
	return lw
}

// UnmarshalXML unmarshals and connects the module.
func (r *binkyNetRouter) UnmarshalXML(d *xml.Decoder, start xml.StartElement) error {
	if err := d.DecodeElement(&r.binkyNetRouterFields, &start); err != nil {
		return err
	}
	return nil
}

// Gets the local worker this object belongs to
func (r *binkyNetRouter) GetLocalWorker() model.BinkyNetLocalWorker {
	if r.container != nil {
		return r.container.GetLocalWorker()
	}
	return nil
}

// Get the identifier of the module.
// That is: (Alias(or ID) or the local worker) '-' (normalized description of router)
func (r *binkyNetRouter) GetModuleID() string {
	if lw := r.GetLocalWorker(); lw != nil {
		alias := lw.GetAlias()
		if alias == "" {
			alias = lw.GetID()
		}
		return model.NormalizeName(fmt.Sprintf("%s-%s", alias, r.GetDescription()))
	}
	return ""
}

// SetContainer links this instance to its container
func (r *binkyNetRouter) SetContainer(container *binkyNetRouterSet) {
	r.container = container
}

// Accept a visit by the given visitor
func (r *binkyNetRouter) Accept(v model.EntityVisitor) interface{} {
	return v.VisitBinkyNetRouter(r)
}

// OnModified triggers the modified function of the parent (if any)
func (r *binkyNetRouter) OnModified() {
	if r.container != nil {
		r.container.OnModified()
	}
	r.entity.OnModified()
}
