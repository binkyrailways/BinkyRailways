using System;
using System.IO;
using BinkyRailways.Core.Logging;
using NLog;
using media = System.Media;
using BinkyRailways.Core.State;

namespace BinkyRailways.WinApp.Sound
{
    internal sealed class Sound : ISound
    {
        private static readonly Logger log = LogManager.GetLogger(LogNames.Sound);
        private readonly media.SoundPlayer soundPlayer;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal Sound(Stream stream)
        {
            try
            {
                soundPlayer = new media.SoundPlayer(stream);
            }
            catch (Exception ex)
            {
                // Log exceptions
                log.LogException(LogLevel.Warn, "Failed to initialize sound", ex);
            }
        }

        /// <summary>
        /// Play this sound in the given stream.
        /// This method returns immediately.
        /// </summary>
        void ISound.Play()
        {
            if (soundPlayer == null)
                return;
            try
            {
                soundPlayer.Play();   
            }
            catch (Exception ex)
            {
                // Log exceptions
                log.LogException(LogLevel.Warn, "Failed to play sound", ex);
            }
        }
    }
}
