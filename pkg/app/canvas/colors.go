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

package canvas

import "github.com/binkyrailways/BinkyRailways/pkg/app/widgets"

var (
	HoverBg    = widgets.ARGB(0xC0333333)
	BlockBg    = widgets.ARGB(0xA0CCCCCC)
	BlockFront = widgets.ARGB(0xA000CCCC)

	SensorBg         = widgets.ARGB(0xA0ffa726)
	ActiveSensorBg   = widgets.ARGB(0xFFba000d)
	InactiveSensorBg = widgets.ARGB(0xFF90a4ae)

	OutputBg    = widgets.ARGB(0xA0ce93d8)
	OutputOnBg  = widgets.ARGB(0xFFce93d8)
	OutputOffBg = widgets.ARGB(0xFF6a1b9a)

	InConsistentBorder = widgets.ARGB(0xFFffd740)

	SwitchBg        = widgets.ARGB(0xFFFFFFFF)
	SwitchIndicator = widgets.ARGB(0xFF0000CC)
)
