using System;
using System.Drawing;
using System.Windows.Forms;
using BinkyRailways.Core.State;
using BinkyRailways.Core.State.Automatic;
using BinkyRailways.WinApp.Utils;

namespace BinkyRailways.WinApp.Controls.Run
{
    partial class LocsControl
    {
        /// <summary>
        /// List item for a loc.
        /// </summary>
        private class LocItem : ListViewItem
        {
            private readonly ILocState locState;
            private readonly ListViewGroupCollection groups;
            private readonly ListViewSubItem stateCell;
            private readonly ListViewSubItem speedCell;
            private readonly ListViewSubItem ownerCell;
            private readonly ListViewSubItem advCell;

            /// <summary>
            /// Default ctor
            /// </summary>
            public LocItem(ILocState locState, ListViewGroupCollection groups)
            {
                this.locState = locState;
                this.groups = groups;
                Text = locState.Description;
                stateCell = SubItems.Add("");
                speedCell = SubItems.Add("");
                ownerCell = SubItems.Add("");
                advCell = SubItems.Add("");
                bool routeDurationExceededDetected;
                UpdateFromState(out routeDurationExceededDetected);
            }

            /// <summary>
            /// Gets the loc
            /// </summary>
            public ILocState LocState
            {
                get { return locState; }
            }

            /// <summary>
            /// Is the loc assigned to a block?
            /// </summary>
            public bool IsAssigned { get { return (locState.CurrentBlock.Actual != null); } }

            /// <summary>
            /// Whenthis node is updated, will resorting be needed?
            /// </summary>
            internal bool IsSortNeeded()
            {
                bool routeDurationExceededDetected;
                return DoUpdateFromState(false, out routeDurationExceededDetected);
            }

            /// <summary>
            /// Update this node from it's state
            /// </summary>
            /// <returns>True if the state has changed such that a re-ordering is needed.</returns>
            internal void UpdateFromState(out bool routeDurationExceededDetected)
            {
                DoUpdateFromState(true, out routeDurationExceededDetected);
            }

            /// <summary>
            /// Update this node from it's state. Only actually make changes if apply is set.
            /// </summary>
            /// <returns>True if the state has changed such that a re-ordering is needed.</returns>
            private bool DoUpdateFromState(bool apply, out bool routeDurationExceededDetected)
            {
                var isAssigned = IsAssigned;
                var isCurrentRouteDurationExceeded = locState.IsCurrentRouteDurationExceeded;
                var newImageIndex = isAssigned ? (isCurrentRouteDurationExceeded ? 1 : 0) : 2;
                var oldImageIndex = ImageIndex;

                if (apply)
                {
                    this.SetText(locState.Description);
                    speedCell.SetText(locState.GetSpeedText());
                    stateCell.SetText(locState.GetStateText());
                    ownerCell.SetText(locState.Owner);
                    advCell.SetText(locState.CommandStationInfo);
                    if (locState.ControlledAutomatically.IsConsistent)
                    {
                        var isChecked = locState.ControlledAutomatically.Actual;
                        if (isChecked != Checked)
                            Checked = isChecked;
                    }
                    var group = isAssigned ? groups[0] : groups[1];
                    var textColor = isAssigned
                                        ? (isCurrentRouteDurationExceeded ? Color.Red : DefaultForeColor)
                                        : Color.Gray;
                    if (oldImageIndex != newImageIndex)
                    {
                        ImageIndex = newImageIndex;
                        ForeColor = textColor;
                        Group = group;
                    }
                }
                routeDurationExceededDetected = ((oldImageIndex != newImageIndex) && (newImageIndex == 1));
                return (oldImageIndex != newImageIndex) && ((oldImageIndex == 2) || (newImageIndex == 2));
            }
        }
    }
}
