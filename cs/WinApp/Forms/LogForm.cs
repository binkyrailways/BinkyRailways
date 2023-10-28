using BinkyRailways.Core.State;
namespace BinkyRailways.WinApp.Forms
{
    public partial class LogForm : AppForm
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public LogForm()
        {
            InitializeComponent();
        }

        public void Initialize(IRailwayState state)
        {
            logsControl.Initialize(state);
        }
    }
}
