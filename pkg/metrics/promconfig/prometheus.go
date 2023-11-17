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

package promconfig

import (
	"bytes"
	"fmt"
	"io/ioutil"
	"net/http"
	"strconv"
	"strings"
	"sync"

	"github.com/rs/zerolog"
)

// scrapeTarget holds config per scrape target
type scrapeTarget struct {
	// Target name
	Name string
	// Hostname or IP address to scrape
	Address string
	// Metrics port
	Port int
	// HTTPS (true) or HTTP (false)
	Secure bool
}

// PrometheusConfigBuilder service is responsible for building
// a prometheus config file.
type PrometheusConfigBuilder struct {
	log           zerolog.Logger
	path          string
	promURL       string
	scrapeTargets struct {
		mutex   sync.Mutex
		targets []scrapeTarget
	}
}

// NewPrometheusConfigBuilder constructs a new prometheus config builder
func NewPrometheusConfigBuilder(log zerolog.Logger, path, promURL string) (*PrometheusConfigBuilder, error) {
	pcb := &PrometheusConfigBuilder{
		log:     log.With().Str("path", path).Logger(),
		path:    path,
		promURL: promURL,
	}
	if err := pcb.rebuildConfig(); err != nil {
		return nil, err
	}
	return pcb, nil
}

// RegisterTarget registers a scrape target and rebuilds the prometheus config
// if needed.
func (pcb *PrometheusConfigBuilder) RegisterTarget(name, address string, port int, secure bool) {
	if address == "" || port == 0 {
		// Ignore invalid input
		return
	}

	pcb.scrapeTargets.mutex.Lock()
	defer pcb.scrapeTargets.mutex.Unlock()

	// Does target exist?
	found := false
	for i, target := range pcb.scrapeTargets.targets {
		if target.Name == name {
			// Target found, update address, port & secure (if needed)
			if target.Address == address && target.Port == port && target.Secure == secure {
				// No change
				return
			}
			pcb.scrapeTargets.targets[i].Address = address
			pcb.scrapeTargets.targets[i].Port = port
			pcb.scrapeTargets.targets[i].Secure = secure
			found = true
			break
		}
	}
	if !found {
		// Add scrape target
		pcb.scrapeTargets.targets = append(pcb.scrapeTargets.targets, scrapeTarget{
			Name:    name,
			Address: address,
			Port:    port,
			Secure:  secure,
		})
	}

	// Trigger rebuild
	if err := pcb.rebuildConfig(); err != nil {
		pcb.log.Warn().Err(err).Msg("Failed to rebuild prometheus config")
	}
}

// rebuildConfig generates a new prometheus config file
func (pcb *PrometheusConfigBuilder) rebuildConfig() error {
	buf := bytes.Buffer{}

	// Prepare config
	buf.WriteString("global:\n")
	buf.WriteString("  scrape_interval: 10s\n")
	buf.WriteString("scrape_configs:\n")
	for _, target := range pcb.scrapeTargets.targets {
		scheme := "http"
		if target.Secure {
			scheme = "https"
		}
		buf.WriteString("- job_name: " + target.Name + "\n")
		buf.WriteString("  scheme: " + scheme + "\n")
		buf.WriteString("  static_configs:\n")
		buf.WriteString("  - targets:\n")
		buf.WriteString("    - " + target.Address + ":" + strconv.Itoa(target.Port) + "\n")
	}

	// Write config to file
	if err := ioutil.WriteFile(pcb.path, buf.Bytes(), 0644); err != nil {
		return fmt.Errorf("failed to write %s: %w", pcb.path, err)
	}

	// Trigger a reload
	go func() {
		if err := pcb.triggerReloadConfig(); err != nil {
			pcb.log.Warn().Err(err).Msg("Failed to reload prometheus config")
		}
	}()

	return nil
}

// triggerReloadConfig invokes a reload of prometheus
func (pcb *PrometheusConfigBuilder) triggerReloadConfig() error {
	if pcb.promURL == "" {
		// Nothing to reload
		return nil
	}
	url := strings.TrimSuffix(pcb.promURL, "/") + "/-/reload"
	_, err := http.DefaultClient.Post(url, "text/plain", strings.NewReader(""))
	return err
}
