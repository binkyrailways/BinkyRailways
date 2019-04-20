namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Start of finish of a route.
    /// </summary>
    public abstract class EndPoint : PositionedModuleEntity, IEndPoint
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        protected EndPoint(int defaultWidth, int defaultHeight)
            : base(defaultWidth, defaultHeight)
        {
        }
    }
}
