using System;
using System.Collections.Generic;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas
{
    partial class VCItem
    {
        /// <summary>
        /// Class used in BeginUpdate.
        /// </summary>
        internal sealed class Update : IDisposable
        {
            private readonly VCItem item;
            private bool disposed = false;
            private List<IDisposable> otherUpdates;

            /// <summary>
            /// Default ctor
            /// </summary>
            /// <param name="item"></param>
            internal Update(VCItem item)
            {
                this.item = item;
            }

            /// <summary>
            /// Add another update.
            /// </summary>
            /// <param name="otherUpdate"></param>
            internal void Add(IDisposable otherUpdate)
            {
                lock (this)
                {
                    if (disposed) { throw new InvalidOperationException("disposed"); }
                    if (otherUpdates == null) { otherUpdates = new List<IDisposable>(); }
                    otherUpdates.Add(otherUpdate);
                }
            }

            /// <summary>
            /// End the update
            /// </summary>
            void IDisposable.Dispose()
            {
                List<IDisposable> otherUpdates = null;

                lock (this)
                {
                    if (!disposed)
                    {
                        disposed = true;
                        otherUpdates = this.otherUpdates;
                        item.EndUpdate();
                    }
                }

                if (otherUpdates != null)
                {
                    foreach (var otherUpdate in otherUpdates)
                    {
                        otherUpdate.Dispose();
                    }
                }
            }
        }
    }
}
