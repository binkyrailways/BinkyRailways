using System;
using System.Drawing;
using System.Windows.Forms;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers;
using BinkyRailways.WinApp.Controls.VirtualCanvas.Layout;
using BinkyRailways.WinApp.Items;
using BinkyRailways.WinApp.Items.Messages;
using BinkyRailways.WinApp.Utils;

namespace BinkyRailways.WinApp.Controls
{
    /// <summary>
    /// Virtual canvas specific to entities.
    /// </summary>
    public class EntityCanvasControl : VirtualCanvasControl
    {
        private readonly CustomMessageProcessor messageProcessor;
        private readonly ToolTip toolTip;

        /// <summary>
        /// Default ctor
        /// </summary>
        public EntityCanvasControl()
        {
            messageProcessor = new CustomMessageProcessor(this);

            LayoutManager = new StackLayoutManager
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };

            MouseHandler = new VisibleZoomMouseHandler(this, MouseHandler,
                0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1.0f, 1.25f, 1.5f, 2.0f);

            var c = 210;
            CanvasColor = Color.FromArgb(c, c, c);
            BorderStyle = BorderStyle.Fixed3D;
            toolTip = new ToolTip();
            CustomItemMessage += OnItemMessage;
        }

        /// <summary>
        /// Process a custom message coming from an item
        /// </summary>
        private void OnItemMessage(object sender, ArgumentEventArgs e)
        {
            var msg = e.Argument as ItemMessage;
            if (msg != null)
            {
                msg.Accept(messageProcessor);
            }
        }

        protected override void Dispose(bool disposing)
        {
            messageProcessor.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// How to handle custom messages from items.
        /// </summary>
        private class CustomMessageProcessor : ItemMessageVisitor, IDisposable
        {
            private const int ToolTipShowInterval = 250;
            private const int ToolTipHideInterval = 3 * 1000;

            private readonly EntityCanvasControl control;
            private string lastToolTip;
            private Point toolTipPosition;
            private readonly Timer tooltipShowTimer;
            private readonly Timer tooltipHideTimer;
            private bool disposed;

            /// <summary>
            /// Default ctor
            /// </summary>
            public CustomMessageProcessor(EntityCanvasControl control)
            {
                this.control = control;
                tooltipShowTimer = new Timer();
                tooltipShowTimer.Tick += OnTooltipShowTimerTick;
                tooltipHideTimer = new Timer();
                tooltipHideTimer.Tick += OnTooltipHideTimerTick;
            }

            /// <summary>
            /// Tooltip timer expired.
            /// </summary>
            private void OnTooltipShowTimerTick(object sender, EventArgs e)
            {
                if (disposed)
                    return;
                tooltipShowTimer.Enabled = false;
                if (!string.IsNullOrEmpty(lastToolTip))
                {
                    control.toolTip.Show(lastToolTip, control, toolTipPosition);
                    tooltipHideTimer.Interval = ToolTipHideInterval;
                    tooltipHideTimer.Start();
                }
            }

            /// <summary>
            /// Hide the tooltip
            /// </summary>
            private void OnTooltipHideTimerTick(object sender, EventArgs e)
            {
                if (!disposed)
                {
                    tooltipShowTimer.Enabled = false;
                    control.toolTip.Hide(control);
                }
            }

            /// <summary>
            /// Show tooltip
            /// </summary>
            internal override void Visit(ShowToolTipMessage msg)
            {
                if ((msg.ToolTip != lastToolTip) && !disposed)
                {
                    lastToolTip = msg.ToolTip;
                    tooltipHideTimer.Enabled = false;
                    if (string.IsNullOrEmpty(msg.ToolTip))
                    {
                        control.toolTip.Hide(control);
                        tooltipShowTimer.Enabled = false;
                    }
                    else
                    {
                        var sender = msg.Sender;
                        var pt = sender.Local2Control(new Point(sender.Size.Width, 0));
                        toolTipPosition = new Point(pt.X + 20, pt.Y + 20);
                        tooltipShowTimer.Interval = ToolTipShowInterval;
                        tooltipShowTimer.Start();
                    }
                }
            }

            /// <summary>
            /// Show context menu
            /// </summary>
            internal override void Visit(ShowContextMenuMessage msg)
            {
                var sender = msg.Sender as IEntityItem;
                if (sender != null)
                {
                    var ctxMenu = new ContextMenuStrip();
                    sender.BuildContextMenu(ctxMenu);
                    if (ctxMenu.Items.Count > 0)
                    {
                        //ctxMenu.Items.SetClient(this.control.client);
                        var pt = msg.Sender.Local2Control(msg.Location);
                        ctxMenu.Show(control, pt.Round());
                    }
                }
            }

            /// <summary>
            /// Cleanup
            /// </summary>
            public void Dispose()
            {
                disposed = true;
                tooltipShowTimer.Dispose();
                tooltipHideTimer.Dispose();
            }
        }
    }
}
