using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Storage;
using BinkyRailways.Core.Util;
using BinkyRailways.Import;
using BinkyRailways.WinApp.Controls.Edit.Settings;
using BinkyRailways.WinApp.Forms;
using BinkyRailways.WinApp.Preferences;
using BinkyRailways.WinApp.Utils;

namespace BinkyRailways.WinApp.Controls.Edit
{
    /// <summary>
    /// Outer control used to edit a railway
    /// </summary>
    public partial class EditRailwayControl : UserControl, IEditContext
    {
        /// <summary>
        /// Fired when the title bar of the application needs to be updated.
        /// </summary>
        public event EventHandler UpdateAppTitle;

        private readonly ToolStripMerge toolStripMerge;
        private AppState appState;
        private EntityNode railwayNode;
        private readonly TreeNode activeRoot;
        private readonly TreeNode activeLocsRoot;
        private readonly TreeNode locGroupsRoot;
        private readonly TreeNode activeModulesRoot;
        private readonly TreeNode activeCommandStationsRoot;
        private readonly TreeNode moduleConnectionsRoot;
        private readonly TreeNode inactiveRoot;
        private readonly TreeNode inactiveLocsRoot;
        private readonly TreeNode inactiveModulesRoot;
        private readonly TreeNode inactiveCommandStationsRoot;
        private bool reloading;

        /// <summary>
        /// Default ctor
        /// </summary>
        public EditRailwayControl()
        {
            InitializeComponent();
            activeRoot = new TreeNode("Active");
            activeLocsRoot = new TreeNode("Locomotives");
            locGroupsRoot = new TreeNode("Locomotive groups");
            activeModulesRoot = new TreeNode("Modules");
            moduleConnectionsRoot = new TreeNode("Module connections");
            activeCommandStationsRoot = new TreeNode("Command stations");
            activeRoot.Nodes.Add(activeLocsRoot);
            activeRoot.Nodes.Add(locGroupsRoot);
            activeRoot.Nodes.Add(activeModulesRoot);
            activeRoot.Nodes.Add(moduleConnectionsRoot);
            activeRoot.Nodes.Add(activeCommandStationsRoot);
            inactiveRoot = new TreeNode("Archived");
            inactiveLocsRoot = new TreeNode("Locomotives");
            inactiveModulesRoot = new TreeNode("Modules");
            inactiveCommandStationsRoot = new TreeNode("Command stations");
            inactiveRoot.Nodes.Add(inactiveLocsRoot);
            inactiveRoot.Nodes.Add(inactiveModulesRoot);
            inactiveRoot.Nodes.Add(inactiveCommandStationsRoot);
            tvItems.Nodes.Add(activeRoot);
            tvItems.Nodes.Add(inactiveRoot);

            splitContainer.Panel1MinSize = 200;
            splitContainer.Panel2MinSize = 300;

            toolStripMerge = new ToolStripMerge(toolStrip1);
            toolStrip1.Hide();
        }

        /// <summary>
        /// Items to merge into the toolbar.
        /// </summary>
        internal ToolStripMerge ToolStripMerge { get { return toolStripMerge; } }

        /// <summary>
        /// Connect to the given application state
        /// </summary>
        internal void Initialize(AppState appState)
        {
            this.appState = appState;
            appState.PackageChanged += (s, _) => ReloadPackage();
            ReloadPackage();
            viewEditor.Initialize(appState);
        }

        /// <summary>
        /// Reload the package data into this control
        /// </summary>
        private void ReloadPackage()
        {
            try
            {
                reloading = true;
                tvItems.BeginUpdate();
                tvItems.SelectedNodes.Clear();
                activeLocsRoot.Nodes.Clear();
                locGroupsRoot.Nodes.Clear();
                activeModulesRoot.Nodes.Clear();
                moduleConnectionsRoot.Nodes.Clear();
                activeCommandStationsRoot.Nodes.Clear();
                inactiveLocsRoot.Nodes.Clear();
                inactiveModulesRoot.Nodes.Clear();
                inactiveCommandStationsRoot.Nodes.Clear();

                if (railwayNode != null)
                {
                    activeRoot.Nodes.Remove(railwayNode);
                    railwayNode = null;
                }

                var package = appState.Package;
                if (package != null)
                {
                    var railway = package.Railway;

                    railwayNode = new EntityNode(railway, false, true) { Text = "Railway" };
                    activeRoot.Nodes.Insert(0, railwayNode);

                    foreach (var locRef in railway.Locs.OrderBy(x => x.ToString(), NameWithNumbersComparer.Instance))
                    {
                        ILoc loc;
                        if (locRef.TryResolve(out loc))
                        {
                            activeLocsRoot.Nodes.Add(new EntityRefNode<ILoc>(locRef, loc));
                        }
                    }
                    foreach (var group in railway.LocGroups.OrderBy(x => x.ToString(), NameWithNumbersComparer.Instance))
                    {
                        locGroupsRoot.Nodes.Add(new EntityNode(group, null));
                    }
                    foreach (var moduleRef in railway.Modules.OrderBy(x => x.ToString(), NameWithNumbersComparer.Instance))
                    {
                        IModule module;
                        if (moduleRef.TryResolve(out module))
                        {
                            activeModulesRoot.Nodes.Add(new EntityRefNode<IModule>(moduleRef, module));
                        }
                    }
                    foreach (var connection in railway.ModuleConnections.OrderBy(x => x.ToString(), NameWithNumbersComparer.Instance))
                    {
                        moduleConnectionsRoot.Nodes.Add(new EntityNode(connection, null));
                    }
                    foreach (var csRef in railway.CommandStations.OrderBy(x => x.ToString(), NameWithNumbersComparer.Instance))
                    {
                        ICommandStation cs;
                        if (csRef.TryResolve(out cs))
                        {
                            activeCommandStationsRoot.Nodes.Add(new EntityRefNode<ICommandStation>(csRef, cs));
                        }
                    }
                    activeLocsRoot.Expand();
                    locGroupsRoot.Expand();
                    activeModulesRoot.Expand();
                    moduleConnectionsRoot.Expand();
                    activeCommandStationsRoot.Expand();
                    foreach (var loc in package.GetLocs().OrderBy(x => x.ToString(), NameWithNumbersComparer.Instance))
                    {
                        if (!railway.Locs.ContainsId(loc.Id))
                        {
                            inactiveLocsRoot.Nodes.Add(new EntityNode(loc, true, false));
                        }
                    }
                    foreach (var module in package.GetModules().OrderBy(x => x.ToString(), NameWithNumbersComparer.Instance))
                    {
                        if (!railway.Modules.ContainsId(module.Id))
                        {
                            inactiveModulesRoot.Nodes.Add(new EntityNode(module, true, false));
                        }
                    }
                    foreach (var cs in package.GetCommandStations().OrderBy(x => x.ToString(), NameWithNumbersComparer.Instance))
                    {
                        if (!railway.CommandStations.ContainsId(cs.Id))
                        {
                            inactiveCommandStationsRoot.Nodes.Add(new EntityNode(cs, true, false));
                        }
                    }
                }

                activeRoot.Expand();
                tvItems.EndUpdate();
                UpdateSelectedGridObject();
                ValidateRailway();
                viewEditor.ReloadView();

                var prefs = UserPreferences.Preferences;
                if (!prefs.EditLocsOpen) activeLocsRoot.Collapse();
                if (!prefs.EditLocGroupsOpen) locGroupsRoot.Collapse();
                if (!prefs.EditModulesOpen) activeModulesRoot.Collapse();
                if (!prefs.EditModuleConnectionsOpen) moduleConnectionsRoot.Collapse();
                if (!prefs.EditCommandStationsOpen) activeCommandStationsRoot.Collapse();
            }
            finally
            {
                reloading = false;
            }
        }

        /// <summary>
        /// Reload the entire package
        /// </summary>
        void IEditContext.ReloadPackage()
        {
            ReloadPackage();
        }

        /// <summary>
        /// Gets the current package
        /// </summary>
        IPackage IEditContext.Package
        {
            get { return (appState != null) ? appState.Package : null; }
        }

        /// <summary>
        /// Validate the railway for integrity
        /// </summary>
        private void ValidateRailway()
        {
            validationResultsControl.ValidateEntity(appState.Package);
            SetContextMenu(tvItems.Nodes);
        }

        /// <summary>
        /// Make sure all context menu strip properties are set.
        /// </summary>
        private void SetContextMenu(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node is EntityNode)
                {
                    node.ContextMenuStrip = contextMenuStrip;
                }
                SetContextMenu(node.Nodes);
            }
        }

        /// <summary>
        /// Update the state of the toolbar.
        /// </summary>
        private void UpdateToolbar()
        {
            var pkg = appState.Package;
            var railway = (pkg != null) ? pkg.Railway : null;
            var selection = tvItems.SelectedNodes;
            var node = tvItems.SelectedNode as EntityNode;
            var module = (node != null) ? node.Entity as IModule : null;
            tbEdit.Enabled = (module != null);
            tbArchive.Visible = selection.OfType<IEntityRefNode>().Any(x => !x.IsArchived);
            tbActivate.Visible = selection.OfType<EntityNode>().Any(x => x.IsArchived);
            tbRemove.Enabled = selection.Any();
            tbAddModuleConnection.Enabled = (railway != null) && (railway.Modules.Any());
        }

        /// <summary>
        /// Update the object shown in the grid.
        /// </summary>
        private void UpdateSelectedGridObject()
        {
            var settings = new List<object>();
            foreach (var node in tvItems.SelectedNodes.OfType<EntityNode>())
            {
                var entity = (node != null) ? node.Entity : null;
                var refNode = node as IEntityRefNode;
                var entityRef = (refNode != null) ? refNode.EntityRef : null;
                var context = new GridContext(appState, null, entityRef, viewEditor.ReloadView);
                var x = (node != null) ? entity.Accept(Default<SettingsBuilder>.Instance, context) : null;
                if (x != null)
                {
                    settings.Add(x);
                }
            }
            propertyGrid.SelectedObjects = settings.ToArray();
            UpdateToolbar();
        }

        /// <summary>
        /// Handle keyboard shortcuts
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F5:
                    tbRun_Click(this, EventArgs.Empty);
                    break;
                case Keys.Control | Keys.F5:
                    tbRunVirtual_Click(this, EventArgs.Empty);
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Add a new locomotive
        /// </summary>
        private void tbAddLoc_Click(object sender, EventArgs e)
        {
            var loc = appState.Package.AddNewLoc();
            loc.Description = Strings.NewLocDescription;
            var locRef = appState.Package.Railway.Locs.Add(loc);
            var node = new EntityRefNode<ILoc>(locRef, loc);
            activeLocsRoot.Nodes.Add(node);
            tvItems.SelectedNode = node;
            UpdateAppTitle.Fire(this);
            ValidateRailway();
        }

        /// <summary>
        /// Add a new loc group
        /// </summary>
        private void tbAddLocGroup_Click(object sender, EventArgs e)
        {
            var group = appState.Package.Railway.LocGroups.AddNew();
            group.Description = Strings.NewLocGroupDescription;
            var node = new EntityNode(group, null);
            locGroupsRoot.Nodes.Add(node);
            tvItems.SelectedNode = node;
            UpdateAppTitle.Fire(this);
            ValidateRailway();
        }

        /// <summary>
        /// Add a new module
        /// </summary>
        private void tbAddModule_Click(object sender, EventArgs e)
        {
            var module = appState.Package.AddNewModule();
            module.Description = Strings.NewModuleDescription;
            var moduleRef = appState.Package.Railway.Modules.Add(module);
            var node = new EntityRefNode<IModule>(moduleRef, module);
            activeModulesRoot.Nodes.Add(node);
            tvItems.SelectedNode = node;
            UpdateAppTitle.Fire(this);
            ValidateRailway();
        }

        /// <summary>
        /// Add a new connection between modules
        /// </summary>
        private void tbAddModuleConnection_Click(object sender, EventArgs e)
        {
            var connection = appState.Package.Railway.ModuleConnections.AddNew();
            var node = new EntityNode(connection, null);
            moduleConnectionsRoot.Nodes.Add(node);
            tvItems.SelectedNode = node;
            UpdateAppTitle.Fire(this);
            ValidateRailway();
        }

        /// <summary>
        /// Add a new locobuffer command station
        /// </summary>
        private void tbAddLocoBuffer_Click(object sender, EventArgs e)
        {
            var cs = appState.Package.AddNewLocoBufferCommandStation();
            cs.Description = Strings.NewLocoBufferDescription;
            var csRef = appState.Package.Railway.CommandStations.Add(cs);
            var node = new EntityRefNode<ICommandStation>(csRef, cs);
            activeCommandStationsRoot.Nodes.Add(node);
            tvItems.SelectedNode = node;
            UpdateAppTitle.Fire(this);
            ValidateRailway();
        }

        /// <summary>
        /// Add a new DCC over RS232 command station
        /// </summary>
        private void tbAddDccOverRs232_Click(object sender, EventArgs e)
        {
            var cs = appState.Package.AddNewDccOverRs232CommandStation();
            cs.Description = Strings.NewDccOverRs232CommandStationDescription;
            var csRef = appState.Package.Railway.CommandStations.Add(cs);
            var node = new EntityRefNode<ICommandStation>(csRef, cs);
            activeCommandStationsRoot.Nodes.Add(node);
            tvItems.SelectedNode = node;
            UpdateAppTitle.Fire(this);
            ValidateRailway();
        }

        /// <summary>
        /// Add ECoS command station
        /// </summary>
        private void tbAddEcos_Click(object sender, EventArgs e)
        {
            var cs = appState.Package.AddNewEcosCommandStation();
            cs.Description = Strings.NewEcosCommandStationDescription;
            var csRef = appState.Package.Railway.CommandStations.Add(cs);
            var node = new EntityRefNode<ICommandStation>(csRef, cs);
            activeCommandStationsRoot.Nodes.Add(node);
            tvItems.SelectedNode = node;
            UpdateAppTitle.Fire(this);
            ValidateRailway();
        }

        /// <summary>
        /// Add MQTT command station
        /// </summary>
        private void tbAddMqtt_Click(object sender, EventArgs e)
        {
            var cs = appState.Package.AddNewMqttCommandStation();
            cs.Description = Strings.NewMqttCommandStationDescription;
            var csRef = appState.Package.Railway.CommandStations.Add(cs);
            var node = new EntityRefNode<ICommandStation>(csRef, cs);
            activeCommandStationsRoot.Nodes.Add(node);
            tvItems.SelectedNode = node;
            UpdateAppTitle.Fire(this);
            ValidateRailway();
        }

        /// <summary>
        /// Add P50x command station
        /// </summary>
        private void tbbAddP50x_Click(object sender, EventArgs e)
        {
            var cs = appState.Package.AddNewP50xCommandStation();
            cs.Description = Strings.NewP50xCommandStationDescription;
            var csRef = appState.Package.Railway.CommandStations.Add(cs);
            var node = new EntityRefNode<ICommandStation>(csRef, cs);
            activeCommandStationsRoot.Nodes.Add(node);
            tvItems.SelectedNode = node;
            UpdateAppTitle.Fire(this);
            ValidateRailway();
        }

        /// <summary>
        /// Node selection has changed.
        /// </summary>
        private void tvItems_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateSelectedGridObject();
        }

        /// <summary>
        /// Selected item in module view has changed.
        /// </summary>
        private void viewEditor_SelectionChanged(object sender, System.EventArgs e)
        {
            var selection = viewEditor.SelectedEntities.FirstOrDefault();
            var node = (selection != null) ? FindNode(selection) : null;
            if (node != null)
            {
                tvItems.SelectedNode = node;
            }
        }

        /// <summary>
        /// View requests a full reload.
        /// </summary>
        private void viewEditor_Reload(object sender, EventArgs e)
        {
            ReloadPackage();
            UpdateAppTitle.Fire(this);
        }

        /// <summary>
        /// A value has changed.
        /// Update treenode.
        /// </summary>
        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            foreach (var node in tvItems.SelectedNodes.OfType<EntityNode>()) 
            {
                node.UpdateFromEntity();
            }
            propertyGrid.Refresh();
            ValidateRailway();
            UpdateAppTitle.Fire(this);
            viewEditor.ReloadView();
        }

        /// <summary>
        /// Edit the module contained in the given node (if any)
        /// </summary>
        private void EditModule(EntityNode node)
        {
            var module = (node != null) ? node.Entity as IModule : null;
            if (module != null)
            {
                using (var dialog = new EditModuleForm(appState, module, viewEditor.Context))
                {
                    dialog.ShowDialog(this);
                }
                viewEditor.ReloadView();
                ValidateRailway();
                UpdateAppTitle.Fire(this);
            }
        }

        /// <summary>
        /// Edit the selected object.
        /// </summary>
        private void tbEdit_Click(object sender, EventArgs e)
        {
            var node = tvItems.SelectedNode as EntityNode;
            EditModule(node);
        }

        /// <summary>
        /// Import from another railway package
        /// </summary>
        private void tbImport_Click(object sender, EventArgs e)
        {
            var filters = ImportFilters.Filters.ToList();
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = string.Join("|", filters.Select(x => x.OpenFileDialogFilter).ToArray());
                dialog.FilterIndex = 1;
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
                try
                {
                    var filter = filters[dialog.FilterIndex - 1];
                    if (filter.Import(appState.Package, dialog.FileName))
                    {
                        ReloadPackage();
                        UpdateAppTitle.Fire(this);
                        viewEditor.ReloadView();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Strings.ImportOfXFailedBecauseY, dialog.FileName, ex.Message), Strings.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Double click edits the clicked on item.
        /// </summary>
        private void tvItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var node = tvItems.GetNodeAt(e.Location) as EntityNode;
            EditModule(node);
        }

        /// <summary>
        /// Switch to run mode
        /// </summary>
        private void tbRun_Click(object sender, System.EventArgs e)
        {
            Run(false);
        }

        /// <summary>
        /// Switch to run (virtual) mode
        /// </summary>
        private void tbRunVirtual_Click(object sender, EventArgs e)
        {
            Run(true);
        }

        /// <summary>
        /// Handle keys in the tree.
        /// </summary>
        private void tvItems_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Enter:
                    tbEdit_Click(sender, e);
                    break;
            }
        }

        /// <summary>
        /// Switch to run mode
        /// </summary>
        private void Run(bool virtualMode)
        {
            appState.CreateRailwayState(virtualMode);
            UserPreferences.Preferences.RunMode = true;
            UserPreferences.Preferences.VirtualMode = virtualMode;
            UserPreferences.SaveNow();            
        }

        /// <summary>
        /// Archive the selected entity.
        /// </summary>
        private void tbArchive_Click(object sender, EventArgs e)
        {
            var selection = tvItems.SelectedNodes.OfType<IEntityRefNode>().Where(x => !x.IsArchived).ToList();
            if (selection.Any())
            {
                foreach (var node in selection)
                {
                    node.EntityRef.Accept(Default<ArchiveVisitor>.Instance, appState.Package.Railway);
                }
                UpdateAppTitle.Fire(this);
                ReloadPackage();
            }
        }

        /// <summary>
        /// Activate the selected entity.
        /// </summary>
        private void tbActivate_Click(object sender, EventArgs e)
        {
            var selection = tvItems.SelectedNodes.OfType<EntityNode>().Where(x => x.IsArchived).ToList();
            if (selection.Any())
            {
                foreach (var node in selection)
                {
                    node.Entity.Accept(Default<ActivateVisitor>.Instance, appState.Package.Railway);
                }
                UpdateAppTitle.Fire(this);
                ReloadPackage();
            }
        }

        /// <summary>
        /// Remove the entity completely from the package.
        /// </summary>
        private void tbRemove_Click(object sender, EventArgs e)
        {
            var selection = tvItems.SelectedNodes.OfType<EntityNode>().Select(x => x.GetEntityRefOrEntity()).ToList();
            if (selection.Any())
            {
                var msg = (selection.Count == 1) ? 
                    string.Format(Strings.SuretoRemoveXFromPackage, selection[0]) :
                    string.Format(Strings.SuretoRemoveAllXEntitiesFromPackage, selection.Count);
                if (MessageBox.Show(msg, Strings.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    foreach (var entity in selection)
                    {
                        entity.Accept(Default<RemoveFromPackageVisitor>.Instance, appState.Package);
                    }
                    UpdateAppTitle.Fire(this);
                    ReloadPackage();
                }
            }
        }

        /// <summary>
        /// Validation result clicked.
        /// </summary>
        private void OnValidationResultActivated(object sender, PropertyEventArgs<ValidationResult> e)
        {
            var node = FindNode(e.Value.Entity);
            if (node != null)
            {
                tvItems.SelectedNode = node;
            }
        }

        /// <summary>
        /// Find the node referring to the given entity.
        /// </summary>
        private EntityNode FindNode(IEntity entity)
        {
            return FindNode(entity, tvItems.Nodes);
        }

        /// <summary>
        /// Find the node referring to the given entity.
        /// </summary>
        private EntityNode FindNode(IEntity entity, TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                var entityNode = node as EntityNode;
                if (entityNode != null)
                {

                    if (entityNode.Entity == entity)
                        return entityNode;
                    var entityRefNode = node as IEntityRefNode;
                    if ((entityRefNode != null) && (entityRefNode.EntityRef == entity))
                        return entityNode;
                }
                var result = FindNode(entity, node.Nodes);
                if (result != null)
                    return result;
            }
            return null;
        }

        /// <summary>
        /// Save prefs
        /// </summary>
        private void OnAfterCollapseOrExpand(object sender, TreeViewEventArgs e)
        {
            if (reloading) return;
            var prefs = UserPreferences.Preferences;
            if (activeLocsRoot.Nodes.Count > 0) prefs.EditLocsOpen = activeLocsRoot.IsExpanded;
            if (locGroupsRoot.Nodes.Count > 0) prefs.EditLocGroupsOpen = locGroupsRoot.IsExpanded;
            if (activeModulesRoot.Nodes.Count > 0) prefs.EditModulesOpen = activeModulesRoot.IsExpanded;
            if (moduleConnectionsRoot.Nodes.Count > 0) prefs.EditModuleConnectionsOpen = moduleConnectionsRoot.IsExpanded;
            if (activeCommandStationsRoot.Nodes.Count > 0) prefs.EditCommandStationsOpen = activeCommandStationsRoot.IsExpanded;
            prefs.Save();
        }

        /// <summary>
        /// Context menu strip is opening
        /// </summary>
        private void OnContextMenuStripOpening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var selectedNode = tvItems.SelectedNodes.OfType<EntityNode>().FirstOrDefault();
            var selection = (selectedNode != null) ? selectedNode.Entity : null;
            if ((selection == null) || (appState == null))
            {
                e.Cancel = true;
                return;
            }

            var builder = new ContextMenuBuilder(appState.Package, this);
            contextMenuStrip.Items.Clear();
            selection.Accept(builder, contextMenuStrip);

            if (contextMenuStrip.Items.Count == 0)
            {
                e.Cancel = true;
            }
        }
    }
}
