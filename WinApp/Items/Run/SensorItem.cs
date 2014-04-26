using System.Drawing;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;

namespace BinkyRailways.WinApp.Items.Run
{
    /// <summary>
    /// Item showing a sensor state
    /// </summary>
    public sealed class SensorItem : Edit.SensorItem
    {
        private readonly ISensorState state;

        /// <summary>
        /// Default ctor
        /// </summary>
        public SensorItem(ISensor entity, ISensorState state, ItemContext context, bool interactive)
            : base(entity, false, context)
        {
            this.state = state;
            if (interactive)
                MouseHandler = new EntityClickHandler(null, state);
        }

        /// <summary>
        /// Gets the color of the background.
        /// </summary>
        protected override Color BackgroundColor
        {
            get { return state.Active.Actual ? Color.Red : Color.Green; }
        }

        /// <summary>
        /// Gets the color of the border.
        /// </summary>
        protected override Color BorderColor
        {
            get { return state.DestinationBlocks.Any() ? base.BorderColor : Color.Orange; }
        }
    }
}
