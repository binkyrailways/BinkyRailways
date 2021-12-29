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

package model

// ModuleRef is a reference to a module
type ModuleRef interface {
	PositionedEntity

	/// Zoomfactor used in displaying the module (in percentage).
	/// <value>100 means 100%</value>
	GetZoomFactor() int
	SetZoomFactor(value int) error

	// Is this module a reference to the given module?
	IsReferenceTo(module Module) bool

	// Try to resolve the module reference.
	// Returns non-nil Module or nil if not found.
	TryResolve() (Module, error)
}
