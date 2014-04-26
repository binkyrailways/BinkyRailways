namespace BinkyRailways.Protocols.Ecos.Objects
{
    /// <summary>
    /// ECoS basic object (id=1)
    /// </summary>
    public class Ecos : Object
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public Ecos(Client client)
            : base(client, 1)
        {
        }

        /// <summary>
        /// Send a power off command
        /// </summary>
        public void Stop()
        {
            Send(CmdSet, OptStop).ContinueWith(t => HandleReplyError(t)).Wait(Timeout);
        }

        /// <summary>
        /// Send a power on command
        /// </summary>
        public void Go()
        {
            Send(CmdSet, OptGo).ContinueWith(t => HandleReplyError(t)).Wait(Timeout);
        }
    }
}
