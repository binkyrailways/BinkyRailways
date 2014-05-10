using System;
using System.Linq;
using System.Windows.Forms;
using BinkyRailways.Core.State;

namespace BinkyRailways.WinApp.Controls.Run
{
    /// <summary>
    /// Control showing the route options of a loc.
    /// </summary>
    public partial class RouteInspectionControl : UserControl
    {
        private ILocState loc;

        /// <summary>
        /// Default ctor
        /// </summary>
        public RouteInspectionControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets/sets the current loc
        /// </summary>
        public ILocState Loc
        {
            get { return loc; }
            set
            {
                if (loc == value)
                    return;
                if (loc != null)
                {
                    loc.LastRouteOptions.ActualChanged -= LastRouteOptionsOnActualChanged;
                }
                loc = value;
                if (loc != null)
                {
                    loc.LastRouteOptions.ActualChanged += LastRouteOptionsOnActualChanged;                    
                }
                ReloadList();
            }
        }

        /// <summary>
        /// Reload the list view.
        /// </summary>
        private void ReloadList()
        {
            lvRouteOptions.BeginUpdate();
            lvRouteOptions.Items.Clear();
            if (loc != null)
            {
                var options = loc.LastRouteOptions.Actual;
                if (options != null)
                {
                    foreach (var option in options.OrderBy(x => x.Route.Description))
                    {
                        var item = new ListViewItem(option.Route.Description);
                        item.SubItems.Add(option.ReasonDescription);
                        lvRouteOptions.Items.Add(item);
                    }
                }
            }
            lvRouteOptions.EndUpdate();
        }

        /// <summary>
        /// Actual changed event handler
        /// </summary>
        private void LastRouteOptionsOnActualChanged(object sender, EventArgs eventArgs)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(LastRouteOptionsOnActualChanged), sender, eventArgs);
            }
            else
            {
                ReloadList();
            }
        }
    }
}
