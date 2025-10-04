// Copyright 2025 Ewout Prangsma
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

	"github.com/rs/zerolog"
	"golang.org/x/sync/errgroup"
	"golang.org/x/sync/semaphore"

	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkynet/BinkyNet/sshlog"
	"github.com/binkynet/LocalWorker/pkg/server"
	"github.com/binkynet/LocalWorker/pkg/service"
	"github.com/binkynet/LocalWorker/pkg/service/bridge"
	"github.com/binkynet/LocalWorker/pkg/ui"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

type binkyNetEsphomeLocalWorker struct {
	log            zerolog.Logger
	bnlw           model.BinkyNetLocalWorker
	lokiLogger     service.LokiLogger
	basePort       int
	serverHost     string
	serverGrpcPort int
}

// Construct a esphome local worker
func newBinkyNetEsphomeLocalWorker(
	log zerolog.Logger,
	bnlw model.BinkyNetLocalWorker,
	lokiLogger service.LokiLogger,
	basePort int,
	serverHost string, serverGrpcPort int) *binkyNetEsphomeLocalWorker {
	return &binkyNetEsphomeLocalWorker{
		log:            log,
		bnlw:           bnlw,
		lokiLogger:     lokiLogger,
		basePort:       basePort,
		serverHost:     serverHost,
		serverGrpcPort: serverGrpcPort,
	}
}

// Run until the given context is canceled
func (vlw *binkyNetEsphomeLocalWorker) Run(ctx context.Context) error {
	// Prepare virtual bridge
	log := vlw.log
	br, err := bridge.NewVirtualBridge()
	if err != nil {
		log.Debug().Err(err).Msg("Failed to create virtual bridge")
		return err
	}

	// Collect router names
	var routerNames []string
	vlw.bnlw.GetRouters().ForEach(func(router model.BinkyNetRouter) {
		routerNames = append(routerNames, router.GetDescription())
	})

	// Prepare service
	svc, err := service.NewService(service.Config{
		HostID:         vlw.bnlw.GetHardwareID(),
		ProgramVersion: "0.0.1",
		MetricsPort:    vlw.basePort + 1,
		GRPCPort:       vlw.basePort + 2,
		SSHPort:        vlw.basePort + 3,
		IsVirtual:      true,
		RouterNames:    routerNames,
		VirtualServiceInfo: &api.ServiceInfo{
			ApiAddress: vlw.serverHost,
			ApiPort:    int32(vlw.serverGrpcPort),
		},
	}, service.Dependencies{
		Logger:     vlw.log,
		Bridge:     br,
		LokiLogger: vlw.lokiLogger,
		NcsSem:     semaphore.NewWeighted(1),
		WorkerSem:  semaphore.NewWeighted(1),
	})
	if err != nil {
		log.Debug().Err(err).Msg("Failed to initialize Service")
		return err
	}

	// Prepare server
	uiProv := ui.NewUIProvider()
	svr, err := server.New(server.Config{
		Host:      "0.0.0.0",
		HTTPPort:  vlw.basePort + 4,
		GRPCPort:  vlw.basePort + 2,
		SSHPort:   vlw.basePort + 3,
		IsVirtual: true,
	},
		vlw.log, sshlog.NewSshLogger(), uiProv, svc)
	if err != nil {
		log.Debug().Err(err).Msg("Failed to initialize Server")
		return err
	}

	// Run service & server
	g, ctx := errgroup.WithContext(ctx)
	g.Go(func() error {
		svc.Run(ctx)
		return nil
	})
	g.Go(func() error {
		return svr.Run(ctx)
	})
	return g.Wait()
}
