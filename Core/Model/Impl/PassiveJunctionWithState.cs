namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Switch reference with intended state.
    /// </summary>
    public sealed class PassiveJunctionWithState : JunctionWithState, IPassiveJunctionWithState
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public PassiveJunctionWithState()
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public PassiveJunctionWithState(PassiveJunction junction)
        {
            Junction = junction;
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
            return new PassiveJunctionWithState((PassiveJunction)Junction);
        }
    }
}
