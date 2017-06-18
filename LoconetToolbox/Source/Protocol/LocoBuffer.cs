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

namespace LocoNetToolBox.Protocol
{
    /// <summary>
    /// LocoBuffer communication.
    /// </summary>
    public abstract partial class LocoBuffer : IDisposable
    {
        internal event MessageHandler SendMessage;
        public event MessageHandler PreviewMessage;
        internal event EventHandler Opened;
        internal event EventHandler Closed;

        private ReceiveProcessor receiveProcessor;
        private readonly object handlersLock = new object();
        private readonly List<MessageHandler> handlers = new List<MessageHandler>();

        /// <summary>
        /// Create a receive processor and fire the Opened event.
        /// </summary>
        protected void OnOpened()
        {
            receiveProcessor = new ReceiveProcessor(this);
            if (Opened != null) { Opened(this, EventArgs.Empty); }
        }

        /// <summary>
        /// Close the connection (if any)
        /// </summary>
        public abstract void Close();

        /// <summary>
        /// Disconnect from the receive processor and fire the Closed event.
        /// </summary>
        protected void OnClosed()
        {
            receiveProcessor = null;
            if (Closed != null) { Closed(this, EventArgs.Empty); }
        }

        /// <summary>
        /// Send the given message
        /// </summary>
        public void Send(Message decoded, params byte[] msg)
        {
            Message.UpdateChecksum(msg, msg.Length);
            if (SendMessage != null) { SendMessage(msg, decoded); }
            var length = Message.GetMessageLength(msg);
            try
            {
                Send(msg, length);
            }
            catch (InvalidOperationException)
            {
                Close();
                throw;
            }
        }

        protected abstract void Send(byte[] msg, int length);

        /// <summary>
        /// Read a single message
        /// </summary>
        protected abstract byte[] ReadMessage();

        /// <summary>
        /// Add a new handler to the handler queue.
        /// </summary>
        /// <returns>A registration. Dispose to unregister.</returns>
        public IDisposable AddHandler(MessageHandler handler)
        {
            lock (handlersLock)
            {
                handlers.Add(handler);
                return new HandlerRegistration(this, handler);
            }
        }

        /// <summary>
        /// Remove a new handler from the handler queue.
        /// </summary>
        private void RemoveHandler(MessageHandler handler)
        {
            lock (handlersLock)
            {
                handlers.Remove(handler);
            }
        }

        /// <summary>
        /// Call all appriopriate handlers for the given message.
        /// </summary>
        private void HandleMessage(byte[] msg, Message decoded)
        {
            lock (handlersLock)
            {
                var preview = this.PreviewMessage;
                if (preview != null) { preview(msg, decoded); }

                for (int i = handlers.Count-1; i >= 0; i--)
                {
                    var handler = handlers[i];
                    var done = handler(msg, decoded);
                    if (done) { break; }
                }
            }
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        void IDisposable.Dispose()
        {
            Close();
        }

        private class HandlerRegistration : IDisposable
        {
            private readonly MessageHandler handler;
            private readonly LocoBuffer lb;

            /// <summary>
            /// Default ctor
            /// </summary>
            internal HandlerRegistration(LocoBuffer lb, MessageHandler handler)
            {
                this.lb = lb;
                this.handler = handler;
            }

            void IDisposable.Dispose()
            {
                lb.RemoveHandler(handler);
            }

        }
    }
}
