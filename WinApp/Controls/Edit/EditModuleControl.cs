using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;
using BinkyRailways.WinApp.Controls.Edit.Settings;
using BinkyRailways.WinApp.Forms;
using BinkyRailways.WinApp.Items;
using BinkyRailways.WinApp.Preferences;
using BinkyRailways.WinApp.Utils;

namespace BinkyRailways.WinApp.Controls.Edit
{
    /// <summary>
    /// Outer control used to edit a module
    /// </summary>
    public partial class EditModuleControl : UserControl
    {
        /// <summary>
        /// Fired when the title bar of the dialog needs to be updated.
        /// </summary>
        public event EventHandler UpdateFormTitle;

        private AppState appState;
        private IModule module;
        private readonly ModuleTree moduleTree;
        private readonly SelectionManager selectionManager = new SelectionManager();

        /// <summary>
        /// Default ctor
        /// </summary>
        public EditModuleControl()
        {
            InitializeComponent();
            tvItems.SelectionManager = selectionManager;
            viewEditor.SelectionManager = selectionManager;
            selectionManager.Changed += OnSelectionChanged;
            moduleTree = new ModuleTree(tvItems.Nodes);

            splitContainer.Panel1MinSize = 200;
            splitContainer.Panel2MinSize = 300;
        }

        /// <summary>
        /// Connect to the given application state
        /// </summary>
        internal void Initialize(AppState appState, IModule module, ItemContext railwayContext)
        {
            this.appState = appState;
            this.module = module;
            ReloadModule();
            var context = new ItemContext(railwayContext, null, () => selectionManager.SelectedEntities, ReloadModule, null);
            viewEditor.Initialize(appState, module, context, false);
        }

        /// <summary>
        /// Handle keyboard shortcuts
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Delete:
                    if (tvItems.HasFocus()) 
                        tbRemove_Click(this, EventArgs.Empty);
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Reload the package data into this control
        /// </summary>
        private void ReloadModule()
        {
            using (tvItems.BeginUpdate())
            {
                moduleTree.ReloadModule(module, contextMenus, true);
            }
            tvItems.Refresh();
            ValidateModule();
        }

        /// <summary>
        /// Validate the module for integrity
        /// </summary>
        private void ValidateModule()
        {
            validationResultsControl.ValidateEntity(module);
        }

        /// <summary>
        /// Update the state of the toolbar.
        /// </summary>
        private void UpdateToolbar()
        {
            tbRemove.Enabled = (selectionManager.Count > 0);
            tbRename.Enabled = (selectionManager.SelectedEntities.Any(x => !x.HasAutomaticDescription));
        }

        /// <summary>
        /// Node selection has changed.
        /// </summary>
        private void  OnSelectionChanged(object sender, EventArgs e)
        {
            //var node = e.Node as EntityNode;
            var context = new GridContext(appState, module, viewEditor.ReloadModule);
            var settings = selectionManager.SelectedEntities.Select(x => x.Accept(Default<SettingsBuilder>.Instance, context));
            propertyGrid.SelectedObjects = settings.Where(x => x != null).ToArray();
            UpdateToolbar();
            viewEditor.Refresh();
        }

        /// <summary>
        /// A value has changed.
        /// Update treenode.
        /// </summary>
        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            foreach (var entity in selectionManager.SelectedEntities)
            {
                tvItems.UpdateNodeFromEntity(entity);
            }
            propertyGrid.Refresh();
            UpdateFormTitle.Fire(this);
            ValidateModule();
        }

        /// <summary>
        /// Add the given entity to the tree.
        /// </summary>
        private void AddEntity(IEntity entity, TreeNode root)
        {
            AddEntity(entity, root, true);
        }

        /// <summary>
        /// Add the given entity to the tree.
        /// </summary>
        private void AddEntity(IEntity entity, TreeNode root, bool reloadAndValidate)
        {
            var node = new EntityNode(entity, contextMenus);
            root.Nodes.Add(node);
            using (selectionManager.BeginUpdate())
            {
                selectionManager.Clear();
                selectionManager.Add(entity);
            }
            if (reloadAndValidate)
            {
                viewEditor.ReloadModule();
                ValidateModule();
            }
            UpdateFormTitle.Fire(this);
        }

        /// <summary>
        /// Add a new block.
        /// </summary>
        private void tbAddBlock_Click(object sender, EventArgs e)
        {
            var entity = module.Blocks.AddNew();
            entity.Description = Strings.NewBlockDescription;
            AddEntity(entity, moduleTree.BlocksRoot);
        }

        /// <summary>
        /// Add a new edge
        /// </summary>
        private void tbAddEdge_Click(object sender, EventArgs e)
        {
            var entity = module.Edges.AddNew();
            entity.Description = Strings.NewEdgeDescription;
            AddEntity(entity, moduleTree.EdgesRoot);
        }

        /// <summary>
        /// Add a new route
        /// </summary>
        private void tbAddRoute_Click(object sender, EventArgs e)
        {
            var entity = module.Routes.AddNew();
            entity.Description = Strings.NewRouteDescription;
            AddEntity(entity, moduleTree.RoutesRoot);
        }

        /// <summary>
        /// Add a standard switch
        /// </summary>
        private void tbAddSwitch_Click(object sender, EventArgs e)
        {
            var entity = module.Junctions.AddSwitch();
            entity.Description = Strings.NewSwitchDescription;
            AddEntity(entity, moduleTree.JunctionsRoot);
        }

        /// <summary>
        /// Add a turntable
        /// </summary>
        private void tbAddTurnTable_Click(object sender, EventArgs e)
        {
            var entity = module.Junctions.AddTurnTable();
            entity.Description = Strings.NewTurnTableDescription;
            AddEntity(entity, moduleTree.JunctionsRoot);
        }

        /// <summary>
        /// Add a passive junction
        /// </summary>
        private void miAddPassiveJunction_Click(object sender, EventArgs e)
        {
            var entity = module.Junctions.AddPassiveJunction();
            entity.Description = Strings.NewPassiveJunctionDescription;
            AddEntity(entity, moduleTree.JunctionsRoot);
        }

        /// <summary>
        /// Add a binary sensor
        /// </summary>
        private void tbAddBinarySensor_Click(object sender, EventArgs e)
        {
            var entity = module.Sensors.AddNewBinarySensor();
            entity.Description = Strings.NewBinarySensorDescription;
            AddEntity(entity, moduleTree.SensorsRoot);
        }

        /// <summary>
        /// Add a binary output
        /// </summary>
        private void tbAddBinaryOutput_Click(object sender, EventArgs e)
        {
            var entity = module.Outputs.AddNewBinaryOutput();
            entity.Description = Strings.NewBinaryOutputDescription;
            AddEntity(entity, moduleTree.OutputsRoot);
        }

        /// <summary>
        /// Add a 4-stage clock output
        /// </summary>
        private void tbAdd4stageClockOutput_Click(object sender, EventArgs e)
        {
            var entity = module.Outputs.AddNewClock4StageOutput();
            entity.Description = Strings.NewClock4StageOutputDescription;
            AddEntity(entity, moduleTree.OutputsRoot);
        }

        /// <summary>
        /// Add a block signal
        /// </summary>
        private void tbAddBlockSignal_Click(object sender, EventArgs e)
        {
            var entity = module.Signals.AddNewBlockSignal();
            entity.Description = Strings.NewBlockSignalDescription;
            AddEntity(entity, moduleTree.SignalsRoot);
        }

        /// <summary>
        /// Remove the selected entity
        /// </summary>
        private void tbRemove_Click(object sender, EventArgs e)
        {
            var selection = selectionManager.SelectedEntities.ToList();
            if (selection.Count > 0)
            {
                var msg = (selection.Count == 1)
                              ? string.Format(Strings.AreYouSureToRemoveX, selection[0])
                              : string.Format(Strings.AreYouSureToRemoveAllXEntities, selection.Count);
                if (MessageBox.Show(msg, Strings.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    foreach (var entity in selection)
                    {
                        entity.Accept(Default<RemoveVisitor>.Instance, module);
                    }
                    ReloadModule();
                    viewEditor.ReloadModule();
                    UpdateFormTitle.Fire(this);
                    ValidateModule();
                }
            }
        }

        /// <summary>
        /// Rename the selected nodes
        /// </summary>
        private void tbRename_Click(object sender, EventArgs e)
        {
            var selection = selectionManager.SelectedEntities.Where(x => !x.HasAutomaticDescription).ToList();
            if (selection.Count > 0)
            {
                using (var dialog = new RenameEntitiesForm(selection))
                {
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        foreach (var entity in selection)
                        {
                            tvItems.UpdateNodeFromEntity(entity);
                        }
                        propertyGrid.Refresh();
                        UpdateFormTitle.Fire(this);
                        ValidateModule();
                    }
                }
            }
        }

        /// <summary>
        /// Validation result activated.
        /// Select it's node
        /// </summary>
        private void validationResultsControl_ResultActivated(object sender, PropertyEventArgs<ValidationResult> e)
        {
            using (selectionManager.BeginUpdate())
            {
                selectionManager.Clear();
                selectionManager.Add(e.Value.Entity);
            }
        }

        /// <summary>
        /// Add a route that is the reverse of the selected route.
        /// </summary>
        private void miAddReverseRoute_Click(object sender, EventArgs e)
        {
            var selection = selectionManager.SelectedEntities.OfType<IRoute>().Where(x => x.GetReverse() == null).ToList();
            var nodesAdded = false;
            foreach (var route in selection)
            {
                var reverse = route.AddReverse();
                if (reverse != null)
                {
                    AddEntity(reverse, moduleTree.RoutesRoot, false);
                    nodesAdded = true;
                }
            }
            if (nodesAdded)
            {
                propertyGrid.Refresh();
                viewEditor.ReloadModule();
                UpdateFormTitle.Fire(this);
                ValidateModule();
            }
        }

        /// <summary>
        /// Sort routes by "From" block.
        /// </summary>
        private void miSortByFromBlock_Click(object sender, EventArgs e)
        {
            var prefs = UserPreferences.Preferences;
            if (prefs.SortRoutesByFrom) return;
            prefs.SortRoutesByFrom = true;
            prefs.Save();            
            ReloadModule();
        }

        /// <summary>
        /// Sort routes by "To" block.
        /// </summary>
        private void miSortByToBlock_Click(object sender, EventArgs e)
        {
            var prefs = UserPreferences.Preferences;
            if (!prefs.SortRoutesByFrom) return;
            prefs.SortRoutesByFrom = false;
            prefs.Save();
            ReloadModule();
        }

        /// <summary>
        /// Context menu or tree is about to open.
        /// </summary>
        private void contextMenus_Opening(object sender, CancelEventArgs e)
        {
            bool addReverseRoute;
            miAddReverseRoute.Visible = addReverseRoute = selectionManager.SelectedEntities.OfType<IRoute>().Any(x => x.GetReverse() == null);
            var hasRoutes = (moduleTree.RoutesRoot.Nodes.Count > 0);
            var prefs = UserPreferences.Preferences;
            miSortByFromBlock.Visible = hasRoutes && !prefs.SortRoutesByFrom;
            miSortByToBlock.Visible = hasRoutes && prefs.SortRoutesByFrom;
            e.Cancel = !addReverseRoute && !hasRoutes;
        }

        /// <summary>
        /// Save state
        /// </summary>
        private void OnAfterCollapseOrExpand(object sender, EventArgs e)
        {
            if (moduleTree != null)
            {
                moduleTree.SaveState();
            }
        }
    }
}
