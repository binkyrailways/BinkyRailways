using System;
using System.Collections.Generic;
using System.Drawing;
using BinkyRailways.Core.Util;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas
{
    /// <summary>
    /// Collection of selected canvas item placements.
    /// </summary>
    public class SelectedVCItemPlacementCollection : IEnumerable<VCItemPlacement>
    {
        /// <summary>
        /// Select has changed
        /// </summary>
        internal EventHandler Changed;

        private readonly List<VCItemPlacement> selection = new List<VCItemPlacement>();
        private readonly ISupportBeginUpdate updateContext;

        /// <summary>
        /// Default ctor
        /// </summary>
        public SelectedVCItemPlacementCollection(ISupportBeginUpdate updateContext)
        {
            this.updateContext = updateContext;
        }

        /// <summary>
        /// Gets the number of items
        /// </summary>
        public int Count
        {
            get { return selection.Count; }
        }

        /// <summary>
        /// Gets the selected item at the given index (0..count-1)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public VCItemPlacement this[int index]
        {
            get { return selection[index]; }
        }

        /// <summary>
        /// Is the given placement contained in this selection.
        /// </summary>
        public bool Contains(VCItemPlacement placement)
        {
            return selection.Contains(placement);
        }

        /// <summary>
        /// Add a placement to the selection.
        /// </summary>
        public void Add(VCItemPlacement placement)
        {
            AddInternal(placement, true);
        }

        /// <summary>
        /// Add a placement to the selection.
        /// </summary>
        private bool AddInternal(VCItemPlacement placement, bool fireChanged)
        {
            if (selection.Contains(placement))
                return false;

            selection.Add(placement);
            if (fireChanged)
                Changed.Fire(this);
            return true;
        }

        /// <summary>
        /// Add all given placements to the selection.
        /// </summary>
        public void AddAll(IEnumerable<VCItemPlacement> placements)
        {
            var changed = false;
            using (updateContext.BeginUpdate())
            {
                foreach (var placement in placements)
                {
                    if (AddInternal(placement, false))
                        changed = true;
                }
            }
            if (changed)
                Changed.Fire(this);
        }

        /// <summary>
        /// Remove the given placement from the selection.
        /// </summary>
        public bool Remove(VCItemPlacement placement)
        {
            return RemoveInternal(placement, true);
        }

        /// <summary>
        /// Remove the given placement from the selection.
        /// </summary>
        private bool RemoveInternal(VCItemPlacement placement, bool fireChanged)
        {
            if (!selection.Remove(placement))
                return false;
            if (fireChanged)
                Changed.Fire(this);
            return true;
        }

        /// <summary>
        /// Remove all placements from this selection
        /// </summary>
        public void Clear()
        {
            var changed = false;
            using (updateContext.BeginUpdate())
            {
                while (selection.Count > 0)
                {
                    if (RemoveInternal(selection[0], false))
                        changed = true;
                }
            }
            if (changed)
                Changed.Fire(this);
        }

        /// <summary>
        /// Find the first item that contains the given point (in virtual space).
        /// </summary>
        /// <param name="pt"></param>
        /// <returns>The first item that contains the given point or null if no item contains the given point.</returns>
        public VCItemPlacement Find(PointF pt, Predicate<VCItemPlacement> filter)
        {
            for (int i = selection.Count - 1; i >= 0; i--)
            {
                var placement = selection[i];
                if (placement.Contains(pt))
                {
                    if ((filter == null) || filter(placement))
                    {
                        return placement;
                    }
                }
            }
            return null;
        }

        #region IEnumerable<VCItemPlacement> Members

        IEnumerator<VCItemPlacement> IEnumerable<VCItemPlacement>.GetEnumerator()
        {
            return selection.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return selection.GetEnumerator();
        }

        #endregion
    }
}
