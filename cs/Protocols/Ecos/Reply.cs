namespace BinkyRailways.Protocols.Ecos
{
    public class Reply : Message
    {
        /// <summary>
        /// The original command
        /// </summary>
        public Command Command { get; internal set; }

        /// <summary>
        /// Is this a "no error" reply?
        /// </summary>
        public bool IsSucceeded { get { return (ErrorCode == 0); } }
    }
}
