using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BinkyRailways.Core.Util;
using BinkyRailways.WinApp.Utils;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas
{
    public partial class VirtualCanvasControl : UserControl, IZoomableVCItemContainer, ISupportBeginUpdate
    {
        /// <summary>
        /// This message is fired in response to CustomMessage messages from items.
        /// </summary>
        public event EventHandler<ArgumentEventArgs> CustomItemMessage;

        /// <summary>
        /// This event is fired when the ZoomFactor setting has changed.
        /// </summary>
        public event EventHandler ZoomFactorChanged;

        private readonly RootContainer rootContainer;
        private int updateDepth = 0;
        private bool invalidateAfterUpdate = false;
        private bool updateScrollBarsAfterUpdate = false;
        private float zoomFactor = 1.0f;

        /// <summary>
        /// Default ctor
        /// </summary>
        public VirtualCanvasControl()
        {
            this.rootContainer = new RootContainer(this);
            InitializeComponent();
            this.AllowDrop = true;
            this.SetStyle(ControlStyles.Selectable, true);

            // Connect to root container
            rootContainer.ContainerEvent += OnRootContainerEvent;

            // Forward panel events
            panel.KeyDown += (sender, e) => OnKeyDown(e);
            panel.KeyUp += (sender, e) => OnKeyUp(e);
            panel.MouseClick += (sender, e) => OnMouseClick(e);
            panel.MouseDoubleClick += (sender, e) => OnMouseDoubleClick(e);
            panel.MouseDown += (sender, e) => OnMouseDown(e);
            panel.MouseMove += (sender, e) => OnMouseMove(e);
            panel.MouseUp += (sender, e) => OnMouseUp(e);
            panel.MouseEnter += (sender, e) => OnMouseEnter(e);
            panel.MouseLeave += (sender, e) => OnMouseLeave(e);
            panel.MouseWheel += (sender, e) => OnMouseWheel(e);
            panel.Click += (sender, e) => OnClick(e);
            panel.DoubleClick += (sender, e) => OnDoubleClick(e);
            panel.DragDrop += (sender, e) => OnDragDrop(e);
            panel.DragEnter += (sender, e) => OnDragEnter(e);
            panel.DragLeave += (sender, e) => OnDragLeave(e);
            panel.DragOver += (sender, e) => OnDragOver(e);
            panel.GiveFeedback += (sender, e) => OnGiveFeedback(e);
            panel.QueryContinueDrag += (sender, e) => OnQueryContinueDrag(e);

            // Connect to scrollbar events
            hScrollBar.Scroll += (sender, e) => Invalidate();
            vScrollBar.Scroll += (sender, e) => Invalidate();
        }

        /// <summary>
        /// Handle events from the root container
        /// </summary>
        private void OnRootContainerEvent(object sender, ContainerEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<ContainerEventArgs>(OnRootContainerEvent), sender, e);
            }
            else
            {
                switch (e.Message)
                {
                    case ContainerMessage.SizeChanged:
                        UpdateScrollbars();
                        Invalidate();
                        break;
                    case ContainerMessage.Redraw:
                        Invalidate();
                        break;
                    case ContainerMessage.ZoomToRectangle:
                        rootContainer.ZoomToRectangle(e.Rectangle);
                        break;
                    case ContainerMessage.BeginUpdate:
                        e.Update.Add(BeginUpdate());
                        break;
                    case ContainerMessage.EnsureVisible:
                        if (!rootContainer.IsVisible(e.Point))
                        {
                            rootContainer.ZoomTo(ZoomFactor, e.Point);
                        }
                        break;
                    case ContainerMessage.Local2ControlMatrix:
                        rootContainer.Local2Global(e.Transform);
                        break;
                    case ContainerMessage.CustomMessage:
                        if (CustomItemMessage != null)
                        {
                            CustomItemMessage(e.Sender, new ArgumentEventArgs(e.Argument));
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Form a rectangle from a top-left and bottom-right point.
        /// </summary>
        private static Rectangle GetRectangle(Point pt1, Point pt2)
        {
            var left = Math.Min(pt1.X, pt2.X);
            var right = Math.Max(pt1.X, pt2.X);
            var top = Math.Min(pt1.Y, pt2.Y);
            var bottom = Math.Max(pt1.Y, pt2.Y);
            return new Rectangle(left, top, right - left, bottom - top);
        }

        /// <summary>
        /// Form a rectangle from a top-left and bottom-right point.
        /// </summary>
        private static RectangleF GetRectangle(PointF pt1, PointF pt2)
        {
            var left = Math.Min(pt1.X, pt2.X);
            var right = Math.Max(pt1.X, pt2.X);
            var top = Math.Min(pt1.Y, pt2.Y);
            var bottom = Math.Max(pt1.Y, pt2.Y);
            return new RectangleF(left, top, right - left, bottom - top);
        }

        private static Point BottomRight(Rectangle rect) { return new Point(rect.Right, rect.Bottom); }
        private static PointF BottomRight(RectangleF rect) { return new PointF(rect.Right, rect.Bottom); }

        /// <summary>
        /// How the root is scaled.
        /// </summary>
        public float ZoomFactor
        {
            get { return zoomFactor; }
            set
            {
                if (zoomFactor == value)
                    return;
                zoomFactor = value;
                UpdateScrollbars();
                Invalidate();
                ZoomFactorChanged.Fire(this);
            }
        }

        /// <summary>
        /// Convert a point from my items space to the controls space 
        /// </summary>
        PointF IZoomableVCItemContainer.Local2Global(PointF pt)
        {
            return rootContainer.Local2Global(pt);
        }

        /// <summary>
        /// Color of the canvas background.
        /// </summary>
        public Color CanvasColor
        {
            get { return panel.BackColor; }
            set { panel.BackColor = value; }
        }

        /// <summary>
        /// X coordinate in local space of top-left point of the control.
        /// </summary>
        public int VisibleLeft
        {
            get { return hScrollBar.Visible ? (int)(hScrollBar.Value / ZoomFactor) : 0; }
            set
            {
                value = (int)(value * ZoomFactor);
                value = Math.Max(hScrollBar.Minimum, Math.Min(value, hScrollBar.Maximum - hScrollBar.LargeChange));

                if (hScrollBar.Value != value)
                {
                    hScrollBar.Value = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Y coordinate in local space of top-left point of the control.
        /// </summary>
        public int VisibleTop
        {
            get { return vScrollBar.Visible ? (int)(vScrollBar.Value / ZoomFactor) : 0; }
            set
            {
                value = (int)(value * ZoomFactor);
                value = Math.Max(vScrollBar.Minimum, Math.Min(value, vScrollBar.Maximum - vScrollBar.LargeChange));

                // Only invalidate if the visible top has actually changed.
                if (vScrollBar.Value != value)
                {
                    vScrollBar.Value = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Zoom to a specific zoomfactor.
        /// Adjust the visible part of the screen to the given center point.
        /// </summary>
        /// <param name="zoomFactor">New zoom factor</param>
        /// <param name="centerPoint">New visible center in virtual space</param>
        public void ZoomTo(float zoomFactor, Point centerPoint)
        {
            rootContainer.ZoomTo(zoomFactor, centerPoint);
        }

        /// <summary>
        /// Zoom the virtual canvas to fit the root container.
        /// </summary>
        public void ZoomToFit()
        {
            rootContainer.ZoomToFit();
        }

        /// <summary>
        /// Gets / sets handler to control mouse behavior.
        /// </summary>
        public Handlers.MouseHandler MouseHandler
        {
            get { return rootContainer.MouseHandler; }
            set { rootContainer.MouseHandler = value; }
        }

        /// <summary>
        /// Gets the item placements.
        /// </summary>
        [Browsable(false), ReadOnly(true)]
        public VCItemPlacementCollection Items
        {
            get { return rootContainer.Items; }
        }

        /// <summary>
        /// Gets all selected items within this container.
        /// </summary>
        public SelectedVCItemPlacementCollection SelectedItems
        {
            get { return rootContainer.SelectedItems; }
        }

        /// <summary>
        /// Gets / Sets layout manager used to layout items.
        /// </summary>
        [Browsable(false), ReadOnly(true)]
        public Layout.IVCLayoutManager LayoutManager
        {
            get { return rootContainer.LayoutManager; }
            set { rootContainer.LayoutManager = value; }
        }

        /// <summary>
        /// Draw in the context of the container using the given draw handler.
        /// The graphics provided to the draw handler is adjust for the 
        /// zoom factor and position of the container.
        /// </summary>
        /// <param name="g"></param>
        void IVCItemContainer.DrawContainer(ItemPaintEventArgs e, DrawContainerHandler drawHandler)
        {
            rootContainer.DrawContainer(e, drawHandler);
        }

        /// <summary>
        /// Draw all child items using the given draw handler.
        /// </summary>
        /// <param name="g"></param>
        void IVCItemContainer.DrawItems(ItemPaintEventArgs e, DrawItemHandler drawHandler)
        {
            rootContainer.DrawItems(e, drawHandler);
        }

        /// <summary>
        /// Gets the size that is available to this container.
        /// Overriding this property is only done in the root container.
        /// </summary>
        Size IVCItemContainer.AvailableSize { get { return this.PanelSize; } }

        /// <summary>
        /// Setup correct layout
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLayout(LayoutEventArgs e)
        {
            LayoutControls();
        }

        /// <summary>
        /// Pass key down to appropriate item
        /// </summary>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            using (var cursorController = new Handlers.CursorController(this))
            {
                rootContainer.OnKeyDown(new Handlers.ItemKeyEventArgs(e, cursorController));
            }
            base.OnKeyDown(e);
        }

        /// <summary>
        /// Pass key up to appropriate item
        /// </summary>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            using (var cursorController = new Handlers.CursorController(this))
            {
                rootContainer.OnKeyUp(new Handlers.ItemKeyEventArgs(e, cursorController));
            }
            base.OnKeyUp(e);
        }

        /// <summary>
        /// Pass mouse click to appropriate item
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            panel.Focus();
            using (var cursorController = new Handlers.CursorController(this))
            {
                var localArgs = rootContainer.Global2Local(e);
                rootContainer.OnMouseClick(new Handlers.ItemMouseEventArgs(localArgs, cursorController));
            }
            base.OnMouseClick(e);
        }

        /// <summary>
        /// Pass mouse double click to appropriate item
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            panel.Focus();
            using (var cursorController = new Handlers.CursorController(this))
            {
                var localArgs = rootContainer.Global2Local(e);
                rootContainer.OnMouseDoubleClick(new Handlers.ItemMouseEventArgs(localArgs, cursorController));
            }
            base.OnMouseDoubleClick(e);
        }

        /// <summary>
        /// Pass mouse down to appropriate item
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            panel.Focus();
            using (var cursorController = new Handlers.CursorController(this))
            {
                var localArgs = rootContainer.Global2Local(e);
                rootContainer.OnMouseDown(new Handlers.ItemMouseEventArgs(localArgs, cursorController));
            }
            base.OnMouseDown(e);
        }

        /// <summary>
        /// Pass mouse up to appropriate item
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            panel.Focus();
            using (var cursorController = new Handlers.CursorController(this))
            {
                var localArgs = rootContainer.Global2Local(e);
                rootContainer.OnMouseUp(new Handlers.ItemMouseEventArgs(localArgs, cursorController));
            }
            base.OnMouseUp(e);
        }

        /// <summary>
        /// Pass mouse move to appropriate item
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            using (var cursorController = new Handlers.CursorController(this))
            {
                var localArgs = rootContainer.Global2Local(e);
                rootContainer.OnMouseMove(new Handlers.ItemMouseEventArgs(localArgs, cursorController));
            }
            base.OnMouseMove(e);
        }

        /// <summary>
        /// Handle mouse leaves.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            rootContainer.OnMouseLeave(e);
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// Handle mouse scrolling
        /// </summary>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            int delta = (int)(vScrollBar.SmallChange / ZoomFactor);
            if (e.Delta < 0) { delta *= -1; }
            this.VisibleTop -= delta;
            base.OnMouseWheel(e);
        }

        /// <summary>
        /// Forward drag drop events to root container
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDragDrop(DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
            var pt = rootContainer.Global2Local(PointToClient(new Point(e.X, e.Y)));
            rootContainer.OnDragDrop(new Handlers.ItemDragEventArgs(e, pt));
            base.OnDragDrop(e);
        }

        /// <summary>
        /// Forward drag enter events to root container
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDragEnter(DragEventArgs e)
        {
            // Please note that we first call OnDragEnter, followed by OnDragOver 
            // This is because OnDragEnter is has a different signature for VCItems then for Controls.

            rootContainer.OnDragEnter(EventArgs.Empty);
            e.Effect = DragDropEffects.None;
            var pt = rootContainer.Global2Local(PointToClient(new Point(e.X, e.Y)));
            rootContainer.OnDragOver(new Handlers.ItemDragEventArgs(e, pt));
            base.OnDragEnter(e);
        }

        /// <summary>
        /// Forward drag leave events to root container
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDragLeave(EventArgs e)
        {
            rootContainer.OnDragLeave(e);
            base.OnDragLeave(e);
        }

        /// <summary>
        /// Forward drag over events to root container
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDragOver(DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
            var pt = rootContainer.Global2Local(PointToClient(new Point(e.X, e.Y)));
            rootContainer.OnDragOver(new Handlers.ItemDragEventArgs(e, pt));
            base.OnDragOver(e);
        }

        /// <summary>
        /// Update scrollbars after size change.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            using (var update = BeginUpdate())
            {
                base.OnSizeChanged(e);
                UpdateScrollbars();
                rootContainer.LayoutItems();

                if (!hScrollBar.Visible) { VisibleLeft = 0; }
                if (!vScrollBar.Visible) { VisibleTop = 0; }
            }
        }

        /// <summary>
        /// Notify root container if no longer visible
        /// </summary>
        /// <param name="e"></param>
        protected override void OnVisibleChanged(EventArgs e)
        {
            using (var update = BeginUpdate())
            {
                if (!Visible)
                {
                    rootContainer.OnLostVisibility();
                }
                else
                {
                    rootContainer.LayoutItems();
                }
                base.OnVisibleChanged(e);
            }
        }

        /// <summary>
        /// Update the scrollbar values and visibility to reflect the virtual size,
        /// visible top-left and zoomfactor.
        /// </summary>
        private void UpdateScrollbars()
        {
            if (updateDepth == 0)
            {
                updateScrollBarsAfterUpdate = false;
                Size sz = rootContainer.Size;
                int panelWidth = this.PanelWidth;
                int panelHeight = this.PanelHeight;

                // Update visibility
                bool showHSBar = sz.Width > (panelWidth + 1);
                bool showVSBar = sz.Height > (panelHeight + 1);

                hScrollBar.Visible = showHSBar;
                vScrollBar.Visible = showVSBar;

                // Update horizontal scrollbar values
                hScrollBar.Maximum = (int)Math.Max(0, sz.Width - (panelWidth * 0.75f));
                hScrollBar.SmallChange = Math.Max(1, panelWidth / 16);
                hScrollBar.LargeChange = Math.Max(1, panelWidth / 4);
                hScrollBar.Value = Math.Max(0,
                                            Math.Min(hScrollBar.Value, hScrollBar.Maximum - hScrollBar.LargeChange - 1));

                // Update vertical scrollbar values
                vScrollBar.Maximum = (int)Math.Max(0, sz.Height - (panelHeight * 0.75f));
                vScrollBar.SmallChange = Math.Max(1, panelHeight / 16);
                vScrollBar.LargeChange = Math.Max(1, panelHeight / 4);
                vScrollBar.Value = Math.Max(0,
                                            Math.Min(vScrollBar.Value, vScrollBar.Maximum - vScrollBar.LargeChange - 1));

                Invalidate();
            }
            else
            {
                updateScrollBarsAfterUpdate = true;
            }
        }

        /// <summary>
        /// Update the position of the panel and scrollbars.
        /// </summary>
        private void LayoutControls()
        {
            // Width / Height of scrollbar
            var size = ClientSize;
            int h = size.Height;
            int w = size.Width;
            int panelH = PanelHeight;
            int panelW = PanelWidth;

            panel.SetBounds(0, 0, panelW, panelH);
            hScrollBar.SetBounds(0, panelH, panelW, hScrollBar.Height);
            vScrollBar.SetBounds(panelW, 0, vScrollBar.Width, panelH);
        }

        /// <summary>
        /// Gets intended width of background panel dealing with scrollbar visibility.
        /// </summary>
        private int PanelWidth
        {
            get
            {
                var width = ClientSize.Width;
                return vScrollBar.Visible ? (width - vScrollBar.Width) : width;
            }
        }

        /// <summary>
        /// Gets intended height of background panel dealing with scrollbar visibility.
        /// </summary>
        private int PanelHeight
        {
            get
            {
                var height = ClientSize.Height;
                return hScrollBar.Visible ? (height - hScrollBar.Height) : height;
            }
        }

        /// <summary>
        /// Start an update cycle. Block all redraw requests until
        /// EndUpdate.
        /// </summary>
        public IDisposable BeginUpdate()
        {
            if (updateDepth == 0)
            {
                invalidateAfterUpdate = false;
                updateScrollBarsAfterUpdate = false;
            }
            updateDepth++;

            System.Diagnostics.Debug.WriteLine(string.Format("BeginUpdate; updateDepth={0}", updateDepth));

            return new Update(this);
        }

        /// <summary>
        /// End an update cycle.
        /// </summary>
        private delegate void EndUpdateDelegate(Action onEndUpdate);
        private void EndUpdate(Action onEndUpdate)
        {
            if (InvokeRequired)
            {
                Invoke(new EndUpdateDelegate(EndUpdate), onEndUpdate);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(string.Format("..EndUpdate; updateDepth={0}", updateDepth));

                updateDepth = Math.Max(0, updateDepth - 1);
                if (updateDepth == 0)
                {
                    if (onEndUpdate != null)
                        onEndUpdate();
                    if (updateScrollBarsAfterUpdate)
                    {
                        updateScrollBarsAfterUpdate = false;
                        UpdateScrollbars();
                    }
                    if (invalidateAfterUpdate)
                    {
                        invalidateAfterUpdate = false;
                        panel.Invalidate();
                    }
                }
            }
        }

        /// <summary>
        /// Invalid the panel so it gets repainted.
        /// </summary>
        public new void Invalidate()
        {
            if (updateDepth == 0)
            {
                panel.Invalidate();
            }
            else
            {
                invalidateAfterUpdate = true;
            }
        }

        /// <summary>
        /// Force a repaint
        /// </summary>
        public new void Refresh()
        {
            if (updateDepth == 0)
            {
                base.Refresh();
            }
            else
            {
                invalidateAfterUpdate = true;
            }            
        }

        /// <summary>
        /// Gets the size of the control in virtual units.
        /// </summary>
        private Size PanelSize
        {
            get { return new Size(PanelWidth, PanelHeight); }
        }

        /// <summary>
        /// Update utility
        /// </summary>
        private sealed new class Update : IDisposable
        {
            private readonly VirtualCanvasControl control;
            private bool disposed = false;
            private readonly int visibleLeft;
            private readonly int visibleTop;
            private readonly float zoomFactor;
            private readonly Size size;

            internal Update(VirtualCanvasControl control)
            {
                this.control = control;
                visibleLeft = control.VisibleLeft;
                visibleTop = control.VisibleTop;
                zoomFactor = control.ZoomFactor;
                size = control.rootContainer.Size;
            }

            void IDisposable.Dispose()
            {
                if (!disposed)
                {
                    disposed = true;
                    control.EndUpdate(RestoreVisibleRegion);
                }
            }

            /// <summary>
            /// Set the visible region back.
            /// </summary>
            private void RestoreVisibleRegion()
            {
                if ((Math.Abs(zoomFactor - control.ZoomFactor) < 0.001f) && (control.rootContainer.Size == size))
                {
                    control.VisibleLeft = visibleLeft;
                    control.VisibleTop = visibleTop;
                }
            }
        }

        /// <summary>
        /// Background panel where the actual painting occurs.
        /// </summary>
        private sealed class ItemsPanel : Control
        {
            private readonly VirtualCanvasControl canvas;

            /// <summary>
            /// Setup styles
            /// </summary>
            public ItemsPanel(VirtualCanvasControl canvas)
            {
                this.canvas = canvas;
                SetStyle(ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.DoubleBuffer |
                    ControlStyles.Selectable |
                    ControlStyles.UserPaint, true);
            }

            /// <summary>
            /// Catch desired keys
            /// </summary>
            protected override bool IsInputKey(Keys keyData)
            {
                return canvas.rootContainer.IsInputKey(keyData);
            }

            /// <summary>
            /// Do actual painting
            /// </summary>
            /// <param name="e"></param>
            protected override void OnPaint(PaintEventArgs e)
            {
                try
                {
                    var graphics = e.Graphics;
                    graphics.Clear(BackColor);
                    var visRect = new Rectangle(Point.Empty, canvas.PanelSize);
                    canvas.rootContainer.Draw(new ItemPaintEventArgs(graphics, visRect));
                }
                catch
                {
                    // Catch errors, but ignore them
                }
            }

            /// <summary>
            /// Background is painted in Paint.
            /// </summary>
            /// <param name="pevent"></param>
            protected override void OnPaintBackground(PaintEventArgs pevent)
            {
                // Do nothing
            }
        }

        /// <summary>
        /// Container used by the control itself.
        /// </summary>
        private sealed class RootContainer : VCItemContainer
        {
            private readonly VirtualCanvasControl canvas;

            /// <summary>
            /// Default ctor
            /// </summary>
            /// <param name="canvas"></param>
            internal RootContainer(VirtualCanvasControl canvas)
            {
                this.canvas = canvas;
            }

            /// <summary>
            /// Gets size of root container.
            /// Setting is not supported.
            /// </summary>
            public override Size Size
            {
                get { return Local2Global(LayoutManager.Size); }
                set { throw new NotSupportedException(); }
            }

            /// <summary>
            /// Setup the graphics such that items can be drawn on it.
            /// Typically only override in the root container.
            /// </summary>
            protected override void SetupDrawItemsTransform(Graphics g, ref Rectangle visibleRectangle)
            {
                var scale = canvas.ZoomFactor;
                g.ScaleTransform(scale, scale);
                g.TranslateTransform(-VisibleLeft, -VisibleTop);

                var tx = g.Transform;
                tx.Invert();
                visibleRectangle = tx.Transform(visibleRectangle);
            }

            /// <summary>
            /// Zoom to a specific zoomfactor.
            /// Adjust the visible part of the screen to the given center point.
            /// </summary>
            /// <param name="zoomFactor">New zoom factor</param>
            /// <param name="centerPoint">New visible center in virtual space</param>
            public void ZoomTo(float zoomFactor, PointF centerPoint)
            {
                canvas.ZoomFactor = zoomFactor;
                zoomFactor = canvas.ZoomFactor;

                var dx = (canvas.PanelWidth / 2.0f) / zoomFactor;
                var dy = (canvas.PanelHeight / 2.0f) / zoomFactor;
                canvas.VisibleLeft = (int)(centerPoint.X - dx);
                canvas.VisibleTop = (int)(centerPoint.Y - dy);
            }

            /// <summary>
            /// Is the given point in virtual space visible on screen?
            /// </summary>
            public bool IsVisible(PointF pt)
            {
                var zoomFactor = canvas.ZoomFactor;
                var localPanelWidth = canvas.PanelWidth / zoomFactor;
                var localPanelHeight = canvas.PanelHeight / zoomFactor;
                var visibleLeft = this.VisibleLeft;
                var visibleTop = this.VisibleTop;

                return ((pt.X >= visibleLeft) && (pt.X < visibleLeft + localPanelWidth) &&
                    (pt.Y >= visibleTop) && (pt.Y < visibleTop + localPanelHeight));
            }

            /// <summary>
            /// Zoom to a given rectangle.
            /// The rectangle is in local space.
            /// </summary>
            public override void ZoomToRectangle(RectangleF rectangle)
            {
                using (var update = canvas.BeginUpdate())
                {
                    var origRect = rectangle;

                    // Adjust (correct) rectangle
                    Size sz = Global2Local(this.Size);
                    if (rectangle.Left < 0) { rectangle.Offset(-rectangle.Left, 0); }
                    if (rectangle.Top < 0) { rectangle.Offset(0, -rectangle.Top); }
                    rectangle.Width = Math.Min(rectangle.Width, sz.Width);
                    rectangle.Height = Math.Min(rectangle.Height, sz.Height);

                    Size panelSize = LayoutManager.ClientSize(canvas.PanelSize);
                    float h = (float)panelSize.Width / rectangle.Width;
                    float v = (float)panelSize.Height / rectangle.Height;
                    canvas.ZoomFactor = Math.Min(h, v);

                    panelSize = canvas.PanelSize;
                    var zoomFactor = canvas.ZoomFactor;
                    float localPanelWidth = panelSize.Width / zoomFactor;
                    float localPanelHeight = panelSize.Height / zoomFactor;
                    float dx = (localPanelWidth - rectangle.Width) / 2;
                    float dy = (localPanelHeight - rectangle.Height) / 2;

                    canvas.VisibleLeft = (int)(rectangle.Left - dx);
                    canvas.VisibleTop = (int)(rectangle.Top - dy);
                }
            }

            /// <summary>
            /// X coordinate in virtual space of top-left point of the control.
            /// </summary>
            private int VisibleLeft
            {
                get { return canvas.VisibleLeft; }
            }

            /// <summary>
            /// Y coordinate in virtual space of top-left point of the control.
            /// </summary>
            private int VisibleTop
            {
                get { return canvas.VisibleTop; }
            }

            /// <summary>
            /// The preferred size of the root container is equal to the
            /// available size in the canvas.
            /// </summary>
            protected override Size AvailableSize
            {
                get { return canvas.PanelSize; }
            }

            /// <summary>
            /// Convert a size from local (items in root container) to global (control) 
            /// </summary>
            private Size Local2Global(Size sz)
            {
                var zoomFactor = canvas.ZoomFactor;
                return new Size((int) (sz.Width * zoomFactor), (int) (sz.Height * zoomFactor));
            }

            /// <summary>
            /// Convert a size from global (control) to local (items in root container)
            /// </summary>
            private Size Global2Local(Size sz)
            {
                var zoomFactor = canvas.ZoomFactor;
                return new Size((int)(sz.Width / zoomFactor), (int)(sz.Height / zoomFactor));
            }

            /// <summary>
            /// Convert a point from the controls space to my items space
            /// </summary>
            internal Point Global2Local(Point pt)
            {
                var zoomFactor = canvas.ZoomFactor;
                return new Point((int) (pt.X / zoomFactor + VisibleLeft), (int) (pt.Y / zoomFactor + VisibleTop));
            }

            /// <summary>
            /// Convert a point from the controls space to my items space
            /// </summary>
            internal PointF Global2Local(PointF pt)
            {
                var zoomFactor = canvas.ZoomFactor;
                return new PointF((pt.X / zoomFactor + VisibleLeft), (pt.Y / zoomFactor + VisibleTop));
            }

            /// <summary>
            /// Convert a point from my items space to the controls space 
            /// </summary>
            internal Point Local2Global(Point pt)
            {
                var zoomFactor = canvas.ZoomFactor;
                return new Point((int)((pt.X - VisibleLeft) * zoomFactor), (int)((pt.Y -VisibleTop) * zoomFactor));
            }

            /// <summary>
            /// Convert a point from my items space to the controls space 
            /// </summary>
            internal PointF Local2Global(PointF pt)
            {
                var zoomFactor = canvas.ZoomFactor;
                return new PointF(((pt.X - VisibleLeft) * zoomFactor), ((pt.Y - VisibleTop) * zoomFactor));
            }

            /// <summary>
            /// Convert a point from my items space to the controls space 
            /// </summary>
            internal void Local2Global(Matrix tx)
            {
                var zoomFactor = canvas.ZoomFactor;
                tx.Translate(-VisibleLeft, -VisibleTop, MatrixOrder.Append);
                tx.Scale(zoomFactor, zoomFactor, MatrixOrder.Append);
            }

            /// <summary>
            /// Convert a point from the controls space to my items space
            /// </summary>
            internal MouseEventArgs Global2Local(MouseEventArgs e)
            {
                var pt = Global2Local(e.Location);
                return new MouseEventArgs(e.Button, e.Clicks, pt.X, pt.Y, e.Delta);
            }
        }
    }
}
