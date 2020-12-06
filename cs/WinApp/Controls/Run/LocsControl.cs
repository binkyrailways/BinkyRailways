using System;
using System.Linq;
using System.Windows.Forms;
using BinkyRailways.Core.State;
using BinkyRailways.Core.Util;

namespace BinkyRailways.WinApp.Controls.Run
{
    public partial class LocsControl : UserControl
    {
        /// <summary>
        /// Fired to show loc properties.
        /// </summary>
        public event EventHandler<ObjectEventArgs<ILocState>> ShowLocProperties
        {
            add { lvLocs.ShowLocProperties += value; }
            remove { lvLocs.ShowLocProperties -= value; }
        }

        private ISound locProblemSound;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocsControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize for the given state
        /// </summary>
        internal void Initialize(AppState appState, IRailwayState railway)
        {
            locProblemSound = appState.SoundPlayer.Create(Strings.wehaveaprob);
            locControlPanel.Loc = null;
            lvLocs.Initialize(appState, railway);
            railwayStateControlPanel.Initialize(railway);
            unexpectedSensorControl.Initialize(appState);
            unexpectedSensorControl.Clear();

            UpdateToolbar();
            UpdateControls();
            updateTimer.Enabled = (railway != null);
        }

        /// <summary>
        /// An unexpected sensor was activated.
        /// Show UI.
        /// </summary>
        internal void OnUnexpectedSensorActivated(UnexpectedSensorActivatedEventArgs e)
        {
            unexpectedSensorControl.OnUnexpectedSensorActivated(e);
            UpdateControls();
        }

        /// <summary>
        /// Update the state of the toolbar
        /// </summary>
        private void UpdateToolbar()
        {
            var selection = SelectedItem;
            var selectedLoc = (selection != null) ? selection.LocState : null;
            if (selectedLoc != null)
            {
                tbRemoveFromTrack.Visible = true;
                tbRemoveFromTrack.Enabled = (selectedLoc.CurrentBlock.Actual != null);
            } 
            else
            {
                // No selection
                tbRemoveFromTrack.Visible = false;
            }
        }

        /// <summary>
        /// Update the state of the controls
        /// </summary>
        private void UpdateControls()
        {
            unexpectedSensorControl.Visible = unexpectedSensorControl.Show;
        }

        /// <summary>
        /// Actual state of a loc has changed.
        /// </summary>
        private void OnLocActualStateChanged(object sender, EventArgs e)
        {
            UpdateToolbar();
            /*if (routeDurationExceededDetected)
            {
                locProblemSound.Play();
            }*/
        }

        /// <summary>
        /// Gets the selected item
        /// </summary>
        private LocListView.LocItem SelectedItem
        {
            get { return lvLocs.SelectedItem; }
        }

        /// <summary>
        /// Selection has changed
        /// </summary>
        private void OnLocsSelectedIndexChanged(object sender, EventArgs e)
        {
            var selection = SelectedItem;
            if ((selection != null) || (sender == null) || !IsHandleCreated)
            {
                UpdateSelection();
            }
            else
            {
                // Call again (with sender null). 
                // If the selection has changed in the mean time, this avoids flickering.
                BeginInvoke(new EventHandler(OnLocsSelectedIndexChanged), null, e);
            }
        }

        /// <summary>
        /// Pass the selected item to the control panel
        /// </summary>
        private void UpdateSelection()
        {
            var selection = SelectedItem;
            // Update controls
            locControlPanel.Loc = (selection != null) ? selection.LocState : null;
            unexpectedSensorControl.Loc = (selection != null) ? selection.LocState : null;
            UpdateControls();
            UpdateToolbar();            
        }

        /// <summary>
        /// Remove loc from current block
        /// </summary>
        private void OnRemoveFromTrackClick(object sender, EventArgs e)
        {
            foreach (var loc in from LocListView.LocItem item in lvLocs.SelectedItems select item.LocState)
            {
                loc.Reset();
            }
        }

        /// <summary>
        /// Update the loc state
        /// </summary>
        private void OnUpdateTimerTick(object sender, EventArgs e)
        {
            unexpectedSensorControl.UpdateSensors();
            UpdateControls();
        }

        /// <summary>
        /// Route duration exceeded, play sound
        /// </summary>
        private void OnRouteDurationExceeded(object sender, EventArgs e)
        {
            locProblemSound.Play();
        }

        public void SelectLoc(ILocState value)
        {
            lvLocs.SelectLoc(value);
        }
    }
}
