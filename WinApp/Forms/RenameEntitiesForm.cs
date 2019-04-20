using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Services;
using BinkyRailways.WinApp.Preferences;

namespace BinkyRailways.WinApp.Forms
{
    public partial class RenameEntitiesForm : AppForm
    {
        /// <summary>
        /// Designer ctor
        /// </summary>
        public RenameEntitiesForm()
            : this(new IEntity[0])
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public RenameEntitiesForm(IEnumerable<IEntity> entities)
        {
            InitializeComponent();
            foreach (var variableName in Expression.VariableNames)
            {
                var item = new ToolStripMenuItem(variableName);
                var expression = Expression.CreateVariableExpression(variableName);
                item.Click += (s, x) => InsertExpression(expression);
                tbInsertVariable.DropDownItems.Add(item);
            }
            foreach (var entity in entities)
            {
                var item = new ListViewItem(entity.ToString());
                item.Tag = entity;
                item.SubItems.Add("");
                lvEntities.Items.Add(item);
            }
            tbExpression.Text = UserPreferences.Preferences.LastRenamePattern;
            UpdatePreview();
        }

        /// <summary>
        /// Insert the given expression into the pattern.
        /// </summary>
        private void InsertExpression(string expression)
        {
            tbExpression.Paste(expression);
        }

        /// <summary>
        /// Update the preview items
        /// </summary>
        private void UpdatePreview()
        {
            var pattern = tbExpression.Text.Trim();
            var expression = string.IsNullOrEmpty(pattern) ? null : new Expression(pattern);
            foreach (ListViewItem item in lvEntities.Items)
            {
                var entity = (IEntity) item.Tag;
                item.SubItems[1].Text = (expression != null) ? expression.Evaluate(entity) : entity.ToString();
            }
            cmdOk.Enabled = (expression != null);
        }

        /// <summary>
        /// Expression has changed, update preview.
        /// </summary>
        private void tbExpression_TextChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        /// <summary>
        /// Rename entities now
        /// </summary>
        private void cmdOk_Click(object sender, EventArgs e)
        {
            var pattern = tbExpression.Text.Trim();
            var expression = string.IsNullOrEmpty(pattern) ? null : new Expression(pattern);
            if (expression != null)
            {
                foreach (ListViewItem item in lvEntities.Items)
                {
                    var entity = (IEntity) item.Tag;
                    entity.Description = expression.Evaluate(entity);
                }
                UserPreferences.Preferences.LastRenamePattern = pattern;
                UserPreferences.SaveNow();
                DialogResult = DialogResult.OK;
            }
        }
    }
}
