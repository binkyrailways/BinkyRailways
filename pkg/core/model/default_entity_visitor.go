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

// DefaultEntityVisitor is a default implementation of EntityVisitor.
type DefaultEntityVisitor struct {
	defaultVisitor EntityVisitor
}

var _ EntityVisitor = &DefaultEntityVisitor{}

func (v *DefaultEntityVisitor) SetDefaultVisitor(x EntityVisitor) {
	v.defaultVisitor = x
}

func (v *DefaultEntityVisitor) VisitAction(x Action) interface{} {
	return nil
}

func (v *DefaultEntityVisitor) VisitBidibCommandStation(x BidibCommandStation) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitCommandStation(x)
	}
	return nil
}

func (v *DefaultEntityVisitor) VisitBinaryOutput(x BinaryOutput) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitOutput(x)
	}
	return nil
}

func (v *DefaultEntityVisitor) VisitBinaryOutputWithState(x BinaryOutputWithState) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitOutputWithState(x)
	}
	return nil
}

func (v *DefaultEntityVisitor) VisitBinkyNetCommandStation(x BinkyNetCommandStation) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitCommandStation(x)
	}
	return nil
}

func (v *DefaultEntityVisitor) VisitBinarySensor(x BinarySensor) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitSensor(x)
	}
	return nil
}

func (v *DefaultEntityVisitor) VisitBlock(x Block) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitEndPoint(x)
	}
	return nil
}

func (v *DefaultEntityVisitor) VisitBlockGroup(x BlockGroup) interface{} {
	return nil
}

func (v *DefaultEntityVisitor) VisitBlockSignal(x BlockSignal) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitSignal(x)
	}
	return nil
}

func (v *DefaultEntityVisitor) VisitClock4StageOutput(x Clock4StageOutput) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitOutput(x)
	}
	return nil

}

func (v *DefaultEntityVisitor) VisitCommandStation(x CommandStation) interface{} {
	return nil
}

func (v *DefaultEntityVisitor) VisitCommandStationRef(x CommandStationRef) interface{} {
	return nil
}

func (v *DefaultEntityVisitor) VisitDccOverRs232CommandStation(x DccOverRs232CommandStation) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitCommandStation(x)
	}
	return nil
}

func (v *DefaultEntityVisitor) VisitEcosCommandStation(x EcosCommandStation) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitCommandStation(x)
	}
	return nil
}

func (v *DefaultEntityVisitor) VisitEdge(x Edge) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitEndPoint(x)
	}
	return nil
}

func (v *DefaultEntityVisitor) VisitEndPoint(x EndPoint) interface{} {
	return nil
}

func (v *DefaultEntityVisitor) VisitLoc(x Loc) interface{} {
	return nil
}

func (v *DefaultEntityVisitor) VisitLocFunction(x LocFunction) interface{} {
	return nil
}

func (v *DefaultEntityVisitor) VisitLocGroup(x LocGroup) interface{} {
	return nil
}

func (v *DefaultEntityVisitor) VisitLocRef(x LocRef) interface{} {
	return nil
}

func (v *DefaultEntityVisitor) VisitLocoBufferCommandStation(x LocoBufferCommandStation) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitCommandStation(x)
	}
	return nil
}

func (v *DefaultEntityVisitor) VisitModule(x Module) interface{} {
	return nil
}

func (v *DefaultEntityVisitor) VisitModuleConnection(x ModuleConnection) interface{} {
	return nil
}

func (v *DefaultEntityVisitor) VisitModuleRef(x ModuleRef) interface{} {
	return nil
}

func (v *DefaultEntityVisitor) VisitMqttCommandStation(x MqttCommandStation) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitCommandStation(x)
	}
	return nil
}

func (v *DefaultEntityVisitor) VisitOutput(x Output) interface{} {
	return nil
}

func (v *DefaultEntityVisitor) VisitOutputWithState(x OutputWithState) interface{} {
	return nil
}

func (v *DefaultEntityVisitor) VisitP50xCommandStation(x P50xCommandStation) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitCommandStation(x)
	}
	return nil
}

func (v *DefaultEntityVisitor) VisitRailway(x Railway) interface{} {
	return nil
}

func (v *DefaultEntityVisitor) VisitRoute(x Route) interface{} {
	return nil
}

func (v *DefaultEntityVisitor) VisitRouteEvent(x RouteEvent) interface{} {
	return nil
}

func (v *DefaultEntityVisitor) VisitRouteEventBehavior(x RouteEventBehavior) interface{} {
	return nil
}

func (v *DefaultEntityVisitor) VisitSensor(x Sensor) interface{} {
	return nil
}

func (v *DefaultEntityVisitor) VisitSignal(x Signal) interface{} {
	return nil
}

func (v *DefaultEntityVisitor) VisitVirtualCommandStation(x VirtualCommandStation) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitCommandStation(x)
	}
	return nil
}

// Junctions
func (v *DefaultEntityVisitor) VisitJunction(x Junction) interface{} {
	return nil
}
func (v *DefaultEntityVisitor) VisitJunctionWithState(x JunctionWithState) interface{} {
	return nil
}
func (v *DefaultEntityVisitor) VisitPassiveJunction(x PassiveJunction) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitJunction(x)
	}
	return nil
}

func (v *DefaultEntityVisitor) VisitPassiveJunctionWithState(x PassiveJunctionWithState) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitJunctionWithState(x)
	}
	return nil
}

func (v *DefaultEntityVisitor) VisitSwitch(x Switch) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitJunction(x)
	}
	return nil
}

func (v *DefaultEntityVisitor) VisitSwitchWithState(x SwitchWithState) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitJunctionWithState(x)
	}
	return nil
}

func (v *DefaultEntityVisitor) VisitTurnTable(x TurnTable) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitJunction(x)
	}
	return nil
}

func (v *DefaultEntityVisitor) VisitTurnTableWithState(x TurnTableWithState) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitJunctionWithState(x)
	}
	return nil
}

// Predicate
func (v *DefaultEntityVisitor) VisitLocPredicate(x LocPredicate) interface{} {
	return nil
}

func (v *DefaultEntityVisitor) VisitLocAndPredicate(x LocAndPredicate) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitLocPredicate(x)
	}
	return nil
}

func (v *DefaultEntityVisitor) VisitLocOrPredicate(x LocOrPredicate) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitLocPredicate(x)
	}
	return nil
}

func (v *DefaultEntityVisitor) VisitLocCanChangeDirectionPredicate(x LocCanChangeDirectionPredicate) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitLocPredicate(x)
	}
	return nil
}

func (v *DefaultEntityVisitor) VisitLocEqualsPredicate(x LocEqualsPredicate) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitLocPredicate(x)
	}
	return nil
}

func (v *DefaultEntityVisitor) VisitLocGroupEqualsPredicate(x LocGroupEqualsPredicate) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitLocPredicate(x)
	}
	return nil
}

func (v *DefaultEntityVisitor) VisitLocStandardPredicate(x LocStandardPredicate) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitLocPredicate(x)
	}
	return nil
}

//func (v*DefaultEntityVisitor) VisitLocTimePredicate(x LocTimePredicate) interface{}

// Actions
func (v *DefaultEntityVisitor) VisitInitializeJunctionAction(x InitializeJunctionAction) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitAction(x)
	}
	return nil
}

func (v *DefaultEntityVisitor) VisitLocAction(x LocAction) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitAction(x)
	}
	return nil
}

func (v *DefaultEntityVisitor) VisitLocFunctionAction(x LocFunctionAction) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitAction(x)
	}
	return nil
}

func (v *DefaultEntityVisitor) VisitModuleAction(x ModuleAction) interface{} {
	if v.defaultVisitor != nil {
		return v.defaultVisitor.VisitAction(x)
	}
	return nil
}

//func (v*DefaultEntityVisitor) VisitPlaySoundAction(x PlaySoundAction) interface{}

// BinkyNet
func (v *DefaultEntityVisitor) VisitBinkyNetDevice(BinkyNetDevice) interface{} {
	return nil
}
func (v *DefaultEntityVisitor) VisitBinkyNetLocalWorker(BinkyNetLocalWorker) interface{} {
	return nil
}
func (v *DefaultEntityVisitor) VisitBinkyNetObject(BinkyNetObject) interface{} {
	return nil
}
