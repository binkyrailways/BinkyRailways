using System;
using System.Collections;
using System.Collections.Generic;

namespace BinkyRailways.Core.State.Impl
{
    internal class RouteSequence : IRouteSequence
    {
        private readonly List<IRouteState> sequence;

        /// <summary>
        /// Create an empty sequence.
        /// </summary>
        public RouteSequence()
        {            
            sequence = new List<IRouteState>();
        }

        /// <summary>
        /// Create an 1 length sequence.
        /// </summary>
        public RouteSequence(IRouteState first)
        {
            sequence = new List<IRouteState> { first };
        }

        /// <summary>
        /// Create an copy of the given sequence.
        /// </summary>
        public RouteSequence(RouteSequence source)
        {
            sequence = new List<IRouteState>(source.sequence);
        }

        /// <summary>
        /// Add the given route to the end of this sequence.
        /// </summary>
        public void Add(IRouteState route)
        {
            if (sequence.Count > 0)
            {
                // Route must start from destination of last route
                if (sequence[sequence.Count-1].To != route.From)
                {
                    throw new ArgumentException("Route does not connect to existing sequence.");
                }
            }
            sequence.Add(route);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<IRouteState> GetEnumerator()
        {
            return sequence.GetEnumerator();
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

        /// <summary>
        /// Gets the number of routes in this sequence.
        /// </summary>
        public int Length
        {
            get { return sequence.Count; }
        }

        /// <summary>
        /// Gets the first route in this sequence.
        /// Returns null if the sequence it empty.
        /// </summary>
        public IRouteState First
        {
            get { return (sequence.Count > 0) ? sequence[0] : null; }
        }

        /// <summary>
        /// Gets the last route in this sequence.
        /// Returns null if the sequence it empty.
        /// </summary>
        public IRouteState Last
        {
            get { return (sequence.Count > 0) ? sequence[sequence.Count - 1] : null; }
        }
    }
}
