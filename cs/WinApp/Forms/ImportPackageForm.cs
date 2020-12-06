using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Forms
{
    /// <summary>
    /// Import specific elements of a package.
    /// </summary>
    public partial class ImportPackageForm : AppForm
    {
        private readonly IPackage target;
        private readonly IPackage source;

        /// <summary>
        /// Default ctor
        /// </summary>
        [Obsolete("Designer only")]
        public ImportPackageForm()
            : this(null, null)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public ImportPackageForm(IPackage target, IPackage source)
        {
            this.target = target;
            this.source = source;
            InitializeComponent();

            if ((target != null) && (source != null))
            {
                lbAll.BeginUpdate();
                Add(source.GetCommandStations(), x => target.GetCommandStation(x.Id));
                Add(source.GetLocs(), x => target.GetLoc(x.Id));
                Add(source.Railway.LocGroups, x => target.Railway.LocGroups.FirstOrDefault(y => y.Id == x.Id));
                Add(source.GetModules(), x => target.GetModule(x.Id));
                lbAll.EndUpdate();
            }
            UpdateButtons();
        }

        /// <summary>
        /// Add a given entity.
        /// </summary>
        private void Add<T>(IEnumerable<T> sourceEntities, Func<T, T> getTarget)
            where T : IImportableEntity
        {
            foreach (var x in sourceEntities)
            {
                var item = new Item(x, getTarget(x));
                lbAll.Items.Add(item);
            }
        }

        /// <summary>
        /// Update the state of the buttons
        /// </summary>
        private void UpdateButtons()
        {
            cmdOk.Enabled = (lbAll.Items.Count > 0) ;//&& lbAll.Items.Cast<Item>().Any(x => x.Checked);
        }

        /// <summary>
        /// Checked state has changed.
        /// </summary>
        private void lbAll_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            UpdateButtons();
        }

        /// <summary>
        /// Import now
        /// </summary>
        private void cmdImport_Click(object sender, EventArgs e)
        {
            // Import all checked items
            try
            {
                foreach (var item in lbAll.Items.Cast<Item>().Where(x => x.Checked))
                {
                    item.Entity.Import(target);
                }
            }
            catch (Exception ex)
            {
                var msg = string.Format(Strings.ImportPackageFailedBecauseX, ex.Message);
                MessageBox.Show(msg, Strings.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Check all imports
        /// </summary>
        private void cmdCheckAll_Click(object sender, EventArgs e)
        {
            foreach (var item in lbAll.Items.Cast<Item>())
            {
                item.Checked = item.SafeToImport;
            }
        }

        /// <summary>
        /// Uncheck all items
        /// </summary>
        private void cmdCheckNone_Click(object sender, EventArgs e)
        {
            foreach (var item in lbAll.Items.Cast<Item>())
            {
                item.Checked = false;
            }
        }
    }
}
