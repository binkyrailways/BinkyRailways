using System;
using System.Drawing;
using System.Windows.Forms;
using BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Items
{
    /// <summary>
    /// Label that is a link.
    /// </summary>
    public class VCHyperLinkItem : VCLabelItem
    {
        private readonly EventHandler click;
        private Font drawFont;

        /// <summary>
        /// Default ctor
        /// </summary>
        public VCHyperLinkItem(EventHandler click)
        {
            this.click = click;
            this.MouseHandler = new ClickMouseHandler(this, this.MouseHandler);
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public VCHyperLinkItem(string text, EventHandler click)
            : base(text)
        {
            this.click = click;
            this.MouseHandler = new ClickMouseHandler(this, this.MouseHandler);
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public VCHyperLinkItem(string text, ContentAlignment textAlign, EventHandler click)
            : base(text, textAlign)
        {
            this.click = click;
            this.MouseHandler = new ClickMouseHandler(this, this.MouseHandler);
        }

        /// <summary>
        /// Raise the click handler
        /// </summary>
        protected virtual void RaiseClick()
        {
            if (click != null)
            {
                click(this, EventArgs.Empty);
            }
        }

        protected override void OnMouseOverChanged()
        {
            RaiseRedraw();
            base.OnMouseOverChanged();
        }

        /// <summary>
        /// Draw this item
        /// </summary>
        /// <param name="e"></param>
        public override void Draw(ItemPaintEventArgs e)
        {
            if (IsMouseOver)
            {
                using (drawFont = new Font(this.Font, FontStyle.Underline))
                {
                    base.Draw(e);
                }
            }
            else
            {
                drawFont = this.Font;
                base.Draw(e);
            }
        }

        /// <summary>
        /// Gets the font used to draw the text.
        /// </summary>
        protected override Font TextFont
        {
            get { return drawFont; }
        }

        protected class ClickMouseHandler : MouseHandler
        {
            private readonly VCHyperLinkItem item;

            /// <summary>
            /// Default ctor
            /// </summary>
            /// <param name="item"></param>
            /// <param name="next"></param>
            public ClickMouseHandler(VCHyperLinkItem item, MouseHandler next)
                : base(next)
            {
                this.item = item;
            }

            public override bool OnMouseClick(VCItem sender, ItemMouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    item.RaiseClick();
                    return true;
                }
                else
                {
                    return base.OnMouseClick(sender, e);
                }
            }

            public override void OnMouseEnter(VCItem sender, EventArgs e)
            {
                item.IsMouseOver = true;
                base.OnMouseEnter(sender, e);
            }

            public override bool OnMouseMove(VCItem sender, ItemMouseEventArgs e)
            {
                item.IsMouseOver = true;
                e.Cursor = Cursors.Hand;
                return true;
            }

            public override void OnMouseLeave(VCItem sender, EventArgs e)
            {
                item.IsMouseOver = false;
                base.OnMouseLeave(sender, e);
            }
        }
    }
}
