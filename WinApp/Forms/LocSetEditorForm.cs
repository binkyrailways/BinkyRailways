using System;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Controls;

namespace BinkyRailways.WinApp.Forms
{
    /// <summary>
    /// Editor for loc sets.
    /// </summary>
    public partial class LocSetEditorForm : AppForm
    {
        private readonly IEntitySet3<ILoc> locs;

        /// <summary>
        /// Default ctor
        /// </summary>
        [Obsolete("Designer only")]
        public LocSetEditorForm()
            : this(null, null)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocSetEditorForm(IRailway railway, IEntitySet3<ILoc> locs)
        {
            this.locs = locs;
            InitializeComponent();
            if (railway != null)
            {
                foreach (var loc in railway.GetLocs().Where(x => !locs.Contains(x)).OrderBy(x => x.Description))
                {
                    lbAll.Items.Add(loc);
                }
                foreach (var loc in locs.OrderBy(x => x.Description))
                {
                    lbSet.Items.Add(loc);
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
        /// Block selection changed
        /// </summary>
        private void lbBlocks_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateComponents();
        }

        /// <summary>
        /// Add selected to set.
        /// </summary>
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            var item = lbAll.SelectedItem;
            lbAll.Items.Remove(item);
            lbSet.Items.Add(item);
        }

        /// <summary>
        /// Add on double click.
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
            var item = lbSet.SelectedItem;
            lbSet.Items.Remove(item);
            lbAll.Items.Add(item);
        }

        /// <summary>
        /// Remove on double click.
        /// </summary>
        private void lbSet_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (lbSet.SelectedItemContains(e.Location))
            {
                cmdRemove_Click(sender, e);
            }
        }

        /// <summary>
        /// Commit change
        /// </summary>
        private void cmdOk_Click(object sender, EventArgs e)
        {
            var toRemove = locs.Where(x => !lbSet.Items.Contains(x)).ToList();
            foreach (ILoc loc in lbSet.Items)
            {
                locs.Add(loc);
            }
            foreach (var loc in toRemove)
            {
                locs.Remove(loc);
            }
        }
    }
}
