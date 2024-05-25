// Copyright 2024 Ewout Prangsma
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

package service

import (
	"testing"

	"github.com/stretchr/testify/assert"
)

func TestHash(t *testing.T) {
	assert.Equal(t, "da39a3ee5e6b4b0d3255bfef95601890afd80709", hash(""))
	assert.Equal(t, "0beec7b5ea3f0fdbc95d0dd47f3c5bc275da8a33", hash("foo"))
	assert.Equal(t, "19ea609d030e88c0f8bf5ce3343215fbb0695430", hash("this is very long this is very long this is very long this is very long this is very long this is very long this is very long this is very long this is very long this is very long this is very long this is very long this is very long this is very long this is very long this is very long this is very long this is very long this is very long this is very long this is very long this is very long this is very long this is very long this is very long this is very long this is very long "))
}
