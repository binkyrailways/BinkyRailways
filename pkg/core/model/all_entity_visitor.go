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

// allEntityVisitor is an implementation of EntityVisitor
// that ensures that all entities are visited.
type allEntityVisitor struct {
	visited map[any]bool
	Visitor EntityVisitor
}

// Accept a visit by given entity from our visitor.
func (v *allEntityVisitor) visit(e Entity) interface{} {
	if e == nil {
		return nil
	}
	if v.visited[e] {
		return nil
	}
	v.visited[e] = true
	return e.Accept(v.Visitor)
}

// Construct a new "All" EntityVisitor.
func NewAllEntityVisitor(visitor EntityVisitor) EntityVisitor {
	return &allEntityVisitor{
		visited: make(map[any]bool),
		Visitor: visitor,
	}
}

var _ EntityVisitor = &allEntityVisitor{}

func (v *allEntityVisitor) VisitAction(x Action) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitBidibCommandStation(x BidibCommandStation) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitBinaryOutput(x BinaryOutput) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitBinaryOutputWithState(x BinaryOutputWithState) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitBinkyNetCommandStation(x BinkyNetCommandStation) interface{} {
	x.GetLocalWorkers().ForEach(func(bnlw BinkyNetLocalWorker) {
		v.visit(bnlw)
	})
	return nil
}

func (v *allEntityVisitor) VisitBinarySensor(x BinarySensor) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitBlock(x Block) interface{} {
	v.visit(x.GetWaitPermissions())
	return nil
}

func (v *allEntityVisitor) VisitBlockGroup(x BlockGroup) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitBlockSignal(x BlockSignal) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitClock4StageOutput(x Clock4StageOutput) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitCommandStation(x CommandStation) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitCommandStationRef(x CommandStationRef) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitDccOverRs232CommandStation(x DccOverRs232CommandStation) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitEcosCommandStation(x EcosCommandStation) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitEdge(x Edge) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitEndPoint(x EndPoint) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitLoc(x Loc) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitLocFunction(x LocFunction) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitLocGroup(x LocGroup) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitLocRef(x LocRef) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitLocoBufferCommandStation(x LocoBufferCommandStation) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitModule(x Module) interface{} {
	x.GetBlocks().ForEach(func(b Block) { v.visit(b) })
	x.GetBlockGroups().ForEach(func(bg BlockGroup) { v.visit(bg) })
	x.GetEdges().ForEach(func(e Edge) { v.visit(e) })
	x.GetJunctions().ForEach(func(j Junction) { v.visit(j) })
	x.GetSensors().ForEach(func(s Sensor) { v.visit(s) })
	x.GetSignals().ForEach(func(s Signal) { v.visit(s) })
	x.GetOutputs().ForEach(func(o Output) { v.visit(o) })
	x.GetRoutes().ForEach(func(r Route) { v.visit(r) })
	return nil
}

func (v *allEntityVisitor) VisitModuleConnection(x ModuleConnection) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitModuleRef(x ModuleRef) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitMqttCommandStation(x MqttCommandStation) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitOutput(x Output) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitOutputWithState(x OutputWithState) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitP50xCommandStation(x P50xCommandStation) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitRailway(x Railway) interface{} {
	x.GetCommandStations().ForEach(func(csr CommandStationRef) {
		if cs, err := csr.TryResolve(); err == nil {
			v.visit(cs)
		}
	})
	x.GetLocs().ForEach(func(lr LocRef) {
		if loc, err := lr.TryResolve(); err == nil {
			v.visit(loc)
		}
	})
	x.GetLocGroups().ForEach(func(lg LocGroup) { lg.Accept(v.Visitor) })
	x.GetModules().ForEach(func(mr ModuleRef) {
		if mod, err := mr.TryResolve(); err == nil {
			v.visit(mod)
		}
	})
	return nil
}

func (v *allEntityVisitor) VisitRoute(x Route) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitRouteEvent(x RouteEvent) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitRouteEventBehavior(x RouteEventBehavior) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitSensor(x Sensor) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitSignal(x Signal) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitVirtualCommandStation(x VirtualCommandStation) interface{} {
	return nil
}

// Junctions
func (v *allEntityVisitor) VisitJunction(x Junction) interface{} {
	return nil
}
func (v *allEntityVisitor) VisitJunctionWithState(x JunctionWithState) interface{} {
	return nil
}
func (v *allEntityVisitor) VisitPassiveJunction(x PassiveJunction) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitPassiveJunctionWithState(x PassiveJunctionWithState) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitSwitch(x Switch) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitSwitchWithState(x SwitchWithState) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitTurnTable(x TurnTable) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitTurnTableWithState(x TurnTableWithState) interface{} {
	return nil
}

// Predicate
func (v *allEntityVisitor) VisitLocPredicate(x LocPredicate) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitLocAndPredicate(x LocAndPredicate) interface{} {
	x.GetPredicates().ForEach(func(lp LocPredicate) { v.visit(lp) })
	return nil
}

func (v *allEntityVisitor) VisitLocOrPredicate(x LocOrPredicate) interface{} {
	x.GetPredicates().ForEach(func(lp LocPredicate) { v.visit(lp) })
	return nil
}

func (v *allEntityVisitor) VisitLocCanChangeDirectionPredicate(x LocCanChangeDirectionPredicate) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitLocEqualsPredicate(x LocEqualsPredicate) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitLocGroupEqualsPredicate(x LocGroupEqualsPredicate) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitLocStandardPredicate(x LocStandardPredicate) interface{} {
	v.visit(x.GetIncludes())
	v.visit(x.GetExcludes())
	return nil
}

//func (v*allEntityVisitor) VisitLocTimePredicate(x LocTimePredicate) interface{}

// Actions
func (v *allEntityVisitor) VisitInitializeJunctionAction(x InitializeJunctionAction) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitLocAction(x LocAction) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitLocFunctionAction(x LocFunctionAction) interface{} {
	return nil
}

func (v *allEntityVisitor) VisitModuleAction(x ModuleAction) interface{} {
	return nil
}

//func (v*allEntityVisitor) VisitPlaySoundAction(x PlaySoundAction) interface{}

// BinkyNet
func (v *allEntityVisitor) VisitBinkyNetDevice(BinkyNetDevice) interface{} {
	return nil
}
func (v *allEntityVisitor) VisitBinkyNetLocalWorker(x BinkyNetLocalWorker) interface{} {
	for r := range x.GetRouters().All() {
		v.visit(r)
	}
	for d := range x.GetDevices().All() {
		v.visit(d)
	}
	for o := range x.GetObjects().All() {
		v.visit(o)
	}
	return nil
}
func (v *allEntityVisitor) VisitBinkyNetObject(BinkyNetObject) interface{} {
	return nil
}
func (v *allEntityVisitor) VisitBinkyNetRouter(BinkyNetRouter) interface{} {
	return nil
}
