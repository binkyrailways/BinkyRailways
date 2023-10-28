namespace BinkyRailways.Protocols.Ecos.Objects
{
    /// <summary>
    /// Lok object (id=dynamic)
    /// </summary>
    public class Loc : Object
    {
        private bool hasControl;

        /// <summary>
        /// Default ctor
        /// </summary>
        public Loc(Client client, int id)
            : base(client, id)
        {
        }

        /// <summary>
        /// Do we have control over this loc?
        /// </summary>
        public bool HasControl { get { return hasControl; } }

        /// <summary>
        /// Request control of this loc.
        /// </summary>
        public void RequestControl(bool force = false)
        {
            // Send command
            Send(CmdRequest, OptControl, force ? OptForce : null).ContinueWith(t => HandleReplyError(t)).ContinueWith(t => hasControl = true).Wait(Timeout);
        }

        /// <summary>
        /// Release control
        /// </summary>
        public void ReleaseControl()
        {
            // Send command
            hasControl = false;
            Send(CmdRelease, OptControl).ContinueWith(t => HandleReplyError(t)).Wait(Timeout);
        }

        /// <summary>
        /// Gets Address of this loc.
        /// </summary>
        public string GetAddress() { return Get(OptAddress); }

        /// <summary>
        /// Gets Name of this loc.
        /// </summary>
        public string GetName() { return Get(OptName); }

        /// <summary>
        /// Gets Protocol of this loc.
        /// </summary>
        public string GetProtocol() { return Get(OptProtocol); }

        /// <summary>
        /// Set the direction of this loc.
        /// </summary>
        public void SetDirection(bool forward)
        {
            Send(CmdSet, new Option(OptDirection, forward ? "0" : "1")).ContinueWith(t => HandleReplyError(t)).Wait(Timeout);
        }

        /// <summary>
        /// Set the speed (0..127) of this loc.
        /// </summary>
        public void SetSpeed(int value)
        {
            Send(CmdSet, new Option(OptSpeed, value)).ContinueWith(t => HandleReplyError(t)).Wait(Timeout);
        }

        /// <summary>
        /// Set the speed (step) of this loc.
        /// </summary>
        public void SetSpeedStep(int value)
        {
            Send(CmdSet, new Option(OptSpeedStep, value)).ContinueWith(t => HandleReplyError(t)).Wait(Timeout);
        }

        /// <summary>
        /// Set a function on or off.
        /// </summary>
        public void SetFunction(int funcNr, bool value)
        {
            var optVal = string.Format("{0}, {1}", funcNr, value ? 1 : 0);
            Send(CmdSet, new Option(OptFunc, optVal)).ContinueWith(t => HandleReplyError(t)).Wait(Timeout);            
        }
    }
}
