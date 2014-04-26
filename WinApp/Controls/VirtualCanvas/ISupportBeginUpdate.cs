using System;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas
{
    public interface ISupportBeginUpdate
    {
        /// <summary>
        /// Notify the canvas to block update request till Dispose is called on the returned object.
        /// </summary>
        IDisposable BeginUpdate();
    }
}
