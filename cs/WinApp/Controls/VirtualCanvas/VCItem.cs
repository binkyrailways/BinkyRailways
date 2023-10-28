using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BinkyRailways.WinApp.Utils;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas
{
    public abstract partial class VCItem : ISupportBeginUpdate
    {
        /// <summary>
        /// This event is used to communicate with the container of this item.
        /// </summary>
        public event EventHandler<ContainerEventArgs> ContainerEvent;

        #region Keyboard event
        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyUp;
        #endregion

        #region Drag/drop events
        public event EventHandler<Handlers.ItemDragEventArgs> DragDrop;
        public event EventHandler DragEnter;
        public event EventHandler DragLeave;
        public event EventHandler<Handlers.ItemDragEventArgs> DragOver;
        #endregion

        #region Private members
        private Handlers.KeyboardHandler keyboardHandler;
        private Handlers.MouseHandler mouseHandler;
        private Handlers.DragDropHandler dragDropHandler;
        private bool visible = true;
        private int updateDepth = 0;
        #endregion

        /// <summary>
        /// Priority is used to order items.
        /// Low priority is drawn first, high is drawn on top.
        /// </summary>
        public virtual int Priority { get { return 0; } }

        /// <summary>
        /// Gets the preferred size of this item, expressed in units chosen for the virtual canvas.
        /// </summary>
        public abstract Size PreferredSize { get; }

        /// <summary>
        /// Gets / sets size assigned to this item.
        /// The size is expressed in units chosen for the virtual canvas.
        /// </summary>
        public abstract Size Size { get; set; }

        /// <summary>
        /// Is this item visible.
        /// </summary>
        public virtual bool Visible
        {
            get { return visible; }
            set
            {
                if (visible != value)
                {
                    visible = value;
                    RaiseSizeChanged();
                }
            }
        }

        /// <summary>
        /// Draw this item on the given graphics.
        /// The graphics is transformed such that this item can draw starting at (0, 0).
        /// </summary>
        public virtual void Draw(ItemPaintEventArgs e)
        {
            if (keyboardHandler != null) { keyboardHandler.OnPostPaint(this, e); }
            if (mouseHandler != null) { mouseHandler.OnPostPaint(this, e); }
            if (dragDropHandler != null) { dragDropHandler.OnPostPaint(this, e); }
        }

        /// <summary>
        /// Gets the region occupied by this item when it's located at 0,0 in the coordinate space
        /// of the container.
        /// </summary>
        public virtual Region GetRegion()
        {
            return new Region(new Rectangle(Point.Empty, Size));
        }

        /// <summary>
        /// Gets / sets handler to control keyboard behavior.
        /// </summary>
        [Browsable(false), ReadOnly(true)]
        public Handlers.KeyboardHandler KeyboardHandler
        {
            get { return keyboardHandler; }
            set
            {
                if (value != keyboardHandler)
                {
                    keyboardHandler = value;
                    RaiseRedraw();
                }
            }
        }

        /// <summary>
        /// Gets / sets handler to control mouse behavior.
        /// </summary>
        [Browsable(false), ReadOnly(true)]
        public Handlers.MouseHandler MouseHandler
        {
            get { return mouseHandler; }
            set
            {
                if (value != mouseHandler)
                {
                    if (mouseHandler != null)
                    {
                        mouseHandler.OnMouseLeave(this, EventArgs.Empty);
                    }
                    mouseHandler = value;
                    RaiseRedraw();
                }
            }
        }

        /// <summary>
        /// Gets / sets handler to control drag/drop behavior.
        /// </summary>
        [Browsable(false), ReadOnly(true)]
        public Handlers.DragDropHandler DragDropHandler
        {
            get { return dragDropHandler; }
            set
            {
                if (value != dragDropHandler)
                {
                    if (dragDropHandler != null)
                    {
                        dragDropHandler.OnDragLeave(this, EventArgs.Empty);
                    }
                    dragDropHandler = value;
                    RaiseRedraw();
                }
            }
        }

        /// <summary>
        /// Catch desired keys
        /// </summary>
        internal bool IsInputKey(Keys keyData)
        {
            if (keyboardHandler != null) { return keyboardHandler.IsInputKey(keyData); }
            return false;
        }

        /// <summary>
        /// Key down event
        /// </summary>
        internal bool OnKeyDown(Handlers.ItemKeyEventArgs e)
        {
            bool result = false;
            if (keyboardHandler != null) { result |= keyboardHandler.OnKeyDown(this, e); }
            if (KeyDown != null) { KeyDown(this, e); }
            return result;
        }

        /// <summary>
        /// Key up event
        /// </summary>
        internal bool OnKeyUp(Handlers.ItemKeyEventArgs e)
        {
            bool result = false;
            if (keyboardHandler != null) { result |= keyboardHandler.OnKeyUp(this, e); }
            if (KeyUp != null) { KeyUp(this, e); }
            return result;
        }

        /// <summary>
        /// Mouse is clicked on this item
        /// </summary>
        /// <param name="e"></param>
        internal bool OnMouseClick(Handlers.ItemMouseEventArgs e) 
        {
            bool result = false;
            if (mouseHandler != null) { result |= mouseHandler.OnMouseClick(this, e); }
            return result;
        }

        /// <summary>
        /// Mouse is double clicked on this item
        /// </summary>
        /// <param name="e"></param>
        internal bool OnMouseDoubleClick(Handlers.ItemMouseEventArgs e)
        {
            bool result = false;
            if (mouseHandler != null) { result |= mouseHandler.OnMouseDoubleClick(this, e); }
            return result;
        }

        /// <summary>
        /// Mouse button down on this item
        /// </summary>
        /// <param name="e"></param>
        internal bool OnMouseDown(Handlers.ItemMouseEventArgs e)
        {
            bool result = false;
            if (mouseHandler != null) { result |= mouseHandler.OnMouseDown(this, e); }
            return result;
        }

        /// <summary>
        /// Mouse button up on this item
        /// </summary>
        /// <param name="e"></param>
        internal bool OnMouseUp(Handlers.ItemMouseEventArgs e)
        {
            bool result = false;
            if (mouseHandler != null) { result |= mouseHandler.OnMouseUp(this, e); }
            return result;
        }

        /// <summary>
        /// Mouse move within this item
        /// </summary>
        /// <param name="e"></param>
        internal bool OnMouseMove(Handlers.ItemMouseEventArgs e)
        {
            bool result = false;
            if (mouseHandler != null) { result |= mouseHandler.OnMouseMove(this, e); }
            return result;
        }

        /// <summary>
        /// Mouse has entered this item
        /// </summary>
        /// <param name="e"></param>
        internal void OnMouseEnter(EventArgs e)
        {
            if (mouseHandler != null) { mouseHandler.OnMouseEnter(this, e); }
        }

        /// <summary>
        /// Mouse has left this item
        /// </summary>
        /// <param name="e"></param>
        internal void OnMouseLeave(EventArgs e)
        {
            if (mouseHandler != null) { mouseHandler.OnMouseLeave(this, e); }
        }

        /// <summary>
        /// Occurs when a drag-and-drop operation is completed.
        /// </summary>
        /// <param name="e"></param>
        internal bool OnDragDrop(Handlers.ItemDragEventArgs e)
        {
            bool result = false;
            if (dragDropHandler != null) { result |= dragDropHandler.OnDragDrop(this, e); }
            if (DragDrop != null) { DragDrop(this, e); }
            return result;
        }

        /// <summary>
        /// Occurs when an object is dragged into the item's bounds. 
        /// </summary>
        /// <param name="e"></param>
        internal void OnDragEnter(EventArgs e)
        {
            if (dragDropHandler != null) { dragDropHandler.OnDragEnter(this, e); }
            if (DragEnter != null) { DragEnter(this, e); }
        }

        /// <summary>
        /// Occurs when an object is dragged out of the item's bounds. 
        /// </summary>
        /// <param name="e"></param>
        internal void OnDragLeave(EventArgs e)
        {
            if (dragDropHandler != null) { dragDropHandler.OnDragLeave(this, e); }
            if (DragLeave != null) { DragLeave(this, e); }
        }

        /// <summary>
        /// Occurs when an object is dragged over the item's bounds. 
        /// </summary>
        /// <param name="e"></param>
        internal bool OnDragOver(Handlers.ItemDragEventArgs e)
        {
            bool result = false;
            if (dragDropHandler != null) { result |= dragDropHandler.OnDragOver(this, e); }
            if (DragOver != null) { DragOver(this, e); }
            return result;
        }

        /// <summary>
        /// This method is called when the item is no longer visible.
        /// </summary>
        /// <param name="visible"></param>
        public virtual void OnLostVisibility()
        {
        }

        /// <summary>
        /// Zoom the virtual canvas to fit this item.
        /// </summary>
        public void ZoomToFit()
        {
            using (var update = BeginUpdate())
            {

                Size sz = PreferredSize;
                RaiseZoomToRectangle(new Rectangle(Point.Empty, sz));

                // Do it a second time.
                // The first time may have changed the zoom factor and our position.
                sz = PreferredSize;
                RaiseZoomToRectangle(new Rectangle(Point.Empty, sz));
            }
        }

        /// <summary>
        /// Redraw this item
        /// </summary>
        public void Invalidate()
        {
            RaiseRedraw();
        }

        /// <summary>
        /// Notify the canvas to block update request till Dispose is called on the returned object.
        /// </summary>
        IDisposable ISupportBeginUpdate.BeginUpdate()
        {
            return BeginUpdate();
        }

        /// <summary>
        /// Notify the canvas to block update request till EndUpdate.
        /// </summary>
        protected IDisposable BeginUpdate()
        {
            var update = new Update(this);
            updateDepth++;
            if ((updateDepth == 1) && (ContainerEvent != null))
            {
                var args = new ContainerEventArgs(ContainerMessage.BeginUpdate, this) { Update = update };
                ContainerEvent(this, args);
            }
            return update;
        }

        /// <summary>
        /// Notify the canvas to resume update request.
        /// </summary>
        private void EndUpdate()
        {
            try
            {
                updateDepth--;
            }
            finally
            {
                if (updateDepth == 0)
                {
                    OnUpdateCompleted();
                }
            }
        }

        /// <summary>
        /// This method is called in EndUpdate when all updates are done.
        /// </summary>
        protected virtual void OnUpdateCompleted()
        {
        }

        /// <summary>
        /// Is this item in an Begin/EndUpdate cycle.
        /// </summary>
        public bool IsUpdating
        {
            get { return (updateDepth > 0); }
        }

        /// <summary>
        /// Fire the SizeChanged event
        /// </summary>
        protected void RaiseSizeChanged()
        {
            if (ContainerEvent != null)
            {
                ContainerEvent(this, new ContainerEventArgs(ContainerMessage.SizeChanged, this));
            }
        }

        /// <summary>
        /// Fire the Redraw event
        /// </summary>
        protected void RaiseRedraw()
        {
            if (ContainerEvent != null)
            {
                ContainerEvent(this, new ContainerEventArgs(ContainerMessage.Redraw, this));
            }
        }

        /// <summary>
        /// Fire the ZoomToRectangle event
        /// </summary>
        protected void RaiseZoomToRectangle(RectangleF rectangle)
        {
            if (ContainerEvent != null)
            {
                ContainerEvent(this, new ContainerEventArgs(ContainerMessage.ZoomToRectangle, this, rectangle));
            }
        }

        /// <summary>
        /// Fire the CustomMessage event
        /// </summary>
        protected void RaiseCustomMessage(object argument)
        {
            if (ContainerEvent != null)
            {
                ContainerEvent(this, new ContainerEventArgs(ContainerMessage.CustomMessage, this, argument));
            }
        }

        /// <summary>
        /// Fire the ContainerEvent with given argument.
        /// </summary>
        /// <param name="e"></param>
        protected void RaiseContainerEvent(ContainerEventArgs e)
        {
            if (ContainerEvent != null)
            {
                ContainerEvent(this, e);
            }
        }

        /// <summary>
        /// Gets a transformation used to convert from local space to control space.
        /// </summary>
        public Matrix GetLocal2ControlTransform()
        {
            var args = new ContainerEventArgs(ContainerMessage.Local2ControlMatrix, this, new Matrix());
            RaiseContainerEvent(args);
            return args.Transform;
        }

        /// <summary>
        /// Gets a transformation used to convert from control space to local space.
        /// </summary>
        public Matrix GetControl2LocalTransform()
        {
            var tx = GetLocal2ControlTransform();
            tx.Invert();
            return tx;
        }

        /// <summary>
        /// Convert a point from local space to control space.
        /// </summary>
        public Point Local2Control(Point pt)
        {
            return GetLocal2ControlTransform().Transform(pt);
        }

        /// <summary>
        /// Convert a point from local space to control space.
        /// </summary>
        public PointF Local2Control(PointF pt)
        {
            return GetLocal2ControlTransform().Transform(pt);
        }

        /// <summary>
        /// Convert a rectangle from local space to control space.
        /// </summary>
        public Rectangle Local2Control(Rectangle rect)
        {
            return GetLocal2ControlTransform().Transform(rect);
        }

        /// <summary>
        /// Convert a rectangle from local space to control space.
        /// </summary>
        public RectangleF Local2Control(RectangleF rect)
        {
            return GetLocal2ControlTransform().Transform(rect);
        }

        /// <summary>
        /// Convert a point from control space to local space.
        /// </summary>
        public Point Control2Local(Point pt)
        {
            return GetControl2LocalTransform().Transform(pt);
        }

        /// <summary>
        /// Convert a point from control space to local space.
        /// </summary>
        public PointF Control2Local(PointF pt)
        {
            return GetControl2LocalTransform().Transform(pt);
        }

        /// <summary>
        /// Convert a rectangle from control space to local space.
        /// </summary>
        public Rectangle Control2Local(Rectangle rect)
        {
            return GetControl2LocalTransform().Transform(rect);
        }

        /// <summary>
        /// Convert a rectangle from control space to local space.
        /// </summary>
        public RectangleF Control2Local(RectangleF rect)
        {
            return GetControl2LocalTransform().Transform(rect);
        }
    }
}
