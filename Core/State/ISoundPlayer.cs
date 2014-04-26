using System.IO;

namespace BinkyRailways.Core.State
{
    public interface ISoundPlayer
    {
        /// <summary>
        /// Create a playable sound from the given stream.
        /// </summary>
        ISound Create(Stream soundData);
    }
}
