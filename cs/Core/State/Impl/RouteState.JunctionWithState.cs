using System.Collections.Generic;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl
{
    partial class RouteState
    {
        /// <summary>
        /// State setter builder
        /// </summary>
        private sealed class JunctionWithStateBuilder : EntityVisitor<JunctionWithState, IJunctionState>
        {
            public override JunctionWithState Visit(IPassiveJunctionWithState entity, IJunctionState data)
            {
                return new PassiveJunctionWithState((IPassiveJunctionState) data);
            }
            public override JunctionWithState Visit(ISwitchWithState entity, IJunctionState data)
            {
                return new SwitchWithState((ISwitchState)data, entity.Direction);
            }
            public override JunctionWithState Visit(ITurnTableWithState entity, IJunctionState data)
            {
                return new TurnTableWithState((ITurnTableState)data, entity.Position);
            }
        }

        /// <summary>
        /// State setter
        /// </summary>
        private abstract class JunctionWithState
        {
            /// <summary>
            /// Is the junction in the correct state?
            /// </summary>
            public abstract bool IsPrepared { get; }

            /// <summary>
            /// Set the junction in the correct state
            /// </summary>
            public abstract void Prepare();

            /// <summary>
            /// Does this route contains the given junction
            /// </summary>
            public abstract bool Contains(IJunctionState junctionState);

            /// <summary>
            /// Gets all entities that must be locked in order to lock me.
            /// </summary>
            public abstract IEnumerable<ILockableState> UnderlyingLockableEntities { get; }

            /// <summary>
            /// Is this junction for this state in the non-straight position?
            /// </summary>
            public abstract bool IsNonStraight { get; }
        }

        /// <summary>
        /// State setter
        /// </summary>
        private sealed class PassiveJunctionWithState : JunctionWithState
        {
            private readonly IPassiveJunctionState junction;

            /// <summary>
            /// Default ctor
            /// </summary>
            public PassiveJunctionWithState(IPassiveJunctionState junction)
            {
                this.junction = junction;
            }

            /// <summary>
            /// Is the junction in the correct state?
            /// </summary>
            public override bool IsPrepared { get { return true; } }

            /// <summary>
            /// Set the junction in the correct state
            /// </summary>
            public override void Prepare()
            {
            }

            /// <summary>
            /// Does this route contains the given junction
            /// </summary>
            public override bool Contains(IJunctionState junctionState)
            {
                return (junction == junctionState);
            }

            /// <summary>
            /// Is this junction for this state in the non-straight position?
            /// </summary>
            public override bool IsNonStraight
            {
                get { return false; }
            }

            /// <summary>
            /// Gets all entities that must be locked in order to lock me.
            /// </summary>
            public override IEnumerable<ILockableState> UnderlyingLockableEntities { get { yield return junction; } }
        }

        /// <summary>
        /// State setter
        /// </summary>
        private sealed class SwitchWithState : JunctionWithState
        {
            private readonly ISwitchState @switch;
            private readonly SwitchDirection direction;

            /// <summary>
            /// Default ctor
            /// </summary>
            public SwitchWithState(ISwitchState @switch, SwitchDirection direction)
            {
                this.@switch = @switch;
                this.direction = direction;
            }

            /// <summary>
            /// Is the junction in the correct state?
            /// </summary>
            public override bool IsPrepared { get { return (@switch.Direction.Actual == direction); } }

            /// <summary>
            /// Set the junction in the correct state
            /// </summary>
            public override void Prepare()
            {
                @switch.Direction.Requested = direction;
            }

            /// <summary>
            /// Does this route contains the given junction
            /// </summary>
            public override bool Contains(IJunctionState junctionState)
            {
                return (@switch == junctionState);
            }

            /// <summary>
            /// Is this junction for this state in the non-straight position?
            /// </summary>
            public override bool IsNonStraight
            {
                get { return (direction != SwitchDirection.Straight); }
            }

            /// <summary>
            /// Gets all entities that must be locked in order to lock me.
            /// </summary>
            public override IEnumerable<ILockableState> UnderlyingLockableEntities { get { yield return @switch; } }
        }

        /// <summary>
        /// State setter
        /// </summary>
        private sealed class TurnTableWithState : JunctionWithState
        {
            private readonly ITurnTableState turnTable;
            private readonly int position;

            /// <summary>
            /// Default ctor
            /// </summary>
            public TurnTableWithState(ITurnTableState turnTable, int position)
            {
                this.turnTable = turnTable;
                this.position = position;
            }

            /// <summary>
            /// Is the junction in the correct state?
            /// </summary>
            public override bool IsPrepared { get { return (turnTable.Position.Actual == position); } }

            /// <summary>
            /// Set the junction in the correct state
            /// </summary>
            public override void Prepare()
            {
                turnTable.Position.Requested = position;
            }

            /// <summary>
            /// Does this route contains the given junction
            /// </summary>
            public override bool Contains(IJunctionState junctionState)
            {
                return (turnTable == junctionState);
            }

            /// <summary>
            /// Is this junction for this state in the non-straight position?
            /// </summary>
            public override bool IsNonStraight
            {
                get { return true; }
            }

            /// <summary>
            /// Gets all entities that must be locked in order to lock me.
            /// </summary>
            public override IEnumerable<ILockableState> UnderlyingLockableEntities { get { yield return turnTable; } }
        }
    }
}
