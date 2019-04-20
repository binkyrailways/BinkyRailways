using System.Windows.Forms;

namespace BinkyRailways.WinApp.Utils
{
    internal static class ControlExtensions
    {
        /// <summary>
        /// Is the focused control equal to the given control or one of it's children?
        /// </summary>
        internal static bool HasFocus(this Control control)
        {
            var focus = Win32.GetFocusedControl();
            if (focus == null)
                return false;
            return (control == focus) || control.Contains(focus);
        }

        /// <summary>
        /// Change the text of the given item.
        /// This method will only set the text when it is really different.
        /// </summary>
        internal static void SetText(this ListViewItem item, string text)
        {
            text = text ?? string.Empty;
            if (item.Text != text)
            {
                item.Text = text;
            }
        }

        /// <summary>
        /// Change the text of the given item.
        /// This method will only set the text when it is really different.
        /// </summary>
        internal static void SetText(this ListViewItem.ListViewSubItem item, string text)
        {
            text = text ?? string.Empty;
            if (item.Text != text)
            {
                item.Text = text;
            }
        }
    }
}
