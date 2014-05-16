namespace BinkyRailways.Core.Model
{
    public static class DefaultValues
    {
        /// <summary>
        /// Default value of <see cref="IPositionedEntity.Rotation"/>
        /// </summary>
        public const int DefaultRotation = 0;

        /// <summary>
        /// Default value of <see cref="IPositionedEntity.Locked"/>
        /// </summary>
        public const bool DefaultLocked = false;

        /// <summary>
        /// Default value of <see cref="IBlock.WaitProbability"/>
        /// </summary>
        public const int DefaultBlockWaitProbability = 100;

        /// <summary>
        /// Default value of <see cref="IBlock.MinimumWaitTime"/>
        /// </summary>
        public const int DefaultBlockMinimumWaitTime = 10;

        /// <summary>
        /// Default value of <see cref="IBlock.MaximumWaitTime"/>
        /// </summary>
        public const int DefaultBlockMaximumWaitTime = 30;

        /// <summary>
        /// Default value of <see cref="IBlock.ReverseSides"/>
        /// </summary>
        public const bool DefaultBlockReverseSides = false;

        /// <summary>
        /// Default value of <see cref="IBlock.ChangeDirection"/>
        /// </summary>
        public const ChangeDirection DefaultBlockChangeDirection = ChangeDirection.Avoid;

        /// <summary>
        /// Default value of <see cref="IBlock.ChangeDirectionReversingLocs"/>
        /// </summary>
        public const bool DefaultBlockChangeDirectionReversingLocs = false;

        /// <summary>
        /// Default value of <see cref="IBlock.StationMode"/>
        /// </summary>
        public const StationMode DefaultBlockStationMode = StationMode.Auto;

        /// <summary>
        /// Default value of <see cref="IBlockGroup.MinimumLocsInGroup"/>
        /// </summary>
        public const int DefaultBlockGroupMinimumLocsInGroup = 0;

        /// <summary>
        /// Default value of <see cref="IBlockGroup.MinimumLocsOnTrackForMinimumLocsInGroupStart"/>
        /// </summary>
        public const int DefaultBlockGroupMinimumLocsOnTrackForMinimumLocsInGroupStart = 0;

        /// <summary>
        /// Default value of <see cref="ILoc.SpeedSteps"/>
        /// </summary>
        public const int DefaultLocSpeedSteps = 128;

        /// <summary>
        /// Default value of <see cref="ILoc.SlowSpeed"/>
        /// </summary>
        public const int DefaultLocSlowSpeed = 10;

        /// <summary>
        /// Default value of <see cref="ILoc.MediumSpeed"/>
        /// </summary>
        public const int DefaultLocMediumSpeed = 50;

        /// <summary>
        /// Default value of <see cref="ILoc.MaximumSpeed"/>
        /// </summary>
        public const int DefaultLocMaximumSpeed = 100;

        /// <summary>
        /// Default value of <see cref="ILoc.ChangeDirection"/>
        /// </summary>
        public const ChangeDirection DefaultLocChangeDirection = ChangeDirection.Avoid;

        /// <summary>
        /// Default value of <see cref="ILoc.Owner"/>
        /// </summary>
        public const string DefaultLocOwner = "";

        /// <summary>
        /// Default value of <see cref="ILoc.Remarks"/>
        /// </summary>
        public const string DefaultLocRemarks = "";

        /// <summary>
        /// Default value of <see cref="IRoute.Speed"/>
        /// </summary>
        public const int DefaultRouteSpeed = 100;

        /// <summary>
        /// Default value of <see cref="IRoute.ChooseProbability"/>
        /// </summary>
        public const int DefaultRouteChooseProbability = 100;

        /// <summary>
        /// Default value of <see cref="IRoute.MaxDuration"/>
        /// </summary>
        public const int DefaultRouteMaxDuration = 60;

        /// <summary>
        /// Default value of <see cref="ITurnTable.InvertPositions"/>
        /// </summary>
        public const bool DefaultTurnTableInvertPositions = false;

        /// <summary>
        /// Default value of <see cref="ITurnTable.InvertWrite"/>
        /// </summary>
        public const bool DefaultTurnTableInvertWrite = false;

        /// <summary>
        /// Default value of <see cref="ITurnTable.InvertBusy"/>
        /// </summary>
        public const bool DefaultTurnTableInvertBusy = false;

        /// <summary>
        /// Default value of <see cref="ITurnTable.FirstPosition"/>
        /// </summary>
        public const int DefaultTurnTableFirstPosition = 1;

        /// <summary>
        /// Default value of <see cref="ITurnTable.LastPosition"/>
        /// </summary>
        public const int DefaultTurnTableLastPosition = 63;

        /// <summary>
        /// Default value of <see cref="ITurnTable.InitialPosition"/>
        /// </summary>
        public const int DefaultTurnTableInitialPosition = 1;

        /// <summary>
        /// Default value of <see cref="IBlockSignal.RedPattern"/>
        /// </summary>
        public const int DefaultBlockSignalRedPattern = 0x01;

        /// <summary>
        /// Default value of <see cref="IBlockSignal.GreenPattern"/>
        /// </summary>
        public const int DefaultBlockSignalGreenPattern = 0x02;

        /// <summary>
        /// Default value of <see cref="IBlockSignal.YellowPattern"/>
        /// </summary>
        public const int DefaultBlockSignalYellowPattern = 0x04;

        /// <summary>
        /// Default value of <see cref="IBlockSignal.WhitePattern"/>
        /// </summary>
        public const int DefaultBlockSignalWhitePattern = 0x08;

        /// <summary>
        /// Default value of <see cref="IBlockSignal.Position"/>
        /// </summary>
        public const BlockSide DefaultBlockSignalPosition = BlockSide.Front;

        /// <summary>
        /// Default value of <see cref="IBlockSignal.Type"/>
        /// </summary>
        public const BlockSignalType DefaultBlockSignalType = BlockSignalType.Entry;

        /// <summary>
        /// Default value of <see cref="IClock4StageOutput.MorningPattern"/>
        /// </summary>
        public const int DefaultClock4StageOutputMorningPattern = 0x00;

        /// <summary>
        /// Default value of <see cref="IClock4StageOutput.AfternoonPattern"/>
        /// </summary>
        public const int DefaultClock4StageOutputAfternoonPattern = 0x01;

        /// <summary>
        /// Default value of <see cref="IClock4StageOutput.EveningPattern"/>
        /// </summary>
        public const int DefaultClock4StageOutputEveningPattern = 0x02;

        /// <summary>
        /// Default value of <see cref="IClock4StageOutput.NightPattern"/>
        /// </summary>
        public const int DefaultClock4StageOutputNightPattern = 0x03;

        /// <summary>
        /// Default value of <see cref="IModuleRef.ZoomFactor"/>
        /// </summary>
        public const int DefaultModuleRefZoomFactor = 100;

        /// <summary>
        /// Default value of <see cref="IRouteEventBehavior.StateBehavior"/>
        /// </summary>
        public const RouteStateBehavior DefaultRouteEventBehaviorStateBehavior = RouteStateBehavior.NoChange;

        /// <summary>
        /// Default value of <see cref="IRouteEventBehavior.SpeedBehavior"/>
        /// </summary>
        public const LocSpeedBehavior DefaultRouteEventBehaviorSpeedBehavior = LocSpeedBehavior.Default;

        /// <summary>
        /// Default value of <see cref="IRailway.ClockSpeedFactor"/>
        /// </summary>
        public const int DefaultRailwayClockSpeedFactor = 72;

        /// <summary>
        /// Default value of <see cref="ISensor.Shape"/>
        /// </summary>
        public const Shapes DefaultSensorShape = Shapes.Circle;
    }
}
