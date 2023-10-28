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

// Shape indicates if a visual shape
type Shape string

const (
	// ShapeCircle results in a circle
	ShapeCircle Shape = "Circle"
	// ShapeTriangle results in a triangle
	ShapeTriangle Shape = "Triangle"
	// ShapeSquare result in a square
	ShapeSquare Shape = "Square"
	// ShapeDiamond results in a diamond
	ShapeDiamond Shape = "Diamond"
)
