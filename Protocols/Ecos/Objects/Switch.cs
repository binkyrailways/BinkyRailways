using System.Text;

namespace BinkyRailways.Protocols.Ecos.Objects
{
    /// <summary>
    /// Switch object (id=dynamic)
    /// </summary>
    public class Switch : Object
    {
        private bool hasControl;

        /// <summary>
        /// Default ctor
        /// </summary>
        public Switch(Client client, int id)
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
        /// Gets Address of this switch.
        /// </summary>
        public string GetAddress() { return Get(OptAddress); }

        /// <summary>
        /// Gets Protocol of this switch.
        /// </summary>
        public string GetProtocol() { return Get(OptProtocol); }

        /// <summary>
        /// Gets Symbol of this switch.
        /// </summary>
        public string GetSymbol() { return Get(OptSymbol); }

        /// <summary>
        /// Gets Mode of this switch.
        /// <returns>PULSE or SWITCH</returns>
        /// </summary>
        public string GetMode() { return Get(OptMode); }

        /// <summary>
        /// Gets Duration of this switch.
        /// </summary>
        public string GetDuration() { return Get(OptDuration); }

        /// <summary>
        /// Gets Name1 of this switch.
        /// </summary>
        public string GetName1() { return Get(OptName1); }

        /// <summary>
        /// Gets Name2 of this switch.
        /// </summary>
        public string GetName2() { return Get(OptName2); }

        /// <summary>
        /// Gets Name3 of this switch.
        /// </summary>
        public string GetName3() { return Get(OptName3); }

        /// <summary>
        /// Gets the entire name of this switch.
        /// </summary>
        public string GetFullName()
        {
            var name = new StringBuilder(GetName1());
            var ext = GetName2();
            if (string.IsNullOrEmpty(ext))
                return name.ToString();
            name.Append(' ');
            name.Append(ext);
            ext = GetName3();
            if (string.IsNullOrEmpty(ext))
                return name.ToString();
            name.Append(' ');
            name.Append(ext);
            return name.ToString();
        }
    }
}
