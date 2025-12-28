// Copyright 2025 Ewout Prangsma
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

// AllEntityVisitor is an implementation of EntityVisitor
// that ensures that all entities are visited.
type AllEntityVisitor struct {
	Visitor EntityVisitor
}

var _ EntityVisitor = &AllEntityVisitor{}

func (v *AllEntityVisitor) VisitAction(x Action) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitBidibCommandStation(x BidibCommandStation) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitBinaryOutput(x BinaryOutput) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitBinaryOutputWithState(x BinaryOutputWithState) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitBinkyNetCommandStation(x BinkyNetCommandStation) interface{} {
	x.GetLocalWorkers().ForEach(func(bnlw BinkyNetLocalWorker) {
		bnlw.Accept(v.Visitor)
	})
	return nil
}

func (v *AllEntityVisitor) VisitBinarySensor(x BinarySensor) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitBlock(x Block) interface{} {
	x.GetWaitPermissions().Accept(v.Visitor)
	return nil
}

func (v *AllEntityVisitor) VisitBlockGroup(x BlockGroup) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitBlockSignal(x BlockSignal) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitClock4StageOutput(x Clock4StageOutput) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitCommandStation(x CommandStation) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitCommandStationRef(x CommandStationRef) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitDccOverRs232CommandStation(x DccOverRs232CommandStation) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitEcosCommandStation(x EcosCommandStation) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitEdge(x Edge) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitEndPoint(x EndPoint) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitLoc(x Loc) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitLocFunction(x LocFunction) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitLocGroup(x LocGroup) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitLocRef(x LocRef) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitLocoBufferCommandStation(x LocoBufferCommandStation) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitModule(x Module) interface{} {
	x.GetBlocks().ForEach(func(b Block) { b.Accept(v.Visitor) })
	x.GetBlockGroups().ForEach(func(bg BlockGroup) { bg.Accept(v.Visitor) })
	x.GetEdges().ForEach(func(e Edge) { e.Accept(v.Visitor) })
	x.GetJunctions().ForEach(func(j Junction) { j.Accept(v.Visitor) })
	x.GetSensors().ForEach(func(s Sensor) { s.Accept(v.Visitor) })
	x.GetSignals().ForEach(func(s Signal) { s.Accept(v.Visitor) })
	x.GetOutputs().ForEach(func(o Output) { o.Accept(v.Visitor) })
	x.GetRoutes().ForEach(func(r Route) { r.Accept(v.Visitor) })
	return nil
}

func (v *AllEntityVisitor) VisitModuleConnection(x ModuleConnection) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitModuleRef(x ModuleRef) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitMqttCommandStation(x MqttCommandStation) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitOutput(x Output) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitOutputWithState(x OutputWithState) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitP50xCommandStation(x P50xCommandStation) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitRailway(x Railway) interface{} {
	x.GetCommandStations().ForEach(func(csr CommandStationRef) {
		if cs, err := csr.TryResolve(); err == nil {
			cs.Accept(v.Visitor)
		}
	})
	x.GetLocs().ForEach(func(lr LocRef) {
		if loc, err := lr.TryResolve(); err == nil {
			loc.Accept(v.Visitor)
		}
	})
	x.GetLocGroups().ForEach(func(lg LocGroup) { lg.Accept(v.Visitor) })
	x.GetModules().ForEach(func(mr ModuleRef) {
		if mod, err := mr.TryResolve(); err == nil {
			mod.Accept(v.Visitor)
		}
	})
	return nil
}

func (v *AllEntityVisitor) VisitRoute(x Route) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitRouteEvent(x RouteEvent) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitRouteEventBehavior(x RouteEventBehavior) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitSensor(x Sensor) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitSignal(x Signal) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitVirtualCommandStation(x VirtualCommandStation) interface{} {
	return nil
}

// Junctions
func (v *AllEntityVisitor) VisitJunction(x Junction) interface{} {
	return nil
}
func (v *AllEntityVisitor) VisitJunctionWithState(x JunctionWithState) interface{} {
	return nil
}
func (v *AllEntityVisitor) VisitPassiveJunction(x PassiveJunction) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitPassiveJunctionWithState(x PassiveJunctionWithState) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitSwitch(x Switch) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitSwitchWithState(x SwitchWithState) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitTurnTable(x TurnTable) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitTurnTableWithState(x TurnTableWithState) interface{} {
	return nil
}

// Predicate
func (v *AllEntityVisitor) VisitLocPredicate(x LocPredicate) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitLocAndPredicate(x LocAndPredicate) interface{} {
	x.GetPredicates().ForEach(func(lp LocPredicate) { lp.Accept(v.Visitor) })
	return nil
}

func (v *AllEntityVisitor) VisitLocOrPredicate(x LocOrPredicate) interface{} {
	x.GetPredicates().ForEach(func(lp LocPredicate) { lp.Accept(v.Visitor) })
	return nil
}

func (v *AllEntityVisitor) VisitLocCanChangeDirectionPredicate(x LocCanChangeDirectionPredicate) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitLocEqualsPredicate(x LocEqualsPredicate) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitLocGroupEqualsPredicate(x LocGroupEqualsPredicate) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitLocStandardPredicate(x LocStandardPredicate) interface{} {
	x.GetIncludes().Accept(v.Visitor)
	x.GetExcludes().Accept(v.Visitor)
	return nil
}

//func (v*AllEntityVisitor) VisitLocTimePredicate(x LocTimePredicate) interface{}

// Actions
func (v *AllEntityVisitor) VisitInitializeJunctionAction(x InitializeJunctionAction) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitLocAction(x LocAction) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitLocFunctionAction(x LocFunctionAction) interface{} {
	return nil
}

func (v *AllEntityVisitor) VisitModuleAction(x ModuleAction) interface{} {
	return nil
}

//func (v*AllEntityVisitor) VisitPlaySoundAction(x PlaySoundAction) interface{}

// BinkyNet
func (v *AllEntityVisitor) VisitBinkyNetDevice(BinkyNetDevice) interface{} {
	return nil
}
func (v *AllEntityVisitor) VisitBinkyNetLocalWorker(x BinkyNetLocalWorker) interface{} {
	for r := range x.GetRouters().All() {
		r.Accept(v.Visitor)
	}
	for d := range x.GetDevices().All() {
		d.Accept(v.Visitor)
	}
	for o := range x.GetObjects().All() {
		o.Accept(v.Visitor)
	}
	return nil
}
func (v *AllEntityVisitor) VisitBinkyNetObject(BinkyNetObject) interface{} {
	return nil
}
func (v *AllEntityVisitor) VisitBinkyNetRouter(BinkyNetRouter) interface{} {
	return nil
}
