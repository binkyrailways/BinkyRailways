using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LocoNetToolBox
{
    /// <summary>
    /// Control extensions methods
    /// </summary>
    internal static class ControlExtensions
    {
        /// <summary>
        /// Call the given handler of the GUI thread of the given control.
        /// </summary>
        internal static void PostOnGuiThread(this Control context, Action handler)
        {
            if (context.InvokeRequired)
            {
                context.BeginInvoke(handler);
            }
            else
            {
                handler();
            }
        }

        /// <summary>
        /// Call the given handler of the GUI thread of the given control.
        /// </summary>
        internal static void PostOnGuiThread<T1>(this Control context, Action<T1> handler, T1 arg1)
        {
            if (context.InvokeRequired)
            {
                context.BeginInvoke(handler, arg1);
            }
            else
            {
                handler(arg1);
            }
        }

        /// <summary>
        /// Call the given handler of the GUI thread of the given control.
        /// </summary>
        internal static void PostOnGuiThread<T1, T2>(this Control context, Action<T1, T2> handler, T1 arg1, T2 arg2)
        {
            if (context.InvokeRequired)
            {
                context.BeginInvoke(handler, arg1, arg2);
            }
            else
            {
                handler(arg1, arg2);
            }
        }
    }
}
