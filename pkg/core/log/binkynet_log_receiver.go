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
	"context"
	"encoding/json"
	"strings"

	"github.com/rs/zerolog"

	"github.com/binkynet/BinkyNet/netlog"
)

// BinkyNetLogReceiver implements a receiver for log messages
type BinkyNetLogReceiver struct {
	log zerolog.Logger
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
		evt := lr.log.WithLevel(m.Level).Str("address", m.Address)
		var mm map[string]interface{}
		if err := json.Unmarshal(m.Message, &mm); err == nil {
			var message string
			if messageRaw, ok := mm["message"]; ok {
				message, _ = messageRaw.(string)
			}
			for k, v := range mm {
				if k != "message" && k != "level" {
					evt = evt.Interface(k, v)
				}
			}
			evt.Msg(message)
		} else {
			evt.Msg(strings.TrimSpace(string(m.Message)))
		}
	})
	if err != nil {
		lr.log.Error().Err(err).Msg("Failed to receive log messages")
	}
	lr.log.Info().Msg("Started BinkyNet log receiver")
	<-ctx.Done()
}
