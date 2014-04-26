using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Base class for actions that are invoked upon a changing sensor value.
    /// </summary>
    [XmlInclude(typeof(LocFunction))]
    public sealed class LocFunctions : EntitySet<LocFunction, ILocFunction>, ILocFunctions
    {
        private readonly Loc owner;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal LocFunctions(Loc owner)
            : base(owner)
        {
            this.owner = owner;
            var me = (ILocFunctions) this;
            me.Add(Model.LocFunction.Light);
        }

        /// <summary>
        /// The given item has been added to this set.
        /// </summary>
        protected override void OnAdded(LocFunction item)
        {
            base.OnAdded(item);
            item.Owner = owner;
        }

        /// <summary>
        /// The given item will been added to this set.
        /// </summary>
        protected override void OnAdding(LocFunction item)
        {
            base.OnAdding(item);
            // Remove existing with same number.
            var existing = Entries.Values.FirstOrDefault(x => x.Function == item.Function);
            if (existing != null)
            {
                Remove(existing);
            }
        }

        /// <summary>
        /// The given item has been removed from this set.
        /// </summary>
        protected override void OnRemoved(LocFunction item)
        {
            base.OnRemoved(item);
            item.Owner = null;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        IEnumerator<ILocFunction> IEnumerable<ILocFunction>.GetEnumerator()
        {
            return Entries.Values.Cast<ILocFunction>().OrderBy(x => x.Function).GetEnumerator();
        }

        /// <summary>
        /// Add the given action to this list.
        /// </summary>
        ILocFunction ILocFunctions.Add(Model.LocFunction function)
        {
            var existing = Entries.Values.FirstOrDefault(x => x.Function == function);
            if (existing != null)
                return existing;

            var item = new LocFunction { Function = function };
            item.Description = (function == Model.LocFunction.Light) ? "Light" : string.Format("F{0}", (int) function);
            Add(item);
            return item;
        }

        /// <summary>
        /// Remove the given item from this set.
        /// </summary>
        /// <returns>True if it was removed, false otherwise</returns>
        bool IEntitySet<ILocFunction>.Remove(ILocFunction item)
        {
            return Remove((LocFunction) item);
        }

        /// <summary>
        /// Does this set contain the given item?
        /// </summary>
        bool IEntitySet<ILocFunction>.Contains(ILocFunction item)
        {
            return Contains((LocFunction)item);
        }
    }
}
