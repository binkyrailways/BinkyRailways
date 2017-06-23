using System;
using System.ComponentModel;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Address of an object in the railway.
    /// </summary>
    [TypeConverter(typeof(AddressTypeConverter))]
    public sealed class Address : IEquatable<Address>, IComparable<Address>
    {
        private readonly Network network;
        private readonly string value;

        /// <summary>
        /// Default ctor
        /// </summary>
        public Address(AddressType type, string addressSpace, string value) :
            this(new Network(type, addressSpace), value)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public Address(AddressType type, string addressSpace, int value) :
            this(new Network(type, addressSpace), Convert.ToString(value))
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public Address(Network network, int value) :
            this(network, Convert.ToString(value))
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public Address(Network network, string value)
        {
            if (network == null)
                throw new ArgumentNullException("network");
            this.network = network;
            if (network.Type.RequiresNumericValue()) {
                int nValue;
                if (!int.TryParse(value, out nValue)) {
                    throw new ArgumentNullException("value must be numeric");                    
                }
                nValue = Math.Min(Math.Max(nValue, network.Type.MinValue()), network.Type.MaxValue());
                this.value = nValue.ToString();
            } else {
                this.value = value;
            }
        }

        /// <summary>
        /// Network containing this address
        /// </summary>
        public Network Network
        {
            get { return network; }
        }

        /// <summary>
        /// Address space. A user specified identifier, which can be used to
        /// split an network into multiple spaces controlled by different command stations.
        /// </summary>
        public string AddressSpace
        {
            get { return network.AddressSpace; }
        }

        /// <summary>
        /// Type of address
        /// </summary>
        public AddressType Type
        {
            get { return network.Type; }
        }

        /// <summary>
        /// Address value.
        /// This depends on the address type.
        /// </summary>
        public string Value
        {
            get { return value; }
        }

        /// <summary>
        /// Address value as integer.
        /// This depends on the address type.
        /// Return -1 when the value is not an integer.
        /// </summary>
        public int ValueAsInt
        {
            get { 
                int result;
                if (int.TryParse(value, out result)) {
                    return result;
                }
                return -1;
            }
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
            return Equals(obj as Address);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.
        ///                 </param>
        public bool Equals(Address other)
        {
            return (other != null) &&
                   (other.network.Equals(network)) &&
                   (other.value == value);
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(Address other)
        {
            var rc = network.CompareTo(other.network);
            if (rc != 0) return rc;
            var ovalue = other.value;
            var myvalue = value;

            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Convert to human readable string
        /// </summary>
        public override string ToString()
        {
            return network + ", " + value;
        }

        /// <summary>
        /// Hashing
        /// </summary>
        public override int GetHashCode()
        {
            return (network.GetHashCode() << 8) ^ (value.GetHashCode());
        }
    }
}
