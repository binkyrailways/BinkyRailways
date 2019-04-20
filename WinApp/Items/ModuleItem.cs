using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Controls.VirtualCanvas.Items;
using BinkyRailways.WinApp.Controls.VirtualCanvas.Layout;
using BinkyRailways.WinApp.Items.Edit;
using BinkyRailways.WinApp.Items.Messages;
using BinkyRailways.WinApp.TypeConverters;

namespace BinkyRailways.WinApp.Items
{
    /// <summary>
    /// Item showing an entire module
    /// </summary>
    public abstract class ModuleItem : VCItemContainer
    {
        /// <summary>
        /// The selection has changed.
        /// </summary>
        public event EventHandler SelectionChanged;

        private readonly IModuleRef moduleRef;
        private readonly IModule module;
        private readonly ItemContext context;
        private readonly PositionedItemContainer positionedEntities;
        private VCImageItem backgroundImageItem;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected ModuleItem(IModuleRef moduleRef, IModule module, bool contentsEditable, ItemContext context)
        {
            this.moduleRef = moduleRef;
            this.context = context;
            this.module = module;
            LayoutManager = new StackLayoutManager {
                HorizontalAlignment = HorizontalAlignment.Left, 
                VerticalAlignment = VerticalAlignment.Top,
                Padding = new Padding(0)                
            };

            // Build positioned entities layer
            positionedEntities = new PositionedItemContainer();
            positionedEntities.SelectedItems.Changed += (s, _) => SelectionChanged.Fire(this);
            if (contentsEditable)
            {
                var selectHandler = new ItemSelectMouseHandler(positionedEntities, positionedEntities.MouseHandler);
                var moveHandler = new ItemMoveMouseHandler(positionedEntities, selectHandler);
                var resizeHandler = new ItemResizeMouseHandler(positionedEntities, moveHandler);
                positionedEntities.MouseHandler = resizeHandler;
            }
            Items.Add(positionedEntities, new LayoutConstraints(FillDirection.Both));

            // Build background
            ReloadBackgroundImage();
        }

        /// <summary>
        /// Gets the container for all positioned items
        /// </summary>
        protected PositionedItemContainer PositionedEntities
        {
            get { return positionedEntities; }
        }

        /// <summary>
        /// Try to get the module item showing the given module
        /// </summary>
        internal IPositionedEntityItem GetItem(IPositionedEntity entity)
        {
            return positionedEntities.GetItem(entity);
        }

        /// <summary>
        /// Gets the drawing context
        /// </summary>
        public ItemContext Context
        {
            get { return context; }
        }

        /// <summary>
        /// Gets reference to module shown in this item
        /// </summary>
        public IModuleRef ModuleRef
        {
            get { return moduleRef; }
        }

        /// <summary>
        /// Gets the module shown in this item
        /// </summary>
        public IModule Module
        {
            get { return module; }
        }

        /// <summary>
        /// Create a tooltip for this item
        /// </summary>
        public virtual string ToolTip { get { return module.ToString(); } }

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
        public abstract void ShowContextMenu(PointF pt);

        /// <summary>
        /// Fill the entries of a context menu
        /// </summary>
        public virtual void BuildContextMenu(ContextMenuStrip menu)
        {
            // Override me
        }

        /// <summary>
        /// Gets the location of this module
        /// </summary>
        public Point PreferredLocation
        {
            get { return (moduleRef != null) ? new Point(moduleRef.X, moduleRef.Y) : Point.Empty; }
        }

        /// <summary>
        /// Make the given entity the selection.
        /// </summary>
        public void SetSelection(IEntity entity)
        {
            var selectedItems = positionedEntities.SelectedItems;
            selectedItems.Clear();
            var placement = positionedEntities.Items.FirstOrDefault(
                    x => (x.Item is IEntityItem) && (((IEntityItem)x.Item).Entity == entity));
            if (placement != null)
            {
                selectedItems.Add(placement);
            }
        }

        /// <summary>
        /// Are there selected entities?
        /// </summary>
        public bool HasSelection { get { return positionedEntities.SelectedItems.Any(); } }

        /// <summary>
        /// Gets all selected entities.
        /// </summary>
        public IEnumerable<IEntity> SelectedEntities
        {
            get { return positionedEntities.SelectedItems.Select(x => x.Item).OfType<IEntityItem>().Select(x => x.Entity); }
            set
            {
                ISupportBeginUpdate beginUpdate = positionedEntities;
                using (beginUpdate.BeginUpdate())
                {
                    positionedEntities.SelectedItems.Clear();
                    foreach (var entity in value)
                    {
                        var placement = positionedEntities.Items.FirstOrDefault(x => (x.Item is IEntityItem) && (((IEntityItem)x.Item).Entity == entity));
                        if (placement != null)
                        {
                            positionedEntities.SelectedItems.Add(placement);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Show a new background image
        /// </summary>
        internal void ReloadBackgroundImage()
        {
            // Build background
            using (BeginUpdate())
            {
                // Remove old image
                if (backgroundImageItem != null)
                {
                    positionedEntities.Items.Remove(backgroundImageItem);
                    backgroundImageItem = null;
                }

                // Add new images
                var imageStream = module.BackgroundImage;
                if (imageStream != null)
                {
                    var image = (Image) Default<ImageTypeConverter>.Instance.ConvertTo(imageStream, typeof (Image));
                    backgroundImageItem = new VCImageItem(image);
                    positionedEntities.Items.Insert(0, backgroundImageItem, null);
                }
            }
        }
    }
}
