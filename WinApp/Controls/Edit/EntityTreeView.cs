using System;
using System.Linq;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;

namespace BinkyRailways.WinApp.Controls.Edit
{
    public sealed class EntityTreeView : UserControl
    {
        /// <summary>
        /// Fired when a node is collapsed or expanded.
        /// </summary>
        public event EventHandler ExpandStateChanged;

        private readonly TreeViewX treeView;
        private SelectionManager selectionManager;
        private ContextMenuStrip contextMenuStrip;
        private System.ComponentModel.IContainer components;
        private ToolStripMenuItem miDummyToolStripMenuItem;
        private bool updatingSelection;

        /// <summary>
        /// Default ctor
        /// </summary>
        public EntityTreeView()
        {
            SelectionManager = new SelectionManager();
            treeView = new TreeViewX { Dock = DockStyle.Fill, HideSelection = false };
            Controls.Add(treeView);
            treeView.AfterExpand += (s, x) => ExpandStateChanged.Fire(this);
            treeView.AfterCollapse += (s, x) => ExpandStateChanged.Fire(this);
            treeView.AfterSelect += OnTreeViewAfterSelect;
            treeView.ContextMenuStrip = contextMenuStrip;
        }

        /// <summary>
        /// Gets the current context
        /// </summary>
        internal IEditContext EditContext { get; set; }

        /// <summary>
        /// Selection manager
        /// </summary>
        internal SelectionManager SelectionManager
        {
            get { return selectionManager; }
            set
            {
                if (value== null)
                    throw new ArgumentNullException();
                if (selectionManager != null)
                {
                    selectionManager.Changed -= OnSelectionManagerChanged;
                }
                selectionManager = value;
                selectionManager.Changed += OnSelectionManagerChanged;
            }
        }

        /// <summary>
        /// Block updates until the returned object is disposed.
        /// </summary>
        public IDisposable BeginUpdate()
        {
            treeView.BeginUpdate();
            return new Update(this);
        }

        /// <summary>
        /// Expand all nodes
        /// </summary>
        public void ExpandAll()
        {
            treeView.ExpandAll();
        }

        /// <summary>
        /// Update the node for the given entity.
        /// </summary>
        public void UpdateNodeFromEntity(IEntity entity)
        {
            var node = FindNode(entity);
            if (node != null) node.UpdateFromEntity();
        }

        /// <summary>
        /// Gets the root node collection
        /// </summary>
        internal TreeNodeCollection Nodes
        {
            get { return treeView.Nodes; }
        }

        /// <summary>
        /// Gets the module that is the given entity or contains the given entity.
        /// </summary>
        internal IModule GetModule(IEntity entity)
        {
            var module = entity as IModule;
            if (module != null)
                return module;
            TreeNode node = FindNode(entity);
            while (node != null)
            {
                var entityNode = node as EntityNode;
                module = (entityNode != null) ? entityNode.Entity as IModule : null;
                if (module != null)
                    return module;
                node = node.Parent;
            }
            return null;
        }

        /// <summary>
        /// Selection has changed.
        /// </summary>
        private void OnSelectionManagerChanged(object sender, EventArgs e)
        {
            if (updatingSelection)
                return;
            try
            {
                updatingSelection = true;
                treeView.BeginUpdate();
                treeView.SelectedNodes.Clear();
                foreach (var entity in SelectionManager.SelectedEntities)
                {
                    var node = FindNode(entity);
                    if (node !=null)
                    {
                        treeView.SelectedNodes.Add(node);
                    }
                }
            }
            finally
            {
                treeView.EndUpdate();
                updatingSelection = false;
            }
        }


        /// <summary>
        /// Selection in the treeview has changed.
        /// </summary>
        private void OnTreeViewAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (updatingSelection)
                return;
            var selMgr = SelectionManager;
            using (selMgr.BeginUpdate())
            {
                selMgr.Clear();
                foreach (var node in treeView.SelectedNodes)
                {
                    var entityNode = node as EntityNode;
                    if ((entityNode != null) && (entityNode.Entity != null))
                    {
                        selMgr.Add(entityNode.Entity);
                    }
                }
            }
        }

        /// <summary>
        /// Node context menu is about to open.
        /// </summary>
        private void OnContextMenuStripOpening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var selection = SelectionManager.SelectedEntities.FirstOrDefault();
            if ((selection == null) || (EditContext == null) || (EditContext.Package == null))
            {
                e.Cancel = true;
                return;
            }

            var builder = new ContextMenuBuilder(EditContext.Package, EditContext);
            contextMenuStrip.Items.Clear();
            selection.Accept(builder, contextMenuStrip);

            if (contextMenuStrip.Items.Count == 0)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Find the node referring to the given entity.
        /// </summary>
        private EntityNode FindNode(IEntity entity)
        {
            return FindNode(entity, treeView.Nodes);
        }

        /// <summary>
        /// Find the node referring to the given entity.
        /// </summary>
        private EntityNode FindNode(IEntity entity, TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                var entityNode = node as EntityNode;
                if ((entityNode != null) && (entityNode.Entity == entity))
                    return entityNode;
                var result = FindNode(entity, node.Nodes);
                if (result != null)
                    return result;
            }
            return null;
        }

        private sealed class Update : IDisposable
        {
            private readonly EntityTreeView treeView;

            public Update(EntityTreeView treeView)
            {
                this.treeView = treeView;
            }

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            /// <filterpriority>2</filterpriority>
            void IDisposable.Dispose()
            {
                treeView.treeView.EndUpdate();
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miDummyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miDummyToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(153, 48);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.OnContextMenuStripOpening);
            // 
            // miDummyToolStripMenuItem
            // 
            this.miDummyToolStripMenuItem.Name = "miDummyToolStripMenuItem";
            this.miDummyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.miDummyToolStripMenuItem.Text = "miDummy";
            // 
            // EntityTreeView
            // 
            this.Name = "EntityTreeView";
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
