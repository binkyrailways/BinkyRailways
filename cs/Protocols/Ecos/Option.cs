using System;

namespace BinkyRailways.Protocols.Ecos
{
    /// <summary>
    /// Single command option
    /// </summary>
    public class Option : IEquatable<Option>
    {
        internal static readonly Option[] None = new Option[0];

        private readonly string name;
        private readonly string value;
        private readonly bool isString;

        /// <summary>
        /// Default ctor
        /// </summary>
        public Option(string name)
            : this(name, null)
        {            
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public Option(string name, int value)
            : this(name, value.ToString())
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public Option(string name, string value, bool isString = false)
        {
            this.name = name;
            this.value = value;
            this.isString = isString;
        }

        /// <summary>
        /// Option name
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Option value
        /// </summary>
        public string Value
        {
            get { return value; }
        }

        public bool Equals(Option other)
        {
            return (other != null) && (isString == other.isString) && (name == other.name) && (value == other.value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Option);
        }

        public override int GetHashCode()
        {
            var hash = name.GetHashCode();
            if (value != null) hash = hash | (value.GetHashCode() << 16);
            if (isString) hash <<= 1;
            return hash;
        }

        /// <summary>
        /// Convert to string as used by the protocol.
        /// </summary>
        public override string ToString()
        {
            if (value == null)
                return name;
            return name + "[" + EncodeValue(value, isString) + "]";
        }

        private static string EncodeValue(string value, bool isString)
        {
            if (!isString)
                return value;
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }
    }
}
