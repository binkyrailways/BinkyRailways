using System;
using System.Diagnostics;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Address with direction.
    /// </summary>
    [DebuggerDisplay("{Address} {Direction}")]
    public sealed class AddressUsage : IEquatable<AddressUsage>, IComparable<AddressUsage>
    {
        private readonly Address address;
        private readonly AddressDirection direction;

        /// <summary>
        /// Default ctor
        /// </summary>
        public AddressUsage(Address address, AddressDirection direction)
        {
            this.address = address;
            this.direction = direction;
        }

        /// <summary>
        /// Address
        /// </summary>
        public Address Address
        {
            get { return address; }
        }

        /// <summary>
        /// How is the address used.
        /// From railway to computer or from computer to railway.
        /// </summary>
        public AddressDirection Direction
        {
            get { return direction; }
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(AddressUsage other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(address, other.address) && direction.Equals(other.direction);
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(AddressUsage other)
        {
            var rc = address.CompareTo(other.address);
            if (rc != 0) return rc;
            if (direction < other.direction) return -1;
            if (direction > other.direction) return 1;
            return 0;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is AddressUsage && Equals((AddressUsage) obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((address != null ? address.GetHashCode() : 0) * 397) ^ direction.GetHashCode();
            }
        }
    } 
}
