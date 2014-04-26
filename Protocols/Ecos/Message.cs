using System.Collections.Generic;

namespace BinkyRailways.Protocols.Ecos
{
    public abstract class Message
    {
        private readonly List<MessageRow> rows;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected Message()
        {
            rows = new List<MessageRow>();
        }

        public int ErrorCode { get; internal set; }
        public string ErrorMessage { get; internal set; }

        public List<MessageRow> Rows
        {
            get { return rows; }
        }
    }
}
