using System.Collections;
using System.Collections.Generic;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// FIFO list of recently visited blocks.
    /// </summary>
    internal class RecentlyVisitedBlocks : IEnumerable<IBlockState>
    {
        private const int MaxBlocks = 5;
        private readonly object listLock = new object();
        private readonly List<IBlockState> list = new List<IBlockState>(MaxBlocks);

        /// <summary>
        /// Insert the given block to the front of the list.
        /// </summary>
        internal void Insert(IBlockState blockState)
        {
            if (blockState == null)
                return;
            lock (listLock)
            {
                list.Remove(blockState);
                list.Insert(0, blockState);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<IBlockState> GetEnumerator()
        {
            List<IBlockState> clone;
            lock (listLock)
            {
                clone = new List<IBlockState>(list);
            }
            return clone.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
