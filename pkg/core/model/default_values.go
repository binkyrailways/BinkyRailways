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

package model

// Default value of <see cref="IPositionedEntity.Rotation"/>
const DefaultRotation = 0

// Default value of <see cref="IPositionedEntity.Locked"/>
const DefaultLocked = false

// Default value of <see cref="IBlock.WaitProbability"/>
const DefaultBlockWaitProbability = 100

// Default value of <see cref="IBlock.MinimumWaitTime"/>
const DefaultBlockMinimumWaitTime = 10

// Default value of <see cref="IBlock.MaximumWaitTime"/>
const DefaultBlockMaximumWaitTime = 30

// Default value of <see cref="IBlock.ReverseSides"/>
const DefaultBlockReverseSides = false

// Default value of <see cref="IBlock.ChangeDirection"/>
const DefaultBlockChangeDirection = ChangeDirectionAvoid

// Default value of <see cref="IBlock.ChangeDirectionReversingLocs"/>
const DefaultBlockChangeDirectionReversingLocs = false

// Default value of <see cref="IBlock.StationMode"/>
const DefaultBlockStationMode = StationModeAuto

// Default value of <see cref="IBlockGroup.MinimumLocsInGroup"/>
const DefaultBlockGroupMinimumLocsInGroup = 0

// Default value of <see cref="IBlockGroup.MinimumLocsOnTrackForMinimumLocsInGroupStart"/>
const DefaultBlockGroupMinimumLocsOnTrackForMinimumLocsInGroupStart = 0

// Default value of <see cref="ILoc.SpeedSteps"/>
const DefaultLocSpeedSteps = 128

// Default value of <see cref="ILoc.SlowSpeed"/>
const DefaultLocSlowSpeed = 10

// Default value of <see cref="ILoc.MediumSpeed"/>
const DefaultLocMediumSpeed = 50

// Default value of <see cref="ILoc.MaximumSpeed"/>
const DefaultLocMaximumSpeed = 100

// Default value of <see cref="ILoc.ChangeDirection"/>
const DefaultLocChangeDirection = ChangeDirectionAvoid

// Default value of <see cref="ILoc.Owner"/>
const DefaultLocOwner = ""

// Default value of <see cref="ILoc.Remarks"/>
const DefaultLocRemarks = ""

// Default value of <see cref="Loc.VehicleType"/>
const DefaultLocVehicleType = VehicleTypeLoc

// Default value of <see cref="IRoute.Speed"/>
const DefaultRouteSpeed = 100

// Default value of <see cref="IRoute.ChooseProbability"/>
const DefaultRouteChooseProbability = 100

// Default value of <see cref="IRoute.MaxDuration"/>
const DefaultRouteMaxDuration = 60

// Default value of <see cref="ITurnTable.InvertPositions"/>
const DefaultTurnTableInvertPositions = false

// Default value of <see cref="ITurnTable.InvertWrite"/>
const DefaultTurnTableInvertWrite = false

// Default value of <see cref="ITurnTable.InvertBusy"/>
const DefaultTurnTableInvertBusy = false

// Default value of <see cref="ITurnTable.FirstPosition"/>
const DefaultTurnTableFirstPosition = 1

// Default value of <see cref="ITurnTable.LastPosition"/>
const DefaultTurnTableLastPosition = 63

// Default value of <see cref="ITurnTable.InitialPosition"/>
const DefaultTurnTableInitialPosition = 1

// Default value of <see cref="IBlockSignal.RedPattern"/>
const DefaultBlockSignalRedPattern = 0x01

// Default value of <see cref="IBlockSignal.GreenPattern"/>
const DefaultBlockSignalGreenPattern = 0x02

// Default value of <see cref="IBlockSignal.YellowPattern"/>
const DefaultBlockSignalYellowPattern = 0x04

// Default value of <see cref="IBlockSignal.WhitePattern"/>
const DefaultBlockSignalWhitePattern = 0x08

// Default value of <see cref="IBlockSignal.Position"/>
const DefaultBlockSignalPosition = BlockSideFront

// Default value of <see cref="IBlockSignal.Type"/>
const DefaultBlockSignalType = BlockSignalTypeEntry

// Default value of <see cref="IClock4StageOutput.MorningPattern"/>
const DefaultClock4StageOutputMorningPattern = 0x00

// Default value of <see cref="IClock4StageOutput.AfternoonPattern"/>
const DefaultClock4StageOutputAfternoonPattern = 0x01

// Default value of <see cref="IClock4StageOutput.EveningPattern"/>
const DefaultClock4StageOutputEveningPattern = 0x02

// Default value of <see cref="IClock4StageOutput.NightPattern"/>
const DefaultClock4StageOutputNightPattern = 0x03

// Default value of <see cref="IModuleRef.ZoomFactor"/>
const DefaultModuleRefZoomFactor = 100

// Default value of <see cref="IRouteEventBehavior.StateBehavior"/>
//const DefaultRouteEventBehaviorStateBehavior = RouteStateBehavior.NoChange

// Default value of <see cref="IRouteEventBehavior.SpeedBehavior"/>

//const DefaultRouteEventBehaviorSpeedBehavior = LocSpeedBehavior.Default

// Default value of <see cref="IRailway.ClockSpeedFactor"/>
const DefaultRailwayClockSpeedFactor = 72

// Default value of <see cref="ISensor.Shape"/>

//const DefaultSensorShape = Shapes.Circle
