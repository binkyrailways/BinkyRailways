using System.Drawing;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Items.Handlers;
using BinkyRailways.WinApp.Items.Menu;
using BinkyRailways.WinApp.Items.Messages;

namespace BinkyRailways.WinApp.Items.Edit
{
    /// <summary>
    /// Item showing an entire module
    /// </summary>
    public sealed class ModuleEditItem : ModuleItem, IEntitySelection, IPositionedEntityItem
    {
        private readonly bool contentsEditable;
        private readonly bool railwayEditable;

        /// <summary>
        /// Default ctor
        /// </summary>
        public ModuleEditItem(IModuleRef moduleRef, IModule module, bool contentsEditable, bool railwayEditable, ItemContext context)
            : base(moduleRef, module, contentsEditable, context)
        {
            this.railwayEditable = railwayEditable;
            this.contentsEditable = contentsEditable;

            // Add all items
            AddPositionedItems(module, PositionedEntities, contentsEditable, railwayEditable);

            // Record mouse handler
            MouseHandler = new ToolTipMouseHandler(MouseHandler);
        }

        /// <summary>
        /// Can the contents of this module be edited?
        /// </summary>
        internal bool ContentsEditable { get { return contentsEditable; } }

        /// <summary>
        /// Add items for each positioned items
        /// </summary>
        private void AddPositionedItems(IModule module, VCItemContainer target, bool contentsEditable, bool railwayEditable)
        {
            foreach (var entity in module.GetPositionedEntities())
            {
                var item = entity.Accept(new ItemBuilder(Context, railwayEditable), contentsEditable);
                if (item != null)
                {
                    target.Items.Add(item, null);
                    entity.PositionChanged += (s, _) => target.LayoutItems();
                }
            }            
        }

        /// <summary>
        /// Gets the preferred size of this item, expressed in units chosen for the virtual canvas.
        /// </summary>
        public override Size PreferredSize
        {
            get
            {
                if (contentsEditable)
                {
                    var sz = base.PreferredSize;
                    return new Size(sz.Width + 50, sz.Height + 50);
                }
                return new Size(Module.Width, Module.Height);
            }
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

        public override void Draw(ItemPaintEventArgs e)
        {
            var rect = new Rectangle(Point.Empty, Size);
            if (!contentsEditable)
            {
                e.Graphics.DrawRectangle(Pens.Gray, rect);
            }
            base.Draw(e);
            if (railwayEditable)
            {
                using (var brush = new SolidBrush(Color.FromArgb(128, Color.White)))
                {
                    e.Graphics.FillRectangle(brush, rect);                    
                }
                var format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;                
                e.Graphics.DrawString(Entity.Description, new Font(FontFamily.GenericSansSerif, 20), SystemBrushes.ControlText, rect, format);
            }
        }

        /// <summary>
        /// Show the context menu for this item
        /// </summary>
        public override void ShowContextMenu(PointF pt)
        {
            RaiseCustomMessage(new ShowContextMenuMessage(this, pt, this));
        }

        /// <summary>
        /// Fill the entries of a context menu
        /// </summary>
        public override void BuildContextMenu(ContextMenuStrip menu)
        {
            base.BuildContextMenu(menu);
            if (railwayEditable)
            {
                var context = Context;
                var railway = context.Railway;
                menu.Items.Add(new DisconnectModuleMenuItem(railway, Module, context));
            }
        }
    }
}
