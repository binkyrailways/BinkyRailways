// Copyright 2023 Ewout Prangsma
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

package metrics

import "github.com/prometheus/client_golang/prometheus"

const (
	namespace = "binkyrailways"
)

// MustRegisterCounter creates and registers a counter with given subSystem & name.
func MustRegisterCounter(subSubsystem, name, help string) prometheus.Counter {
	c := prometheus.NewCounter(prometheus.CounterOpts{
		Namespace: namespace,
		Subsystem: subSubsystem,
		Name:      name,
		Help:      help,
	})
	prometheus.MustRegister(c)
	return c
}

// MustRegisterGauge creates and registers a gauge with given subSystem & name.
func MustRegisterGauge(subSubsystem, name, help string) prometheus.Gauge {
	c := prometheus.NewGauge(prometheus.GaugeOpts{
		Namespace: namespace,
		Subsystem: subSubsystem,
		Name:      name,
		Help:      help,
	})
	prometheus.MustRegister(c)
	return c
}
