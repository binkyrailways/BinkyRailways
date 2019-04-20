namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// A junction that is not controlled from the software.
    /// </summary>
    public sealed class PassiveJunction : Junction, IPassiveJunction
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public PassiveJunction()
            : base(16, 12)
        {
        }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }
    }
}
