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

package widgets

import (
	"strconv"

	"gioui.org/widget"
)

// IntEditor is an editor of integers
type IntEditor struct {
	widget.Editor
}

// SetValue updates the editor to the current value
func (e *IntEditor) SetValue(value int) {
	e.SetText(strconv.Itoa(value))
}

// GetValue returns the current value
func (e *IntEditor) GetValue() (int, error) {
	return strconv.Atoi(e.Editor.Text())
}
