using System.Collections.Generic;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State of a single sensor.
    /// </summary>
    public interface ISensorState  : IEntityState<ISensor>, IInputState
    {
        /// <summary>
        /// Gets all blocks for which this sensor is either an "entering" or a "reached"
        /// sensor or to which this sensor is attached.
        /// </summary>
        IEnumerable<IBlockState> DestinationBlocks { get; }

        /// <summary>
        /// Shape used to visualize this sensor
        /// </summary>
        Shapes Shape { get; }
    }
}
