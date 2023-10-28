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
	"bytes"
	"encoding/json"
	"fmt"
	"strings"
	"time"
)

// parse the given json string into the LogEvent
func (le *LogEvent) parse(msg []byte) error {
	dec := json.NewDecoder(bytes.NewReader(msg))
	return le.parseObject(msg, dec)
}

// parse the given json string into the LogEvent
func (le *LogEvent) parseObject(msg []byte, dec *json.Decoder) error {
	// Expect '{'
	tok, err := dec.Token()
	if err != nil {
		return err
	}
	if delim, ok := tok.(json.Delim); ok {
		if delim.String() != "{" {
			return fmt.Errorf("'{' expected")
		}
	}
	// Parse key/value pairs
	for {
		tok, err = dec.Token()
		if err != nil {
			return err
		}
		var key string
		switch ttok := tok.(type) {
		case json.Delim:
			switch ttok.String() {
			case "}":
				return nil
			default:
				return fmt.Errorf("unexpected token '%s'", ttok.String())
			}
		case string:
			key = ttok
		default:
			return fmt.Errorf("unexpected type %T (%v)", tok, tok)
		}
		// Parse value
		ofs := dec.InputOffset()
		tok, err = dec.Token()
		if err != nil {
			// Take remaining buffer as string
			tok = strings.TrimPrefix(strings.TrimPrefix(string(msg[ofs:]), ":"), "\"")
		}
		switch key {
		case "message":
			le.Message = fmt.Sprintf("%v", tok)
		case "id", "module-id":
			le.Address = fmt.Sprintf("%v", tok)
		case "error":
			le.Error = fmt.Sprintf("%v", tok)
		case "level":
			// Ignore
		case "time":
			le.Time, _ = time.Parse(time.RFC3339, fmt.Sprintf("%v", tok))
		default:
			le.Fields = le.Fields.Set(key, tok)
		}
		if err != nil {
			return err
		}
	}
}
