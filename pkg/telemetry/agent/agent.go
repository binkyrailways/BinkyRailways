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
	"time"

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
	for {
		if err := a.registerPrometheusTargets(ctx); err != nil {
			log.Error().Err(err).Msg("failed to collect & register prometheus targets")
		}
		select {
		case <-ctx.Done():
			// Context canceled
			return nil
		case <-time.After(time.Second * 5):
			// Continue
		}
	}
}

// registerPrometheusTargets asks the binky server for all
// known prometheus targets and registers them such that the
// prometheus config file is updated.
func (a *agent) registerPrometheusTargets(ctx context.Context) error {
	// TODO
	return nil
}
