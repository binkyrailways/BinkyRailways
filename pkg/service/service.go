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

package service

import (
	"context"
	"fmt"
	"net"
	"path/filepath"
	"strconv"
	"sync"

	api "github.com/binkyrailways/BinkyRailways/pkg/api/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/state"
	"github.com/binkyrailways/BinkyRailways/pkg/core/storage"
	"github.com/dchest/uniuri"
	"github.com/mattn/go-pubsub"
	"github.com/rs/zerolog"
	"google.golang.org/grpc/metadata"
)

// Config for the service
type Config struct {
	// Version of the project
	ProjectVersion string
	// Commit hash of the build of the project
	ProjectBuild string
	// Path to Railway storage folder
	RailwayStoragePath string
	// Port number our HTTP server is using
	HTTPPort int
	// If set, our HTTP server is using TLS
	HTTPSecure bool
	// If set, the web application is served from live filesystem instead of embedding.
	WebDevelopment bool
}

type Dependencies struct {
	Logger zerolog.Logger
}

// New constructs a new service.
func New(cfg Config, deps Dependencies) (*service, error) {
	if cfg.RailwayStoragePath == "" {
		return nil, fmt.Errorf("RailwayStoragePath is empty")
	}
	s := &service{
		Config:       cfg,
		Dependencies: deps,
		stateChanges: pubsub.New(),
		processUUID:  uniuri.New(),
	}

	/*
	   TODO open default railway
	   	if cfg.RailwayPath != "" {
	   		s.openRailway(cfg.RailwayPath)
	   	}*/
	return s, nil
}

// service implements the railway service.
type service struct {
	Config
	Dependencies

	processUUID             string
	mutex                   sync.Mutex
	railway                 model.Railway
	railwayEntryName        string
	railwayState            state.Railway
	cancelEventSubscription context.CancelFunc
	stateChanges            *pubsub.PubSub
}

const (
	railwayExt = ".brw"
)

// Run the service
func (s *service) Run(ctx context.Context) error {
	<-ctx.Done()
	return nil
}

// Open a new railway from the storage folder.
func (s *service) openRailway(name string) error {
	s.Logger.Info().Str("name", name).Msg("Loading railway")
	path := filepath.Join(s.RailwayStoragePath, name+railwayExt)
	pkg, err := storage.NewPackageFromFile(path)
	if err != nil {
		s.Logger.Error().Err(err).Str("path", path).Msg("Failed to load railway")
		return fmt.Errorf("failed to load railway '%s': %w", name, err)
	}
	pkg.OnError().Add(func(i interface{}) {
		s.Logger.Error().Msgf("%v", i)
	})
	s.railway = pkg.GetRailway()
	s.railwayEntryName = name
	return nil
}

// Gets the current railway
func (s *service) getRailway() (model.Railway, error) {
	result := s.railway
	if result == nil {
		return nil, api.NotFound("No railway loaded")
	}
	return result, nil
}

// Gets the current railway state
func (s *service) getRailwayState() (state.Railway, error) {
	result := s.railwayState
	if result == nil {
		return nil, api.PreconditionFailed("Railway not in run mode")
	}
	return result, nil
}

// getHttpHost returns the server:port of the HTTP server this
// process is running.
// The server is derived from the authority found in the GRPC metadata
// of the given context.
func (s *service) getHttpHost(ctx context.Context) (string, error) {
	md, ok := metadata.FromIncomingContext(ctx)
	if !ok {
		panic("No grpc metadata")
		return "", fmt.Errorf("no GRPC metadata found")
	}
	auth := md[":authority"]
	if len(auth) == 0 {
		fmt.Println("No authority")
		return "", fmt.Errorf("no authority found in GRPC metadata")
	}
	host, _, err := net.SplitHostPort(auth[0])
	if err != nil {
		fmt.Printf("Invalid authority: %s\n", auth)
		return "", fmt.Errorf("invalid authority ('%s') found in GRPC metadata", auth)
	}
	return net.JoinHostPort(host, strconv.Itoa(s.HTTPPort)), nil
}
