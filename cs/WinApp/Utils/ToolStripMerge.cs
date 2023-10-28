using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BinkyRailways.WinApp.Utils
{
    internal class ToolStripMerge
    {
        private readonly List<ToolStripItem> items = new List<ToolStripItem>();

        /// <summary>
        /// Default ctor
        /// </summary>
        public ToolStripMerge(ToolStrip source)
        {
            items.AddRange(source.Items.Cast<ToolStripItem>());
            source.Items.Clear();
        }

        /// <summary>
        /// Merge my items into the given target at the given index.
        /// </summary>
        public void MergeInto(ToolStrip target, int index)
        {
            MergeInto(target.Items, index);
        }

        /// <summary>
        /// Merge my items into the given target at the given index.
        /// </summary>
        public void MergeInto(MenuStrip target, int index)
        {
            MergeInto(target.Items, index);
        }

        /// <summary>
        /// Merge my items into the given target at the given index.
        /// </summary>
        public void MergeInto(ToolStripItemCollection target, int index)
        {
            if (index > 0)
            {
                var sep = new ToolStripSeparator();
                items.Insert(0, sep);
            }
            foreach (var item in items)
            {
                target.Insert(index++, item);
            }
        }

        /// <summary>
        /// Make all items visible.
        /// </summary>
        public void ShowAll()
        {
            foreach (var item in items)
            {
                item.Visible = true;
            }
        }

        /// <summary>
        /// Make all items invisible.
        /// </summary>
        public void HideAll()
        {
            foreach (var item in items)
            {
                item.Visible = false;
            }
        }
    }
}
