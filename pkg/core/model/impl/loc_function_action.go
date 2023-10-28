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

// LocFunctionAction extends implementation methods to model.LocFunctionAction
type LocFunctionAction interface {
	LocAction
	model.LocFunctionAction
}

type locFunctionAction struct {
	locAction

	Function model.LocFunctionNumber  `xml:"Function"`
	Command  model.LocFunctionCommand `xml:"Command"`
}

var _ LocFunctionAction = &locFunctionAction{}

// newLocFunctionAction creates a new LocFunctionAction.
func newLocFunctionAction() *locFunctionAction {
	a := &locFunctionAction{}
	return a
}

// GetEntityType returns the type of this entity
func (a *locFunctionAction) GetEntityType() string {
	return TypeLocFunctionAction
}

// Accept a visit by the given visitor
func (a *locFunctionAction) Accept(v model.EntityVisitor) interface{} {
	return v.VisitLocFunctionAction(a)
}

func (a *locFunctionAction) Clone() model.Action {
	c := newLocFunctionAction()
	l, _ := a.GetLoc()
	c.SetLoc(l)
	c.Function = a.GetFunction()
	c.Command = a.GetCommand()
	return c
}

// The function involved in the action.
func (a *locFunctionAction) GetFunction() model.LocFunctionNumber {
	return a.Function
}
func (a *locFunctionAction) SetFunction(value model.LocFunctionNumber) error {
	if a.Function != value {
		a.Function = value
		a.OnModified()
	}
	return nil
}

// What to do with the function
func (a *locFunctionAction) GetCommand() model.LocFunctionCommand {
	return a.Command
}
func (a *locFunctionAction) SetCommand(value model.LocFunctionCommand) error {
	if a.Command != value {
		a.Command = value
		a.OnModified()
	}
	return nil
}
