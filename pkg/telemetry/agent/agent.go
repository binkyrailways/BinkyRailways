// Copyright 2024 Ewout Prangsma
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

package agent

import (
	"context"
	"strings"

	api "github.com/binkynet/BinkyNet/apis/v1"
	"github.com/binkynet/BinkyNet/discovery"
	"github.com/binkyrailways/BinkyRailways/pkg/metrics/promconfig"
	"github.com/rs/zerolog"
)

type Agent interface {
	// Run the agent until the given context is canceled.
	Run(context.Context) error
}

// New constructs a new telemetry agent.
func New(log zerolog.Logger, pcb *promconfig.PrometheusConfigBuilder) (Agent, error) {
	return &agent{
		log: log,
		pcb: pcb,
	}, nil
}

type agent struct {
	log zerolog.Logger
	pcb *promconfig.PrometheusConfigBuilder
}

// Run the agent until the given context is canceled.
func (a *agent) Run(ctx context.Context) error {
	log := a.log
	l := discovery.NewServiceListener(log, api.ServiceTypePrometheusProvider, true, func(info api.ServiceInfo) {
		name := createName(info)
		log.Info().
			Str("server", info.GetApiAddress()).
			Int32("port", info.GetApiPort()).
			Bool("secure", info.GetSecure()).
			Str("name", name).
			Msg("New Prometheus metrics provider detected")

		a.pcb.RegisterTarget(name, info.GetApiAddress(), int(info.GetApiPort()), info.GetSecure())
	})
	return l.Run(ctx)
}

func createName(info api.ServiceInfo) string {
	if info.ProviderName != "" {
		return strings.ToLower(info.ProviderName)
	}
	return strings.Replace(info.GetApiAddress(), ".", "_", -1)
}
