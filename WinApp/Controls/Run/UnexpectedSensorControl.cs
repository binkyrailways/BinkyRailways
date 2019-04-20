using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;

namespace BinkyRailways.WinApp.Controls.Run
{
    public partial class UnexpectedSensorControl : UserControl
    {
        private ILocState loc;
        private EventHandler actualStateChangedHandler;
        private readonly List<ISensorState> unexpectedSensors = new List<ISensorState>();
        private AppState appState;

        /// <summary>
        /// Default ctor
        /// </summary>
        public UnexpectedSensorControl()
        {
            InitializeComponent();
        }

        internal void Initialize(AppState appState)
        {
            this.appState = appState;
        }

        /// <summary>
        /// Set the current loc
        /// </summary>
        internal ILocState Loc
        {
            get { return loc; }
            set
            {
                if (loc != value)
                {
                    if ((loc != null) && (actualStateChangedHandler != null))
                    {
                        loc.ActualStateChanged -= actualStateChangedHandler;
                    }
                    loc = value;
                    actualStateChangedHandler = null;
                    if (loc != null)
                    {
                        actualStateChangedHandler = this.ASynchronize(OnLocActualStateChanged);
                        loc.ActualStateChanged += actualStateChangedHandler;
                        OnLocActualStateChanged(null, null);
                    }
                    UpdateControls();
                }
            }
        }

        /// <summary>
        /// An unexpected sensor was activated.
        /// Show UI.
        /// </summary>
        internal void OnUnexpectedSensorActivated(UnexpectedSensorActivatedEventArgs e)
        {
            unexpectedSensors.Add(e.Value);
            UpdateBlocks();
        }

        /// <summary>
        /// Remove those sensors that are no longer active.
        /// </summary>
        internal void UpdateSensors()
        {
            var removed = unexpectedSensors.RemoveAll(x => !x.Active.Actual);
            if (removed > 0)
            {
                UpdateBlocks();
            }
        }

        /// <summary>
        /// Remove all assignment opportunities
        /// </summary>
        internal void Clear()
        {
            lvOptions.Items.Clear();
            unexpectedSensors.Clear();
        }

        /// <summary>
        /// Should this control be visible?
        /// </summary>
        internal new bool Show
        {
            get { return (lvOptions.Items.Count > 0); }
        }

        /// <summary>
        /// Update the list of blocks.
        /// </summary>
        private void UpdateBlocks()
        {
            var blocks = new List<IBlockState>();
            foreach (var sensor in unexpectedSensors)
            {
                foreach (var iterator in sensor.DestinationBlocks.Where(x => !x.IsLocked()))
                {
                    var block = iterator;
                    if (!blocks.Contains(block))
                    {
                        blocks.Add(block);
                    }
                }
            }

            // Update the block list
            lvOptions.BeginUpdate();
            var toRemove = lvOptions.Items.Cast<BlockItem>().Where(item => !blocks.Contains(item.Block)).ToList();
            foreach (var item in toRemove)
            {
                lvOptions.Items.Remove(item);
            }
            foreach (var iterator in blocks)
            {
                var block = iterator;
                if (!lvOptions.Items.Cast<BlockItem>().Any(x => x.Block == block))
                {
                    lvOptions.Items.Add(new BlockItem(block));
                }
            }
            lvOptions.Sort();
            lvOptions.EndUpdate();

            // What was the last known assigment for the selected loc?
            lvOptions.Select();
            if ((Loc != null) && (Loc.CurrentBlock.Actual == null) && (appState != null))
            {
                IBlockState lastKnownBlock;
                BlockSide lastKnownEnterSide;
                LocDirection direction;
                if (appState.StatePersistence.TryGetLocState(appState.RailwayState, Loc, out lastKnownBlock, out lastKnownEnterSide, out direction))
                {
                    if (lastKnownBlock != null)
                    {
                        var item = lvOptions.Items.Cast<BlockItem>().FirstOrDefault(x => x.Block == lastKnownBlock);
                        if (item != null)
                        {
                            item.Selected = true;
                            if (lastKnownEnterSide == BlockSide.Back)
                            {
                                cmdAssignFront.Select();
                                cmdAssignFront.Focus();
                            }
                            else
                            {
                                cmdAssignBack.Select();
                                cmdAssignBack.Focus();
                            }
                        }
                    }
                }
            }
            if ((lvOptions.SelectedItems.Count == 0) && (lvOptions.Items.Count > 0))
            {
                lvOptions.Items[0].Selected = true;
            }
        }

        /// <summary>
        /// Gets the block which item is selected.
        /// </summary>
        private IBlockState SelectedBlock
        {
            get
            {
                var selection = lvOptions.SelectedItems;
                return (selection.Count > 0) ? ((BlockItem) selection[0]).Block : null;
            }
        }

        /// <summary>
        /// Update the state of the controls
        /// </summary>
        private void UpdateControls()
        {
            var selection = SelectedBlock;
            var canAssign = (selection != null) && (loc != null) && (loc.CurrentBlock.Actual == null);
            cmdAssignFront.Enabled = canAssign;
            cmdAssignBack.Enabled = canAssign;
        }

        /// <summary>
        /// Actual state of this loc has changed
        /// </summary>
        private void OnLocActualStateChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        /// <summary>
        /// Selection has changed
        /// </summary>
        private void lvOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        /// <summary>
        /// Assign the current loc to the selected block facing the given side.
        /// </summary>
        private void Assign(BlockSide side)
        {
            var block = SelectedBlock;
            if ((loc != null) && (block != null))
            {
                var task = Task<bool>.Factory.StartNew(() => loc.AssignTo(block, side.Invert()));
                task.ContinueWith(t => {
                    if (t.Result)
                    {
                        UpdateBlocks();
                        UpdateControls();
                    }
                    else
                    {
                        var msg = string.Format("Cannot lock block '{0}'", block);
                        MessageBox.Show(msg, Strings.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        private void cmdAssignBack_Click(object sender, EventArgs e)
        {
            Assign(BlockSide.Back);
        }

        private void cmdAssignFront_Click(object sender, EventArgs e)
        {
            Assign(BlockSide.Front);
        }
    }
}
