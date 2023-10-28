using System.Windows.Forms;
using BinkyRailways.Core.State;

namespace BinkyRailways.WinApp.Forms
{
    /// <summary>
    /// Form used to inspect the railway state.
    /// </summary>
    public partial class StateInspectionForm : AppForm
    {
        private IRailwayState state;

        /// <summary>
        /// Default ctor
        /// </summary>
        public StateInspectionForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets/sets the currently visible state
        /// </summary>
        public IRailwayState State
        {
            get { return state; }
            set
            {
                if (state != value)
                {
                    state = value;
                    grid.SelectedObject = value;
                }
            }
        }
    }
}
