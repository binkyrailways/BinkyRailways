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

package service

import (
	"context"
	"fmt"
	"os"
	"path/filepath"
	"strings"

	api "github.com/binkyrailways/BinkyRailways/pkg/api/v1"
)

// Return a list of available serial ports
func (s *service) GetSerialPorts(ctx context.Context, req *api.Empty) (*api.SerialPortList, error) {
	entries, err := os.ReadDir("/dev")
	if err != nil {
		return nil, fmt.Errorf("failed to read dev dir: %w", err)
	}
	var ports []string
	for _, entry := range entries {
		if entry.IsDir() {
			continue
		}
		if strings.HasPrefix(entry.Name(), "tty.") {
			ports = append(ports, filepath.Join("/dev", entry.Name()))
		}
	}
	return &api.SerialPortList{
		Ports: ports,
	}, nil
}
