/*
Loconet toolbox
Copyright (C) 2010 Modelspoorgroep Venlo, Ewout Prangsma

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LocoNetToolBox.Protocol
{
    public partial class LocoBuffer
    {
        private class ReceiveProcessor
        {
            private readonly LocoBuffer lb;
            private readonly Thread thread;

            /// <summary>
            /// Create and start a new processor
            /// </summary>
            public ReceiveProcessor(LocoBuffer lb)
            {
                this.lb = lb;
                this.thread = new Thread(OnRun);
                this.thread.Start();
            }

            /// <summary>
            /// Run the processor.
            /// </summary>
            private void OnRun()
            {
                while (true)
                {
                    try
                    {
                        var msg = lb.ReadMessage();
                        if (msg == null) { break; }
                        lb.HandleMessage(msg, Message.Decode(msg));
                    }
                    catch (TimeoutException)
                    {
                        // Ignore timeouts
                    }
                    catch
                    {
                        // Ignore other exceptions
                    }
                }
            }
        }
    }
}
