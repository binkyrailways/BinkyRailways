using System;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Model.Impl;
using BinkyRailways.WinApp.Controls.Edit.Settings;

namespace BinkyRailways.WinApp.Forms
{
    public partial class ActionsEditorForm : AppForm
    {
        private readonly GridContext gridContext;

        /// <summary>
        /// Default ctor
        /// </summary>
        public ActionsEditorForm()
            : this(null, null, null)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        internal ActionsEditorForm(IActionTriggerSource source, IRailway railway, GridContext gridContext)
        {
            this.gridContext = gridContext;
            InitializeComponent();
            if (source != null)
            {
                AddAddAction("Loc function action", () => new LocFunctionAction());
                AddAddAction("Initialize junction action", () => new InitializeJunctionAction());
                AddAddAction("Play sound action", () => new PlaySoundAction());
                tvItems.BeginUpdate();
                foreach (var trigger in source.Triggers)
                {
                    tvItems.Nodes.Add(new TriggerItem(trigger));
                }
                tvItems.EndUpdate();
                UpdateToolbar();
            }
        }

        /// <summary>
        /// Add a drop down item to the Add button.
        /// </summary>
        private void AddAddAction(string text, Func<IAction> builder)
        {
            var item = new ToolStripMenuItem(text);
            item.Click += (s, x) =>
                              {
                                  var action = builder();
                                  var node = SelectedItem.Add(action);
                                  tvItems.SelectedNode = node;
                              };
            tbAdd.DropDownItems.Add(item);
        }

        /// <summary>
        /// Remove selected node.
        /// </summary>
        private void tbRemove_Click(object sender, System.EventArgs e)
        {
            var node = SelectedItem as ActionItem;
            var parent = (node != null) ? node.Parent as TriggerItem : null;
            if ((node != null) && (parent != null))
            {
                parent.Nodes.Remove(node);
            }
            UpdateToolbar();
        }

        /// <summary>
        /// Gets the selected item (if any)
        /// </summary>
        private Item SelectedItem
        {
            get { return tvItems.SelectedNode as Item; }
        }

        /// <summary>
        /// Selection has changed.
        /// </summary>
        private void tvItems_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateToolbar();
            var selection = SelectedItem as ActionItem;
            grid.SelectedObject = (selection != null)
                                      ? selection.Action.Accept(new SettingsBuilder(), gridContext)
                                      : null;
        }

        /// <summary>
        /// Update the state of the controls
        /// </summary>
        private void UpdateToolbar()
        {
            var selection = SelectedItem;
            tbAdd.Enabled = (selection != null) && selection.CanAdd;
            tbRemove.Enabled = (selection is ActionItem);
        }

        /// <summary>
        /// Save the actions
        /// </summary>
        private void cmdOk_Click(object sender, System.EventArgs e)
        {
            foreach (TriggerItem item in tvItems.Nodes)
            {
                item.Save();
            }
        }

        /// <summary>
        /// Property value has changed.
        /// </summary>
        private void grid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            foreach (TriggerItem item in tvItems.Nodes)
            {
                item.OnValueChanged();
            }
        }
    }
}
