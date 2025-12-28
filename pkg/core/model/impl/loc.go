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
	"context"
	"iter"

	"github.com/binkyrailways/BinkyRailways/pkg/core/model"
)

// Loc extends model Loc with implementation methods
type Loc interface {
	Entity
	model.Loc
	PersistentEntity
}

type loc struct {
	entity
	persistentEntity

	Address         model.Address
	SlowSpeed       int
	MediumSpeed     int
	MaximumSpeed    int
	SpeedSteps      int
	ChangeDirection model.ChangeDirection
	Owner           string
	Remarks         string
	VehicleType     model.VehicleType
	Functions       locFunctions `xml:"Functions"`
}

var (
	_ Loc = &loc{}
)

const (
	imageID = "Image"
)

// NewLoc initialize a new loc
func NewLoc(p model.Package) Loc {
	l := &loc{
		SlowSpeed:       model.DefaultLocSlowSpeed,
		MediumSpeed:     model.DefaultLocMediumSpeed,
		MaximumSpeed:    model.DefaultLocMaximumSpeed,
		SpeedSteps:      model.DefaultLocSpeedSteps,
		ChangeDirection: model.DefaultLocChangeDirection,
		Owner:           model.DefaultLocOwner,
		Remarks:         model.DefaultLocRemarks,
		VehicleType:     model.DefaultLocVehicleType,
	}
	l.EnsureID()
	l.SetPackage(p)
	l.SetDescription("New loc")
	l.persistentEntity.Initialize(l.entity.OnModified)
	l.Functions.SetContainer(l)
	return l
}

// GetEntityType returns the type of this entity
func (l *loc) GetEntityType() string {
	return TypeLoc
}

// Accept a visit by the given visitor
func (l *loc) Accept(v model.EntityVisitor) interface{} {
	return v.VisitLoc(l)
}

// ForEachAddress iterates all addresses in this entity and any child entities.
func (l *loc) ForEachAddress(cb func(addr model.Address, onUpdate func(context.Context, model.Address) error)) {
	cb(l.Address, l.SetAddress)
}

// Get the Address of the entity
func (l *loc) GetAddress() model.Address {
	return l.Address
}

// Set the Address of the entity
func (l *loc) SetAddress(ctx context.Context, value model.Address) error {
	if !l.Address.Equals(value) {
		l.Address = value
		l.OnModified()
	}
	return nil
}

// Return a sequence of all (non-empty) addresses configured in this
// entity with the direction their being used.
func (l *loc) AllAddressUsages() iter.Seq[model.AddressUsage] {
	return func(yield func(model.AddressUsage) bool) {
		if !l.Address.IsEmpty() {
			yield(model.AddressUsage{
				Address:   l.Address,
				Direction: model.AddressDirectionOutput,
			})
		}
	}
}

// Get percentage of speed steps for the slowest speed of this loc.
// Value between 1 and 100.
func (l *loc) GetSlowSpeed() int {
	return l.SlowSpeed
}
func (l *loc) SetSlowSpeed(value int) error {
	if l.SlowSpeed != value {
		l.SlowSpeed = value
		l.OnModified()
	}
	return nil
}

// Get percentage of speed steps for the medium speed of this loc.
// Value between 1 and 100.
func (l *loc) GetMediumSpeed() int {
	return l.MediumSpeed
}

// Set percentage of speed steps for the medium speed of this loc.
// Value between 1 and 100.
func (l *loc) SetMediumSpeed(value int) error {
	if l.MediumSpeed != value {
		l.MediumSpeed = value
		l.OnModified()
	}
	return nil
}

// Get percentage of speed steps for the maximum speed of this loc.
// Value between 1 and 100.
func (l *loc) GetMaximumSpeed() int {
	return l.MaximumSpeed
}

// Set percentage of speed steps for the maximum speed of this loc.
// Value between 1 and 100.
func (l *loc) SetMaximumSpeed(value int) error {
	if l.MaximumSpeed != value {
		l.MaximumSpeed = value
		l.OnModified()
	}
	return nil
}

// Gets the number of speed steps supported by this loc.
func (l *loc) GetSpeedSteps() int {
	return l.SpeedSteps
}

// Sets the number of speed steps supported by this loc.
func (l *loc) SetSpeedSteps(value int) error {
	if l.SpeedSteps != value {
		l.SpeedSteps = value
		l.OnModified()
	}
	return nil
}

// Gets the image (content) of this loc
func (l *loc) GetImage() []byte {
	img, _ := l.GetPackage().GetGenericPart(l, imageID)
	return img
}

// Sets the image (content) of this loc
// Image must be png, bmp, gif, jpg, wmf or emf.
func (l *loc) SetImage(value []byte) error {
	if len(value) == 0 {
		return l.GetPackage().RemoveGenericPart(l, imageID)
	} else {
		return l.GetPackage().SetGenericPart(l, imageID, value)
	}
}

// Get if it is allowed for this loc to change direction.
func (l *loc) GetChangeDirection() model.ChangeDirection {
	return l.ChangeDirection
}

// Set if it is allowed for this loc to change direction.
func (l *loc) SetChangeDirection(value model.ChangeDirection) error {
	if l.ChangeDirection != value {
		l.ChangeDirection = value
		l.OnModified()
	}
	return nil
}

// Gets the names of all functions supported by this loc.
func (l *loc) GetFunctions() model.LocFunctions {
	return &l.Functions
}

// Gets the name of the person that owns this loc.
func (l *loc) GetOwner() string {
	return l.Owner
}

// Sets the name of the person that owns this loc.
func (l *loc) SetOwner(value string) error {
	if l.Owner != value {
		l.Owner = value
		l.OnModified()
	}
	return nil
}

// Gets remarks (free text) about this loc.
func (l *loc) GetRemarks() string {
	return l.Remarks
}

// Sets remarks (free text) about this loc.
func (l *loc) SetRemarks(value string) error {
	if l.Remarks != value {
		l.Remarks = value
		l.OnModified()
	}
	return nil
}

// Gets the type of vehicle this loc is.
func (l *loc) GetVehicleType() model.VehicleType {
	return l.VehicleType
}

// Sets the type of vehicle this loc is.
func (l *loc) SetVehicleType(value model.VehicleType) error {
	if l.VehicleType != value {
		l.VehicleType = value
		l.OnModified()
	}
	return nil
}

// Upgrade to latest version
func (l *loc) Upgrade() {
	if l.VehicleType == "" {
		l.VehicleType = model.DefaultLocVehicleType
	}
}

// GetRailway returns the containing railway
func (l *loc) GetRailway() model.Railway {
	if pkg := l.GetPackage(); pkg != nil {
		return pkg.GetRailway()
	}
	return nil
}
