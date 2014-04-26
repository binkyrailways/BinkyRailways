using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BinkyRailways.WinApp.Utils
{
    /// <summary>
    /// Win32 interop.
    /// </summary>
    internal static class Win32
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr GetFocus();

        /// <summary>
        /// Gets the focused control.
        /// </summary>
        /// <returns></returns>
        public static Control GetFocusedControl()
        {
            // To get hold of the focused control:
            IntPtr focusedHandle = GetFocus();
            if (focusedHandle != IntPtr.Zero)
            {
                // Note that if the focused Control is not a .Net control, then this will return null.
                return Control.FromHandle(focusedHandle);
            }
            // Not found
            return null;
        }
    }
}
