using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using BinkyRailways.Core.Storage;
using BinkyRailways.Core.Util;
using Newtonsoft.Json;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Single locomotive.
    /// </summary>
    [XmlRoot]
    public class Loc : PersistentEntity, ILoc
    {
        private const string ImageId = "Image";


        private readonly Property<Address> address;
        private readonly Property<int> speedSteps;
        private readonly Property<int> slowSpeed;
        private readonly Property<int> mediumSpeed;
        private readonly Property<int> maximumSpeed;
        private readonly Property<ChangeDirection> changeDirection;
        private readonly Property<string> owner;
        private readonly Property<string> remarks;
        private readonly LocFunctions functions;

        /// <summary>
        /// Default ctor
        /// </summary>
        public Loc()
        {
            address = new Property<Address>(this, null);
            speedSteps = new Property<int>(this, DefaultValues.DefaultLocSpeedSteps);
            slowSpeed = new Property<int>(this, DefaultValues.DefaultLocSlowSpeed);
            mediumSpeed = new Property<int>(this, DefaultValues.DefaultLocMediumSpeed);
            maximumSpeed = new Property<int>(this, DefaultValues.DefaultLocMaximumSpeed);
            changeDirection = new Property<ChangeDirection>(this, DefaultValues.DefaultLocChangeDirection);
            owner = new Property<string>(this, DefaultValues.DefaultLocOwner);
            remarks = new Property<string>(this, DefaultValues.DefaultLocRemarks);
            functions = new LocFunctions(this);
        }

        /// <summary>
        /// Address of the locomotive
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public Address Address
        {
            get { return address.Value; }
            set { address.Value = value; }
        }

        /// <summary>
        /// Gets all (non-null) addresses configured in this entity.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public virtual IEnumerable<AddressUsage> AddressUsages
        {
            get
            {
                var addr = Address;
                if (addr != null) yield return new AddressUsage(addr, AddressDirection.Output);
            }
        }

        /// <summary>
        /// Percentage of speed steps for the slowest speed of this loc.
        /// Value between 1 and 100.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultLocSlowSpeed)]
        public int SlowSpeed
        {
            get { return slowSpeed.Value; }
            set { slowSpeed.Value = LimitSpeedValue(value); }
        }

        /// <summary>
        /// Percentage of speed steps for the medium speed of this loc.
        /// Value between 1 and 100.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultLocMediumSpeed)]
        public int MediumSpeed
        {
            get { return mediumSpeed.Value; }
            set { mediumSpeed.Value = LimitSpeedValue(value); }
        }

        /// <summary>
        /// Percentage of speed steps for the maximum speed of this loc.
        /// Value between 1 and 100.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultLocMaximumSpeed)]
        public int MaximumSpeed
        {
            get { return maximumSpeed.Value; }
            set { maximumSpeed.Value = LimitSpeedValue(value); }
        }

        /// <summary>
        /// Gets the number of speed steps supported by this loc.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultLocSpeedSteps)]
        public int SpeedSteps
        {
            get { return speedSteps.Value; }
            set { speedSteps.Value = value; }
        }

        /// <summary>
        /// Gets/sets the image of the given loc.
        /// </summary>
        /// <value>Null if there is no image.</value>
        /// <remarks>Image must be png, bmp, gif, jpg, wmf or emf</remarks>
        [JsonIgnore]
        [XmlIgnore]
        public Stream Image
        {
            get 
            { 
                var pkg = Package;
                return (pkg != null) ? pkg.GetGenericPart(this, ImageId) : null;
            }
            set
            {
                var pkg = Package;
                if (pkg == null)
                {
                    throw new ArgumentException("Cannot set image when not part of a package");
                }
                if (value == null)
                {
                    pkg.RemoveGenericPart(this, ImageId);
                }
                else
                {
                    pkg.SetGenericPart(this, ImageId, value);
                }
            }
        }

        /// <summary>
        /// Is it allowed for this loc to change direction?
        /// </summary>
        [DefaultValue(DefaultValues.DefaultLocChangeDirection)]
        public ChangeDirection ChangeDirection
        {
            get { return changeDirection.Value; }
            set { changeDirection.Value = value; }
        }

        /// <summary>
        /// Gets the names of all functions supported by this loc.
        /// </summary>
        public LocFunctions Functions
        {
            get { return functions; }
        }

        /// <summary>
        /// Gets the names of all functions supported by this loc.
        /// </summary>
        ILocFunctions ILoc.Functions
        {
            get { return Functions; }
        }

        /// <summary>
        /// Gets/sets the name of the person that owns this loc.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultLocOwner)]
        public string Owner
        {
            get { return owner.Value; }
            set { owner.Value = value ?? string.Empty; }
        }

        /// <summary>
        /// Gets/sets remarks (free text) about this loc.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultLocRemarks)]
        public string Remarks
        {
            get { return remarks.Value; }
            set { remarks.Value = value ?? string.Empty; }
        }

        /// <summary>
        /// Address of the locomotive.
        /// Used for serialization.
        /// </summary>
        [JsonProperty("Address")]
        [XmlElement("Address")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string AddressString
        {
            get { return Default<AddressTypeConverter>.Instance.ConvertToString(Address); }
            set { Address = (Address) Default<AddressTypeConverter>.Instance.ConvertFromString(value); }
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
            var slow = SlowSpeed;
            var medium = MediumSpeed;
            var max = MaximumSpeed;
            if (slow > medium) 
                results.Warn(this, Strings.WarnSlowSpeedHigherThenMedium);
            if (max < medium) 
                results.Warn(this, Strings.WarnMaxSpeedLowerThenMedium);
        }

        /// <summary>
        /// Human readable name of this type of entity.
        /// </summary>
        public override string TypeName
        {
            get { return Strings.TypeNameLoc; }
        }

        /// <summary>
        /// Gets package relative folder for this type of entity.
        /// </summary>
        internal override string PackageFolder
        {
            get { return PackageFolders.Loc; }
        }

        /// <summary>
        /// Compare the last modification of this entity (from the import source) with the given entity found in
        /// the target package.
        /// </summary>
        /// <param name="target">The equal entity in the target package. Can be null.</param>
        ImportComparison IImportableEntity.CompareTo(IImportableEntity target)
        {
            return CompareTo((IPersistentEntity)target);
        }

        /// <summary>
        /// Import this entity into the given package.
        /// </summary>
        void IImportableEntity.Import(IPackage target)
        {
            ((Package)target).Import(this);
            target.Railway.Locs.Add(target.GetLoc(Id));
        }

        /// <summary>
        /// Limit the given value such that is falls between 1 and 100
        /// </summary>
        private static int LimitSpeedValue(int speed)
        {
            return Math.Max(1, Math.Min(speed, 100));
        }
    }
}
