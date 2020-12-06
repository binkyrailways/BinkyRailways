using System;
using System.Collections;
using System.Collections.Generic;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Results from a used by request.
    /// </summary>
    public class UsedByInfos : IEnumerable<UsedByInfo>
    {
        private readonly List<UsedByInfo> list = new List<UsedByInfo>();

        /// <summary>
        /// Gets the number of used by entries.
        /// </summary>
        public int Count { get { return list.Count; } }

        /// <summary>
        /// Add a usage info
        /// </summary>
        internal void UsedBy(IEntity usedBy, string message, params object[] args)
        {
            if (usedBy == null)
            {
                throw new ArgumentNullException("usedBy");
            }
            if ((args != null) && (args.Length > 0))
            {
                message = string.Format(message, args);
            }
            list.Add(new UsedByInfo(usedBy, message));
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        public IEnumerator<UsedByInfo> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
