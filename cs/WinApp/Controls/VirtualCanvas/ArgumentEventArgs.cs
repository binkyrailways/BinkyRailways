using System;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas
{
    /// <summary>
    /// Event arguments for zooming the virtual canvas control to a given rectangle.
    /// </summary>
    public class ArgumentEventArgs : EventArgs
    {
        private readonly object argument;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="argument"></param>
        public ArgumentEventArgs(object argument)
        {
            this.argument = argument;
        }

        /// <summary>
        /// Gets custom argument
        /// </summary>
        public object Argument
        {
            get { return argument; }
        }
    }
}
