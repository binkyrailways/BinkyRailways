using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BinkyRailways.WinApp.Controls
{
    internal static class Extensions
    {
        /// <summary>
        /// Does the selected item of the given listbox contain the 
        /// given location?
        /// </summary>
        internal static bool SelectedItemContains(this ListBox lb, Point location)
        {
            var index = lb.SelectedIndex;
            if (index < 0)
                return false;
            return lb.GetItemRectangle(index).Contains(location);
        }
    }
}
