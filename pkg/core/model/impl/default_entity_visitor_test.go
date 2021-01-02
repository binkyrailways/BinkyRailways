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
	"testing"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/stretchr/testify/assert"
)

// DefaultEntityVisitor is a default implementation of EntityVisitor.
func TestDefaultEntityVisitor(t *testing.T) {
	v := &testEntityVisitor{}
	v.SetDefaultVisitor(v)
	cs := NewLocoBufferCommandStation()
	result := cs.Accept(v)
	assert.Equal(t, "foo", result)
}

type testEntityVisitor struct {
	model.DefaultEntityVisitor
}

func (v *testEntityVisitor) VisitCommandStation(x model.CommandStation) interface{} {
	return "foo"
}
