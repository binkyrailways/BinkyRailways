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

// BlockSide indicates a side on the block.
type BlockSide string

const (
	// BlockSideFront indicates the end of normal driving direction
	BlockSideFront BlockSide = "Front"
	// BlockSideBack indicates the begining of normal driving direction
	BlockSideBack BlockSide = "Back"
)

// Invert returns the inverted side.
func (bs BlockSide) Invert() BlockSide {
	switch bs {
	case BlockSideFront:
		return BlockSideBack
	case BlockSideBack:
		return BlockSideFront
	default:
		panic("Unknown block side: " + string(bs))
	}
}

// String returns block side as string
func (bs BlockSide) String() string {
	return string(bs)
}
