using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Standard two-way left/right switch
    /// </summary>
    public sealed class Switch : Junction, ISwitch
    {
        private readonly Property<bool> hasFeedback;
        private readonly Property<int> switchDuration;
        private readonly Property<bool> invert;
        private readonly Property<bool> invertFeedback;
        private readonly Property<SwitchDirection> initialDirection;

        /// <summary>
        /// Default ctor
        /// </summary>
        public Switch()
            : base(16, 12)
        {
            hasFeedback = new Property<bool>(this, true);    
            switchDuration = new Property<int>(this, 1000);
            invert = new Property<bool>(this, false);
            invertFeedback = new Property<bool>(this, false);
            initialDirection = new Property<SwitchDirection>(this, SwitchDirection.Straight);
        }

        /// <summary>
        /// Address used to control the switch
        /// </summary>
        [XmlIgnore]
        public Address Address { get; set; }

        /// <summary>
        /// Address of the feedback unit of the entity
        /// </summary>
        [XmlIgnore]
        public Address FeedbackAddress { get; set; }

        /// <summary>
        /// Gets all (non-null) addresses configured in this entity.
        /// </summary>
        [XmlIgnore]
        public IEnumerable<AddressUsage> AddressUsages
        {
            get
            {
                var addr = Address;
                if (addr != null) yield return new AddressUsage(addr, AddressDirection.Output);
                if (HasFeedback)
                {
                    addr = FeedbackAddress ?? Address;
                    if (addr != null) yield return new AddressUsage(addr, AddressDirection.Input);
                }
            }
        }

        /// <summary>
        /// Does this switch send a feedback when switched?
        /// </summary>
        public bool HasFeedback
        {
            get { return hasFeedback.Value; }
            set { hasFeedback.Value = value; }
        }

        /// <summary>
        /// Time (in ms) it takes for the switch to move from one direction to the other?
        /// This property is only used when <see cref="HasFeedback"/> is false.
        /// </summary>
        public int SwitchDuration
        {
            get { return switchDuration.Value; }
            set { switchDuration.Value = Math.Max(0, value); }
        }

        /// <summary>
        /// If set, the straight/off commands are inverted.
        /// </summary>
        public bool Invert
        {
            get { return invert.Value; }
            set { invert.Value = value; }
        }

        /// <summary>
        /// If there is a different feedback address and this is set, the straight/off feedback states are inverted.
        /// </summary>
        public bool InvertFeedback
        {
            get { return invertFeedback.Value; }
            set { invertFeedback.Value = value; }
        }

        /// <summary>
        /// At which direction should the switch be initialized?
        /// </summary>
        [DefaultValue(SwitchDirection.Straight)]
        public SwitchDirection InitialDirection
        {
            get { return initialDirection.Value; }
            set { initialDirection.Value = value; }
        }

        /// <summary>
        /// Address of the locomotive
        /// </summary>
        [XmlElement("Address")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string AddressString
        {
            get { return Default<AddressTypeConverter>.Instance.ConvertToString(Address); }
            set { Address = (Address)Default<AddressTypeConverter>.Instance.ConvertFromString(value); }
        }

        /// <summary>
        /// Address of the feedback unit
        /// </summary>
        [XmlElement("FeedbackAddress")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string FeedbackAddressString
        {
            get { return Default<AddressTypeConverter>.Instance.ConvertToString(FeedbackAddress); }
            set { FeedbackAddress = (Address)Default<AddressTypeConverter>.Instance.ConvertFromString(value); }
        }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

        /// <summary>
        /// Validate the integrity of this entity.
        /// </summary>
        public override void Validate(IEntity validationRoot, ValidationResults results)
        {
            base.Validate(validationRoot, results);
            if (Address == null)
            {
                results.Warn(this, Strings.WarnNoAddressSpecified);
            }
            if (SwitchDuration > 5000)
            {
                results.Warn(this, Strings.WarnSwitchDurationVeryHigh);
            }
        }

        /// <summary>
        /// Human readable name of this type of entity.
        /// </summary>
        public override string TypeName
        {
            get { return Strings.TypeNameSwitch; }
        }
    }
}
