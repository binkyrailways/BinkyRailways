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

const (
	TypeLoc     = "Loc"
	TypeModule  = "Module"
	TypeRailway = "Railway"

	TypeBinkyNetCommandStation     = "BinkyNetCommandStation"
	TypeLocoBufferCommandStation   = "LocoBufferCommandStation"
	TypeDccOverRs232CommandStation = "DccOverRs232CommandStation"
	TypeEcosCommandStation         = "EcosCommandStation"
	TypeMqttCommandStation         = "MqttCommandStation"
	TypeP50xCommandStation         = "P50xCommandStation"

	TypeBlockSignal = "BlockSignal"

	TypeSwitch          = "Switch"
	TypePassiveJunction = "PassiveJunction"
	TypeTurnTable       = "TurnTable"

	TypeSwitchWithState          = "SwitchWithState"
	TypePassiveJunctionWithState = "PassiveJunctionWithState"
	TypeTurnTableWithState       = "TurnTableWithState"

	TypeBinaryOutput      = "BinaryOutput"
	TypeClock4StageOutput = "Clock4StageOutput"

	TypeBinarySensor = "BinarySensor"

	TypeLocAndPredicate                = "LocAndPredicate"
	TypeLocOrPredicate                 = "LocOrPredicate"
	TypeLocCanChangeDirectionPredicate = "LocCanChangeDirectionPredicate"
	TypeLocEqualsPredicate             = "LocEqualsPredicate"
	TypeLocGroupEqualsPredicate        = "LocGroupEqualsPredicate"
	TypeLocStandardPredicate           = "LocStandardPredicate"

	TypeInitializeJunctionAction = "InitializeJunctionAction"
	TypeLocFunctionAction        = "LocFunctionAction"
)

// TypedEntity is used to return the Type of an entity
type TypedEntity interface {
	GetEntityType() string
}
