using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;

namespace BinkyRailways.WinApp.Items.Run
{
    /// <summary>
    /// Item showing an entire module
    /// </summary>
    public sealed class RailwayRunItem : RailwayItem
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public RailwayRunItem(IRailwayState railwayState, ItemContext context)
            : base(railwayState.Model, context)
        {
            AddModules(railwayState);
        }

        /// <summary>
        /// Add items for each positioned items
        /// </summary>
        private void AddModules(IRailwayState railwayState)
        {
            var railway = railwayState.Model;
            foreach (var moduleRef in railway.Modules)
            {
                IModule module;
                if (moduleRef.TryResolve(out module))
                {
                    var item = new ModuleRunItem(railwayState, moduleRef, module, false, Context);
                    Items.Add(item, null);
                }
            }            
        }

        /// <summary>
        /// Try to get the module item showing the given module
        /// </summary>
        protected override ModuleItem GetModuleItem(IModule module)
        {
            return Items.Select(x => x.Item).OfType<ModuleRunItem>().FirstOrDefault(x => x.Module == module);
        }
    }
}
