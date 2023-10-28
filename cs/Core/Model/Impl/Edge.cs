namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Connection point (track) of a module.
    /// </summary>
    public sealed class Edge : EndPoint, IEdge
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public Edge()
            : base(8, 8)
        {
        }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

        /// <summary>
        /// Human readable name of this type of entity.
        /// </summary>
        public override string TypeName
        {
            get { return Strings.TypeNameEdge; }
        }
    }
}
