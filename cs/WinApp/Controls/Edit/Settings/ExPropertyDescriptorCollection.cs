using System.ComponentModel;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    /// <summary>
    /// Extension of <see cref="PropertyDescriptorCollection"/>
    /// </summary>
    internal class ExPropertyDescriptorCollection : PropertyDescriptorCollection
    {
        private readonly bool inRunningState;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal ExPropertyDescriptorCollection(bool inRunningState)
            : base(new PropertyDescriptor[0])
        {
            this.inRunningState = inRunningState;
        }

        /// <summary>
        /// Is the railway in a running state?
        /// </summary>
        public bool InRunningState
        {
            get { return inRunningState; }
        }
    }
}
