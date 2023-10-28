namespace BinkyRailways.Core.State
{
    public interface IVirtualMode
    {
        /// <summary>
        /// Is virtual mode enabled?
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// Automatically run locs?
        /// </summary>
        bool AutoRun { get; set; }

        /// <summary>
        /// Entity is being clicked on.
        /// </summary>
        void EntityClick(IEntityState entity);
    }
}
