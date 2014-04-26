using System.Drawing;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;

namespace BinkyRailways.WinApp.Items.Run
{
    /// <summary>
    /// Item showing a output state
    /// </summary>
    public sealed class BinaryOutputItem : Edit.BinaryOutputItem
    {
        private readonly IBinaryOutputState state;

        /// <summary>
        /// Default ctor
        /// </summary>
        public BinaryOutputItem(IBinaryOutput entity, IBinaryOutputState state, ItemContext context, bool interactive)
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
    }
}
