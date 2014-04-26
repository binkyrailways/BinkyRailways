using System.Collections.Generic;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Entity that has one of more actions triggers.
    /// </summary>
    public interface IActionTriggerSource : IEntity
    {
        /// <summary>
        /// Gets all triggers of this entity.
        /// </summary>
        IEnumerable<IActionTrigger> Triggers { get; }
    }
}
