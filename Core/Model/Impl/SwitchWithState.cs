namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Switch reference with intended state.
    /// </summary>
    public sealed class SwitchWithState : JunctionWithState, ISwitchWithState
    {
        private readonly Property<SwitchDirection> direction;

        /// <summary>
        /// Default ctor
        /// </summary>
        public SwitchWithState()
        {
            direction = new Property<SwitchDirection>(this, SwitchDirection.Straight);
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public SwitchWithState(Switch junction, SwitchDirection direction)
            : this()
        {
            Junction = junction;
            Direction = direction;
        }

        /// <summary>
        /// Desired direction
        /// </summary>
        public SwitchDirection Direction
        {
            get { return direction.Value; }
            set { direction.Value = value; }
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
            return new SwitchWithState((Switch)Junction, Direction);
        }
    }
}
