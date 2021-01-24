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
	"gioui.org/layout"
)

func HorizontalSplit(start, end layout.Widget) SplitContainer {
	return Split(layout.Horizontal, start, end)
}

func VerticalSplit(start, end layout.Widget) SplitContainer {
	return Split(layout.Vertical, start, end)
}

func Split(axis layout.Axis, start, end layout.Widget) SplitContainer {
	return SplitContainer{
		Axis: axis,
		Start: SplitContainerEntry{
			Widget: start,
			Weight: 1,
			Rigid:  false,
		},
		End: SplitContainerEntry{
			Widget: end,
			Weight: 1,
			Rigid:  false,
		},
	}
}

// SplitContainer draws 2 widgets splitted horizontally or vertically
type SplitContainer struct {
	Axis  layout.Axis
	Start SplitContainerEntry
	End   SplitContainerEntry
}

type SplitContainerEntry struct {
	Widget layout.Widget
	Weight float32
	Rigid  bool
}

func (e SplitContainerEntry) AsFlexChild() layout.FlexChild {
	if e.Rigid {
		return layout.Rigid(e.Widget)
	}
	return layout.Flexed(e.Weight, e.Widget)
}

func (sc SplitContainer) Layout(gtx C) D {
	return layout.Flex{Axis: sc.Axis}.Layout(gtx,
		sc.Start.AsFlexChild(),
		sc.End.AsFlexChild(),
	)
}
