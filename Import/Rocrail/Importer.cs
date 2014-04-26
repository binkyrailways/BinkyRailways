using System.Collections.Generic;
using System.Xml.Linq;

namespace BinkyRailways.Import.Rocrail
{
    /// <summary>
    /// Base importer
    /// </summary>
    internal abstract class Importer
    {
        private readonly XElement root;
        private readonly string path;
        private readonly List<ImportMessage> messages = new List<ImportMessage>();

        /// <summary>
        /// Importer for locs.
        /// </summary>
        protected Importer(XElement root, string path)
        {
            this.root = root;
            this.path = System.IO.Path.IsPathRooted(path) ? path : System.IO.Path.GetFullPath(path);
        }

        /// <summary>
        /// Document root
        /// </summary>
        protected XElement Root { get { return root; } }

        /// <summary>
        /// Gets the folder containing this file.
        /// </summary>
        protected string Folder { get { return System.IO.Path.GetDirectoryName(path); } }

        /// <summary>
        /// Gets the entire Path.
        /// </summary>
        protected string Path { get { return path; } }

        /// <summary>
        /// Gets all messages
        /// </summary>
        public IEnumerable<ImportMessage> Messages
        {
            get { return messages; }
        }

        /// <summary>
        /// Record a message
        /// </summary>
        protected void Message(string format, params object[] args)
        {
            var msg = string.Format(format, args);
            messages.Add(new ImportMessage(msg, path));
        }

        /// <summary>
        /// Add all messages
        /// </summary>
        protected void AddMessages(IEnumerable<ImportMessage> source)
        {
            messages.AddRange(source);
        }
    }
}
