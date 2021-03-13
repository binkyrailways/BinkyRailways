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

package editors

import (
	"context"
	"fmt"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// createOnDelete an onDelete helper for the given entity
func createOnDelete(etx EditorContext, entity interface{}) func(context.Context) error {
	if entity == nil {
		return nil
	}
	return func(ctx context.Context) error {
		switch entity := entity.(type) {
		case model.BinkyNetDevice:
			lw := entity.GetLocalWorker()
			if !lw.GetDevices().Remove(entity) {
				return fmt.Errorf("Failed to remove device")
			}
		case model.BinkyNetObject:
			lw := entity.GetLocalWorker()
			if !lw.GetObjects().Remove(entity) {
				return fmt.Errorf("Failed to remove object")
			}
		}
		return nil
	}
}
