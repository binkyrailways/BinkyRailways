using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;

namespace BinkyRailways.WinApp.Items.Run
{
    /// <summary>
    /// Item showing a 4-stage clock output state
    /// </summary>
    public sealed class Clock4StageOutputItem : Edit.Clock4StageOutputItem
    {
        private readonly IClock4StageOutputState state;

        /// <summary>
        /// Default ctor
        /// </summary>
        public Clock4StageOutputItem(IClock4StageOutput entity, IClock4StageOutputState state, ItemContext context, bool interactive)
            : base(entity, false, context)
        {
            this.state = state;
            if (interactive)
                MouseHandler = new EntityClickHandler(null, state);
        }

        protected override string Text
        {
            get { return state.Period.Actual.ToString(); }
        }
    }
}
