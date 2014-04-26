using System.IO;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Action that plays a specific sound.
    /// </summary>
    public interface IPlaySoundAction : IAction
    {
        /// <summary>
        /// Sound track
        /// </summary>
        Stream Sound { get; set; }
    }
}
