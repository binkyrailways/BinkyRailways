using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using BinkyRailways.Core.State;
using BinkyRailways.Core.Util;

namespace BinkyRailways.WinApp.Controls.Run
{
    public partial class LocListView : UserControl
    {
        /// <summary>
        /// Fired to show loc properties.
        /// </summary>
        public event EventHandler<ObjectEventArgs<ILocState>> ShowLocProperties;

        /// <summary>
        /// Fired when the selected loc has changed.
        /// </summary>
        public event EventHandler SelectionChanged;

        /// <summary>
        /// Fired when a route duration has exceeded.
        /// </summary>
        public event EventHandler RouteDurationExceeded;

        /// <summary>
        /// The actual state of a loc has changed.
        /// </summary>
        public event EventHandler LocActualStateChanged;

        private static readonly float[] columnWidths = new[] { 0.3f, 0.40f, 0.15f, 0.15f, 0.5f };
        private readonly ListViewGroup groupAssigned;
        private readonly ListViewGroup groupUnassigned;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocListView()
        {
            InitializeComponent();
            groupAssigned = new ListViewGroup(Strings.LocsControlAssignedLocs);
            groupUnassigned = new ListViewGroup(Strings.LocsControlUnassignedLocs);
            lvLocs.Groups.AddRange(new[] { groupAssigned, groupUnassigned });
            lvLocs.ListViewItemSorter = new LocItemSorter();
            lvLocs.ShowGroups = true;
            AllowItemDrag = true;
        }

        /// <summary>
        /// Size has changed
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ResizeColumns();
        }

        /// <summary>
        /// Initialize for the given state
        /// </summary>
        internal void Initialize(AppState appState, IRailwayState railway)
        {
            lvLocs.BeginUpdate();
            lvLocs.Items.Clear();

            if (railway != null)
            {
                foreach (var locState in railway.LocStates)
                {
                    lvLocs.Items.Add(new LocItem(locState, lvLocs.Groups));
                    locState.ActualStateChanged += this.ASynchronize(OnLocActualStateChanged);
                }
                // Select first loc
                if (lvLocs.Items.Count > 0)
                {
                    lvLocs.Items[0].Selected = true;
                }
            }

            lvLocs.Sort();
            lvLocs.EndUpdate();
            ResizeColumns();
            updateTimer.Enabled = (railway != null);
        }

        /// <summary>
        /// Show checkboxes in the list?
        /// </summary>
        public bool ShowCheckboxes
        {
            get { return lvLocs.CheckBoxes; }
            set { lvLocs.CheckBoxes = value; }
        }

        /// <summary>
        /// Allow dragging items
        /// </summary>
        public bool AllowItemDrag { get; set; }

        /// <summary>
        /// Is the update timer enabled?
        /// </summary>
        public bool UpdateTimerEnabled
        {
            get { return updateTimer.Enabled; }
            set { updateTimer.Enabled = value; }
        }

        /// <summary>
        /// Update the size of the columns
        /// </summary>
        private void ResizeColumns()
        {
            var colCount = lvLocs.Columns.Count;
            var width = Math.Max(100, Width - 10);
            for (int i = 0; i < colCount; i++)
            {
                lvLocs.Columns[i].Width = (int)(width * columnWidths[i]);
            }
        }

        /// <summary>
        /// Actual state of a loc has changed.
        /// </summary>
        private void OnLocActualStateChanged(object sender, EventArgs e)
        {
            var locState = (ILocState) sender;
            var item = lvLocs.Items.Cast<LocItem>().FirstOrDefault(x => x.LocState == locState);
            var routeDurationExceededDetected = false;
            if (item != null)
            {
                lvLocs.BeginUpdate();
                var sortNeeded = item.IsSortNeeded();
                item.UpdateFromState(out routeDurationExceededDetected);
                if (sortNeeded)
                {
                    lvLocs.Sort();
                    //lvLocs.Refresh();
                }
                lvLocs.EndUpdate();
            }
            LocActualStateChanged.Fire(this);
            if (routeDurationExceededDetected)
            {
                RouteDurationExceeded.Fire(this);
            }
        }

        /// <summary>
        /// Gets the selected item
        /// </summary>
        public LocItem SelectedItem
        {
            get
            {
                var selection = lvLocs.SelectedItems;
                return (selection.Count > 0) ? (LocItem)selection[0] : null;
            }
        }

        /// <summary>
        /// Gets the selected items
        /// </summary>
        public IEnumerable<LocItem> SelectedItems
        {
            get { return lvLocs.SelectedItems.Cast<LocItem>(); }
        }

        /// <summary>
        /// Selection has changed
        /// </summary>
        private void OnLocsSelectedIndexChanged(object sender, EventArgs e)
        {
            var selection = SelectedItem;
            if ((selection != null) || (sender == null))
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
            SelectionChanged.Fire(this);
        }

        /// <summary>
        /// Loc checkbox is about to change.
        /// </summary>
        private void OnLocsItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (ShowCheckboxes)
            {
                var item = lvLocs.Items[e.Index] as LocItem;
                if (item != null)
                {
                    if (!item.LocState.CanSetAutomaticControl && (e.NewValue == CheckState.Checked))
                    {
                        // Cannot check yet
                        e.NewValue = CheckState.Unchecked;
                    }
                }
            }
        }

        /// <summary>
        /// Loc checkbox changed.
        /// </summary>
        private void OnLocsItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (ShowCheckboxes)
            {
                var item = e.Item as LocItem;
                if (item != null)
                {
                    if (!item.Checked || item.LocState.CanSetAutomaticControl)
                    {
                        item.LocState.ControlledAutomatically.Requested = item.Checked;
                    }
                }
            }
        }

        /// <summary>
        /// Update the loc state
        /// </summary>
        private void OnUpdateTimerTick(object sender, EventArgs e)
        {
            var routeDurationExceededDetected = false;
            foreach (LocItem item in lvLocs.Items)
            {
                bool tmp;
                item.UpdateFromState(out tmp);
                routeDurationExceededDetected |= tmp;
            }
            if (routeDurationExceededDetected)
            {
                RouteDurationExceeded.Fire(this);
            }
        }

        /// <summary>
        /// Prevent context menu in case there is no current loc.
        /// </summary>
        private void OnLocContextMenuOpening(object sender, CancelEventArgs e)
        {
            if (SelectedItem == null)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Show loc properties.
        /// </summary>
        private void OnLocPropertiesClick(object sender, EventArgs e)
        {
            var selection = SelectedItem;
            if (selection == null)
                return;
            ShowLocProperties.Fire(this, new ObjectEventArgs<ILocState>(selection.LocState));
        }

        /// <summary>
        /// A loc is being dragged.
        /// </summary>
        private void OnLocsItemDrag(object sender, ItemDragEventArgs e)
        {
            if (AllowItemDrag)
            {
                var item = (LocItem) e.Item;
                if (item.LocState.CurrentBlock.Actual != null)
                {
                    DoDragDrop(new EntityStateContainer(item.LocState), DragDropEffects.All);
                }
            }
        }

        public void SelectLoc(ILocState value)
        {
            var item = lvLocs.Items.Cast<LocItem>().FirstOrDefault(x => x.LocState == value);
            if (item != null)
            {
                item.Selected = true;
            }
        }

        /// <summary>
        /// Sort loc items
        /// </summary>
        private sealed class LocItemSorter : IComparer
        {
            /// <summary>
            /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
            /// </summary>
            /// <returns>
            /// A signed integer that indicates the relative values of <paramref name="x"/> and <paramref name="y"/>, as shown in the following table.Value Meaning Less than zero <paramref name="x"/> is less than <paramref name="y"/>. Zero <paramref name="x"/> equals <paramref name="y"/>. Greater than zero <paramref name="x"/> is greater than <paramref name="y"/>. 
            /// </returns>
            /// <param name="x">The first object to compare. </param><param name="y">The second object to compare. </param><exception cref="T:System.ArgumentException">Neither <paramref name="x"/> nor <paramref name="y"/> implements the <see cref="T:System.IComparable"/> interface.-or- <paramref name="x"/> and <paramref name="y"/> are of different types and neither one can handle comparisons with the other. </exception><filterpriority>2</filterpriority>
            public int Compare(object x, object y)
            {
                var ix = (LocItem) x;
                var iy = (LocItem) y;

                return string.Compare(ix.Text, iy.Text, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}
