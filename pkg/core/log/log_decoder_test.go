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

package log

import (
	"testing"

	"github.com/stretchr/testify/assert"
)

func TestLogEventParse(t *testing.T) {
	t.Run("Valid message", func(t *testing.T) {
		var le LogEvent
		err := le.parse([]byte(`{"message":"foo"}`))
		assert.NoError(t, err)
		assert.Equal(t, "foo", le.Message)
	})
	t.Run("Valid message, more fields", func(t *testing.T) {
		var le LogEvent
		err := le.parse([]byte(`{"message":"foo","intfield":5,"strfield":"string"}`))
		assert.NoError(t, err)
		assert.Equal(t, "foo", le.Message)
		assert.Equal(t, float64(5), le.Fields.Find("intfield"))
		assert.Equal(t, "string", le.Fields.Find("strfield"))
	})
	t.Run("Valid message, duplicate fields", func(t *testing.T) {
		var le LogEvent
		err := le.parse([]byte(`{"message":"foo","intfield":5,"intfield":"string"}`))
		assert.NoError(t, err)
		assert.Equal(t, "foo", le.Message)
		assert.Equal(t, "string", le.Fields.Find("intfield"))
	})
	t.Run("Partial message", func(t *testing.T) {
		var le LogEvent
		err := le.parse([]byte(`{"message":"foo","intfie`))
		assert.Error(t, err)
		assert.Equal(t, "foo", le.Message)
	})
	t.Run("Longer Partial message", func(t *testing.T) {
		var le LogEvent
		err := le.parse([]byte(`{"key1":1,"key2":"two","message":"something ends early`))
		assert.Error(t, err)
		assert.Equal(t, float64(1), le.Fields.Find("key1"))
		assert.Equal(t, "two", le.Fields.Find("key2"))
		assert.Equal(t, "something ends early", le.Message)
	})
}
