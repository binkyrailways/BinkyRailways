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

package v1

import (
	context "context"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// FromModel converts a model bidib command station to an API bidib command station
func (dst *BidibCommandStation) FromModel(ctx context.Context, src model.BidibCommandStation) error {
	//	dst.ServerHost = src.GetServerHost()
	return nil
}

// ToModel converts an API bidib command station to a model bidib command station
func (src *BidibCommandStation) ToModel(ctx context.Context, dst model.BidibCommandStation) error {
	var err error
	//multierr.AppendInto(&err, dst.SetServerHost(src.GetServerHost()))
	return err
}
