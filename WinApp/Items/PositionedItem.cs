using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Items.Handlers;
using BinkyRailways.WinApp.Items.Messages;

namespace BinkyRailways.WinApp.Items
{
    /// <summary>
    /// Item showing a positioned entity
    /// </summary>
    public abstract class PositionedItem<T> : VCItemContainer, IPositionedEntityItem
        where T : IPositionedEntity
    {
        private readonly T entity;
        private readonly ItemContext context;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected PositionedItem(T entity, ItemContext context)
        {
            this.entity = entity;
            this.context = context;
            Size = new Size(entity.Width, entity.Height);
            entity.PropertyChanged += (s, _) => Invalidate();
            MouseHandler = new ToolTipMouseHandler(MouseHandler);
        }

        /// <summary>
        /// Gets the drawing context
        /// </summary>
        public ItemContext Context
        {
            get { return context; }
        }

        /// <summary>
        /// Gets the preferred size of this item, expressed in units chosen for the virtual canvas.
        /// </summary>
        public override Size PreferredSize
        {
            get { return new Size(entity.Width, entity.Height); }
        }

        /// <summary>
        /// Gets / sets size assigned to this item.
        /// The size is expressed in units chosen for the virtual canvas.
        /// </summary>
        public sealed override Size Size { get; set; }

        /// <summary>
        /// Gets the represented entity
        /// </summary>
        protected T Entity
        {
            get { return entity; }
        }

        /// <summary>
        /// Create a tooltip for this item
        /// </summary>
        public virtual string ToolTip { get { return entity.ToString(); } }

        /// <summary>
        /// Start showing the tooltip of this item
        /// </summary>
        public void ShowToolTip()
        {
            RaiseCustomMessage(new ShowToolTipMessage(this, ToolTip));
        }

        /// <summary>
        /// Stop showing the tooltip of this item
        /// </summary>
        public void HideToolTip()
        {
            RaiseCustomMessage(new ShowToolTipMessage(this, null));
        }

        /// <summary>
        /// Show the context menu for this item
        /// </summary>
        public void ShowContextMenu(PointF pt)
        {
            RaiseCustomMessage(new ShowContextMenuMessage(this, pt, this));
        }

        /// <summary>
        /// Fill the entries of a context menu
        /// </summary>
        public virtual void BuildContextMenu(ContextMenuStrip menu)
        {
            // Override me
        }

        /// <summary>
        /// Gets the represented entity
        /// </summary>
        IEntity IEntityItem.Entity
        {
            get { return entity; }
        }

        /// <summary>
        /// Gets the represented entity
        /// </summary>
        IPositionedEntity IPositionedEntityItem.Entity
        {
            get { return entity; }
        }

        /// <summary>
        /// Draw this item.
        /// Setup orientation and call DrawItem
        /// </summary>
        public sealed override void Draw(ItemPaintEventArgs e)
        {
            var w = Entity.Width;
            var h = Entity.Height;
            var state = e.Graphics.Save();
            try
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                base.Draw(e);
                DrawItem(e, new Size(w, h));
            }
            finally
            {
                e.Graphics.Restore(state);
            }
        }

        /// <summary>
        /// Perform a classic container draw.
        /// </summary>
        protected void DrawContainer(ItemPaintEventArgs e)
        {
            base.Draw(e);
        }

        /// <summary>
        /// Draw this item.
        /// Orientation is already handled.
        /// </summary>
        /// <param name="e">Drawing arguments</param>
        /// <param name="sz">Size of item in pixels</param>
        protected abstract void DrawItem(ItemPaintEventArgs e, Size sz);
    }
}
