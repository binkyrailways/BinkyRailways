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

// ModuleConnection is a connection between the edges of two modules.
type ModuleConnection interface {
	ImportableEntity
	RailwayEntity

	// The first module in the connection
	GetModuleA() Module

	// Edge of module A
	GetEdgeA() Edge
	SetEdgeA(value Edge) error

	// The second module in the connection
	GetModuleB() Module

	// Edge of module B
	GetEdgeB() Edge
	SetEdgeB(value Edge) error
}
