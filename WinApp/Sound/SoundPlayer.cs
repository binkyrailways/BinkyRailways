using System.IO;
using BinkyRailways.Core.State;

namespace BinkyRailways.WinApp.Sound
{
    internal sealed class SoundPlayer : ISoundPlayer
    {
        /// <summary>
        /// Create a playable sound from the given stream.
        /// </summary>
        ISound ISoundPlayer.Create(Stream soundData)
        {
            return new Sound(soundData);
        }
    }
}
