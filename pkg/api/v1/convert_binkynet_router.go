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

package v1

import (
	context "context"

	"go.uber.org/multierr"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
	"github.com/binkyrailways/BinkyRailways/pkg/core/model/validation"
	"github.com/samber/lo"
)

// FromModel converts a model BinkyNetRouter to an API BinkyNetRouter
func (dst *BinkyNetRouter) FromModel(ctx context.Context, src model.BinkyNetRouter) error {
	dst.Id = src.GetID()
	dst.Description = src.GetDescription()
	dst.ValidationFindings = lo.Map(validation.Validate(src), func(item model.Finding, _ int) string {
		return item.GetDescription()
	})
	return nil
}

// ToModel converts an API BinkyNetRouter to a model BinkyNetRouter
func (src *BinkyNetRouter) ToModel(ctx context.Context, dst model.BinkyNetRouter) error {
	if src.GetId() != dst.GetID() {
		return InvalidArgument("Unexpected binkynet router ID: '%s'", src.GetId())
	}
	var err error
	multierr.AppendInto(&err, dst.SetDescription(src.GetDescription()))
	return err
}
