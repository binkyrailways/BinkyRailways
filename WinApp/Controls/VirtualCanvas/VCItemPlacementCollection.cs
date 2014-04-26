using System;
using System.Collections.Generic;
using System.Drawing;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas
{
    /// <summary>
    /// Collection of canvas item placements.
    /// With each placement an item and its position is maintained.
    /// </summary>
    public class VCItemPlacementCollection : ICollection<VCItemPlacement>
    {
        /// <summary>
        /// Placement has been added
        /// </summary>
        internal EventHandler<VCItemPlacementEventArgs> Added;

        /// <summary>
        /// Placement has been removed
        /// </summary>
        internal EventHandler<VCItemPlacementEventArgs> Removed;

        /// <summary>
        /// This event is used to communicate with the container of this item.
        /// </summary>
        internal event EventHandler<ContainerEventArgs> ContainerEvent;

        private readonly List<VCItemPlacement> items = new List<VCItemPlacement>();

        /// <summary>
        /// Gets the number of items
        /// </summary>
        public int Count
        {
            get { return items.Count; }
        }

        /// <summary>
        /// Remove all placements
        /// </summary>
        public void Clear()
        {
            while (items.Count > 0)
            {
                Remove(items[0]);
            }
        }

        /// <summary>
        /// Gets the placement as the given index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public VCItemPlacement this[int index]
        {
            get { return items[index]; }
        }

        /// <summary>
        /// Create a placement for an item
        /// </summary>
        /// <param name="item"></param>
        public VCItemPlacement Add(VCItem item, Layout.LayoutConstraints constraints)
        {
            VCItemPlacement placement = new VCItemPlacement(item, constraints);
            items.Add(placement);
            item.ContainerEvent += new EventHandler<ContainerEventArgs>(item_ContainerEvent);

            if (Added != null)
            {
                Added(this, new VCItemPlacementEventArgs(placement));
            }

            return placement;
        }

        /// <summary>
        /// Insert an item at the given index.
        /// </summary>
        /// <param name="item"></param>
        public VCItemPlacement Insert(int index, VCItem item, Layout.LayoutConstraints constraints)
        {
            VCItemPlacement placement = new VCItemPlacement(item, constraints);
            items.Insert(index, placement);
            item.ContainerEvent += new EventHandler<ContainerEventArgs>(item_ContainerEvent);

            if (Added != null)
            {
                Added(this, new VCItemPlacementEventArgs(placement));
            }

            return placement;
        }

        /// <summary>
        /// Replace the item at given index with given item
        /// </summary>
        /// <param name="item"></param>
        public VCItemPlacement Replace(int index, VCItem item, Layout.LayoutConstraints constraints)
        {
            VCItemPlacement oldPlacement = items[index];
            oldPlacement.Item.ContainerEvent -= new EventHandler<ContainerEventArgs>(item_ContainerEvent);

            VCItemPlacement placement = new VCItemPlacement(item, constraints);
            item.ContainerEvent += new EventHandler<ContainerEventArgs>(item_ContainerEvent);
            items[index] = placement;

            if (Removed != null)
            {
                Removed(this, new VCItemPlacementEventArgs(oldPlacement));
            }

            if (Added != null)
            {
                Added(this, new VCItemPlacementEventArgs(placement));
            }

            return placement;
        }

        /// <summary>
        /// Remove the given placement.
        /// </summary>
        /// <param name="placement"></param>
        public bool Remove(VCItemPlacement placement)
        {
            if (items.Remove(placement))
            {
                VCItem item = placement.Item;
                item.ContainerEvent -= new EventHandler<ContainerEventArgs>(item_ContainerEvent);

                if (Removed != null)
                {
                    Removed(this, new VCItemPlacementEventArgs(placement));
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Remove all placements of the given item.
        /// </summary>
        /// <param name="placement"></param>
        public void Remove(VCItem item)
        {
            int i = 0;
            while (i < items.Count)
            {
                if (items[i].Item == item)
                {
                    Remove(items[i]);
                }
                else
                {
                    i++;
                }
            }
        }

        /// <summary>
        /// Remove the item at the given index
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            Remove(items[index]);
        }

        /// <summary>
        /// Find the first item that contains the given point (in virtual space).
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="after">The placement to start after (if null all placements are searched)</param>
        /// <returns>The first item that contains the given point or null if no item contains the given point.</returns>
        public VCItemPlacement Find(PointF pt, VCItemPlacement after)
        {
            return Find(pt, after, null);
        }

        /// <summary>
        /// Find the first item that contains the given point (in virtual space).
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="after">The placement to start after (if null all placements are searched)</param>
        /// <param name="filter">Extra filter to include in the search</param>
        /// <returns>The first item that contains the given point or null if no item contains the given point.</returns>
        public VCItemPlacement Find(PointF pt, VCItemPlacement after, Predicate<VCItemPlacement> filter)
        {
            var index = (after != null) ? items.IndexOf(after) : items.Count;
            for (var i = index - 1; i >= 0; i--)
            {
                var placement = items[i];
                if (placement.Item.Visible)
                {
                    if (placement.Contains(pt))
                    {
                        if ((filter == null) || filter(placement))
                        {
                            return placement;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Find the first item that contains the given item
        /// </summary>
        /// <param name="pt"></param>
        /// <returns>The first item that contains the given item or null if no item contains the given item.</returns>
        public VCItemPlacement Find(VCItem item)
        {
            for (int i = items.Count - 1; i >= 0; i--)
            {
                VCItemPlacement placement = items[i];
                if (placement.Item == item)
                {
                    return placement;
                }
            }
            return null;
        }

        /// <summary>
        /// Forward container events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void item_ContainerEvent(object sender, ContainerEventArgs e)
        {
            if (ContainerEvent != null)
            {
                ContainerEvent(sender, e);
            }
        }

        public bool Contains(VCItemPlacement item)
        {
            return items.Contains(item);
        }


        #region IEnumerable<VCItemPlacement> Members

        IEnumerator<VCItemPlacement> IEnumerable<VCItemPlacement>.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        #endregion

        #region ICollection<VCItemPlacement> Members

        void ICollection<VCItemPlacement>.Add(VCItemPlacement item)
        {
            throw new NotSupportedException();
        }

        void ICollection<VCItemPlacement>.CopyTo(VCItemPlacement[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
        }

        bool ICollection<VCItemPlacement>.IsReadOnly
        {
            get { return true; }
        }

        #endregion
    }
}
