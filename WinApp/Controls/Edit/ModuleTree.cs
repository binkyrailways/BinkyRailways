using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;
using BinkyRailways.WinApp.Preferences;

namespace BinkyRailways.WinApp.Controls.Edit
{
    /// <summary>
    /// Helper for generating a tree on entities in a module.
    /// </summary>
    internal class ModuleTree
    {
        private readonly TreeNodeCollection parent;
        private readonly TreeNode blocksRoot;
        private readonly TreeNode junctionsRoot;
        private readonly TreeNode sensorsRoot;
        private readonly TreeNode outputsRoot;
        private readonly TreeNode routesRoot;
        private readonly TreeNode signalsRoot;
        private readonly TreeNode edgesRoot;
        private EntityNode moduleNode;
        private bool reloading;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal ModuleTree(TreeNodeCollection parent)
        {
            this.parent = parent;
            blocksRoot = new TreeNode("Blocks");
            junctionsRoot = new TreeNode("Junctions");
            sensorsRoot = new TreeNode("Sensors");
            outputsRoot = new TreeNode("Outputs");
            routesRoot = new TreeNode("Routes");
            signalsRoot = new TreeNode("Signals");
            edgesRoot = new TreeNode("Edges");
            parent.Add(blocksRoot);
            parent.Add(junctionsRoot);
            parent.Add(sensorsRoot);
            parent.Add(signalsRoot);
            parent.Add(edgesRoot);
            parent.Add(routesRoot);
            parent.Add(outputsRoot);
        }

        /// <summary>
        /// Reload the package data into this control
        /// </summary>
        internal void ReloadModule(IModule module, ContextMenuStrip contextMenus, bool addModuleNode)
        {
            try
            {
                reloading = true;
                blocksRoot.Nodes.Clear();
                junctionsRoot.Nodes.Clear();
                sensorsRoot.Nodes.Clear();
                signalsRoot.Nodes.Clear();
                edgesRoot.Nodes.Clear();
                routesRoot.Nodes.Clear();
                outputsRoot.Nodes.Clear();

                if (moduleNode != null)
                {
                    parent.Remove(moduleNode);
                    moduleNode = null;
                }

                if (module != null)
                {
                    var prefs = UserPreferences.Preferences;
                    if (addModuleNode)
                    {
                        moduleNode = new EntityNode(module, false, true) { Text = "Module" };
                        parent.Insert(0, moduleNode);
                    }
                    blocksRoot.Nodes.AddRange(module.Blocks.Select(x => new EntityNode(x, contextMenus)).OrderBy(x => x.Text, NameWithNumbersComparer.Instance).ToArray());
                    junctionsRoot.Nodes.AddRange(module.Junctions.Select(x => new EntityNode(x, contextMenus)).OrderBy(x => x.Text, NameWithNumbersComparer.Instance).ToArray());
                    sensorsRoot.Nodes.AddRange(module.Sensors.Select(x => new EntityNode(x, contextMenus)).OrderBy(x => x.Text, NameWithNumbersComparer.Instance).ToArray());
                    signalsRoot.Nodes.AddRange(module.Signals.Select(x => new EntityNode(x, contextMenus)).OrderBy(x => x.Text, NameWithNumbersComparer.Instance).ToArray());
                    edgesRoot.Nodes.AddRange(module.Edges.Select(x => new EntityNode(x, contextMenus)).OrderBy(x => x.Text, NameWithNumbersComparer.Instance).ToArray());
                    IEnumerable<IRoute> routes = module.Routes;
                    routes = prefs.SortRoutesByFrom ? routes.OrderBy(RouteSortByFrom) : routes.OrderBy(RouteSortByTo);
                    routesRoot.Nodes.AddRange(routes.Select(x => new EntityNode(x, contextMenus)).ToArray());
                    outputsRoot.Nodes.AddRange(module.Outputs.Select(x => new EntityNode(x, contextMenus)).OrderBy(x => x.Text, NameWithNumbersComparer.Instance).ToArray());

                    if (!prefs.EditBlocksOpen) blocksRoot.Collapse(); 
                    else blocksRoot.Expand();
                    if (!prefs.EditJunctionsOpen) junctionsRoot.Collapse();
                    else junctionsRoot.Expand();
                    if (!prefs.EditSensorsOpen) sensorsRoot.Collapse();
                    else sensorsRoot.Expand();
                    if (!prefs.EditSignalsOpen) signalsRoot.Collapse();
                    else signalsRoot.Expand();
                    if (!prefs.EditEdgesOpen) edgesRoot.Collapse();
                    else edgesRoot.Expand();
                    if (!prefs.EditRoutesOpen) routesRoot.Collapse();
                    else routesRoot.Expand();
                    if (!prefs.EditOutputsOpen) outputsRoot.Collapse();
                    else outputsRoot.Expand();
                }

                if (contextMenus != null)
                {
                    foreach (TreeNode node in parent)
                    {
                        node.ContextMenuStrip = contextMenus;
                    }
                }
            }
            finally
            {
                reloading = false;
            }
        }

        /// <summary>
        /// Save the state of the nodes.
        /// </summary>
        internal void SaveState()
        {
            if (reloading) return;
            var prefs = UserPreferences.Preferences;
            if (blocksRoot.Nodes.Count > 0) prefs.EditBlocksOpen = blocksRoot.IsExpanded;
            if (junctionsRoot.Nodes.Count > 0) prefs.EditJunctionsOpen = junctionsRoot.IsExpanded;
            if (sensorsRoot.Nodes.Count > 0) prefs.EditSensorsOpen = sensorsRoot.IsExpanded;
            if (signalsRoot.Nodes.Count > 0) prefs.EditSignalsOpen = signalsRoot.IsExpanded;
            if (edgesRoot.Nodes.Count > 0) prefs.EditEdgesOpen = edgesRoot.IsExpanded;
            if (routesRoot.Nodes.Count > 0) prefs.EditRoutesOpen = routesRoot.IsExpanded;
            if (outputsRoot.Nodes.Count > 0) prefs.EditOutputsOpen = outputsRoot.IsExpanded;
            prefs.Save();
        }

        public TreeNode BlocksRoot { get { return blocksRoot; }}
        public TreeNode JunctionsRoot { get { return junctionsRoot; }}
        public TreeNode SensorsRoot { get { return sensorsRoot; } }
        public TreeNode OutputsRoot { get { return outputsRoot; } }
        public TreeNode RoutesRoot { get { return routesRoot; } }
        public TreeNode SignalsRoot { get { return signalsRoot; } }
        public TreeNode EdgesRoot { get { return edgesRoot; } }

        /// <summary>
        /// Sort function.
        /// </summary>
        private static string RouteSortByFrom(IRoute route)
        {
            var from = (route.From != null) ? (route.From.Description ?? "?") : "-";
            var to = (route.To != null) ? (route.To.Description ?? "?") : "-";
            return NameWithNumbersComparer.ExpandNumbers(from) + "->" + NameWithNumbersComparer.ExpandNumbers(to);
        }

        /// <summary>
        /// Sort function.
        /// </summary>
        private static string RouteSortByTo(IRoute route)
        {
            var from = (route.From != null) ? (route.From.Description ?? "?") : "-";
            var to = (route.To != null) ? (route.To.Description ?? "?") : "-";
            return NameWithNumbersComparer.ExpandNumbers(to) + "<-" + NameWithNumbersComparer.ExpandNumbers(from);
        }
    }
}
