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

// EntityVisitor builds the visitor pattern.
type EntityVisitor interface {
	VisitAction(Action) interface{}
	VisitBidibNetCommandStation(BidibCommandStation) interface{}
	VisitBinarySensor(BinarySensor) interface{}
	VisitBinkyNetCommandStation(BinkyNetCommandStation) interface{}
	VisitBlock(Block) interface{}
	VisitBlockGroup(BlockGroup) interface{}
	VisitBlockSignal(BlockSignal) interface{}
	VisitCommandStation(CommandStation) interface{}
	VisitCommandStationRef(CommandStationRef) interface{}
	VisitDccOverRs232CommandStation(DccOverRs232CommandStation) interface{}
	VisitEcosCommandStation(EcosCommandStation) interface{}
	VisitEdge(Edge) interface{}
	VisitEndPoint(EndPoint) interface{}
	VisitLoc(Loc) interface{}
	VisitLocFunction(LocFunction) interface{}
	VisitLocGroup(LocGroup) interface{}
	VisitLocRef(LocRef) interface{}
	VisitLocoBufferCommandStation(LocoBufferCommandStation) interface{}
	VisitModule(Module) interface{}
	VisitModuleConnection(ModuleConnection) interface{}
	VisitModuleRef(ModuleRef) interface{}
	VisitMqttCommandStation(MqttCommandStation) interface{}
	VisitP50xCommandStation(P50xCommandStation) interface{}
	VisitRailway(Railway) interface{}
	VisitRoute(Route) interface{}
	VisitRouteEvent(RouteEvent) interface{}
	VisitRouteEventBehavior(RouteEventBehavior) interface{}
	VisitSensor(Sensor) interface{}
	VisitSignal(Signal) interface{}
	VisitVirtualCommandStation(VirtualCommandStation) interface{}

	// Outputs
	VisitOutput(Output) interface{}
	VisitOutputWithState(OutputWithState) interface{}
	VisitBinaryOutput(BinaryOutput) interface{}
	VisitBinaryOutputWithState(BinaryOutputWithState) interface{}
	VisitClock4StageOutput(Clock4StageOutput) interface{}

	// Junctions
	VisitJunction(Junction) interface{}
	VisitJunctionWithState(JunctionWithState) interface{}
	VisitPassiveJunction(PassiveJunction) interface{}
	VisitPassiveJunctionWithState(PassiveJunctionWithState) interface{}
	VisitSwitch(Switch) interface{}
	VisitSwitchWithState(SwitchWithState) interface{}
	VisitTurnTable(TurnTable) interface{}
	VisitTurnTableWithState(TurnTableWithState) interface{}

	// Predicate
	VisitLocPredicate(LocPredicate) interface{}
	VisitLocAndPredicate(LocAndPredicate) interface{}
	VisitLocOrPredicate(LocOrPredicate) interface{}
	VisitLocCanChangeDirectionPredicate(LocCanChangeDirectionPredicate) interface{}
	VisitLocEqualsPredicate(LocEqualsPredicate) interface{}
	VisitLocGroupEqualsPredicate(LocGroupEqualsPredicate) interface{}
	VisitLocStandardPredicate(LocStandardPredicate) interface{}
	//VisitLocTimePredicate(LocTimePredicate) interface{}

	// Actions
	VisitInitializeJunctionAction(InitializeJunctionAction) interface{}
	VisitLocAction(LocAction) interface{}
	VisitLocFunctionAction(LocFunctionAction) interface{}
	VisitModuleAction(ModuleAction) interface{}
	//VisitPlaySoundAction(PlaySoundAction) interface{}

	// BinkyNet
	VisitBinkyNetDevice(BinkyNetDevice) interface{}
	VisitBinkyNetLocalWorker(BinkyNetLocalWorker) interface{}
	VisitBinkyNetObject(BinkyNetObject) interface{}
}
