using System;

namespace LocoNetToolBox.Model
{
    public class StateMessage : EventArgs
    {
        private readonly AddressState state;

        public StateMessage(AddressState state)
        {
            this.state = state;
        }

        public AddressState State
        {
            get { return state; }
        }
    }
}