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
    public abstract class Request : Message
    {
        /// <summary>
        /// Execute the message on the given buffer
        /// </summary>
        public abstract void Execute(LocoBuffer lb);

        /// <summary>
        /// Execute this request and wait for a valid response.
        /// </summary>
        /// <param name="lb">The locobuffer to execute on</param>
        /// <param name="validateResponse">Predicate used to filter our the valid response</param>
        /// <param name="timeout">Timeout in milliseconds to wait for a valid response</param>
        public T ExecuteAndWaitForResponse<T>(LocoBuffer lb, Predicate<T> validateResponse, int timeout)
            where T : Message
        {
            object waitLock = new object();
            T result = null;

            using (var handler = lb.AddHandler((raw, msg) => {
                var response = msg as T;
                if ((response != null) && (validateResponse(response)))
                {
                    // We've got the response we we're waiting for
                    lock (waitLock)
                    {
                        result = response;
                        Monitor.PulseAll(waitLock);
                    }
                    return true;
                }
                return false;
                }))
            {
                // Execute the request now
                Execute(lb);

                // Wait for a result
                lock (waitLock)
                {
                    if (result == null)
                    {
                        Monitor.Wait(waitLock, timeout);
                    }
                }
            }
            return result;
        }
    }
}
