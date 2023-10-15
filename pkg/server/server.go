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
	"crypto/tls"
	"fmt"
	"net"
	"net/http"
	"strconv"

	"github.com/improbable-eng/grpc-web/go/grpcweb"
	"github.com/labstack/echo/v4"
	"github.com/prometheus/client_golang/prometheus/promhttp"
	"github.com/rs/zerolog"
	"google.golang.org/grpc"
	"google.golang.org/grpc/reflection"

	api "github.com/binkyrailways/BinkyRailways/pkg/api/v1"
)

// Config for the GRPC server.
type Config struct {
	// Host interface to listen on
	Host string
	// Address of the current host that we publish on
	PublishedHost string
	// Port to listen on for HTTP requests
	HTTPPort int
	// Port to listen on for GRPC requests
	GRPCPort int
	// Port to listen on for Loki requests
	LokiPort int
	// URL to proxy loki requests to
	LokiURL string
	// If set, the web application is served from live filesystem instead of embedding.
	WebDevelopment bool
}

// Server runs the GRPC server for the service.
type Server struct {
	Config
	service     Service
	log         zerolog.Logger
	lokiHandler *lokiHandler
}

// Service expected by this server
type Service interface {
	api.ModelServiceServer
	api.StateServiceServer
	api.StorageServiceServer
	// Get image of loc by id
	// Returns: image, contentType, error
	GetLocImage(ctx context.Context, locID string) ([]byte, string, error)
}

// New configures a new Server.
func New(cfg Config, log zerolog.Logger, service Service) (*Server, error) {
	return &Server{
		Config:      cfg,
		service:     service,
		log:         log,
		lokiHandler: newLokiHandler(log),
	}, nil
}

// Run the server until the given context is canceled.
func (s *Server) Run(ctx context.Context) error {
	// Prepare logger
	log := s.log

	// Prepare TLS config
	tlsCfg, err := createSelfSignedCertificate(s.PublishedHost)
	if err != nil {
		log.Fatal().Err(err).Msgf("failed to prepare self signed certificate")
	}

	// Prepare HTTP listener
	httpAddr := net.JoinHostPort(s.Host, strconv.Itoa(s.HTTPPort))
	httpLis, err := net.Listen("tcp", httpAddr)
	if err != nil {
		log.Fatal().Err(err).Msgf("failed to listen on address %s", httpAddr)
	}
	httpsLis := tls.NewListener(httpLis, tlsCfg)

	// Prepare GRPC listener
	grpcAddr := net.JoinHostPort(s.Host, strconv.Itoa(s.GRPCPort))
	grpcLis, err := net.Listen("tcp", grpcAddr)
	if err != nil {
		log.Fatal().Err(err).Msgf("failed to listen on address %s", grpcAddr)
	}
	grpcsLis := tls.NewListener(grpcLis, tlsCfg)

	// Prepare Loki listener
	lokiAddr := net.JoinHostPort(s.Host, strconv.Itoa(s.LokiPort))
	lokiLis, err := net.Listen("tcp", lokiAddr)
	if err != nil {
		log.Fatal().Err(err).Msgf("failed to listen on address %s", lokiAddr)
	}
	/*lokiUrl, err := url.Parse(s.LokiURL)
	if err != nil {
		log.Fatal().Err(err).Msgf("failed to parse loki URL: %s", s.LokiURL)
	}*/
	lokiRouter := echo.New()
	lokiRouter.POST("/loki/api/v1/push", s.handlePushLokiRequest)
	lokiSrv := &http.Server{
		Handler: lokiRouter,
		//Handler: httputil.NewSingleHostReverseProxy(lokiUrl),
	}

	// Prepare GRPC server
	grpcSrv := grpc.NewServer(
	//grpc.StreamInterceptor(grpc_prometheus.StreamServerInterceptor),
	//grpc.UnaryInterceptor(grpc_prometheus.UnaryServerInterceptor),
	)
	api.RegisterModelServiceServer(grpcSrv, s.service)
	api.RegisterStateServiceServer(grpcSrv, s.service)
	api.RegisterStorageServiceServer(grpcSrv, s.service)
	// Register reflection service on gRPC server.
	reflection.Register(grpcSrv)
	wrappedGrpc := grpcweb.WrapServer(grpcSrv)

	// Prepare HTTP server
	httpRouter := echo.New()
	//httpRouter.GET("/", s.handleGetIndex)
	httpRouter.GET("/loc/:id/image", s.handleGetLocImage)
	httpRouter.GET("/metrics", echo.WrapHandler(promhttp.Handler()))
	httpRouter.GET("/*", echo.WrapHandler(http.FileServer(getWebAppFileSystem(s.WebDevelopment))))
	httpSrv := http.Server{
		Handler: http.HandlerFunc(func(resp http.ResponseWriter, req *http.Request) {
			if wrappedGrpc.IsGrpcWebRequest(req) {
				wrappedGrpc.ServeHTTP(resp, req)
				return
			}
			// Fall back to other servers.
			httpRouter.ServeHTTP(resp, req)
		}),
	}

	// Serve apis
	log.Debug().Str("address", httpAddr).Msg("Serving HTTP")
	go func() {
		if err := httpSrv.Serve(httpsLis); err != nil {
			log.Fatal().Err(err).Msg("failed to serve HTTP server")
		}
		log.Debug().Str("address", httpAddr).Msg("Done Serving HTTP")
	}()
	log.Debug().Str("address", grpcAddr).Msg("Serving gRPC")
	go func() {
		if err := grpcSrv.Serve(grpcsLis); err != nil {
			log.Fatal().Err(err).Msg("failed to serve GRPC server")
		}
		log.Debug().Str("address", grpcAddr).Msg("Done Serving gRPC")
	}()
	log.Debug().Str("address", lokiAddr).Msg("Serving Loki")
	go func() {
		if err := lokiSrv.Serve(lokiLis); err != nil {
			log.Fatal().Err(err).Msg("failed to serve Loki server")
		}
		log.Debug().Str("address", lokiAddr).Msg("Done Serving Loki")
	}()
	go s.lokiHandler.Run(ctx)

	// Wait until context closed
	<-ctx.Done()

	log.Info().Msg("Closing GRPC server")
	httpSrv.Shutdown(context.Background())
	grpcSrv.GracefulStop()
	lokiSrv.Shutdown(context.Background())

	return nil
}

func healthHandler(w http.ResponseWriter, r *http.Request) {
	fmt.Fprintln(w, "OK")
}
