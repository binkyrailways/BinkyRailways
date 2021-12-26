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

package server

import (
	"context"
	"fmt"
	"net"
	"net/http"
	"strconv"

	grpc_prometheus "github.com/grpc-ecosystem/go-grpc-prometheus"
	"github.com/rs/zerolog"
	"google.golang.org/grpc"
	"google.golang.org/grpc/reflection"

	api "github.com/binkyrailways/BinkyRailways/pkg/api/v1"
)

// Config for the GRPC server.
type Config struct {
	// Host interface to listen on
	Host string
	// Port to listen on for GRPC requests
	GRPCPort int
}

// Server runs the GRPC server for the service.
type Server struct {
	Config
	service Service
	log     zerolog.Logger
}

// Service expected by this server
type Service interface {
	api.EditorServiceServer
}

// New configures a new Server.
func New(cfg Config, log zerolog.Logger, service Service) (*Server, error) {
	return &Server{
		Config:  cfg,
		service: service,
		log:     log,
	}, nil
}

// Run the server until the given context is canceled.
func (s *Server) Run(ctx context.Context) error {
	// Prepare GRPC listener
	log := s.log
	grpcAddr := net.JoinHostPort(s.Host, strconv.Itoa(s.GRPCPort))
	grpcLis, err := net.Listen("tcp", grpcAddr)
	if err != nil {
		log.Fatal().Err(err).Msgf("failed to listen on address %s", grpcAddr)
	}

	// Prepare GRPC server
	grpcSrv := grpc.NewServer(
		grpc.StreamInterceptor(grpc_prometheus.StreamServerInterceptor),
		grpc.UnaryInterceptor(grpc_prometheus.UnaryServerInterceptor),
	)
	api.RegisterEditorServiceServer(grpcSrv, s.service)
	// Register reflection service on gRPC server.
	reflection.Register(grpcSrv)

	// Serve apis
	log.Debug().Str("address", grpcAddr).Msg("Serving gRPC")
	if err := grpcSrv.Serve(grpcLis); err != nil {
		log.Fatal().Err(err).Msg("failed to serve GRPC server")
	}

	// Wait until context closed
	<-ctx.Done()

	log.Info().Msg("Closing GRPC server")
	grpcSrv.GracefulStop()

	return nil
}

func healthHandler(w http.ResponseWriter, r *http.Request) {
	fmt.Fprintln(w, "OK")
}
