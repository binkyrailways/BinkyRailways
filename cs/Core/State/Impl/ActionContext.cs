namespace BinkyRailways.Core.State.Impl
{
    internal sealed class ActionContext : IActionContext
    {
        private readonly ILocState loc;

        /// <summary>
        /// Default ctor
        /// </summary>
        public ActionContext(ILocState loc)
        {
            this.loc = loc;
        }

        /// <summary>
        /// Gets the current loc.
        /// </summary>
        public ILocState Loc { get { return loc; } }
    }
}
