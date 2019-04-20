using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace BinkyRailways.Core.Net
{
    /// <summary>
    /// TCP Socket with async support.
    /// </summary>
    public class AsyncSocket : IDisposable
    {
        private readonly TcpClient tcpClient;
        private bool closing;

        /// <summary>
        /// Default ctor
        /// </summary>
        public AsyncSocket(string hostName, int port)
        {
            // Open connection
            tcpClient = new TcpClient(hostName, port);
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public void Dispose()
        {
            closing = true;
            tcpClient.Close();
        }

        /// <summary>
        /// Read data from the ECoS
        /// </summary>
        /// <param name="buffer">The buffer to read data into</param>
        /// <param name="callback"></param>
        /// <param name="error"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public Task<int> Receive(byte[] buffer, Action<int> callback, Action<Exception> error, int offset)
        {
            try
            {
                var stream = tcpClient.GetStream();
                Func<AsyncCallback, object, IAsyncResult> begin =
               (cb, s) => stream.BeginRead(buffer, offset, buffer.Length, cb, s);

                var task = Task.Factory.FromAsync<int>(begin, stream.EndRead, null);
                task.ContinueWith(t => callback(t.Result), TaskContinuationOptions.NotOnFaulted)
                    .ContinueWith(t => error(t.Exception), TaskContinuationOptions.OnlyOnFaulted);
                task.ContinueWith(t => error(t.Exception), TaskContinuationOptions.OnlyOnFaulted);
                return task;
            }
            catch (Exception e)
            {
                error(e);
                return null;
            }
        }

        /// <summary>
        /// Send a piece of data.
        /// </summary>
        public Task Send(byte[] buffer, Action callback, Action<Exception> error)
        {
            try
            {
                var stream = tcpClient.GetStream();
                Func<AsyncCallback, object, IAsyncResult> begin =
                    (cb, s) => stream.BeginWrite(buffer, 0, buffer.Length, cb, s);

                var task = Task.Factory.FromAsync(begin, stream.EndWrite, null);
                task.ContinueWith(t => callback(), TaskContinuationOptions.NotOnFaulted)
                    .ContinueWith(t => error(t.Exception), TaskContinuationOptions.OnlyOnFaulted);
                task.ContinueWith(t => error(t.Exception), TaskContinuationOptions.OnlyOnFaulted);

                return task;
            }
            catch (Exception e)
            {
                error(e);
                return null;
            }
        }
    }
}
