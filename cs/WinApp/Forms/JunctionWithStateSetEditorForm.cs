using System;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;
using BinkyRailways.WinApp.Controls;

namespace BinkyRailways.WinApp.Forms
{
    /// <summary>
    /// Editor for junction with state sets.
    /// </summary>
    public partial class JunctionWithStateSetEditorForm : AppForm
    {
        private readonly IJunctionWithStateSet junctions;

        /// <summary>
        /// Default ctor
        /// </summary>
        [Obsolete("Designer only")]
        public JunctionWithStateSetEditorForm()
            : this(null, null)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public JunctionWithStateSetEditorForm(IModule module, IJunctionWithStateSet junctions)
        {
            this.junctions = junctions;
            InitializeComponent();
            if (module != null)
            {
                foreach (var sensor in module.Junctions.Where(x => !junctions.Contains(x)).OrderBy(x => x.Description, NameWithNumbersComparer.Instance))
                {
                    lbAll.Items.Add(sensor);
                }
                foreach (var junction in junctions.OrderBy(x => x.Description, NameWithNumbersComparer.Instance))
                {
                    var stateItem = junction.Accept(Default<StateItemBuilder>.Instance, null);
                    lbSet.Items.Add(stateItem);
                }
                if (lbAll.Items.Count > 0)
                {
                    lbAll.SelectedIndex = 0;
                }
                if (lbSet.Items.Count > 0)
                {
                    lbSet.SelectedIndex = 0;
                }
            }
            UpdateComponents();
        }

        /// <summary>
        /// Update the state of the components
        /// </summary>
        private void UpdateComponents()
        {
            var hasLeftSelection = (lbAll.SelectedIndex >= 0);
            var hasRightSelection = (lbSet.SelectedIndex >= 0);
            cmdAdd.Enabled = hasLeftSelection;
            cmdRemove.Enabled = hasRightSelection;
        }

        /// <summary>
        /// Loc selection changed.
        /// </summary>
        private void lbLocs_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateComponents();
        }

        /// <summary>
        /// Junction with state selection changed
        /// </summary>
        private void lbRight_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = lbSet.SelectedItem as JunctionStateItem;
            if (item != null)
            {
                // First hide all state controls
                cbSwitchDirection.Visible = false;
                udTurnTablePosition.Visible = false;
                // Let item show correct control
                item.InitializeStateControls(this);
            }
            else
            {
                // No selection
                cbSwitchDirection.Enabled = false;
                cbSwitchDirection.Visible = true;
                udTurnTablePosition.Visible = false;
            }
            UpdateComponents();
        }

        /// <summary>
        /// Selected switch direction has changed.
        /// </summary>
        private void cbSwitchDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = lbSet.SelectedItem as SwitchStateItem;
            if (item != null)
            {
                item.Direction = cbSwitchDirection.SelectedItem;
                var index = lbSet.Items.IndexOf(item);
                lbSet.Items.Remove(item);
                lbSet.Items.Insert(index, item);
                lbSet.SelectedIndex = index;
            }
        }

        /// <summary>
        /// Selected turn table position has changed.
        /// </summary>
        private void udTurnTablePosition_ValueChanged(object sender, EventArgs e)
        {
            var item = lbSet.SelectedItem as TurnTableStateItem;
            if (item != null)
            {
                item.Position = (int) udTurnTablePosition.Value;
                var index = lbSet.Items.IndexOf(item);
                lbSet.Items.Remove(item);
                lbSet.Items.Insert(index, item);
                lbSet.SelectedIndex = index;
            }
        }

        /// <summary>
        /// Change direction on double click
        /// </summary>
        private void lbSet_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (lbSet.SelectedItemContains(e.Location))
            {
                var item = (JunctionStateItem) lbSet.SelectedItem;
                var index = lbSet.Items.IndexOf(item);
                lbSet.Items.Remove(item);
                item.ToggleState();
                lbSet.Items.Insert(index, item);
                lbSet.SelectedIndex = index;
            }
        }

        /// <summary>
        /// Add selected to set.
        /// </summary>
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            var item = (IJunction)lbAll.SelectedItem;
            lbAll.Items.Remove(item);
            var stateItem = item.Accept(Default<StateItemBuilder>.Instance, null);
            lbSet.Items.Add(stateItem);
        }

        /// <summary>
        /// Add on double click
        /// </summary>
        private void lbAll_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (lbAll.SelectedItemContains(e.Location))
            {
                cmdAdd_Click(sender, e);
            }
        }

        /// <summary>
        /// Remove from set to all.
        /// </summary>
        private void cmdRemove_Click(object sender, EventArgs e)
        {
            var item = (JunctionStateItem)lbSet.SelectedItem;
            lbSet.Items.Remove(item);
            lbAll.Items.Add(item.Junction);
        }

        /// <summary>
        /// Commit change
        /// </summary>
        private void cmdOk_Click(object sender, EventArgs e)
        {
            // Remove all
            junctions.Clear();
            foreach (JunctionStateItem item in lbSet.Items)
            {
                item.AddTo(junctions);
            }
        }
    }
}
