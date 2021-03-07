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
	"net"
	"strconv"
	"sync"

	"github.com/rs/zerolog"
	"google.golang.org/grpc"

	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkynet/BinkyNet/discovery"
)

// binkyNetLogReceiver implements a receiver for log messages
type binkyNetLogReceiver struct {
	log       zerolog.Logger
	providers map[string]struct{}
	mutex     sync.Mutex
}

// Create a new log receiver
func newBinkyNetLogReceiver(log zerolog.Logger) *binkyNetLogReceiver {
	lr := &binkyNetLogReceiver{
		log:       log,
		providers: make(map[string]struct{}),
	}
	return lr

}

// Run until the given context is canceled
func (lr *binkyNetLogReceiver) Run(ctx context.Context) {
	listener := discovery.NewServiceListener(lr.log, api.ServiceTypeLogProvider, true, func(info api.ServiceInfo) {
		lr.serviceInfoChanged(ctx, info)
	})
	listener.Run(ctx)
}

// Service change has been detected
func (lr *binkyNetLogReceiver) serviceInfoChanged(ctx context.Context, info api.ServiceInfo) {
	addr := net.JoinHostPort(info.ApiAddress, strconv.Itoa(int(info.ApiPort)))

	lr.mutex.Lock()
	defer lr.mutex.Unlock()
	if _, found := lr.providers[addr]; !found {
		lr.providers[addr] = struct{}{}
		lr.log.Info().
			Str("address", addr).
			Str("api_version", info.GetApiVersion()).
			Str("version", info.GetVersion()).
			Bool("secure", info.Secure).
			Msg("New log provider found")
		go lr.fetchLogs(ctx, info, addr)
	}
}

// Service change has been detected
func (lr *binkyNetLogReceiver) fetchLogs(ctx context.Context, info api.ServiceInfo, addr string) {
	log := lr.log.With().Str("address", addr).Logger()
	defer func() {
		lr.mutex.Lock()
		defer lr.mutex.Unlock()
		delete(lr.providers, addr)
	}()

	// Try to connect
	conn, err := dialConn(info)
	if err != nil {
		log.Warn().Err(err).Msg("Failed to dial log provider")
		return
	}
	defer conn.Close()

	// Write API client
	logClient := api.NewLogProviderServiceClient(conn)
	server, err := logClient.GetLogs(ctx, &api.GetLogsRequest{})
	if err != nil {
		log.Warn().Err(err).Msg("GetLogs failed")
		return
	}
	for {
		msg, err := server.Recv()
		if err != nil {
			log.Debug().Err(err).Msg("Log connection failed")
			return
		}
		switch msg.GetLevel() {
		case api.LogLevel_DEBUG:
			log.Debug().Msg(msg.GetMessage())
		case api.LogLevel_INFO:
			log.Info().Msg(msg.GetMessage())
		case api.LogLevel_WARNING:
			log.Warn().Msg(msg.GetMessage())
		case api.LogLevel_ERROR:
			log.Error().Msg(msg.GetMessage())
		case api.LogLevel_FATAL:
			log.Error().Msg(msg.GetMessage()) // Do not log fatal here, we do not want this process tro die.
		default:
			log.Debug().Msg(msg.GetMessage())
		}
	}
}

// dialConn creates a GRPC client connection
func dialConn(info api.ServiceInfo) (*grpc.ClientConn, error) {
	address := net.JoinHostPort(info.GetApiAddress(), strconv.Itoa(int(info.GetApiPort())))
	var opts []grpc.DialOption
	if !info.Secure {
		opts = append(opts, grpc.WithInsecure())
	}
	return grpc.Dial(address, opts...)
}
