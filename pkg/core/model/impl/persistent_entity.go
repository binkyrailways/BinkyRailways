// Copyright 2020 Ewout Prangsma
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

package impl

import (
	"time"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// PersistentEntity extends the model PersistentEntity interface
// with storage methods.
type PersistentEntity interface {
	model.PersistentEntity
	TypedEntity

	GetPackage() model.Package
	SetPackage(model.Package)
	Upgrade()
}

type persistentEntity struct {
	onModified   func()
	lastModified time.Time
	pkg          model.Package
}

func (pe *persistentEntity) Initialize(onModified func()) {
	pe.onModified = onModified
	pe.lastModified = time.Now().UTC()
}

func (pe *persistentEntity) GetPackage() model.Package {
	return pe.pkg
}
func (pe *persistentEntity) SetPackage(value model.Package) {
	pe.pkg = value
}

// Gets last modification date.
func (pe *persistentEntity) GetLastModified() time.Time {
	return pe.lastModified
}

// Gets last modification date.
func (pe *persistentEntity) SetLastModified(value time.Time) {
	pe.lastModified = value
	pe.onModified()
}
