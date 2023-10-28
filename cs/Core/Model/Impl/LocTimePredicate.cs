using System.ComponentModel;
using System.Xml.Serialization;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Predicate that evaluates to true the current (model) time is between <see cref="PeriodStart"/> and <see cref="PeriodEnd"/> (both inclusive).
    /// </summary>
    public sealed class LocTimePredicate : LocPredicate, ILocTimePredicate
    {
        private readonly Property<Time> periodStart;
        private readonly Property<Time> periodEnd;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocTimePredicate()
        {
            periodStart = new Property<Time>(this, Time.MinValue);
            periodEnd = new Property<Time>(this, Time.MaxValue);
        }

        /// <summary>
        /// Start of the (valid) period.
        /// </summary>
        [XmlIgnore]
        public Time PeriodStart
        {
            get { return periodStart.Value; }
            set { periodStart.Value = value; }
        }

        /// <summary>
        /// End of the (valid) period.
        /// </summary>
        [XmlIgnore]
        public Time PeriodEnd
        {
            get { return periodEnd.Value; }
            set { periodEnd.Value = value; }
        }

        /// <summary>
        /// Start of (valid) period.
        /// Used for serialization.
        /// </summary>
        [XmlElement("PeriodStart")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string PeriodStartString
        {
            get { return Default<TimeTypeConverter>.Instance.ConvertToString(PeriodStart); }
            set { PeriodStart = (Time)Default<TimeTypeConverter>.Instance.ConvertFromString(value); }
        }

        /// <summary>
        /// End of (valid) period.
        /// Used for serialization.
        /// </summary>
        [XmlElement("PeriodEnd")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string PeriodEndString
        {
            get { return Default<TimeTypeConverter>.Instance.ConvertToString(PeriodEnd); }
            set { PeriodEnd = (Time)Default<TimeTypeConverter>.Instance.ConvertFromString(value); }
        }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

        /// <summary>
        /// Evaluate this predicate for the given loc.
        /// </summary>
        public override bool Evaluate(ILoc loc)
        {
            return true; // We can only seriously evaluate this at runtime.
        }

        /// <summary>
        /// Create a deep clone.
        /// </summary>
        protected internal override LocPredicate Clone()
        {
            return new LocTimePredicate { PeriodStart = PeriodStart, PeriodEnd = PeriodEnd };
        }

        /// <summary>
        /// Human readable name of this type of entity.
        /// </summary>
        public override string TypeName
        {
            get { return Strings.LocTimePredicateTypeName; }
        }

        /// <summary>
        /// Convert to a string
        /// </summary>
        public override string ToString()
        {
            return string.Format(Strings.LocTimePredicateDescriptionXY, PeriodStart, PeriodEnd);
        }
    }
}
