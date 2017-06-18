using System;

namespace LocoNetToolBox.Model
{
    public class LocoIOEventArgs : EventArgs
    {
        private readonly LocoIO locoio;

        public LocoIOEventArgs(LocoIO locoio)
        {
            this.locoio = locoio;
        }

        public LocoIO LocoIO
        {
            get { return locoio; }
        }
    }
}