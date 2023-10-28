using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;
using BinkyRailways.Core.Net;

namespace BinkyRailways.Protocols.Ecos
{
    /// <summary>
    /// TCP connection to the Ecos.
    /// </summary>
    public class Client : IDisposable
    {
        private const int EcosPort = 15471;
        private readonly AsyncSocket socket;
        private readonly byte[] readBuffer;
        private int readBufferOffset;
        private readonly ConcurrentDictionary<Command, TaskCompletionSource<Reply>> commandsWaitingForReply = new ConcurrentDictionary<Command, TaskCompletionSource<Reply>>();
        private readonly ConcurrentDictionary<int, Object> objects = new ConcurrentDictionary<int, Object>();

        /// <summary>
        /// Default ctor
        /// </summary>
        public Client(string hostName)
        {
            // Create buffer
            readBuffer = new byte[4096];

            // Open connection
            socket = new AsyncSocket(hostName, EcosPort);

            // Start reading
            ReadNextData();
        }

        /// <summary>
        /// Send a command to the ECoS
        /// </summary>
        public Task<Reply> SendCommand(Command command)
        {
            // Create a TCS
            var tcs = new TaskCompletionSource<Reply>();

            // Register action
            commandsWaitingForReply.AddOrUpdate(command, tcs, (c, a) => tcs);

            // Encode command
            var cmdStr = command.ToString() + "\n";
            var buffer = Encoding.UTF8.GetBytes(cmdStr);
            // Send command
            socket.Send(buffer, () => { }, err => { });

            return tcs.Task;
        }

        /// <summary>
        /// Register an object for receiving events.
        /// </summary>
        public void Register(Object @object)
        {
            objects.TryAdd(@object.Id, @object);
        }

        /// <summary>
        /// Read the next batch of data and process it.
        /// </summary>
        private void ReadNextData()
        {
            socket.Receive(readBuffer, r => {
                if (r < 0)
                    return;

                if (r >= 0)
                {
                    readBufferOffset += r;

                    if (Parser.TryParse(readBuffer, readBufferOffset, ProcessMessage))
                    {
                        //
                        readBufferOffset = 0;
                    }
                }

                // Read next batch
                ReadNextData();
            }, HandleReadError, readBufferOffset);
        }

        /// <summary>
        /// Process parsed messages
        /// </summary>
        /// <param name="message"></param>
        private void ProcessMessage(Message message)
        {
            var reply = message as Reply;
            if (reply != null)
            {
                TaskCompletionSource<Reply> tcs;
                if (commandsWaitingForReply.TryRemove(reply.Command, out tcs))
                {
                    tcs.SetResult(reply);
                }
                return;
            }

            var @event = message as Event;
            if (@event != null)
            {
                Object @object;
                if (objects.TryGetValue(@event.Id, out @object))
                {
                    @object.OnEvent(@event);
                }
            }
        }

        /// <summary>
        /// A read error occurred.
        /// </summary>
        private void HandleReadError(Exception ex)
        {

        }

        /// <summary>
        /// Close.
        /// </summary>
        public void Dispose()
        {
            socket.Dispose();
        }
    }
}
