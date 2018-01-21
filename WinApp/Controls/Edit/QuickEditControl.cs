using System;
using System.Linq;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;
using BinkyRailways.Core.Util;
using BinkyRailways.WinApp.Controls.Edit.Settings;
using BinkyRailways.WinApp.Items;

namespace BinkyRailways.WinApp.Controls.Edit
{
    /// <summary>
    /// Outer control used to edit a module
    /// </summary>
    public partial class QuickEditControl : UserControl
    {
        private AppState appState;
        private IRailway railway;
        private ItemContext context;
        private IModule firstModule;
        private IModule visibleModule;

        /// <summary>
        /// Default ctor
        /// </summary>
        public QuickEditControl()
        {
            InitializeComponent();
            tvItems.SelectionManager = new SelectionManager();
            viewEditor.SelectionManager = tvItems.SelectionManager;
            tvItems.SelectionManager.Changed += OnSelectionChanged;
            outerSplitContainer.Panel1MinSize = 100;
            outerSplitContainer.Panel2MinSize = 300;
        }

        /// <summary>
        /// Connect to the given application state
        /// </summary>
        internal void Initialize(AppState appState, IRailway railway, ItemContext context)
        {
            this.appState = appState;
            this.railway = railway;
            this.context = context;
            ReloadNodes();
        }

        /// <summary>
        /// Select the given state
        /// </summary>
        internal void Select(IEntityState selection)
        {
            if (selection == null)
                return;
            var node = FindNodeForState(selection);
            if (node != null)
            {
                var selMgr = tvItems.SelectionManager;
                using (selMgr.BeginUpdate())
                {
                    selMgr.Clear();
                    selMgr.Add(node.Entity);
                }
            }
        }

        /// <summary>
        /// Reload the package data into this control
        /// </summary>
        private void ReloadNodes()
        {
            using (tvItems.BeginUpdate())
            {

                var modulesNodes = new TreeNode("Modules");
                var locsNodes = new TreeNode("Locs");
                var locGroupsNodes = new TreeNode("Loc groups");
                tvItems.Nodes.Add(new EntityNode(railway, null));
                tvItems.Nodes.Add(locsNodes);
                tvItems.Nodes.Add(locGroupsNodes);
                tvItems.Nodes.Add(modulesNodes);

                foreach (var locRef in railway.Locs.OrderBy(x => x.ToString()))
                {
                    ILoc loc;
                    if (locRef.TryResolve(out loc))
                    {
                        locsNodes.Nodes.Add(new EntityRefNode<ILoc>(locRef, loc));
                    }
                }
                foreach (var locGroup in railway.LocGroups.OrderBy(x => x.ToString()))
                {
                    locGroupsNodes.Nodes.Add(new EntityNode(locGroup, null));
                }
                foreach (var moduleRef in railway.Modules.OrderBy(x => x.ToString()))
                {
                    IModule module;
                    if (moduleRef.TryResolve(out module))
                    {
                        if (firstModule == null)
                            firstModule = module;
                        var moduleNode = new EntityRefNode<IModule>(moduleRef, module);
                        modulesNodes.Nodes.Add(moduleNode);

                        var moduleTree = new ModuleTree(moduleNode.Nodes);
                        moduleTree.ReloadModule(module, null, false);
                        moduleNode.Collapse(false);
                    }
                }

                tvItems.ExpandAll();
            }
        }

        /// <summary>
        /// Node selection has changed.
        /// </summary>
        private void OnSelectionChanged(object sender, EventArgs e)
        {
            var selMgr = tvItems.SelectionManager;
            var module = GetSelectedModule();
            if ((module != null) && (module != visibleModule))
            {
                viewEditor.Initialize(appState, module, context, true);
                visibleModule = module;
            }

            // Setup properties grid
            var gridContext = new GridContext(appState, module, () => viewEditor.ReloadModule());
            var settings = selMgr.SelectedEntities.Select(x => x.Accept(Default<SettingsBuilder>.Instance, gridContext));
            grid.InRunningState = true;
            grid.SelectedObjects = settings.Where(x => x != null).ToArray();
        }

        /// <summary>
        /// Gets the best module to display.
        /// </summary>
        private IModule GetSelectedModule()
        {
            var selMgr = tvItems.SelectionManager;
            var module = selMgr.SelectedEntities.OfType<IModule>().FirstOrDefault();
            if (module != null)
                return module;
            return selMgr.SelectedEntities.Select(x => tvItems.GetModule(x)).FirstOrDefault();
        }

        /// <summary>
        /// Search for the entity node that represents the entity in the given state.
        /// Returns null if not found.
        /// </summary>
        private EntityNode FindNodeForState(IEntityState state)
        {
            return FindNodeForState(state, tvItems.Nodes);
        }

        /// <summary>
        /// Search for the entity node that represents the entity in the given state.
        /// Returns null if not found.
        /// </summary>
        private static EntityNode FindNodeForState(IEntityState state, TreeNodeCollection nodes)
        {
            var result = nodes.OfType<EntityNode>().FirstOrDefault(x => state.IsStateOf(x.Entity));
            if (result != null)
                return result;
            return nodes.Cast<TreeNode>().Select(x => FindNodeForState(state, x.Nodes)).FirstOrDefault(x => x != null);
        }
    }
}
