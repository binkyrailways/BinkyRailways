using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers
{
    public abstract class DropFileHandler : DragDropHandler
    {
        private bool canAccept = false;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="next"></param>
        public DropFileHandler(DragDropHandler next)
            : base(next)
        {
        }

        /// <summary>
        /// A set of files have been dropped.
        /// </summary>
        /// <param name="files"></param>
        protected abstract void OnDropFiles(string[] files);

        /// <summary>
        /// Can the set of files be dropped?
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        protected abstract bool Accept(string[] files);

        #region Drag / drop handler implementation
        public override bool OnDragDrop(VCItem sender, ItemDragEventArgs e)
        {
            try
            {
                if (Accept(e))
                {
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    OnDropFiles(files);
                    return true;
                }
                else
                {
                    return base.OnDragDrop(sender, e);
                }
            }
            finally
            {
                SetCanAccept(sender, false);
            }
        }

        public override bool OnDragOver(VCItem sender, ItemDragEventArgs e)
        {
            if (Accept(e))
            {
                e.Effect = DragDropEffects.Copy;
                SetCanAccept(sender, true);
                return true;
            }
            else
            {
                SetCanAccept(sender, false);
                return base.OnDragOver(sender, e);
            }
        }

        public override void OnDragLeave(VCItem sender, EventArgs e)
        {
            SetCanAccept(sender, false);
            base.OnDragLeave(sender, e);
        }

        public override void OnPostPaint(VCItem sender, ItemPaintEventArgs e)
        {
            base.OnPostPaint(sender, e);
            if (canAccept)
            {
                using (Pen pen = new Pen(Color.Black))
                {
                    pen.DashStyle = DashStyle.Dot;
                    e.Graphics.DrawRectangle(pen, new Rectangle(Point.Empty, sender.Size));
                }
            }
        }

        /// <summary>
        /// Set the canAccept only it it has changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="value"></param>
        private void SetCanAccept(VCItem sender, bool value)
        {
            if (canAccept != value)
            {
                canAccept = value;
                sender.Invalidate();
            }
        }

        private bool Accept(ItemDragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
                return (files != null) && (files.Length >= 1) && Accept(files);
            }
            return false;
        }
        #endregion
    }
}