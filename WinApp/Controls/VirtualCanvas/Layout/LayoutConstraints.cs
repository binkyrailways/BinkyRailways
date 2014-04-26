using System;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Layout
{
    public class LayoutConstraints
    {
        private readonly FillDirection fill;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="fill"></param>
        public LayoutConstraints(FillDirection fill)
        {
            this.fill = fill;
        }

        /// <summary>
        /// Is what directions should a placement be filled to the maximum available size.
        /// </summary>
        public FillDirection Fill { get { return fill; } }
    }

    [Flags]
    public enum FillDirection
    {
        None = 0,
        Horizontal = 1,
        Vertical = 2,
        Both = Horizontal | Vertical
    }
}
