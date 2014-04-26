using System;
using System.ComponentModel;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Identification of network on addresses of the same type.
    /// </summary>
    [TypeConverter(typeof(AddressTypeConverter))]
    public sealed class Network : IEquatable<Network>, IComparable<Network>
    {
        private static readonly char[] INVALID_CHARS = new[] { ',', ';', ':', '/', '\\', '\n', '\t', ' ' };
        private readonly AddressType type;
        private readonly string addressSpace;

        /// <summary>
        /// Default ctor
        /// </summary>
        public Network(AddressType type, string addressSpace)
        {
            this.type = type;
            this.addressSpace = addressSpace ?? string.Empty;
            if (this.addressSpace.IndexOfAny(INVALID_CHARS) >= 0)
                throw new ArgumentException("Invalid characters in address space.");
        }

        /// <summary>
        /// Address space. A user specified identifier, which can be used to
        /// split an network into multiple spaces controlled by different command stations.
        /// </summary>
        public string AddressSpace
        {
            get { return addressSpace; }
        }

        /// <summary>
        /// Type of address
        /// </summary>
        public AddressType Type
        {
            get { return type; }
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. 
        ///                 </param><exception cref="T:System.NullReferenceException">The <paramref name="obj"/> parameter is null.
        ///                 </exception><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            return Equals(obj as Network);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.
        ///                 </param>
        public bool Equals(Network other)
        {
            return (other != null) &&
                   (other.type == type) &&
                   (other.addressSpace == addressSpace);
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(Network other)
        {
            var otype = other.type;
            if (type < otype) return -1;
            if (type > otype) return 1;

            var oAddressSpace = other.addressSpace ?? string.Empty;
            var myAddressSpace = addressSpace ?? string.Empty;
            return string.Compare(myAddressSpace, oAddressSpace, StringComparison.Ordinal);
        }

        /// <summary>
        /// Convert to human readable string
        /// </summary>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(addressSpace))
                return type.ToString();
            return type + ", " + addressSpace;
        }

        /// <summary>
        /// Hashing
        /// </summary>
        public override int GetHashCode()
        {
            return ((int)type << 24) ^ addressSpace.GetHashCode();
        }
    }
}
