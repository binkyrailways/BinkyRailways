using System.Drawing;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using NLog;

namespace BinkyRailways.WinApp.Items.Run
{
    /// <summary>
    /// Item showing an entire module
    /// </summary>
    public sealed class ModuleRunItem : ModuleItem, IPositionedEntityItem
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        private readonly bool contentsEditable;

        /// <summary>
        /// Default ctor
        /// </summary>
        public ModuleRunItem(IRailwayState railwayState, IModuleRef moduleRef, IModule module, bool contentsEditable, ItemContext context)
            : base(moduleRef, module, contentsEditable, context)
        {
            this.contentsEditable = contentsEditable;
            // Add all items
            AddPositionedItems(railwayState, module, PositionedEntities);
        }

        /// <summary>
        /// Add items for each positioned items
        /// </summary>
        private void AddPositionedItems(IRailwayState railwayState, IModule module, VCItemContainer target)
        {
            foreach (var entity in module.GetPositionedEntities())
            {
                var state = railwayState.GetState(entity);
                if (state != null)
                {
                    var item = entity.Accept(new ItemBuilder(Context, !contentsEditable), state);
                    if (item != null)
                    {
                        state.ActualStateChanged += (s, _) => item.Invalidate();                        
                        target.Items.Add(item, null);
                        entity.PositionChanged += (s, _) => target.LayoutItems();
                    }
                }
            }            
        }

        /// <summary>
        /// Gets the preferred size of this item, expressed in units chosen for the virtual canvas.
        /// </summary>
        public override Size PreferredSize
        {
            get { return new Size(Module.Width, Module.Height); }
        }

        /// <summary>
        /// Gets the represented entity
        /// </summary>
        IEntity IEntityItem.Entity
        {
            get { return Entity; }
        }

        /// <summary>
        /// Gets the represented entity
        /// </summary>
        public IPositionedEntity Entity
        {
            get { return ModuleRef; }
        }

        /// <summary>
        /// Show the context menu for this item
        /// </summary>
        public override void ShowContextMenu(PointF pt)
        {
            // Do nothing
        }
    }
}
