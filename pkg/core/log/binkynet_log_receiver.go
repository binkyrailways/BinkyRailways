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
	"fmt"
	"sort"
	"time"

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
	// Source address of the event
	Address string
	// Level of the event
	Level zerolog.Level
	// Human readable message of the event
	Message string
	// Error message (if any)
	Error string
	// Additional fields of the event
	Fields LogFields
	// Time of the event
	Time time.Time
}

// LogField captures a single log event key-value pair
type LogField struct {
	Key   string
	Value interface{}
}

// LogFields is a list of log fields
type LogFields []LogField

// Set a log field by its key
func (lf LogFields) Set(key string, value interface{}) LogFields {
	for idx, x := range lf {
		if x.Key == key {
			lf[idx].Value = value
			return lf
		}
	}
	return append(lf, LogField{
		Key:   key,
		Value: value,
	})
}

// Get a log field by its key
func (lf LogFields) Get(key string) (LogField, bool) {
	for _, x := range lf {
		if x.Key == key {
			return x, true
		}
	}
	return LogField{}, false
}

// Find a log field by its key
func (lf LogFields) Find(key string) interface{} {
	for _, x := range lf {
		if x.Key == key {
			return x.Value
		}
	}
	return nil
}

func (lf LogField) ValueAsString() string {
	if lf.Value == nil {
		return ""
	}
	if s, ok := lf.Value.(fmt.Stringer); ok {
		return s.String()
	}
	return fmt.Sprintf("%v", lf.Value)
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
		evt.parse(m.Message)
		sort.Slice(evt.Fields, func(i, j int) bool {
			k1, k2 := evt.Fields[i].Key, evt.Fields[j].Key
			return k1 < k2
		})

		lr.events = append(lr.events, evt)
		lr.InvokeViews(lr.events)
	})
	if err != nil {
		lr.log.Error().Err(err).Msg("Failed to receive log messages")
	}
	lr.log.Info().Msg("Started BinkyNet log receiver")
	<-ctx.Done()
}
