namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// TurnTable reference with intended state.
    /// </summary>
    public sealed class TurnTableWithState : JunctionWithState, ITurnTableWithState
    {
        private readonly Property<int> position;

        /// <summary>
        /// Default ctor
        /// </summary>
        public TurnTableWithState()
        {
            position = new Property<int>(this, DefaultValues.DefaultTurnTableInitialPosition);
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public TurnTableWithState(TurnTable junction, int position)
            : this()
        {
            Junction = junction;
            Position = position;
        }

        /// <summary>
        /// Desired direction
        /// </summary>
        public int Position
        {
            get { return position.Value; }
            set { position.Value = value; }
        }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

        /// <summary>
        /// Create a clone of this entity.
        /// Do not clone the junction.
        /// </summary>
        public override IJunctionWithState Clone()
        {
            return new TurnTableWithState((TurnTable)Junction, Position);
        }
    }
}
