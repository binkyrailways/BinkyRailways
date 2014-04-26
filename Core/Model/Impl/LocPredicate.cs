using System.Xml.Serialization;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Some predicate about locs.
    /// </summary>
    [XmlInclude(typeof(LocAndPredicate))]
    [XmlInclude(typeof(LocOrPredicate))]
    [XmlInclude(typeof(LocCanChangeDirectionPredicate))]
    [XmlInclude(typeof(LocEqualsPredicate))]
    [XmlInclude(typeof(LocGroupEqualsPredicate))]
    [XmlInclude(typeof(LocStandardPredicate))]
    [XmlInclude(typeof(LocTimePredicate))]
    public abstract class LocPredicate : ModuleEntity, ILocPredicate
    {
        /// <summary>
        /// Human readable description
        /// </summary>
        [XmlIgnore]
        public override string Description
        {
            get { return ToString(); }
            set{ /* ignore */ }
        }

        /// <summary>
        /// Create a deep clone.
        /// </summary>
        ILocPredicate ILocPredicate.Clone(bool setModule)
        {
            var clone = Clone();
            if (setModule)
            {
                clone.Module = Module;
            }
            return clone;
        }

        /// <summary>
        /// Create a deep clone.
        /// </summary>
        protected internal abstract LocPredicate Clone();
    }
}
