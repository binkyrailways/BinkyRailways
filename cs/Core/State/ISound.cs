namespace BinkyRailways.Core.State
{
    /// <summary>
    /// A single sound track.
    /// </summary>
    public interface ISound
    {
        /// <summary>
        /// Play this sound in the given stream.
        /// This method returns immediately.
        /// </summary>
        void Play();
    }
}
