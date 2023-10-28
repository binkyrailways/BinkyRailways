using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State of a single junction.
    /// </summary>
    public interface IJunctionState  : IEntityState<IJunction>, ILockableState
    {
    }
}
