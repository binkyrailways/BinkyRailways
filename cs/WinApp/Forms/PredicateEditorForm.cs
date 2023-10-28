using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;

namespace BinkyRailways.WinApp.Forms
{
    public partial class PredicateEditorForm : AppForm
    {
        private readonly ILocPredicate predicate;
        private readonly PredicateItem rootNode;

        /// <summary>
        /// Default ctor
        /// </summary>
        public PredicateEditorForm()
            : this(null, null)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public PredicateEditorForm(ILocPredicate predicate, IRailway railway)
        {
            this.predicate = predicate;
            InitializeComponent();
            if (predicate != null)
            {
                rootNode = predicate.Accept(Default<ItemBuilder>.Instance, railway);
                rootNode.Text = "Permissions";
                tvItems.Nodes.Add(rootNode);
                tvItems.ExpandAll();

                // Locs
                foreach (var locRef in railway.Locs)
                {
                    ILoc loc;
                    if (locRef.TryResolve(out loc))
                    {
                        var menuItem = new LocEqualsMenuItem(loc, railway);
                        menuItem.Click += (s, x) => Execute(menuItem);
                        tbAddLocs.DropDownItems.Add(menuItem);
                    }
                }

                // LocGroups
                foreach (var group in railway.LocGroups)
                {
                    var menuItem = new LocGroupEqualsMenuItem(group, railway);
                    menuItem.Click += (s, x) => Execute(menuItem);
                    tbAddLocGroups.DropDownItems.Add(menuItem);
                }

                // Specials
                {
                    // Change direction
                    {
                        var menuItem = new LocCanChangeDirectionMenuItem(railway);
                        menuItem.Click += (s, x) => Execute(menuItem);
                        tbAddSpecial.DropDownItems.Add(menuItem);
                    }
                    // Time predicate
                    {
                        var menuItem = new LocTimeMenuItem(railway);
                        menuItem.Click += (s, x) => Execute(menuItem);
                        tbAddSpecial.DropDownItems.Add(menuItem);                        
                    }
                    // AND
                    {
                        var menuItem = new LocAndMenuItem(railway);
                        menuItem.Click += (s, x) => Execute(menuItem);
                        tbAddSpecial.DropDownItems.Add(menuItem);
                    }
                    // OR
                    {
                        var menuItem = new LocOrMenuItem(railway);
                        menuItem.Click += (s, x) => Execute(menuItem);
                        tbAddSpecial.DropDownItems.Add(menuItem);
                    }
                }

                UpdateToolbar();
            }
        }

        /// <summary>
        /// Execute the given predicate builder on the selected node.
        /// </summary>
        private void Execute(IPredicateBuilder builder)
        {
            var node = SelectedItem;
            if (node != null)
            {
                var p = builder.CreatePredicate(true);
                if ((p != null) && node.CanAdd(p))
                {
                    node.Add(p);
                }
            }
        }

        /// <summary>
        /// Remove selected node.
        /// </summary>
        private void tbRemove_Click(object sender, System.EventArgs e)
        {
            var node = SelectedItem;
            if (node != null)
            {
                var parent = node.Parent as PredicateItem;
                if ((parent != null) && parent.CanRemove(node))
                {
                    parent.Remove(node);
                }
            }
            UpdateToolbar();
        }

        /// <summary>
        /// Gets the selected item (if any)
        /// </summary>
        private PredicateItem SelectedItem
        {
            get { return tvItems.SelectedNode as PredicateItem; }
        }

        /// <summary>
        /// Selection has changed.
        /// </summary>
        private void tvItems_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            UpdateToolbar();
        }

        private void UpdateToolbar()
        {
            var selection = SelectedItem;
            var parent = (selection != null) ? selection.Parent as PredicateItem : null;
            foreach (ToolStripItem item in toolStrip1.Items)
            {
                UpdateToolStripItem(item, selection);
            }
            tbRemove.Enabled = (parent != null) && parent.CanRemove(selection);
        }

        private static void UpdateToolStripItem(ToolStripItem item, PredicateItem selection)
        {
            var builder = item as IPredicateBuilder;
            item.Enabled = (selection != null) &&
                           ((builder == null) || selection.CanAdd(builder.CreatePredicate(false)));
            var dropDown = item as ToolStripDropDownItem;
            if (dropDown != null)
            {
                foreach (ToolStripItem x in dropDown.DropDownItems)
                {
                    UpdateToolStripItem(x, selection);
                }
            }
        }

        /// <summary>
        /// Save the predicate
        /// </summary>
        private void cmdOk_Click(object sender, System.EventArgs e)
        {
            if (rootNode != null)
            {
                rootNode.Save();
            }
        }
    }
}
