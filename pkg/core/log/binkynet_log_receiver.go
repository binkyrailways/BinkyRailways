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
	"context"
	"encoding/json"
	"fmt"
	"sort"
	"strings"

	"github.com/rs/zerolog"

	"github.com/binkynet/BinkyNet/netlog"
)

// BinkyNetLogReceiver implements a receiver for log messages
type BinkyNetLogReceiver struct {
	log zerolog.Logger

	logViews
	events []LogEvent
}

// LogEvent contains all information of a single log message from a device.
type LogEvent struct {
	Address string
	Level   zerolog.Level
	Message string
	Fields  LogFields
}

// LogField captures a single log event key-value pair
type LogField struct {
	Key   string
	Value interface{}
}

// LogFields is a list of log fields
type LogFields []LogField

// Get a log field by its key
func (lf LogFields) Get(key string) (LogField, bool) {
	for _, x := range lf {
		if x.Key == key {
			return x, true
		}
	}
	return LogField{}, false
}

func (lf LogField) ValueAsString() string {
	if lf.Value == nil {
		return ""
	}
	if s, ok := lf.Value.(fmt.Stringer); ok {
		return s.String()
	}
	return "?"
}

// Create a new log receiver
func NewBinkyNetLogReceiver(log zerolog.Logger) *BinkyNetLogReceiver {
	lr := &BinkyNetLogReceiver{
		log: log,
	}
	return lr
}

// Run until the given context is canceled
func (lr *BinkyNetLogReceiver) Run(ctx context.Context) {
	_, err := netlog.NewLogReceiver(func(m netlog.NetLogMessage) {
		evt := LogEvent{
			Address: m.Address,
			Level:   m.Level,
		}
		var mm map[string]interface{}
		if err := json.Unmarshal(m.Message, &mm); err == nil {
			var message string
			if messageRaw, ok := mm["message"]; ok {
				message, _ = messageRaw.(string)
			}
			for k, v := range mm {
				if k != "message" && k != "level" {
					evt.Fields = append(evt.Fields, LogField{
						Key:   k,
						Value: v,
					})
				}
			}
			evt.Message = message
			sort.Slice(evt.Fields, func(i, j int) bool {
				k1, k2 := evt.Fields[i].Key, evt.Fields[j].Key
				return k1 < k2
			})
		} else {
			evt.Message = strings.TrimSpace(string(m.Message))
		}
		lr.events = append(lr.events, evt)
		lr.InvokeViews(lr.events)
	})
	if err != nil {
		lr.log.Error().Err(err).Msg("Failed to receive log messages")
	}
	lr.log.Info().Msg("Started BinkyNet log receiver")
	<-ctx.Done()
}
