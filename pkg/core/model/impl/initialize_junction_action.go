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
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// InitializeJunctionAction extends implementation methods to model.InitializeJunctionAction
type InitializeJunctionAction interface {
	Action
	model.InitializeJunctionAction
}

type initializeJunctionAction struct {
	moduleAction

	JunctionID JunctionRef `xml:"JunctionId,omitempty"`
}

var _ InitializeJunctionAction = &initializeJunctionAction{}

// newInitializeJunctionAction creates a new InitializeJunctionAction.
func newInitializeJunctionAction() *initializeJunctionAction {
	a := &initializeJunctionAction{}
	return a
}

func (a *initializeJunctionAction) Clone() model.Action {
	c := newInitializeJunctionAction()
	// TODO
	return c
}

func (a *initializeJunctionAction) GetJunction() model.Junction {
	if result, ok := a.JunctionID.Get(a.GetModule()); ok {
		return result
	}
	return nil
}
func (a *initializeJunctionAction) SetJunction(value model.Junction) error {
	return a.JunctionID.Set(value, a.GetModule(), a.OnModified)
}
