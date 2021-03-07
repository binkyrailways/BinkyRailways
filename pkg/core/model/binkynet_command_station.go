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

package model

// BinkyNetCommandStation is a BinkyNet type command station.
type BinkyNetCommandStation interface {
	CommandStation

	// Network host address (defaults to 0.0.0.0)
	GetServerHost() string
	SetServerHost(value string) error

	// Network Port of the command station
	GetGRPCPort() int
	SetGRPCPort(value int) error

	// The required version of local workers
	GetRequiredWorkerVersion() string
	SetRequiredWorkerVersion(value string) error

	// Gets the configuration of local workers on the Binky network
	// that this command station is attached to.
	GetLocalWorkers() BinkyNetLocalWorkerSet
}
