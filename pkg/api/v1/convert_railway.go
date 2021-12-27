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
	"fmt"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// FromModel converts a model railway to an API railway
func (dst *Railway) FromModel(ctx context.Context, src model.Railway) error {
	dst.Id = src.GetID()
	dst.Description = src.GetDescription()

	src.GetModules().ForEach(func(mr model.ModuleRef) {
		dst.Modules = append(dst.Modules, &ModuleRef{
			Id: mr.GetID(),
			// TODO position, zoomfactor, locked
		})
	})

	fmt.Println(*dst)

	return nil
}
