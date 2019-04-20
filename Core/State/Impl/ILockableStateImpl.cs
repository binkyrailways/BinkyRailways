using System.Collections.Generic;

namespace BinkyRailways.Core.State.Impl
{
    public interface ILockableStateImpl : ILockableState
    {
        /// <summary>
        /// Gets all entities that must be locked in order to lock me.
        /// </summary>
        IEnumerable<ILockableStateImpl> UnderlyingLockableEntities { get; }
    }
}
