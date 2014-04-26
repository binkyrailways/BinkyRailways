using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// Internal state info.
    /// </summary>
    internal interface IInternalEntityState<T> : IEntityState <T>
        where T : IEntity
    {
        /// <summary>
        /// Gets the underlying entity.
        /// </summary>
        T Entity { get; }

        /// <summary>
        /// Called when an entity in the railway model has changed.
        /// </summary>
        void OnModelChanged();
    }
}
