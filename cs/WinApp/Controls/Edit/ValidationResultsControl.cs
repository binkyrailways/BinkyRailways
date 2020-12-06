using System;
using System.ComponentModel;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;
using BinkyRailways.Properties;
using NLog;

namespace BinkyRailways.WinApp.Controls.Edit
{
    [DefaultEvent("ResultActivated")]
    public partial class ValidationResultsControl : UserControl
    {
        /// <summary>
        /// Fired when a validation result was activated.
        /// </summary>
        [Category("Behavior")]
        public event EventHandler<PropertyEventArgs<ValidationResult>> ResultActivated;

        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Default ctor
        /// </summary>
        public ValidationResultsControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Validate the given subject.
        /// </summary>
        public void ValidateEntity(IValidationSubject subject)
        {
            try
            {
                var results = new ValidationResults();
                if (subject != null)
                {
                    subject.Validate(results);
                }
                LoadResults(results);
            }
            catch (Exception ex)
            {
                log.ErrorException(Strings.ExValidationFailed, ex);
            }
        }

        /// <summary>
        /// Load the given results into this control
        /// </summary>
        private void LoadResults(ValidationResults results)
        {
            lvLog.BeginUpdate();
            lvLog.Items.Clear();
            if (results != null)
            {
                foreach (var item in results.Errors)
                {
                    lvLog.Items.Add(CreateItem(item, 0));
                }
                foreach (var item in results.Warnings)
                {
                    lvLog.Items.Add(CreateItem(item, 1));
                }
                if (lvLog.Items.Count == 0)
                {
                    var item = lvLog.Items.Add(Resources.No_validation_warnings_errors_found);
                    item.ImageIndex = 2;
                    item.SubItems.Add(string.Empty);
                    item.SubItems.Add(string.Empty);
                }
            }
            lvLog.EndUpdate();
        }

        /// <summary>
        /// Create a list view item for the given result.
        /// </summary>
        private static ListViewItem CreateItem(ValidationResult result, int imageIndex)
        {
            var item = new ListViewItem(result.Message);
            item.SubItems.Add(result.Entity.ToString());
            item.SubItems.Add(result.Entity.TypeName);
            item.ImageIndex = imageIndex;
            item.Tag = result;
            return item;
        }

        /// <summary>
        /// Item was activated.
        /// </summary>
        private void lvLog_ItemActivate(object sender, EventArgs e)
        {
            var selectedItems = lvLog.SelectedItems;
            if (selectedItems.Count > 0)
            {
                var result = selectedItems[0].Tag as ValidationResult;
                if (result != null)
                {
                    ResultActivated.Fire(this, new PropertyEventArgs<ValidationResult>(result));
                }
            }
        }
    }
}
