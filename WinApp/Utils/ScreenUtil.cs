using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BinkyRailways.WinApp.Utils
{
    internal static class ScreenUtil
    {
        /// <summary>
        /// Is the given bounds visible on one of the screens?
        /// </summary>
        internal static bool IsVisibleOnScreens(Rectangle bounds)
        {
            return Screen.AllScreens.Any(screen => screen.Bounds.IntersectsWith(bounds));
        }
    }
}
