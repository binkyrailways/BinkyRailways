using System;
using System.Threading.Tasks;

namespace BinkyRailways.Protocols.Ecos
{
    /// <summary>
    /// Ecos object
    /// </summary>
    public class Object : Constants
    {
        private readonly int id;
        private readonly Client client;

        /// <summary>
        /// Default ctor
        /// </summary>
        public Object(Client client, int id)
        {
            this.client = client;
            this.id = id;
        }

        /// <summary>
        /// Gets the ID of this object.
        /// </summary>
        public int Id { get { return id; } }

        /// <summary>
        /// Gets the client
        /// </summary>
        protected Client Client { get { return client; } }

        /// <summary>
        /// Register as view (so we receive events)
        /// </summary>
        public void RequestView()
        {
            // Make sure we're registered
            client.Register(this);

            // Send command
            Send(CmdRequest, OptView).ContinueWith(t => HandleReplyError(t)).Wait(Timeout);
        }

        /// <summary>
        /// Unregister as view (so we no longer receive events)
        /// </summary>
        public void ReleaseView()
        {
            // Send command
            Send(CmdRelease, OptView).ContinueWith(t => HandleReplyError(t)).Wait(Timeout);
        }

        /// <summary>
        /// Send a query objects command
        /// </summary>
        public Task QueryObjects()
        {
            return Send(CmdQueryObjects, OptName).ContinueWith(t => HandleReplyError(t)).ContinueWith(t => OnQueryObjects(t.Result));
        }

        /// <summary>
        /// Perform a get request.
        /// </summary>
        protected string Get(string optionName)
        {
            return Send(CmdGet, optionName).ContinueWith(t => GetReplyResult(t, optionName)).Result;
        }

        /// <summary>
        /// QueryObjects reply.
        /// </summary>
        protected virtual void OnQueryObjects(Reply reply)
        {
            // Override me
        }

        /// <summary>
        /// Send a command to this object.
        /// </summary>
        protected Task<Reply> Send(string commandName, params string[] options)
        {
            return client.SendCommand(new Command(commandName, id, options));
        }

        /// <summary>
        /// Send a command to this object.
        /// </summary>
        protected Task<Reply> Send(string commandName, params Option[] options)
        {
            return client.SendCommand(new Command(commandName, id, options));
        }

        /// <summary>
        /// Throw an exception if the reply from the given task is not OK.
        /// </summary>
        protected Reply HandleReplyError(Task<Reply> task)
        {
            if (!task.IsCompleted)
                throw new AggregateException("Task failed", task.Exception);
            if (task.IsCompleted)
            {
                var reply = task.Result;
                if (!reply.IsSucceeded)
                {
                    throw new ProtocolException(reply.ErrorMessage);
                }
                return reply;
            }
            throw new ProtocolException("Task not completed");
        }

        /// <summary>
        /// Get the value for the option (in the reply of the task) with the given name.
        /// </summary>
        protected string GetReplyResult(Task<Reply> task, string optionName)
        {
            var reply = HandleReplyError(task);
            foreach (var row in reply.Rows)
            {
                Option option;
                if (row.TryGetValue(optionName, out option))
                    return option.Value;
            }
            throw new ProtocolException("Option not found: " + optionName);
        }

        /// <summary>
        /// Called when an event for this object is received.
        /// </summary>
        protected internal virtual void OnEvent(Event @event)
        {
            
        }
    }
}
