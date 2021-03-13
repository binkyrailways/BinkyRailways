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

package editors

import (
	"context"

	"gioui.org/layout"
	"gioui.org/widget"
	"gioui.org/widget/material"
)

type (
	C = layout.Context
	D = layout.Dimensions
)

// Editor defines the API for editing UI components
type Editor interface {
	// Handle events and draw the editor
	Layout(gtx C, th *material.Theme) D
	// Create the buttons for the "Add resource sheet"
	CreateAddButtons() []AddButton
	// Can the currently selected item be deleted?
	CanDelete() bool
	// Delete the currently selected item
	Delete(context.Context) error
}

// EditorContext is passed to each Editor upon construction.
type EditorContext interface {
	// Select the given entity in the view
	Select(entity interface{})
	// Invalidate the UI
	Invalidate()
}

// AddButton is used to generate a button for adding an entity
type AddButton struct {
	Title     string
	Separator bool
	OnClick   func()
	widget.Clickable
}
