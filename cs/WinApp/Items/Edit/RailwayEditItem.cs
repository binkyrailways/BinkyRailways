using System;
using System.Collections.Generic;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Controls.VirtualCanvas.Layout;

namespace BinkyRailways.WinApp.Items.Edit
{
    /// <summary>
    /// Item showing an entire module
    /// </summary>
    public sealed class RailwayEditItem : RailwayItem
    {
        /// <summary>
        /// The selection has changed.
        /// </summary>
        public event EventHandler SelectionChanged;

        private readonly PositionedItemContainer modulesLayer;

        /// <summary>
        /// Default ctor
        /// </summary>
        public RailwayEditItem(IRailway railway, ItemContext context)
            : base(railway, context)
        {
            modulesLayer = new PositionedItemContainer();
            modulesLayer.SelectedItems.Changed += (s, _) => SelectionChanged.Fire(this);

            var selectHandler = new ItemSelectMouseHandler(modulesLayer, modulesLayer.MouseHandler);
            var moveHandler = new ItemMoveMouseHandler(modulesLayer, selectHandler);
            modulesLayer.MouseHandler = moveHandler;

            Items.Add(modulesLayer, new LayoutConstraints(FillDirection.Both));

            AddModules(railway, modulesLayer);
        }

        /// <summary>
        /// Add items for each positioned items
        /// </summary>
        private void AddModules(IRailway railway, VCItemContainer target)
        {
            foreach (var moduleRef in railway.Modules)
            {
                IModule module;
                if (moduleRef.TryResolve(out module))
                {
                    var item = new ModuleEditItem(moduleRef, module, false, true, Context);
                    target.Items.Add(item, null);
                    moduleRef.PositionChanged += (s, _) => target.LayoutItems();
                }
            }            
        }

        /// <summary>
        /// Try to get the module item showing the given module
        /// </summary>
        protected override ModuleItem GetModuleItem(IModule module)
        {
            return modulesLayer.Items.Select(x => x.Item).OfType<ModuleEditItem>().FirstOrDefault(x => x.Module == module);
        }

        /// <summary>
        /// Make the given entity the selection.
        /// </summary>
        public void SetSelection(IEntity entity)
        {
            var selectedItems = modulesLayer.SelectedItems;
            selectedItems.Clear();
            var placement = modulesLayer.Items.FirstOrDefault(
                    x => (x.Item is IEntityItem) && (((IEntityItem)x.Item).Entity == entity));
            if (placement != null)
            {
                selectedItems.Add(placement);
            }
        }

        /// <summary>
        /// Are there selected entities?
        /// </summary>
        public bool HasSelection { get { return modulesLayer.SelectedItems.Any(); } }

        /// <summary>
        /// Gets all selected entities.
        /// </summary>
        public IEnumerable<IEntity> SelectedEntities
        {
            get { return modulesLayer.SelectedItems.Select(x => x.Item).OfType<IEntityItem>().Select(x => x.Entity); }
        }
    }
}
