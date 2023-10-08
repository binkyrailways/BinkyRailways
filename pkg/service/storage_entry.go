// Copyright 2022 Ewout Prangsma
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
	"sort"
	"strings"

	api "github.com/binkyrailways/BinkyRailways/pkg/api/v1"
	"github.com/binkyrailways/BinkyRailways/pkg/core/storage"
)

// Create a new railway entry. (entry is not automatically loaded)
func (s *service) CreateRailwayEntry(ctx context.Context, req *api.CreateRailwayEntryRequest) (*api.RailwayEntry, error) {
	// Check request
	log := s.Logger.With().Str("name", req.GetName()).Logger()
	if req.GetName() == "" {
		log.Warn().Msg("CreateRailwayEntry with empty name")
		return nil, fmt.Errorf("missing name")
	}
	// Construct path and check for duplicates
	path := filepath.Join(s.RailwayStoragePath, req.GetName()+railwayExt)
	log = log.With().Str("path", path).Logger()
	if _, err := os.Stat(path); !os.IsNotExist(err) {
		log.Warn().Msg("CreateRailwayEntry with duplicate name")
		return nil, fmt.Errorf("duplicate name")
	}
	// Build package
	pkg := storage.NewPackage(path)
	if err := pkg.Save(); err != nil {
		log.Error().Err(err).Msg("Failed to create railway package")
		return nil, fmt.Errorf("failed to create railway '%s': %w", req.GetName(), err)
	}
	result := api.RailwayEntry{
		Name: req.GetName(),
	}
	return &result, nil
}

// Get a list of all known railways.
func (s *service) GetRailwayEntries(ctx context.Context, req *api.GetRailwayEntriesRequest) (*api.RailwayEntryList, error) {
	// List storage folder
	fEntries, err := os.ReadDir(s.RailwayStoragePath)
	if err != nil {
		return nil, fmt.Errorf("failed to read storage entries: %w", err)
	}
	// Wrap entries in result
	result := api.RailwayEntryList{}
	for _, fEntry := range fEntries {
		if fEntry.IsDir() {
			continue
		}
		name := fEntry.Name()
		if filepath.Ext(name) != railwayExt {
			continue
		}
		name = name[:len(name)-len(railwayExt)]
		result.Items = append(result.Items, &api.RailwayEntry{
			Name: name,
		})
	}
	sort.Slice(result.Items, func(i, j int) bool {
		return strings.ToLower(result.Items[i].Name) < strings.ToLower(result.Items[j].Name)
	})
	return &result, nil
}
